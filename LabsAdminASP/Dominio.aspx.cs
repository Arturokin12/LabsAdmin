using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LabsAdminASP.Controlador;
using LabsAdminASP.Modelo;
using System.Net;

namespace LabsAdminASP
{
    public partial class Dominio : System.Web.UI.Page
    {
        controladorUser cont = new controladorUser();
        LabsAdminEntities1 ent = new LabsAdminEntities1();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lbid_usuario.Text = "2";
                config config = ent.config.ToList().ElementAt(0);
                List<string> pcs = cont.GetComputersFromDomain(config.dominio);
                //List<string> pcs = new List<string>();
                //pcs.Add("DESKTOP-IFOB8Q2");
                //pcs.Add("tal_lab0101");
                //pcs.Add("DESKTOP-SGF53VS");
                //pcs.Add("DESKTOP-AEHRM66");
                //pcs.Add("ADOKYNSTORE-PC");

                List<ComputadorDom> listaPcs = new List<ComputadorDom>();
                foreach (var i in pcs)
                {
                    listaPcs.Add(new ComputadorDom(i, "", ""));
                }
                tablaPcsDominio.DataSource = listaPcs;
                tablaPcsDominio.DataBind();

                //obtener laboratorios para DropDownList
                List<laboratorio> labs = ent.laboratorio.ToList();
                cbLaboratorio.DataSource = labs;
                cbLaboratorio.DataTextField = "nombre";
                cbLaboratorio.DataValueField = "id_laboratorio";
                cbLaboratorio.DataBind();

                //obtener laboratorios para DropDownList de lista de pcs
                cbLaboratorio2.DataSource = labs;
                cbLaboratorio2.DataTextField = "nombre";
                cbLaboratorio2.DataValueField = "id_laboratorio";
                cbLaboratorio2.DataBind();

