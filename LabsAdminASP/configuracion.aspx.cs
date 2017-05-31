using LabsAdminASP.Controlador;
using LabsAdminASP.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LabsAdminASP
{
    public partial class configuracion : System.Web.UI.Page
    {
        LabsAdminEntities1 ent = new LabsAdminEntities1();
        controladorUser cont = new controladorUser();
        ControladorPass contPass = new ControladorPass();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                config c = ent.config.ToList().ElementAt(0);
                txtIPDominio.Text = c.ip_dominio;
                txtNombreDominio.Text = c.nombre_dominio;
                txtDominio.Text = c.dominio;
                txtUsuario.Text = c.usuario_admin;
                if (c.usar_dominio == 0)
                {
                    btNoDom.CssClass = "btn btn-danger btn-sm";
                    btYesDom.CssClass = "btn btn-default btn-sm";
                    btYesDom.Enabled = true;
                    btNoDom.Enabled = false;
                }
                else
                {
                    btYesDom.CssClass = "btn btn-info btn-sm";
                    btNoDom.CssClass = "btn btn-default btn-sm";
                    btYesDom.Enabled = false;
                    btNoDom.Enabled = true;
                }

                if (c.usar_usuario == 0)
                {
                    btNoUser.CssClass = "btn btn-danger btn-sm";
                    btSiUser.CssClass = "btn btn-default btn-sm";
                    panelUsuario.Enabled = false;
                    btSiUser.Enabled = true;
                    btNoUser.Enabled = false;
                }
                else
                {
                    btSiUser.CssClass = "btn btn-info btn-sm";
                    btNoUser.CssClass = "btn btn-default btn-sm";
                    btSiUser.Enabled = false;
                    btNoUser.Enabled = true;
                    panelUsuario.Enabled = true;
                }
            }
        }

        protected void btGuardarDominio_Click(object sender, EventArgs e)
        {
            config c = ent.config.ToList().ElementAt(0);
            string nombre_dominio = txtNombreDominio.Text;
            string ip_dominio = txtIPDominio.Text;
            string dominio = txtDominio.Text;
            IPAddress ip = new IPAddress(1);
            if (IPAddress.TryParse(ip_dominio, out ip))
            {
                c.nombre_dominio = nombre_dominio;
                c.ip_dominio = ip_dominio;
                c.dominio = dominio;
                int useDom = 0;
                if (btYesDom.CssClass == "btn btn-info btn-sm")
                {
                    useDom = 1;
                }
                else if (btYesDom.CssClass == "btn btn-danger btn-sm")
                {
                    useDom = 0;
                }
                c.usar_dominio = useDom;
                if (ent.SaveChanges() > 0)
                {
                    lbResDom.Text = "Éxito al guardar configuración";
                }
                else
                {
                    lbResDom.Text = "Error al guardar, inténtelo otra vez.";
                }
            }
            else
            {
                lbResDom.Text = "Ip ingresada no válida";
            }
        }

        protected void btNoDom_Click(object sender, EventArgs e)
        {
            btYesDom.CssClass = "btn btn-default btn-sm";
            btNoDom.CssClass = "btn btn-danger btn-sm";
            btYesDom.Enabled = true;
            btNoDom.Enabled = false;
            UpdatePanelSwitch1.Update();
        }

        protected void btYesDom_Click(object sender, EventArgs e)
        {
            btNoDom.CssClass = "btn btn-default btn-sm";
            btYesDom.CssClass = "btn btn-info btn-sm";
            btYesDom.Enabled = false;
            btNoDom.Enabled = true;
            UpdatePanelSwitch1.Update();
        }

        protected void btNoUser_Click(object sender, EventArgs e)
        {
            btSiUser.CssClass = "btn btn-default btn-sm";
            btNoUser.CssClass = "btn btn-danger btn-sm";
            try
            {
                config c = ent.config.ToList().ElementAt(0);
                c.usar_usuario = 0;
                ent.SaveChanges();
            }
            catch (Exception ex)
            {

            }
            panelUsuario.Enabled = false;
            btNoUser.Enabled = false;
            btSiUser.Enabled = true;
            UpdatePanelSwitch2.Update();
        }

        protected void btSiUser_Click(object sender, EventArgs e)
        {
            btNoUser.CssClass = "btn btn-default btn-sm";
            btSiUser.CssClass = "btn btn-info btn-sm";
            try
            {
                config c = ent.config.ToList().ElementAt(0);
                c.usar_usuario = 1;
                ent.SaveChanges();
            }
            catch (Exception ex)
            {

            }
            btSiUser.Enabled = false;
            btNoUser.Enabled = true;
            panelUsuario.Enabled = true;
            UpdatePanelSwitch2.Update();
        }

        protected void btGuardarUsuarioDom_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtPass.Text == txtPass2.Text)
                {
                    config c = ent.config.ToList().ElementAt(0);
                    c.usuario_admin = txtUsuario.Text;
                    c.pass_admin = contPass.Encrypt(txtPass2.Text);
                    if (ent.SaveChanges() > 0)
                    {
                        lbRes2.Text = "Datos guardados satisfactoriamente";
                        txtPass.Text = "";
                        txtPass2.Text = "";
                    }else
                    {
                        lbRes2.Text = "Ningún dato guardado";
                    }
                }
                else
                {
                    lbRes2.Text = "Las contraseñas no coinciden";
                }
            }
            catch (Exception ex)
            {
                lbRes2.Text = "Error al guardar datos.";
            }
        }
    }
}