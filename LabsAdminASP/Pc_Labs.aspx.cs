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
using System.Threading;

namespace LabsAdminASP
{
    public partial class Pc_Labs : System.Web.UI.Page
    {
        //controlador de Modelo de BD
        LabsAdminEntities1 ent = new LabsAdminEntities1();
        //controlador de funciones
        controladorUser cont = new controladorUser();
        //funciones de encriptación
        ControladorPass contPass = new ControladorPass();
        //lista para registrar archivos subidos y luego copiar
        static List<string> Archivos = new List<string>();
        //lista de pcs seleccionados para realizar acciones
        static List<string> SelectedPcs = new List<string>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //obtener el id del laboratorio seleccionado
                string x = Session["id_laboratorio"].ToString();
                string id_lab = x.Replace("btLab", "");
                laboratorio laboratorio = ent.laboratorio.Find(Convert.ToInt32(id_lab));
                //guardar id del usuario que tiene iniciada la sesión
                lbid_usuario.Text = "3";
                Num_Lab.Text = Session["id_laboratorio"].ToString();
            }
        }

        public void Page_Init(object sender, EventArgs e)
        {
            //funcion para mostrar los pcs del laboratorio seleccionado
            Cargar_Pcs();
        }
        /// <summary>
        /// mostrar todos los pcs de un laboratorio determinado
        /// </summary>
        public void Cargar_Pcs()
        {
            try
            {
                //obtener laboratorio seleccionado
                string x = Session["id_laboratorio"].ToString();
                string id_lab = x.Replace("btLab", "");
                List<computadora> Pcs = cont.getPCs(Convert.ToInt32(id_lab));
                //recorrer pcs del laboratorio para agregar dinamicamente los controles
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

                    LinkButton lbt_cuentas = new LinkButton();
                    lbt_cuentas.ID = "bt_Cuenta" + i.id_computadora;
                    lbt_cuentas.Click += new EventHandler(panelLimpiarCuentas);
                    lbt_cuentas.ToolTip = "Limpiar cuentas inactivas";
                    lbt_cuentas.Text = "<i class='fa fa-eraser' aria-hidden=true'></i>";
                    div5.Controls.Add(lbt_cuentas);

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
        /// <summary>
        /// Acción realiada al dar check a un checkbox, para agregar o quitar de la lista el pc relacionado
        /// </summary>
        public void selectPc(object sender, EventArgs e)
        {
            try
            {
                //obtener id del checkbox relacionado al pc 
                CheckBox chk = (CheckBox)sender;
                string id = chk.ID.Replace("chk", "");
                //verificar si se accionó el checkbox
                if (chk.Checked == true)
                {
                    //agregar a la lista de pcs seleccionados
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
                    //eliminar el pc deseleccionado
                    SelectedPcs.Remove(id);
                }
                //si no hay ningun pc seleccionado
                if (SelectedPcs.Count > 0)
                {
                    //habilitar los los botones de accion
                    lnkApagarSelect.Enabled = true;
                    lnkCopiarSelect.Enabled = true;
                    lnkEncenderSelect.Enabled = true;
                    lnkLimpiarCuentas.Enabled = true;
                    lnkApagarSelect.CssClass = "btn btn-info btn-new";
                    lnkCopiarSelect.CssClass = "btn btn-info btn-new";
                    lnkEncenderSelect.CssClass = "btn btn-info btn-new";
                    lnkLimpiarCuentas.CssClass = "btn btn-info btn-new";
                }
                else
                {
                    //deshabilitar los botones de acción
                    lnkApagarSelect.Enabled = false;
                    lnkCopiarSelect.Enabled = false;
                    lnkEncenderSelect.Enabled = false;
                    lnkLimpiarCuentas.Enabled = false;
                    lnkApagarSelect.CssClass = "btn btn-info btn-new disabled";
                    lnkCopiarSelect.CssClass = "btn btn-info btn-new disabled";
                    lnkEncenderSelect.CssClass = "btn btn-info btn-new disabled";
                    lnkLimpiarCuentas.CssClass = "btn btn-info btn-new disabled";
                }
            }
            catch (Exception ex)
            {

            }
        }
        /// <summary>
        /// Levantar modal de confirmar acción de apagar un pc
        /// </summary>
        public void panelApagar(object sender, EventArgs e)
        {
            try
            {
                //obtener el id del boton presionado (contiene id del pc)
                LinkButton btApagarPc = (LinkButton)sender;
                int id = Convert.ToInt32(btApagarPc.ID.Replace("bt_apagar", ""));
                computadora pc = ent.computadora.Find(id);
                //guardar el id del pc seleccionado en un label hidden
                lbid_pcAccion.Text = id + "";
                //ocultar botones de acción y mostrar el boton de apagar un pc
                btConfirmarEncender.Visible = false;
                btConfirmarEncenderTodos.Visible = false;
                btConfirmarEncenderSelected.Visible = false;

                btConfirmarApagar.Visible = true;
                btConfirmarApagarTodos.Visible = false;
                btConfirmarApagarSelected.Visible = false;

                btConfirmarLimpiarCuentas.Visible = false;
                btConfirmarLimpiarCuentasTodos.Visible = false;
                btConfirmarLimpiarCuentasSelected.Visible = false;
                //mostrar mensaje de confirmacion
                lbMensajeConfirmar.Text = "¿Desea apagar el equipo " + pc.nombre + "?";
                //levantar modal y actualizar panel
                modalPopupConfirmar.Show();
                UpdatePanel3.Update();
            }
            catch (Exception)
            {

            }
        }
        /// <summary>
        /// Apaga el equipo seleccionado
        /// </summary>
        public void apagar(object sender, EventArgs e)
        {
            try
            {
                //obtener id del del boton presionado (contiene id del pc)
                int id = Convert.ToInt32(lbid_pcAccion.Text);
                computadora pc = ent.computadora.Find(id);
                //obtener usuario que tiene iniciado sesión
                int id_user = Convert.ToInt32(lbid_usuario.Text);
                usuario u = cont.getUsuario(id_user);
                //obtener configuración para usar ip de la red
                config c = ent.config.ToList().ElementAt(0);
                string command = "";
                //si esta seleccionada la opcion de realizar acción con la ip en vez del nombre del equipo
                if (chkIP.Checked == true)
                {
                    //obtener pcs en la red con nmap, esto tomara mucho tiempo
                    List<ComputadorDom> pcs = cont.getIpAllPcs(c.ip_red, u);
                    foreach (var i in pcs)
                    {
                        //encontrar el pc a traves de la mac
                        if (i.mac.Equals(pc.Mac))
                        {
                            //crear string del comando usando la ip del pc
                            command = @"\\" + i.ip + " -u DESKTOP-IFOB8Q2\\" + u.nick + " -p " + contPass.Decrypt(u.pass) + " shutdown -p";
                        }
                    }
                }
                else
                {
                    //crear string del comando usando el nombre del pc
                    command = @"\\" + pc.nombre + " -u DESKTOP-IFOB8Q2\\" + u.nick + " -p " + contPass.Decrypt(u.pass) + " shutdown -p";
                }
                //obtener el disco principal del pc
                string disk = cont.getMainDisk();
                //establecer ruta de trabajo del comando cmd
                string directory = disk + @"Windows\System32";
                //ejecutar comando y obtener respuesta
                string respuesta = cont.ExecuteCommand(directory, "psexec", "Arturokin12", "godofwarjaja123", command);
                //mostrar mensaje, edita el titulo del modal, el mesaje y luego lo levanta
                lbTituloMensaje.Text = "Resultado de Apagar Equipo " + pc.nombre;
                lbMensaje.Text = respuesta;
                modalPupoExtenderMensaje.Show();
                //esconder el modal de confirmar acción
                hideModalConfirm(sender, e);
                UpdatePanel1.Update();
            }
            catch (Exception ex)
            {

            }
        }

        /// <summary>
        /// Obtiene todos pcs de un laboratorio para apagarlos
        /// </summary>
        protected void btConfirmarApagarTodos_Click(object sender, EventArgs e)
        {
            //si el label del id del laboratorio no está vacío
            if (Num_Lab.Text != "")
            {
                //obtener laboratorio desde la base de datos
                int id_lab = Convert.ToInt32(Num_Lab.Text.Replace("btLab", ""));
                laboratorio l = ent.laboratorio.Find(id_lab);
                //obtener los pcs del laboratorio seleccionado
                var consulta = from c in ent.computadora where c.id_laboratorio == id_lab select c;
                List<computadora> pcs = consulta.ToList();
                //obtener el usuario que tiene iniciada la sesion desde la base de datos
                int id_user = Convert.ToInt32(lbid_usuario.Text);
                usuario u = cont.getUsuario(id_user);
                //lista para almacenar pcs de la red en caso de usar la funcion de obtener ip
                List<ComputadorDom> pcsRed = new List<ComputadorDom>();
                //obtener configuración para utilizar la ip de la red
                config conf = ent.config.ToList().ElementAt(0);
                //si se seleccionó la opcion de utilizar la ip obtener los pcs de la red con nmap
                if (chkIP.Checked == true)
                {
                    pcsRed = cont.getIpAllPcs(conf.ip_dominio, u);
                }
                //obtener el disco principal del pc
                string disk = cont.getMainDisk();
                string directory = disk + @"Windows\System32";
                //recorred los pcs del laboratorio
                foreach (var i in pcs)
                {
                    //obtener pc de la computadora en recorrido
                    computadora pc = ent.computadora.Find(i.id_computadora);
                    //crear comando para apagar el equipo
                    string command = @"\\" + pc.nombre + " -u " + u.nick + " -p " + contPass.Decrypt(u.pass) + " shutdown -p";
                    //crear proceso en segundo plano de ejecucion del comando
                    ThreadStart thread = new ThreadStart(() => cont.ExecuteCommand(directory, "psexec", "Arturokin12", "godofwarjaja123", command));
                    Thread child = new Thread(thread);
                    child.Start();
                }
                //mensaje de respuesta en un modal
                lbTituloMensaje.Text = "Apagar Laboratorio " + l.nombre;
                lbMensaje.Text = "Laboratorio Apagado";
                //ocultar modal de confirmar acción
                hideModalConfirm(sender, e);
                //levantar modal de mensaje
                modalPupoExtenderMensaje.Show();
                UpdatePanel1.Update();
            }
        }

        /// <summary>
        /// esconde el modal de confirmar acción en administración de labs
        /// </summary>
        public void hideModalConfirm(object sender, EventArgs e)
        {
            //esconder el modal de confirmar acción
            modalPopupConfirmar.Hide();
            UpdatePanel1.Update();
        }
        /// <summary>
        /// Levanta un modal para confirmar acción de encender equipo
        /// </summary>
        public void panelEncender(object sender, EventArgs e)
        {
            try
            {
                //obtener el usuario que tiene iniciado sesión
                int id_user = Convert.ToInt32(lbid_usuario.Text);
                usuario u = cont.getUsuario(2);
                //obtener configuracion para usar ip de la red
                config config = ent.config.ToList().ElementAt(0);
                //obtener boton presionado (contiene el id del pc)
                LinkButton btpc = (LinkButton)sender;
                string id_pc = btpc.ID.Replace("bt_Encender", "");
                //obtener id de el pc en acción
                int id_computadora = Convert.ToInt32(id_pc);
                //ocultar panel de utilizar ip
                panelconIP.Visible = false;
                //obtener computadora seleccionada desde la base de datos
                computadora c = ent.computadora.Find(id_computadora);
                //guardar id de la computadora seleccionada en un label hidden
                lbid_pcAccion.Text = id_pc;
                //mostrar mensaje de confirmación
                lbMensajeConfirmar.Text = "¿Desea encender el equipo " + c.nombre + "?";
                //ocultar botones y mostrar el de "encender pc"
                btConfirmarEncender.Visible = true;
                btConfirmarEncenderTodos.Visible = false;
                btConfirmarEncenderSelected.Visible = false;

                btConfirmarApagar.Visible = false;
                btConfirmarApagarTodos.Visible = false;
                btConfirmarApagarSelected.Visible = false;

                btConfirmarLimpiarCuentas.Visible = false;
                btConfirmarLimpiarCuentasTodos.Visible = false;
                btConfirmarLimpiarCuentasSelected.Visible = false;
                //levantar modal y actualizar updatepanel
                modalPopupConfirmar.Show();
            }
            catch (Exception ex)
            {

            }

        }

        /// <summary>
        /// Levanta un modal para confirmar acción de encender todos los equipos de un laboratorio
        /// </summary>
        public void panelEncenderTodos(object sender, EventArgs e)
        {
            try
            {
                //ocultar panel de utilizar ip
                panelconIP.Visible = false;
                //obtener laboratorio seleccionado
                int id_lab = Convert.ToInt32(Num_Lab.Text.Replace("btLab", ""));
                laboratorio lab = ent.laboratorio.Find(id_lab);
                //mostrar mensaje de confirmación
                lbMensajeConfirmar.Text = "¿Desea encender los equipos del laboratorio " + lab.nombre + "?";
                //ocultar botones y mostrar el de "encender todos"
                btConfirmarEncender.Visible = false;
                btConfirmarEncenderTodos.Visible = true;
                btConfirmarEncenderSelected.Visible = false;

                btConfirmarApagar.Visible = false;
                btConfirmarApagarTodos.Visible = false;
                btConfirmarApagarSelected.Visible = false;

                btConfirmarLimpiarCuentas.Visible = false;
                btConfirmarLimpiarCuentasTodos.Visible = false;
                btConfirmarLimpiarCuentasSelected.Visible = false;
                //mostar modal
                modalPopupConfirmar.Show();
                UpdatePanel1.Update();
            }
            catch (Exception ex)
            {

            }

        }

        /// <summary>
        /// Levanta un modal para confirmar acción de encender todos los equipos de un laboratorio
        /// </summary>
        public void panelEncenderSelected(object sender, EventArgs e)
        {
            try
            {
                //ocultar panel de utilizar ip
                panelconIP.Visible = false;
                //mostrar mensaje de confirmación
                lbMensajeConfirmar.Text = "¿Desea encender los equipos Seleccionados?";
                //ocultar botones y mostrar el de "encender todos"
                btConfirmarEncender.Visible = false;
                btConfirmarEncenderTodos.Visible = false;
                btConfirmarEncenderSelected.Visible = true;

                btConfirmarApagar.Visible = false;
                btConfirmarApagarTodos.Visible = false;
                btConfirmarApagarSelected.Visible = false;

                btConfirmarLimpiarCuentas.Visible = false;
                btConfirmarLimpiarCuentasTodos.Visible = false;
                btConfirmarLimpiarCuentasSelected.Visible = false;
                //mostar modal
                modalPopupConfirmar.Show();
                UpdatePanel1.Update();
            }
            catch (Exception ex)
            {

            }

        }

        /// <summary>
        /// Levanta un modal para confirmar acción de limpiar cuentas de equipo
        /// </summary>
        public void panelLimpiarCuentas(object sender, EventArgs e)
        {
            try
            {
                //obtener el id del boton presionado (contiene id del pc)
                LinkButton btLimpiarCuentas = (LinkButton)sender;
                int id = Convert.ToInt32(btLimpiarCuentas.ID.Replace("bt_Cuenta", ""));
                computadora pc = ent.computadora.Find(id);
                //guardar el id del pc seleccionado en un label hidden
                lbid_pcAccion.Text = id + "";
                //ocultar botones de acción y mostrar el boton de apagar un pc
                btConfirmarEncender.Visible = false;
                btConfirmarEncenderTodos.Visible = false;
                btConfirmarEncenderSelected.Visible = false;

                btConfirmarApagar.Visible = false;
                btConfirmarApagarTodos.Visible = false;
                btConfirmarApagarSelected.Visible = false;

                btConfirmarLimpiarCuentas.Visible = true;
                btConfirmarLimpiarCuentasTodos.Visible = false;
                btConfirmarLimpiarCuentasSelected.Visible = false;
                //mostrar mensaje de confirmacion
                lbMensajeConfirmar.Text = "¿Desea limpiar las cuentas inactivas del equipo " + pc.nombre + "?";
                //levantar modal y actualizar panel
                modalPopupConfirmar.Show();
                UpdatePanel3.Update();
            }
            catch (Exception)
            {

            }
        }

        public void panelLimpiarCuentasTodos(object sender, EventArgs e)
        {
            try
            {
                //ocultar panel de utilizar ip
                panelconIP.Visible = false;
                //obtener laboratorio seleccionado
                int id_lab = Convert.ToInt32(Num_Lab.Text.Replace("btLab", ""));
                laboratorio lab = ent.laboratorio.Find(id_lab);
                //mostrar mensaje de confirmación
                lbMensajeConfirmar.Text = "¿Desea limpiar las cuentas de los equipos del laboratorio " + lab.nombre + "?";
                //ocultar botones de acción y mostrar el boton de apagar un pc
                btConfirmarEncender.Visible = false;
                btConfirmarEncenderTodos.Visible = false;
                btConfirmarEncenderSelected.Visible = false;

                btConfirmarApagar.Visible = false;
                btConfirmarApagarTodos.Visible = false;
                btConfirmarApagarSelected.Visible = false;

                btConfirmarLimpiarCuentas.Visible = false;
                btConfirmarLimpiarCuentasTodos.Visible = true;
                btConfirmarLimpiarCuentasSelected.Visible = false;
                //levantar modal y actualizar panel
                modalPopupConfirmar.Show();
                UpdatePanel3.Update();
            }
            catch (Exception)
            {

            }
        }

        public void panelLimpiarCuentasSelected(object sender, EventArgs e)
        {
            try
            {
                //ocultar panel de utilizar ip
                panelconIP.Visible = false;
                //obtener laboratorio seleccionado
                int id_lab = Convert.ToInt32(Num_Lab.Text.Replace("btLab", ""));
                laboratorio lab = ent.laboratorio.Find(id_lab);
                //mostrar mensaje de confirmación
                lbMensajeConfirmar.Text = "¿Desea limpiar las cuentas de los equipos seleccionados?";
                //ocultar botones de acción y mostrar el boton de apagar un pc
                btConfirmarEncender.Visible = false;
                btConfirmarEncenderTodos.Visible = false;
                btConfirmarEncenderSelected.Visible = false;

                btConfirmarApagar.Visible = false;
                btConfirmarApagarTodos.Visible = false;
                btConfirmarApagarSelected.Visible = false;

                btConfirmarLimpiarCuentas.Visible = false;
                btConfirmarLimpiarCuentasTodos.Visible = false;
                btConfirmarLimpiarCuentasSelected.Visible = true;
                //levantar modal y actualizar panel
                modalPopupConfirmar.Show();
                UpdatePanel3.Update();
            }
            catch (Exception)
            {

            }
        }

        public void confirmarLimpiarCuentas(object sender, EventArgs e)
        {
            try
            {
                //obtener pc desde la base de datos
                int id = Convert.ToInt32(lbid_pcAccion.Text);
                computadora pc = ent.computadora.Find(id);
                //obtener usuario que tiene iniciada la sesión
                int id_user = Convert.ToInt32(lbid_usuario.Text);
                usuario u = cont.getUsuario(id_user);
                //obtener configuración para usar ip de la red
                config c = ent.config.ToList().ElementAt(0);
                if (File.Exists("\\" + pc.nombre + @"\c$\windows\system32\delprof2.exe"))
                {
                    string command = @"\\" + pc.nombre + " -u " + u.nick + " -p " + contPass.Decrypt(u.pass) + " delprof2 /q";
                    //obtener el disco principal del pc
                    string disk = cont.getMainDisk();
                    //establecer ruta de trabajo del comando cmd
                    string directory = disk + @"Windows\System32";
                    ThreadStart thread = new ThreadStart(() => cont.ExecuteCommand(directory, "psexec", u.nick, contPass.Decrypt(u.pass), command));
                    Thread child = new Thread(thread);
                    child.Start();
                    //mostrar mensaje, edita el titulo del modal, el mesaje y luego lo levanta
                    lbTituloMensaje.Text = "Limpiar cuentas de " + pc.nombre;
                    lbMensaje.Text = "El proceso de limpiado de cuentas se ha iniciado en segundo plano.";
                    modalPupoExtenderMensaje.Show();
                    //esconder el modal de confirmar acción
                    hideModalConfirm(sender, e);
                    UpdatePanel1.Update();
                }
                else
                {
                    File.Copy(Server.MapPath("~/Thirds/Delprof2.exe"), @"\\" + pc.nombre + @"\c$\windows\system32\Delprof2.exe");
                    string command = @"\\" + pc.nombre + " -u " + u.nick + " -p " + contPass.Decrypt(u.pass) + " delprof2 /q";
                    //obtener el disco principal del pc
                    string disk = cont.getMainDisk();
                    //establecer ruta de trabajo del comando cmd
                    string directory = disk + @"Windows\System32";
                    ThreadStart thread = new ThreadStart(() => cont.ExecuteCommand(directory, "psexec", "Arturokin12", "godofwarjaja123", command));
                    Thread child = new Thread(thread);
                    child.Start();
                    //mostrar mensaje, edita el titulo del modal, el mesaje y luego lo levanta
                    lbTituloMensaje.Text = "Limpiar cuentas de " + pc.nombre;
                    lbMensaje.Text = "El proceso de limpiado de cuentas se ha iniciado en segundo plano.";
                    modalPupoExtenderMensaje.Show();
                    //esconder el modal de confirmar acción
                    hideModalConfirm(sender, e);
                    UpdatePanel1.Update();
                }
            }
            catch (Exception ex)
            {
                lbTituloMensaje.Text = "Error al limpiar cuentas";
                lbMensaje.Text = ex.ToString().Substring(0, 500);
                modalPupoExtenderMensaje.Show();
                //esconder el modal de confirmar acción
                hideModalConfirm(sender, e);
                UpdatePanel1.Update();
            }
        }

        public void confirmarLimpiarCuentasTodos(object sender, EventArgs e)
        {
            try
            {
                //obtener laboratorio seleccionado
                int id_lab = Convert.ToInt32(Num_Lab.Text.Replace("btLab", ""));
                laboratorio lab = ent.laboratorio.Find(id_lab);
                //lista de pcs del laboratorio
                var consulta = from comp in ent.computadora where comp.id_laboratorio.Equals(id_lab) select comp;
                List<computadora> pcs = consulta.ToList();
                //obtener usuario que tiene iniciada la sesión
                int id_user = Convert.ToInt32(lbid_usuario.Text);
                usuario u = cont.getUsuario(id_user);
                //obtener configuración para usar ip de la red
                config c = ent.config.ToList().ElementAt(0);
                foreach (var pc in pcs)
                {
                    if (File.Exists("\\" + pc.nombre + @"\c$\windows\system32\delprof2.exe"))
                    {
                        string command = @"\\" + pc.nombre + " -u " + u.nick + " -p " + contPass.Decrypt(u.pass) + " delprof2 /q";
                        //obtener el disco principal del pc
                        string disk = cont.getMainDisk();
                        //establecer ruta de trabajo del comando cmd
                        string directory = disk + @"Windows\System32";
                        ThreadStart thread = new ThreadStart(() => cont.ExecuteCommand(directory, "psexec", u.nick, contPass.Decrypt(u.pass), command));
                        Thread child = new Thread(thread);
                        child.Start();
                    }
                    else
                    {
                        File.Copy(Server.MapPath("~/Thirds/Delprof2.exe"), @"\\" + pc.nombre + @"\c$\windows\system32\Delprof2.exe");
                        string command = @"\\" + pc.nombre + " -u " + u.nick + " -p " + contPass.Decrypt(u.pass) + " delprof2 /q";
                        //obtener el disco principal del pc
                        string disk = cont.getMainDisk();
                        //establecer ruta de trabajo del comando cmd
                        string directory = disk + @"Windows\System32";
                        ThreadStart thread = new ThreadStart(() => cont.ExecuteCommand(directory, "psexec", "Arturokin12", "godofwarjaja123", command));
                        Thread child = new Thread(thread);
                        child.Start();
                    }
                }
                //mostrar mensaje, edita el titulo del modal, el mesaje y luego lo levanta
                lbTituloMensaje.Text = "Limpiar cuentas de laboratorio " + lab.nombre;
                lbMensaje.Text = "El proceso de limpiado de cuentas se ha iniciado en segundo plano para el laboratorio.";
                modalPupoExtenderMensaje.Show();
                //esconder el modal de confirmar acción
                hideModalConfirm(sender, e);
                UpdatePanel1.Update();
            }
            catch (Exception ex)
            {
                lbTituloMensaje.Text = "Error al limpiar cuentas";
                lbMensaje.Text = ex.ToString().Substring(0, 500);
                modalPupoExtenderMensaje.Show();
                //esconder el modal de confirmar acción
                hideModalConfirm(sender, e);
                UpdatePanel1.Update();
            }
        }

        public void LimpiarCuentasSelected(object sender, EventArgs e)
        {
            try
            {
                //obtener usuario que tiene iniciada la sesión
                int id_user = Convert.ToInt32(lbid_usuario.Text);
                usuario u = cont.getUsuario(id_user);
                //obtener configuración para usar ip de la red
                config c = ent.config.ToList().ElementAt(0);
                foreach (var i in SelectedPcs)
                {
                    int id_pc = Convert.ToInt32(i);
                    computadora pc = ent.computadora.Find(id_pc);
                    if (File.Exists("\\" + pc.nombre + @"\c$\windows\system32\delprof2.exe"))
                    {
                        string command = @"\\" + pc.nombre + " -u " + u.nick + " -p " + contPass.Decrypt(u.pass) + " delprof2 /q";
                        //obtener el disco principal del pc
                        string disk = cont.getMainDisk();
                        //establecer ruta de trabajo del comando cmd
                        string directory = disk + @"Windows\System32";
                        ThreadStart thread = new ThreadStart(() => cont.ExecuteCommand(directory, "psexec", u.nick, contPass.Decrypt(u.pass), command));
                        Thread child = new Thread(thread);
                        child.Start();
                    }
                    else
                    {
                        File.Copy(Server.MapPath("~/Thirds/Delprof2.exe"), @"\\" + pc.nombre + @"\c$\windows\system32\Delprof2.exe");
                        string command = @"\\" + pc.nombre + " -u " + u.nick + " -p " + contPass.Decrypt(u.pass) + " delprof2 /q";
                        //obtener el disco principal del pc
                        string disk = cont.getMainDisk();
                        //establecer ruta de trabajo del comando cmd
                        string directory = disk + @"Windows\System32";
                        ThreadStart thread = new ThreadStart(() => cont.ExecuteCommand(directory, "psexec", "Arturokin12", "godofwarjaja123", command));
                        Thread child = new Thread(thread);
                        child.Start();
                    }
                }
                //mostrar mensaje, edita el titulo del modal, el mesaje y luego lo levanta
                lbTituloMensaje.Text = "Limpiar cuentas de computadoras";
                string mensaje = "El proceso de limpiado de cuentas se ha iniciado en segundo plano para: \n";
                foreach (var i in SelectedPcs)
                {
                    int id_pc = Convert.ToInt32(i);
                    computadora pc = ent.computadora.Find(id_pc);
                    mensaje = mensaje + pc.nombre + ";   ";
                }
                lbMensaje.Text = mensaje;
                modalPupoExtenderMensaje.Show();
                //esconder el modal de confirmar acción
                hideModalConfirm(sender, e);
                UpdatePanel1.Update();
            }
            catch (Exception ex)
            {
                lbTituloMensaje.Text = "Error al Limpiar cuentas";
                lbMensaje.Text = ex.ToString().Substring(0, 500);
                modalPupoExtenderMensaje.Show();
                //esconder el modal de confirmar acción
                hideModalConfirm(sender, e);
                UpdatePanel1.Update();
            }
        }

        
        /// <summary>
        /// Levantar panel para acción de apagar todos los pcs de un laboratorio
        /// </summary>
        public void panelApagarTodos(object sender, EventArgs e)
        {
            try
            {
                //mostrar panel para utilizar ip al realizar la acción de apagar todos
                panelconIP.Visible = true;
                //obtener laboratorio seleccionado
                int id_lab = Convert.ToInt32(Num_Lab.Text.Replace("btLab", ""));
                laboratorio lab = ent.laboratorio.Find(id_lab);
                //mostrar mensaje de confirmación
                lbMensajeConfirmar.Text = "¿Desea encender los equipos del laboratorio " + lab.nombre + "?";
                //ocultar botones y mostrar el de "apagar todos"
                btConfirmarEncender.Visible = false;
                btConfirmarEncenderTodos.Visible = false;
                btConfirmarEncenderSelected.Visible = false;

                btConfirmarApagar.Visible = false;
                btConfirmarApagarTodos.Visible = true;
                btConfirmarApagarSelected.Visible = false;

                btConfirmarLimpiarCuentas.Visible = false;
                btConfirmarLimpiarCuentasTodos.Visible = false;
                btConfirmarLimpiarCuentasSelected.Visible = false;
                //levantar modal
                modalPopupConfirmar.Show();
                UpdatePanel1.Update();
            }
            catch (Exception ex)
            {

            }
        }

        public void panelApagarSelected(object sender, EventArgs e)
        {
            try
            {
                //mostrar panel para utilizar ip al realizar la acción de apagar todos
                panelconIP.Visible = true;
                //obtener laboratorio seleccionado
                int id_lab = Convert.ToInt32(Num_Lab.Text.Replace("btLab", ""));
                laboratorio lab = ent.laboratorio.Find(id_lab);
                //mostrar mensaje de confirmación
                lbMensajeConfirmar.Text = "¿Desea apagar los equipos seleccionados? ";
                //ocultar botones y mostrar el de "apagar todos"
                btConfirmarEncender.Visible = false;
                btConfirmarEncenderTodos.Visible = false;
                btConfirmarEncenderSelected.Visible = false;

                btConfirmarApagar.Visible = false;
                btConfirmarApagarTodos.Visible = false;
                btConfirmarApagarSelected.Visible = true;

                btConfirmarLimpiarCuentas.Visible = false;
                btConfirmarLimpiarCuentasTodos.Visible = false;
                btConfirmarLimpiarCuentasSelected.Visible = false;
                //levantar modal
                modalPopupConfirmar.Show();
                UpdatePanel1.Update();
            }
            catch (Exception ex)
            {

            }
        }

        /// <summary>
        /// Utiliza la ip y la mac de un equipo para encenderlo a traves de red LAN
        /// </summary>
        public void encender(object sender, EventArgs e)
        {
            //obtener pc desde la base de datos
            int id = Convert.ToInt32(lbid_pcAccion.Text);
            computadora pc = ent.computadora.Find(id);
            //obtener usuario que tiene iniciada la sesión
            int id_user = Convert.ToInt32(lbid_usuario.Text);
            usuario u = cont.getUsuario(id_user);
            //encender equipo, necesita MAC sin guiones y dobles puntos
            cont.WakeUp(pc.Mac.Replace(":", ""), pc.ip);
        }

        /// <summary>
        /// Obtiene todos pcs de un laboratorio para encenderlos
        /// </summary>
        protected void ConfirmarEncenderTodos(object sender, EventArgs e)
        {
            try
            {
                //obtener laboratorio desde la base de datos
                int id_lab = Convert.ToInt32(Num_Lab.Text.Replace("btLab", ""));
                laboratorio l = ent.laboratorio.Find(id_lab);
                //consulta para obtener los pcs de un laboratorio
                var consulta = from c in ent.computadora where c.id_laboratorio == id_lab select c;
                List<computadora> pcs = consulta.ToList();
                //obtener usuario que tiene la sesion inicada de sde la base de datos
                int id_user = Convert.ToInt32(lbid_usuario.Text);
                usuario u = cont.getUsuario(id_user);
                //obtener configuracion para utilizar ip de red
                config config = ent.config.ToList().ElementAt(0);
                //recorred pcs y encenderlos con procesos en segundo plano
                foreach (var i in pcs)
                {
                    computadora pc = ent.computadora.Find(i.id_computadora);
                    ThreadStart thread = new ThreadStart(() => cont.WakeUp(pc.Mac.Replace(":", ""), pc.ip));
                    Thread child = new Thread(thread);
                    child.Start();
                }
                lbTituloMensaje.Text = "Encender Laboratorio";
                lbMensaje.Text = "Los computadore se encenderán en unos momentos...";
                modalPupoExtenderMensaje.Show();
                UpdatePanel1.Update();
            }
            catch (Exception ex)
            {
                lbTituloMensaje.Text = "Error al encender laboratorio";
                lbMensaje.Text = ex.ToString().Substring(0, 500);
                modalPupoExtenderMensaje.Show();
                UpdatePanel1.Update();
            }
        }

        protected void encenderSelected(object sender, EventArgs e)
        {
            try
            {
                if (SelectedPcs.Count > 0)
                {
                    //obtener laboratorio desde la base de datos
                    int id_lab = Convert.ToInt32(Num_Lab.Text.Replace("btLab", ""));
                    laboratorio l = ent.laboratorio.Find(id_lab);
                    //consulta para obtener los pcs de un laboratorio
                    var consulta = from c in ent.computadora where c.id_laboratorio == id_lab select c;
                    List<computadora> pcs = consulta.ToList();
                    //obtener usuario que tiene la sesion inicada de sde la base de datos
                    int id_user = Convert.ToInt32(lbid_usuario.Text);
                    usuario u = cont.getUsuario(id_user);
                    //obtener configuracion para utilizar ip de red
                    config config = ent.config.ToList().ElementAt(0);
                    //recorred pcs y encenderlos con procesos en segundo plano
                    foreach (var i in pcs)
                    {
                        computadora pc = ent.computadora.Find(i.id_computadora);
                        ThreadStart thread = new ThreadStart(() => cont.WakeUp(pc.Mac.Replace(":", ""), pc.ip));
                        Thread child = new Thread(thread);
                        child.Start();
                    }
                    lbTituloMensaje.Text = "Encender Laboratorio";
                    lbMensaje.Text = "Los computadore se encenderán en unos momentos...";
                    modalPupoExtenderMensaje.Show();
                    UpdatePanel1.Update();
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                lbTituloMensaje.Text = "Error al encender laboratorio";
                lbMensaje.Text = ex.ToString().Substring(0, 500);
                modalPupoExtenderMensaje.Show();
                UpdatePanel1.Update();
            }
        }

        public void Remoto(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// Levantar modal para enviar archivos al pc
        /// </summary>
        public void CopiarPopu(object sender, EventArgs e)
        {
            modalPopupExtenderCopiar.Show();
            LinkButton bt = (LinkButton)sender;
            string id = bt.ID.Replace("bt_Copiar", "");
            btCopiarArchivo.Visible = true;
            btCopiarArchivosSelected.Visible = false;
            btCopiarArchivotodos.Visible = false;
            lbSelectedPc.Text = id;
            //establece la view 0
            MultiView1.ActiveViewIndex = 0;
            //eliminar archivos copiados al servidor
            Eliminar_Archivos();

        }

        public void CopiarPopuTodos(object sender, EventArgs e)
        {
            modalPopupExtenderCopiar.Show();
            LinkButton bt = (LinkButton)sender;
            string id = bt.ID.Replace("bt_Copiar", "");
            btCopiarArchivo.Visible = false;
            btCopiarArchivosSelected.Visible = false;
            btCopiarArchivotodos.Visible = true;
            lbSelectedPc.Text = id;
            //establece la view 0
            MultiView1.ActiveViewIndex = 0;
            //eliminar archivos copiados al servidor
            Eliminar_Archivos();

        }

        public void CopiarPopuSelected(object sender, EventArgs e)
        {
            modalPopupExtenderCopiar.Show();
            LinkButton bt = (LinkButton)sender;
            string id = bt.ID.Replace("bt_Copiar", "");
            btCopiarArchivo.Visible = false;
            btCopiarArchivosSelected.Visible = true;
            btCopiarArchivotodos.Visible = false;
            lbSelectedPc.Text = id;
            //establece la view 0
            MultiView1.ActiveViewIndex = 0;
            //eliminar archivos copiados al servidor
            Eliminar_Archivos();
        }
        /// <summary>
        /// Cambiar view en modal de copiar archivos
        /// </summary>
        public void ViewCopiarArchivos(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 0;

        }
        /// <summary>
        /// Cambiar view en modal a enviar carpetas
        /// </summary>
        public void ViewCopiarCarpetas(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 1;
        }
        /// <summary>
        /// Funcion ejecutada al subir un archivo al servidor, se guarda la ruta del archivo en una lista para luego utilizarla
        /// </summary>
        protected void AjaxFileUpload1_UploadComplete(object sender, AjaxControlToolkit.AjaxFileUploadEventArgs e)
        {
            string filePath = Server.MapPath("~/Data/") + e.FileName;
            Archivos.Add(filePath);
            CargarArchivos.SaveAs(filePath);
        }
        /// <summary>
        /// Eliminar contenido de la carpeta DATA
        /// </summary>
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
                string[] subdirec = Directory.GetDirectories(filePath);
                foreach (var sub in subdirec)
                {
                    Directory.Delete(sub, true);
                }
            }
            catch (Exception)
            {


            }
        }
        /// <summary>
        /// Funcion para enviar múltiples archivos a un pc
        /// </summary>
        public void CopiarArchivos(object sender, EventArgs e)
        {
            btCopiarArchivo.Visible = true;
            btCopiarArchivotodos.Visible = false;
            try
            {
                string filePath2 = "";
                int id = Convert.ToInt32(lbSelectedPc.Text);
                computadora c = ent.computadora.Find(id);
                if (rbDiscoC.Checked)
                {
                    filePath2 = @"\\" + c.nombre + @"\C$";
                }
                if (rbEscritorio.Checked)
                {
                    filePath2 = @"\\" + c.nombre + @"\C$\Users\Public";
                }
                if (rbPersonalizado.Checked)
                {
                    filePath2 = @"\\" + c.nombre + @"\C$\" + txt_rutaNueva.Text;
                }
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
            {

            }

        }

        public void CopiarArchivosTodos(object sender, EventArgs e)
        {
            try
            {
                btCopiarArchivo.Visible = false;
                btCopiarArchivotodos.Visible = true;
                string filePath2 = "";
                //obtener laboratorio desde la base de datos
                int id_lab = Convert.ToInt32(Num_Lab.Text.Replace("btLab", ""));
                laboratorio l = ent.laboratorio.Find(id_lab);
                //consulta para obtener los pcs de un laboratorio
                var consulta = from c in ent.computadora where c.id_laboratorio == id_lab select c;
                List<computadora> pcs = consulta.ToList();
                //obtener usuario que tiene la sesion inicada de sde la base de datos
                foreach (var pc in pcs)
                {
                    if (rbDiscoC.Checked)
                    {
                        filePath2 = @"\\" + pc.nombre + @"\C$";
                    }
                    if (rbEscritorio.Checked)
                    {
                        filePath2 = @"\\" + pc.nombre + @"\C$\Users\Public";
                    }
                    if (rbPersonalizado.Checked)
                    {
                        filePath2 = @"\\" + pc.nombre + @"\C$\" + txt_rutaNueva.Text;
                    }
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

            }
            catch (Exception ex)
            {

            }
        }

        public void CopiarArchivosSelected(object sender, EventArgs e)
        {
            try
            {
                string filePath2 = "";
                foreach (var pc in SelectedPcs)
                {
                    computadora c = ent.computadora.Find(pc);
                    if (rbDiscoC.Checked)
                    {
                        filePath2 = @"\\" + c.nombre + @"\C$";
                    }
                    if (rbEscritorio.Checked)
                    {
                        filePath2 = @"\\" + c.nombre + @"\C$\Users\Public";
                    }
                    if (rbPersonalizado.Checked)
                    {
                        filePath2 = @"\\" + c.nombre + @"\C$\" + txt_rutaNueva.Text;
                    }
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

            }
            catch (Exception ex)
            {

            }
        }

        public void CopiarCarpetas(object sender, EventArgs e)
        {
            try
            {
                BtnCopiarCArpetasTodos.Visible = false;
                btnCOpiarCarpeta.Visible = true;
                string filepath1 = txtRuta.Text;
                string filePath2 = Server.MapPath("~/Data/");
                string filePath3 = "";
                DirectoryCopy(filepath1, filePath2, true);
                int id = Convert.ToInt32(lbSelectedPc.Text);
                computadora c = ent.computadora.Find(id);
                if (RbDiscoLocal2.Checked)
                {
                    string[] carp = filepath1.Split('\\');
                    filePath3 = @"\\" + c.nombre + @"\C$";
                    DirectoryCopy(filePath2 + carp[carp.Length - 1], filePath3, true);
                    Eliminar_Archivos();
                }
                if (RbEscritorioPublico2.Checked)
                {
                    string[] carp = filepath1.Split('\\');
                    filePath3 = @"\\" + c.nombre + @"\C$\Users\Public";
                    DirectoryCopy(filePath2 + carp[carp.Length - 1], filePath3, true);
                    Eliminar_Archivos();
                }
                if (RbPersonalizado2.Checked)
                {
                    string[] carp = filepath1.Split('\\');
                    filePath3 = @"\\" + c.nombre + @"\C$\" + txtPersonalizada2.Text;
                    DirectoryCopy(filePath2 + carp[carp.Length - 1], filePath3, true);
                    Eliminar_Archivos();
                }
            }
            catch (Exception ex)
            { }
        }

        public void CopiarCarpetasTodos(object sender, EventArgs e)
        {
            try
            {
                BtnCopiarCArpetasTodos.Visible = true;
                btnCOpiarCarpeta.Visible = false;
                foreach (var pc in SelectedPcs)
                {
                    string filepath1 = txtRuta.Text;
                    string filePath2 = Server.MapPath("~/Data/");
                    string filePath3 = "";
                    computadora c = ent.computadora.Find(pc);
                    DirectoryCopy(filepath1, filePath2, true);
                    if (RbDiscoLocal2.Checked)
                    {
                        string[] carp = filepath1.Split('\\');
                        filePath3 = @"\\" + c.nombre + @"\C$";
                        DirectoryCopy(filePath2 + carp[carp.Length - 1], filePath3, true);
                        Eliminar_Archivos();
                    }
                    if (RbEscritorioPublico2.Checked)
                    {
                        string[] carp = filepath1.Split('\\');
                        filePath3 = @"\\" + c.nombre + @"\C$\Users\Public";
                        DirectoryCopy(filePath2 + carp[carp.Length - 1], filePath3, true);
                        Eliminar_Archivos();
                    }
                    if (RbPersonalizado2.Checked)
                    {
                        string[] carp = filepath1.Split('\\');
                        filePath3 = @"\\" + c.nombre + @"\C$\" + txtPersonalizada2.Text;
                        DirectoryCopy(filePath2 + carp[carp.Length - 1], filePath3, true);
                        Eliminar_Archivos();
                    }
                }
            }
            catch (Exception ex)
            { }
        }

        private void DirectoryCopy(string RutaDirec, string DestinDirec, bool copiarSubDirs)
        {
            try
            {
                if (!Directory.Exists(DestinDirec))
                {
                    lbCarpMovidas.Text = "La ruta de destino NO fue Encontrada: ";
                }
                else
                {
                    DirectoryInfo dir = new DirectoryInfo(RutaDirec);
                    if (!dir.Exists)
                    {
                        lbCarpMovidas.Text = "El Nombre del Directorio NO fue Encontrado: ";
                    }
                    else
                    {
                        DirectoryInfo[] dirs = dir.GetDirectories();

                        string[] carp = RutaDirec.Split('\\');
                        string nuevaRuta = DestinDirec + carp[carp.Length - 1];
                        if (Directory.Exists(nuevaRuta))
                        {
                            Directory.Delete(nuevaRuta, true);
                        }
                        Directory.CreateDirectory(nuevaRuta);

                        FileInfo[] Archivos = dir.GetFiles();
                        foreach (FileInfo i in Archivos)
                        {
                            string PathTemporal = Path.Combine(nuevaRuta, i.Name);
                            i.CopyTo(PathTemporal, true);
                        }
                        if (copiarSubDirs)
                        {
                            foreach (DirectoryInfo subdir in dirs)
                            {
                                string PathTemporal = Path.Combine(nuevaRuta, subdir.Name);
                                DirectoryCopy(subdir.FullName, PathTemporal, copiarSubDirs);
                                lbCarpMovidas.Text = "Carpeta Movida Exitosamente";
                            }
                        }
                    }

                }

            }
            catch (Exception ex)
            { }
        }
    }
}
