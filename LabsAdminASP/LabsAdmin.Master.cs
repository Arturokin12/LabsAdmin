using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace LabsAdmin_Plantilla
{
    public partial class LabsAdmin : System.Web.UI.MasterPage
    {
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
            HtmlGenericControl li = new HtmlGenericControl();
            li.TagName = "li";
            LinkButton BtnLabs = new LinkButton();
            BtnLabs.Click += new EventHandler(btTal_lab01_Click);

            BtnLabs.ID = "btn_lab01";
            BtnLabs.Text = "<i class='fa fa-desktop'></i>tal_lab01";
            li.Controls.Add(BtnLabs);


            divLabs.Controls.Add(li);
        }

        protected void btTal_lab01_Click(object sender, EventArgs e)
        {
            Response.Redirect("users.aspx");
            cargarLabs();
        }

        protected void btInicio_Click(object sender, EventArgs e)
        {
            Response.Redirect("index.aspx");
        }

        protected void btUsuarios_Click(object sender, EventArgs e)
        {
            Response.Redirect("users.aspx");
        }
    }
}