using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using LabsAdminASP.Modelo;
using LabsAdminASP.Controlador;
using System.IO;

namespace LabsAdminASP
{
    public partial class Pc_Labs : System.Web.UI.Page
    {

        LabsAdminEntities1 ent = new LabsAdminEntities1();
        controladorUser cont = new controladorUser();
        ControladorPass contPass = new ControladorPass();
        static List<string> Archivos = new List<string>();
        static List<string> SelectedPcs = new List<string>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                string x = Session["id_laboratorio"].ToString();
                string id_lab = x.Replace("btLab", "");
                laboratorio laboratorio = ent.laboratorio.Find(Convert.ToInt32(id_lab));
                lbid_usuario.Text = "3";
                Num_Lab.Text = Session["id_laboratorio"].ToString();
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
                    lbt_Encender.Click += new EventHandler(panelEncender);
                    lbt_Encender.ToolTip = "Encender Equipo";
                    lbt_Encender.Text = "<i class='fa fa-sun-o'></i>";
                    div5.Controls.Add(lbt_Encender);

                    LinkButton lbt_apagar = new LinkButton();
                    lbt_apagar.ID = "bt_apagar" + i.id_computadora;
                    lbt_apagar.Click += new EventHandler(panelApagar);
                    lbt_apagar.ToolTip = "Apagar Equipo";
                    lbt_apagar.Text = "<i class='fa fa-power-off'></i>";
                    div5.Controls.Add(lbt_apagar);

                    LinkButton lbt_remoto = new LinkButton();
                    lbt_remoto.ID = "bt_Remoto" + i.id_computadora;
                    lbt_remoto.Click += new EventHandler(Remoto);
                    lbt_remoto.ToolTip = "Iniciar Control Remoto";
                    lbt_remoto.Text = "<i class='fa fa-desktop'></i>";
                    div5.Controls.Add(lbt_remoto);

                    LinkButton lbt_Copiar = new LinkButton();
                    lbt_Copiar.ID = "bt_Copiar" + i.id_computadora;
                    lbt_Copiar.Click += new EventHandler(CopiarPopu);
                    lbt_Copiar.ToolTip = "Iniciar archivos o carpetas";
                    lbt_Copiar.Text = "<i class='fa fa-scissors'></i>";
                    div5.Controls.Add(lbt_Copiar);

                    HtmlGenericControl div4 = new HtmlGenericControl();
                    div4.TagName = "div";
                    div4.Attributes["class"] = "caption";
                    div.Controls.Add(div4);
                    div4.Controls.Add(new LiteralControl("<div class='x_title' style='display:inline'><h2 style ='text-align:center'>" + i.nombre + "</h2><br/><br/><small>" + i.Marca + " " + i.Modelo + "</small></div>"));

