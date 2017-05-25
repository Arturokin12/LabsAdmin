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
    }
}