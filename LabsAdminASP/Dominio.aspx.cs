using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LabsAdminASP.Controlador;
using LabsAdminASP.Modelo;

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
                //List<string> pcs = cont.GetComputersFromDomain("labsadmin.cl");
                List<string> pcs = new List<string>();
                pcs.Add("DESKTOP-IFOB8Q2");
                List<ComputadorDom> listaPcs = new List<ComputadorDom>();
                foreach (var i in pcs)
                {
                    listaPcs.Add(new ComputadorDom(i, "",""));
                }
                tablaPcsDominio.DataSource = listaPcs;
                tablaPcsDominio.DataBind();
            }
        }

        protected void tablaPcsDominio_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            Label pc = (Label)tablaPcsDominio.Rows[e.NewSelectedIndex].FindControl("Label1");
            string ip_pc = cont.getIpfromPC(pc.Text);
            lbIP.Text = "ip: "+ip_pc;
            UpdatePanel2.Update();
            modalPopupExtenderAgregarDesdeDom.Show();
        }
    }
}