                    CheckBox chk = new CheckBox();
                    chk.ID = "chk" + i.id_computadora;
                    chk.Text = "";
                    chk.Attributes["style"] = "float:right";
                    chk.CheckedChanged += new EventHandler(selectPc);
                    chk.AutoPostBack = true;
                    div4.Controls.Add(chk);
                    PCs.Controls.Add(divFinal);
                }
            }
            catch (Exception)
            {

            }
        }

        public void selectPc(object sender, EventArgs e)
        {
            try
            {
                CheckBox chk = (CheckBox)sender;
                string id = chk.ID.Replace("chk", "");
                if (chk.Checked == true)
                {
                    SelectedPcs.Add(id);
                }
                else
                {
                    //string found = 
                    //foreach (var i in SelectedPcs)
                    //{
                    //    if (i.Equals(id))
                    //    {

                    //    }
                    //}
                    SelectedPcs.Remove(id);
                }
                if (SelectedPcs.Count > 0)
                {
                    lnkApagarSelect.Enabled = true;
                    lnkCopiarSelect.Enabled = true;
                    lnkEncenderSelect.Enabled = true;
                    lnkApagarSelect.CssClass = "btn btn-info btn-new";
                    lnkCopiarSelect.CssClass = "btn btn-info btn-new";
                    lnkEncenderSelect.CssClass = "btn btn-info btn-new";
                }
                else
                {
                    lnkApagarSelect.Enabled = false;
                    lnkCopiarSelect.Enabled = false;
                    lnkEncenderSelect.Enabled = false;
                    lnkApagarSelect.CssClass = "btn btn-info btn-new disabled";
                    lnkCopiarSelect.CssClass = "btn btn-info btn-new disabled";
                    lnkEncenderSelect.CssClass = "btn btn-info btn-new disabled";
                }
            }
            catch (Exception)
            {

            }
        }
        public void panelApagar(object sender, EventArgs e)
        {
            try
            {
                LinkButton btApagarPc = (LinkButton)sender;
                int id = Convert.ToInt32(btApagarPc.ID.Replace("bt_apagar", ""));
                computadora pc = ent.computadora.Find(id);
                lbid_pcAccion.Text = id + "";
                btConfirmarApagar.Visible = true;
                btConfirmarEncender.Visible = false;
                panelEncenderconIP.Visible = false;
                lbMensajeConfirmar.Text = "¿Desea apagar el equipo " + pc.nombre + "?";
                modalPopupConfirmar.Show();
                UpdatePanel3.Update();
            }
            catch (Exception)
            {

            }
        }

        public void apagar(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(lbid_pcAccion.Text);
                computadora pc = ent.computadora.Find(id);
                int id_user = Convert.ToInt32(lbid_usuario.Text);
                usuario u = cont.getUsuario(id_user);
                string command = @"\\" + pc.nombre + " -u DESKTOP-IFOB8Q2\\" + u.nick + " -p " + contPass.Decrypt(u.pass) + " shutdown -p";
                string disk = cont.getMainDisk();
                string directory = disk + @"Windows\System32";
                string respuesta = cont.ExecuteCommand(directory, "psexec", "Arturokin12", "godofwarjaja123", command);
                lbTituloMensaje.Text = "Resultado de Apagar Equipo " + pc.nombre;
                lbMensaje.Text = respuesta;
                modalPupoExtenderMensaje.Show();
                hideModalConfirm(sender, e);
                UpdatePanel1.Update();
            }
            catch (Exception ex)
            {

            }
        }

        public void hideModalConfirm(object sender, EventArgs e)
        {
            modalPopupConfirmar.Hide();
            UpdatePanel1.Update();
        }
        public void panelEncender(object sender, EventArgs e)
        {
            try
            {
                int id_user = Convert.ToInt32(lbid_usuario.Text);
                usuario u = cont.getUsuario(2);
                config config = ent.config.ToList().ElementAt(0);
                LinkButton btpc = (LinkButton)sender;
                string id_pc = btpc.ID.Replace("bt_Encender", "");
                int id_computadora = Convert.ToInt32(id_pc);
                panelEncenderconIP.Visible = true;
                computadora c = ent.computadora.Find(id_computadora);
                lbid_pcAccion.Text = id_pc;
                lbMensajeConfirmar.Text = "¿Desea encender el equipo " + c.nombre + "?";
                string ip_pc = "";
                btConfirmarEncender.Visible = true;
                btConfirmarApagar.Visible = false;
                modalPopupConfirmar.Show();
                UpdatePanel1.Update();
            }
            catch (Exception ex)
            {

            }

        }

        public void encender()
        {
            int id = Convert.ToInt32(lbid_pcAccion.Text);
            computadora pc = ent.computadora.Find(id);
            int id_user = Convert.ToInt32(lbid_usuario.Text);
            usuario u = cont.getUsuario(id_user);
            if (chkEncenderIP.Checked == true)
            {
                config config = ent.config.ToList().ElementAt(0);
                List<ComputadorDom> listaPcs = cont.getIpAllPcs(config.ip_red, u);
                string ip_pc = "";
                foreach (var i in listaPcs)
                {
                    if (i.mac.Equals(pc.Mac.Replace("-", ":")))
                    {
                        ip_pc = i.ip;
                    }
                }
                cont.WakeUp(pc.Mac.Replace(":", ""), ip_pc);
            }
            else
            {
                cont.WakeUp(pc.Mac.Replace(":", ""), pc.ip);
            }
        }
        public void Remoto(object sender, EventArgs e)
        {

        }
        public void CopiarPopu(object sender, EventArgs e)
        {
            modalPopupExtenderCopiar.Show();
            LinkButton bt = (LinkButton)sender;
            string id = bt.ID.Replace("bt_Copiar", "");
            lbSelectedPc.Text = id;
            MultiView1.ActiveViewIndex = 0;
            Eliminar_Archivos();

        }

        public void ViewCopiarArchivos(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 0;

        }
        public void ViewCopiarCarpetas(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 1;
        }

        protected void AjaxFileUpload1_UploadComplete(object sender, AjaxControlToolkit.AjaxFileUploadEventArgs e)
        {
            string filePath = Server.MapPath("~/Data/") + e.FileName;
            Archivos.Add(filePath);
            CargarArchivos.SaveAs(filePath);

        }
        public void Eliminar_Archivos()
        {
            try
            {
                string filePath = Server.MapPath("~/Data/");
                string[] Lista = Directory.GetFiles(filePath);
                int cantidad = Lista.Length;

                if (cantidad >= 1)
                {
                    foreach (string f in Lista)
                    {
                        File.Delete(f);
                    }

                }

            }
            catch (Exception)
            {


            }
        }

        public void CopiarArchivos(object sender, EventArgs e)
        {
            try
            {
                string filePath2 = "";
                int id = Convert.ToInt32(lbSelectedPc.Text);
                computadora c = ent.computadora.Find(id);
                if (rbDiscoC.Checked)
                {
                    filePath2 = @"\\" + c.nombre + @"\C$";
                }

                filePath2 = Server.MapPath("~/Movidos/");
                foreach (var f in Archivos)
                {
                    string[] archvs = f.Split('\\');
                    if (File.Exists(f))
                    {
                        if (File.Exists(filePath2 + archvs[archvs.Length - 1]))
                        {
                            File.Delete(filePath2 + archvs[archvs.Length - 1]);
                            File.Move(f, filePath2 + archvs[archvs.Length - 1]);
                            Eliminar_Archivos();
                            lbMovidos.Text = "Archivos removido movidos";
                        }
                        else
                        {
                            File.Move(f, filePath2 + archvs[archvs.Length - 1]);
                            lbMovidos.Text = "Archivos movidos";
                            Eliminar_Archivos();
                        }
                    }
                    else
                    {
                        lbMovidos.Text = "error, No hay Archivos";
                    }
                }
            }
            catch (Exception ex)
            { }

        }

        protected void lnkApagar_Click(object sender, EventArgs e)
        {

        }

        protected void btConfirmarApagarTodos_Click(object sender, EventArgs e)
        {
            if (Num_Lab.Text != "")
            {
                int id_lab = Convert.ToInt32(Num_Lab.Text);
                laboratorio l = ent.laboratorio.Find(id_lab);
                var consulta = from c in ent.computadora where c.id_laboratorio == id_lab select c;
                List<computadora> pcs = consulta.ToList();
                foreach (var i in pcs)
                {
                    computadora pc = ent.computadora.Find(i.id_computadora);
                    int id_user = Convert.ToInt32(lbid_usuario.Text);
                    usuario u = cont.getUsuario(id_user);
                    string command = @"\\" + pc.nombre + " -u " + u.nick + " -p " + contPass.Decrypt(u.pass) + " shutdown -p";
                    string disk = cont.getMainDisk();
                    string directory = disk + @"Windows\System32";
                    string respuesta = cont.ExecuteCommand(directory, "psexec", "Arturokin12", "godofwarjaja123", command);
                    modalPupoExtenderMensaje.Show();
                    hideModalConfirm(sender, e);
                }
                lbTituloMensaje.Text = "Apagar Laboratorio "+l.nombre;
                lbMensaje.Text = "Laboratorio Apagado";
                UpdatePanel1.Update();
            }
        }

        protected void btConfirmarEncenderTodos_Click(object sender, EventArgs e)
        {

        }

        protected void btConfirmarEncender_Click(object sender, EventArgs e)
        {

        }
    }
}
