using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LabsAdminASP.Modelo;

namespace LabsAdminASP.Controlador
{
    public class controladorUser
    {
        LabsAdminEntities1 ent = new LabsAdminEntities1();
        public usuario getUsuario(string nick)
        {
            usuario user = new usuario();
            var consulta = from u in ent.usuario where u.nick.Equals(nick) select u;
            if (consulta.ToList().Count == 1)
            {
                user = consulta.ToList<usuario>().ElementAt(0);
            }else
            {
                user = null;
            }
            return user;
        }
    }
}