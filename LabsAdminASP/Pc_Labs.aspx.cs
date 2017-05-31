using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using LabsAdminASP.Modelo;
using LabsAdminASP.Controlador;
using System.Net;

namespace LabsAdminASP
{
    public partial class Pc_Labs : System.Web.UI.Page
    {
        LabsAdminEntities1 ent = new LabsAdminEntities1();
        controladorUser cont = new controladorUser();
        ControladorPass contPass = new ControladorPass();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                string x = Session["id_laboratorio"].ToString();
                string id_lab = x.Replace("btLab", "");
                laboratorio laboratorio = ent.laboratorio.Find(Convert.ToInt32(id_lab));
                lbid_usuario.Text = "4";

            }
        }

        public void Page_Init(object sender, EventArgs e)
        {
            Cargar_Pcs();
        }

        public void Cargar_Pcs()
        {
            try
            {
                string x = Session["id_laboratorio"].ToString();
                string id_lab = x.Replace("btLab", "");
                List<computadora> Pcs = cont.getPCs(Convert.ToInt32(id_lab));
                foreach (var i in Pcs)
                {

                    HtmlGenericControl divFinal = new HtmlGenericControl();
                    divFinal.TagName = "div";
                    divFinal.Attributes["class"] = "col-md-55";

                    HtmlGenericControl div = new HtmlGenericControl();
                    div.TagName = "div";
                    div.Attributes["class"] = "thumbnail";
                    divFinal.Controls.Add(div);

                    HtmlGenericControl div2 = new HtmlGenericControl();
                    div2.TagName = "div";
                    div2.Attributes["class"] = "image view view-first";
                    div.Controls.Add(div2);

                    HtmlGenericControl img = new HtmlGenericControl();
                    img.TagName = "img";
                    img.Attributes["style"] = "width: 100%; display: block;";
                    img.Attributes["src"] = "images/media.jpg";
                    img.Attributes["alt"] = "image";
                    div2.Controls.Add(img);

                    HtmlGenericControl div3 = new HtmlGenericControl();
                    div3.TagName = "div";
                    div3.Attributes["class"] = "mask";
                    div2.Controls.Add(div3);

                    HtmlGenericControl p = new HtmlGenericControl();
                    p.TagName = "p";
                    p.InnerText = i.SO;
                    div3.Controls.Add(p);

                    HtmlGenericControl div5 = new HtmlGenericControl();
                    div5.TagName = "div";
                    div5.Attributes["class"] = "tools tools-bottom";
                    div3.Controls.Add(div5);

                    LinkButton lbt_Encender = new LinkButton();
                    lbt_Encender.ID = "bt_Encender" + i.id_computadora;
                    lbt_Encender.Click += new EventHandler(Encender);
                    lbt_Encender.ToolTip = "Encender Equipo";
                    lbt_Encender.Text = "<i class='fa fa-sun-o'></i>";
                    div5.Controls.Add(lbt_Encender);

                    LinkButton lbt_apagar = new LinkButton();
                    lbt_apagar.ID = "bt_apagar" + i.id_computadora;
                    lbt_apagar.Click += new EventHandler(Apagar);
                    lbt_apagar.ToolTip = "Apagar Equipo";
                    lbt_apagar.Text = "<i class='fa fa-power-off'></i>";
                    div5.Controls.Add(lbt_apagar);

                    LinkButton lbt_remoto = new LinkButton();
                    lbt_remoto.ID = "bt_Remoto" + i.id_computadora;
                    lbt_remoto.Click += new EventHandler(Remoto);
                    lbt_remoto.ToolTip = "Iniciar Control Remoto";
                    lbt_remoto.Text = "<i class='fa fa-desktop'></i>";
                    div5.Controls.Add(lbt_remoto);

                    HtmlGenericControl div4 = new HtmlGenericControl();
                    div4.TagName = "div";
                    div4.Attributes["class"] = "caption";
                    div.Controls.Add(div4);
                    div4.Controls.Add(new LiteralControl("<div class='x_title'><h2 style ='text-align:center'>" + i.nombre + "</h2></br><small>" + i.Marca + " " + i.Modelo + "</small></div>"));

                    PCs.Controls.Add(divFinal);
                }
            }
            catch (Exception)
            {

            }
        }
        public void Apagar(object sender, EventArgs e)
        {
            LinkButton btApagarPc = (LinkButton)sender;
            int id = Convert.ToInt32(btApagarPc.ID.Replace("bt_apagar", ""));
            computadora pc = ent.computadora.Find(id);
            int id_user = Convert.ToInt32(lbid_usuario.Text);
            usuario u = cont.getUsuario(id_user);
            string command = @"\\" + pc.nombre + " -u "+u.nick+" -p "+contPass.Decrypt(u.pass)+" shutdown -p";
            string disk = cont.getMainDisk();
            string directory = disk + @"Windows\System32";
            string respuesta = cont.ExecuteCommand(directory,"psexec", "Arturokin12", "godofwarjaja123", command);
            lbTituloMensaje.Text = "Resultado de Apagar Equipo " + pc.nombre;
            lbMensaje.Text = respuesta;
            modalPupoExtenderMensaje.Show();
        }
        public void Encender(object sender, EventArgs e)
        {
            //cont.WakeUp("54AB3A1BD009");
            //cont.WakeUp();
            //lbMensaje.Text = hola;
            //modalPupoExtenderMensaje.Show();
            int id_user = Convert.ToInt32(lbid_usuario.Text);
            usuario u = cont.getUsuario(id_user);
            List<ComputadorDom> listaPcs = cont.getIpAllPcs("192.168.0.*",u);
            

        }
        public void Remoto(object sender, EventArgs e)
        {

        }

    }
}
