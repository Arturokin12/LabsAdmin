using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LabsAdminASP.Modelo
{
    public class ComputadorDom
    {
        public string nombre { get; set; }
        public string ip { get; set; }
        public string mac { get; set; }

        public ComputadorDom()
        {

        }

        public ComputadorDom(string Name, string IP,string MAC)
        {
            this.nombre = Name;
            this.ip = IP;
            this.mac = MAC;
        }
    }
}