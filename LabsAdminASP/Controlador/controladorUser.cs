using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LabsAdminASP.Modelo;
using System.Diagnostics;
using System.Security;
using System.IO;
using System.Net.Sockets;
using System.Net;
using System.Globalization;
using System.Net.NetworkInformation;
using System.Threading;
using System.DirectoryServices;

namespace LabsAdminASP.Controlador
{
    public class controladorUser
    {
        LabsAdminEntities1 ent = new LabsAdminEntities1();
        ControladorPass contPass = new ControladorPass();
        public usuario getUsuario(string nick)
        {
            usuario user = new usuario();
            var consulta = from u in ent.usuario where u.nick.Equals(nick) select u;
            if (consulta.ToList().Count == 1)
            {
                user = consulta.ToList<usuario>().ElementAt(0);
            }
            else
            {
                user = null;
            }
            return user;
        }


        public List<computadora> getPCs(int id_lab)
        {
            try
            {
                var consulta = from com in ent.computadora where com.id_laboratorio == id_lab select com;
                return consulta.ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public string ExecuteCommand(string directory, string file, string usuario, string pass, string cmd)
        {
            try
            {
                Process p = new Process();
                ProcessStartInfo myProcessInfo = new ProcessStartInfo(); //Initializes a new ProcessStartInfo of name myProcessInfo
                myProcessInfo.WorkingDirectory = directory;
                myProcessInfo.FileName = file;
                myProcessInfo.Arguments = cmd;
                myProcessInfo.UserName = usuario;
                var s = new SecureString();
                for (int i = 0; i < pass.Length; i++)
                {
                    s.AppendChar(pass[i]);
                }
                myProcessInfo.Password = s;
                myProcessInfo.WindowStyle = ProcessWindowStyle.Maximized; //Sets the WindowStyle of myProcessInfo which indicates the window state to use when the process is started to Hidden
                myProcessInfo.Verb = "runas";
                myProcessInfo.UseShellExecute = false;
                myProcessInfo.RedirectStandardInput = true;
                myProcessInfo.RedirectStandardOutput = true;
                myProcessInfo.RedirectStandardError = true;
                myProcessInfo.CreateNoWindow = true;
                p.StartInfo = myProcessInfo;
                p.Start();
                //p.StandardInput.WriteLine(cmd);
                string output = p.StandardError.ReadToEnd();
                //output = output + p.StandardError.ReadToEnd();
                p.WaitForExit();
                p.StandardInput.Close();
                return output;
            }
            catch (Exception ex)
            {
                if (ex.ToString() != null)
                {
                    return ex.ToString();
                }
                else
                {
                    return "Error desconocido, contacte al administrador.";
                }
            }

        }

        public string ExecuteCommand2(string directory, string file, string usuario, string pass, string cmd)
        {
            try
            {
                Process p = new Process();
                ProcessStartInfo myProcessInfo = new ProcessStartInfo(); //Initializes a new ProcessStartInfo of name myProcessInfo
                myProcessInfo.WorkingDirectory = directory;
                myProcessInfo.FileName = file;
                myProcessInfo.Arguments = "";
                myProcessInfo.UserName = usuario;
                var s = new SecureString();
                for (int i = 0; i < pass.Length; i++)
                {
                    s.AppendChar(pass[i]);
                }
                myProcessInfo.Password = s;
                myProcessInfo.WindowStyle = ProcessWindowStyle.Maximized; //Sets the WindowStyle of myProcessInfo which indicates the window state to use when the process is started to Hidden
                myProcessInfo.Verb = "runas";
                myProcessInfo.UseShellExecute = false;
                myProcessInfo.RedirectStandardInput = true;
                myProcessInfo.RedirectStandardOutput = true;
                myProcessInfo.RedirectStandardError = true;
                myProcessInfo.CreateNoWindow = true;
                p.StartInfo = myProcessInfo;
                p.Start();
                p.StandardInput.WriteLine(cmd);
                p.StandardInput.WriteLine("exit");
                //p.WaitForExit(25000);
                string output = p.StandardError.ReadToEnd() + "\n " + p.StandardOutput.ReadToEnd();
                //output = output + p.StandardError.ReadToEnd();
                p.WaitForExit();
                p.StandardInput.Close();
                
                return output;
            }
            catch (Exception ex)
            {
                if (ex.ToString() != null)
                {
                    return ex.ToString();
                }
                else
                {
                    return "Error desconocido, contacte al administrador.";
                }
            }
        }

        public void WakeUp(string macAddr, string ip_addr)
        {

            byte[] mac = new byte[6];

            for (var i = 0; i < 6; i++)
            {
                var t = macAddr.Substring((i * 2), 2);
                mac[i] = Convert.ToByte(t, 16);
            }

            byte[] packet = new byte[17 * 6];

            for (int i = 0; i < 6; i++)
                packet[i] = 0xff;

            for (int i = 1; i <= 16; i++)
                for (int j = 0; j < 6; j++)
                    packet[i * 6 + j] = mac[j];

            UdpClient client = new UdpClient();
            IPAddress ip = IPAddress.Parse(ip_addr);
            client.Connect(ip, 0);
            client.Send(packet, packet.Length);
        }

        public List<string> GetComputersFromDomain(string domain)
        {
            List<string> ComputerNames = new List<string>();

            DirectoryEntry entry = new DirectoryEntry("LDAP://"+domain);
            DirectorySearcher mySearcher = new DirectorySearcher(entry);
            mySearcher.Filter = ("(objectClass=computer)");
            mySearcher.SizeLimit = int.MaxValue;
            mySearcher.PageSize = int.MaxValue;

            foreach (SearchResult resEnt in mySearcher.FindAll())
            {
                //"CN=SGSVG007DC"
                string ComputerName = resEnt.GetDirectoryEntry().Name;
                if (ComputerName.StartsWith("CN="))
                    ComputerName = ComputerName.Remove(0, "CN=".Length);
                ComputerNames.Add(ComputerName);
            }

            mySearcher.Dispose();
            entry.Dispose();

            return ComputerNames;
        }

        public List<ComputadorDom> getIpAllPcs(string ip_red, usuario u)
        {
            try
            {
                string disk = getMainDisk();
                string directory = disk + @"windows\System32";
                string cmd = disk + "nmap-7.40\\nmap.exe -sP 1 " + ip_red;
                string respuesta = ExecuteCommand2(directory, "cmd", u.nick, contPass.Decrypt(u.pass), cmd);
                //agregar todos los pcs a una lista
                //dividir la espuesta por los espacios (saltos de línea)
                string[] palabrs = respuesta.Split('\n');
                //contador para agregar a la lista de pcs, al llegar a 2 lo agrega, ya que obtiene la ip, mac y nombre del pc
                int agregarALista = 0;
                string ip = "";
                string MAC = "";
                string nombre = "";
                List<ComputadorDom> listaPcs = new List<ComputadorDom>();
                //recorrer areglo de resultado split
                for (int i = 0; i < palabrs.Length; i++)
                {
                    //si comienza con nmap es la línea donde está la ip
                    if (palabrs[i].StartsWith("Nmap"))
                    {
                        //obtener el indice de la palabra que antecede a la ip la cual es for
                        int indexIPfor = palabrs[i].IndexOf("for ");
                        string palabra = palabrs[i];
                        //se obtiene la ip con el inicio de la ip sumandole 4 caracteres al valor anterior, llegando hasta
                        //el final del string, y reemplazando el \r en el string
                        ip = palabrs[i].Substring(indexIPfor + 4).Replace("\r", "");
                        //se suma el contador para agregar a la lista de pcs
                        agregarALista++;
                    }
                    else if (palabrs[i].StartsWith("MAC"))
                    {
                        //se obtiene el indice de lo que antecede a la MAC
                        int indexIPfor = palabrs[i].IndexOf(": ");
                        string palabra = palabrs[i];
                        //substring sumando los 17 caracteres que tiene la MAC
                        MAC = palabrs[i].Substring(indexIPfor + 2, 17);
                        //donde comienza el nombre del equipo encontrado
                        int parentesis = palabrs[i].IndexOf("(");
                        //obtiene el nombre del equipo que estaba entre parentesis
                        nombre = palabrs[i].Substring(parentesis + 1).Replace(")", "").Replace("\r", "");
                        //suma el contador para agregar a la lista
                        agregarALista++;
                    }
                    if (agregarALista == 2)
                    {
                        agregarALista = 0;
                        listaPcs.Add(new ComputadorDom { nombre = nombre, ip = ip, mac = MAC});
                        ip = "";
                        MAC = "";
                        nombre = "";
                    }
                }
                return listaPcs;
            }
            catch (Exception)
            {
                return null;
            }
            
        }

        public string getIpfromPC(string nombre_pc, usuario u)
        {
            try
            {
                string ip = "";
                IPAddress IP = new IPAddress(1);
                string disk = getMainDisk();
                int id_pc = Convert.ToInt32(nombre_pc);
                computadora c = ent.computadora.Find(id_pc);
                string cmd = "ping "+c.nombre+" -4";
                string respuesta = ExecuteCommand2(disk,"cmd",u.nick, contPass.Decrypt(u.pass), cmd);
                string[] lineas = respuesta.Split('\n');
                for (int i = 0; i < lineas.Length; i++)
                {
                    if (lineas[i].Contains("bytes="))
                    {
                        string[] palabras = lineas[i].Split(' ');
                        
                        for (int j = 0; j < palabras.Length; j++)
                        {
                            if (IPAddress.TryParse(palabras[j].Replace(":",""), out IP))
                            {
                                ip = palabras[j].Replace(":","");
                            }
                        }
                    }
                }
                return ip;
            }
            catch (Exception)
            {
                return "";
            }
        }

        public string GetMacAddress(string ip, usuario u)
        {
            string cmd = "arp -a "+ip;
            string disk = getMainDisk();
            string directory = disk + @"Windows\System32";
            string respuesta = ExecuteCommand(directory, "cmd",u.nick, contPass.Decrypt(u.pass), cmd);

            return respuesta;
        }

        public string getMainDisk()
        {
            try
            {
                string disk = Path.GetPathRoot(Environment.GetFolderPath(Environment.SpecialFolder.System));
                return disk;
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public usuario getUsuario(int id)
        {
            try
            {
                return ent.usuario.Find(id);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}