                cbLaboratorio2_SelectedIndexChanged(sender, e);
                //modalPopupExtenderAgregarDesdeDom.Hide();
            }
        }

        protected void tablaPcsDominio_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            Label pc = (Label)tablaPcsDominio.Rows[e.NewSelectedIndex].FindControl("Label1");
            lbNombreComputadora.Text = pc.Text;
            txtNombre.Text = pc.Text; txtMarca.Text = ""; txtModelo.Text = ""; txtOS.Text = ""; cbLaboratorio.SelectedIndex = 0;
            txtMac.Text = ""; txtIP.Text = ""; txtDes.Text = "";
            UpdatePanel1.Update();
            UpdatePanel2.Update();
            btAddComputadora1.Visible = true;
            btEditComputadora.Visible = false;
            modalPupoAgregarDom.Show();
        }

        protected void btgetMac_Click(object sender, EventArgs e)
        {
            UpdatePanel2.Update();
            int id_user = Convert.ToInt32(lbid_usuario.Text);
            usuario u = cont.getUsuario(2);
            string ip_pc = cont.getIpfromPC(lbNombreComputadora.Text, u);   
            if (ip_pc != "" && ip_pc != null)
            {
                List<ComputadorDom> pcs = cont.getIpAllPcs("192.168.0.*", u);
                foreach (var i in pcs)
                {
                    if (i.ip.Equals(ip_pc))
                    {
                        txtMac.Text = i.mac;
                        txtDes.Text = i.nombre;
                    }
                }
                UpdatePanel2.Update();
            }
            else
            {
                lbResultadoModal1.Text = "Error al obtener MAC, probable error de conexión \n verifique que el equipo esté encendido y conectado a la red.";
                UpdatePanel2.Update();
            }
        }

        protected void btGetIP_Click(object sender, EventArgs e)
        {
            UpdatePanel2.Update();
            int id_user = Convert.ToInt32(lbid_usuario.Text);
            usuario u = cont.getUsuario(id_user);
            string ip_pc = cont.getIpfromPC(lbNombreComputadora.Text,u);
            if (ip_pc != null && ip_pc != "")
            {
                txtIP.Text = ip_pc;
            }
            else
            {
                lbResultadoModal1.Text = "Error al obtener IP del pc, probable error de conexión \n verifique que el equipo esté encendido y conectado a la red";
            }
            UpdatePanel2.Update();
        }

        protected void agregarComputadora1(object sender, EventArgs e)
        {
            IPAddress outip = new IPAddress(1);
            string nombre = txtNombre.Text;
            string marca = txtMarca.Text;
            string modelo = txtModelo.Text;
            string OS = txtOS.Text;
            string id_lab = cbLaboratorio.SelectedValue.ToString();
            string mac = txtMac.Text;
            string ip = txtIP.Text;
            string des = txtDes.Text;
            if (IPAddress.TryParse(ip, out outip))
            {
                try
                {
                    computadora pc = new computadora
                    {
                        nombre = nombre,
                        Marca = marca,
                        Modelo = modelo,
                        SO = OS,
                        id_laboratorio = Convert.ToInt32(id_lab),
                        Mac = mac,
                        ip = ip,
                        descripcion = des
                    };
                    ent.computadora.Add(pc);
                    if (ent.SaveChanges() > 0)
                    {
                        txtNombre.Text = ""; txtMarca.Text = ""; txtModelo.Text = ""; txtOS.Text = ""; cbLaboratorio.SelectedIndex = 0;
                        txtMac.Text = ""; txtIP.Text = ""; txtDes.Text = "";
                        lbTituloMensaje.Text = "Computadora " + nombre + " agregada";
                        lbMensaje.Text = "Computadora agregada satisfactoriamente";
                        cbLaboratorio2.SelectedValue = id_lab;
                        cbLaboratorio2_SelectedIndexChanged(sender, e);
                        modalPupoExtenderMensaje.Show();
                        UpdatePanel1.Update();
                    }
                    else
                    {
                        lbResultadoModal1.Text = "Error al agregar Computadora.";
                        UpdatePanel2.Update();
                    }
                }
                catch (Exception ex)
                {
                    lbResultadoModal1.Text = "Error al agregar pc, inténtelo otra vez.";
                }
            }
            else
            {
                lbResultadoModal1.Text = "Ingrese una IPV4 válida";
                UpdatePanel2.Update();
            }
        }

        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            UpdatePanel1.Update();
        }

        protected void btCancelMensaje_Click(object sender, EventArgs e)
        {
            UpdatePanel1.Update();
        }

        protected void btActualizar_Click(object sender, EventArgs e)
        {
            UpdatePanel1.Update();
        }

        protected void cbLaboratorio2_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id_lab = Convert.ToInt32(cbLaboratorio2.SelectedValue.ToString());
            var consulta = from c in ent.computadora where c.id_laboratorio == id_lab select c;
            List<computadora> pcs = consulta.ToList();
            tablaComputadoras.DataSource = pcs;
            tablaComputadoras.DataBind();
            UpdatePanel1.Update();
        }

        protected void btEditComputadora_Click(object sender, EventArgs e)
        {
            IPAddress outip = new IPAddress(1);
            string id_pc = lbNombreComputadora.Text;
            string nombre = txtNombre.Text;
            string marca = txtMarca.Text;
            string modelo = txtModelo.Text;
            string OS = txtOS.Text;
            string id_lab = cbLaboratorio.SelectedValue.ToString();
            string mac = txtMac.Text;
            string ip = txtIP.Text;
            string des = txtDes.Text;
            if (IPAddress.TryParse(ip, out outip))
            {
                try
                {
                    int id = Convert.ToInt32(id_pc);
                    computadora pc = ent.computadora.Find(id);
                    pc.nombre = nombre;
                    pc.Marca = marca;
                    pc.Modelo = modelo;
                    pc.SO = OS;
                    pc.id_laboratorio = Convert.ToInt32(id_lab);
                    pc.Mac = mac;
                    pc.ip = ip;
                    pc.descripcion = des;
                    if (ent.SaveChanges() > 0)
                    {
                        txtNombre.Text = ""; txtMarca.Text = ""; txtModelo.Text = ""; txtOS.Text = ""; cbLaboratorio.SelectedIndex = 0;
                        txtMac.Text = ""; txtIP.Text = ""; txtDes.Text = "";
                        lbTituloMensaje.Text = "Computadora " + nombre + " editada";
                        lbMensaje.Text = "Computadora editada satisfactoriamente";
                        cbLaboratorio2.SelectedValue = id_lab;
                        cbLaboratorio2_SelectedIndexChanged(sender,e);
                        modalPupoExtenderMensaje.Show();
                        UpdatePanel1.Update();
                    }
                    else
                    {
                        lbResultadoModal1.Text = "Error al editar Computadora.";
                        UpdatePanel2.Update();
                    }
                }
                catch (Exception ex)
                {
                    lbResultadoModal1.Text = "Error al editar pc, inténtelo otra vez";
                }
            }
            else
            {
                lbResultadoModal1.Text = "Ingrese una IPV4 válida";
                UpdatePanel2.Update();
            }
        }

        protected void tablaComputadoras_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            Label id_pc = (Label)tablaComputadoras.Rows[e.NewSelectedIndex].FindControl("Label1");
            computadora pc = ent.computadora.Find(Convert.ToInt32(id_pc.Text));
            lbNombreComputadora.Text = id_pc.Text;
            txtNombre.Text = pc.nombre; txtMarca.Text = pc.Marca; txtModelo.Text = pc.Modelo;
            txtOS.Text = pc.SO; cbLaboratorio.SelectedValue = pc.id_laboratorio.ToString();
            txtMac.Text = pc.Mac; txtIP.Text = pc.ip; txtDes.Text = pc.descripcion;
            UpdatePanel1.Update();
            UpdatePanel2.Update();
            btEditComputadora.Visible = true;
            btAddComputadora1.Visible = false;
            modalPupoAgregarDom.Show();
        }

        protected void tablaComputadoras_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            Label id_pc = (Label)tablaComputadoras.Rows[e.RowIndex].FindControl("Label1");
            computadora pc = ent.computadora.Find(Convert.ToInt32(id_pc.Text));
            lbidEliminar.Text = id_pc.Text;
            lbMensajeEliminar.Text = "¿Está seguro que desea eliminar el pc '"+pc.nombre+"'?";
            UpdatePanel1.Update();
            UpdatePanel2.Update();
            modalPupoEliminarPc.Show();
        }

        protected void btEliminarPc_Click(object sender, EventArgs e)
        {
            try
            {
                string id_lab = cbLaboratorio.SelectedValue.ToString();
                int id_pc = Convert.ToInt32(lbidEliminar.Text);
                computadora c = ent.computadora.Find(id_pc);
                ent.computadora.Remove(c);
                if (ent.SaveChanges()>0)
                {
                    lbTituloMensaje.Text = "Eliminar Computadora";
                    lbMensaje.Text = "Computadora eliminada correctamente";
                    modalPupoExtenderMensaje.Show();
                    cbLaboratorio2.SelectedValue = id_lab;
                    cbLaboratorio2_SelectedIndexChanged(sender, e);
                    UpdatePanel1.Update();
                }else
                {
                    lbTituloMensaje.Text = "Error al Eliminar Computadora";
                    lbMensaje.Text = "No se ha podido eliminar la computadora";
                    modalPupoExtenderMensaje.Show();
                    UpdatePanel1.Update();
                }
            }
            catch (Exception ex)
            {
                lbTituloMensaje.Text = "Error al Eliminar Computadora";
                lbMensaje.Text = ex.ToString().Substring(0,400);
                modalPupoExtenderMensaje.Show();
                UpdatePanel1.Update();
            }
        }
    }
}