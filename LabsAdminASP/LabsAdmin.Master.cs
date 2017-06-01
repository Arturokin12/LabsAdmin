using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using LabsAdminASP.Modelo;

namespace LabsAdmin_Plantilla
{
    public partial class LabsAdmin : System.Web.UI.MasterPage
    {
        LabsAdminEntities1 ent = new LabsAdminEntities1();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                usuario u = ent.usuario.Find(3);
                //lbid_usuario.Text = u.id_usuario + "";
                //.Text = u.nick;
                if (u.nombre.Split().Length > 1)
                {
                    lbNombreUsuario.Text = u.nombre.Split()[0] + " " + u.nombre.Split()[1]; lbNombreUsuario2.Text = u.nombre.Split()[0] + " " + u.nombre.Split()[1];
                }else
                {
                    lbNombreUsuario.Text = u.nombre; lbNombreUsuario2.Text = u.nombre.Split()[0] + " " + u.nombre.Split()[1];
                }
                
                imgUsuario1.ImageUrl = u.imagen; imgUsuario2.ImageUrl = u.imagen;
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            cargarLabs();
        }

        public void cargarLabs()
        {
            List<laboratorio> laboratorios = ent.laboratorio.ToList();
            foreach (var labs in ent.laboratorio)
            {
                try
                {
                    HtmlGenericControl li = new HtmlGenericControl();
                    li.TagName = "li";
                    LinkButton BtnLabs = new LinkButton();
                    BtnLabs.Click += new EventHandler(btTal_lab01_Click);

                    BtnLabs.ID = "btLab"+ labs.id_laboratorio;
                    BtnLabs.Text = "<i class='fa fa-desktop'></i>"+labs.nombre;
                    li.Controls.Add(BtnLabs);


                    divLabs.Controls.Add(li);
                }
                catch (Exception)
                {

                    
                }
            }
            
        }

        protected void btTal_lab01_Click(object sender, EventArgs e)
        {
            LinkButton asd = (LinkButton)sender;
            
            Session["id_laboratorio"] = asd.ID;
            Response.Redirect("Pc_Labs.aspx");
        }

        protected void btInicio_Click(object sender, EventArgs e)
        {
            Response.Redirect("index.aspx");
        }

        protected void btUsuarios_Click(object sender, EventArgs e)
        {
            Response.Redirect("users.aspx");
        }

        protected void btPcsDominio_Click(object sender, EventArgs e)
        {
            Response.Redirect("Dominio.aspx");
        }

        protected void btConfig_Click(object sender, EventArgs e)
        {
            Response.Redirect("configuracion.aspx");
        }

        protected void btPerfil_Click(object sender, EventArgs e)
        {
            Response.Redirect("perfil.aspx");
        }
    }
}