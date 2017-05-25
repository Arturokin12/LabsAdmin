using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LabsAdminASP.Modelo;

namespace LabsAdminASP
{
    public partial class Users : System.Web.UI.Page
    {
        LabsAdminEntities1 ent = new LabsAdminEntities1();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                List<usuario> usuarios = ent.usuario.ToList();
                tablaUsuarios.DataSource = usuarios;
                tablaUsuarios.DataBind();
            }
        }



        protected void tablaUsuarios_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            Label id_usuario = (Label)tablaUsuarios.Rows[e.RowIndex].FindControl("Label1");
            lbidUsuarioEliminar.Text = id_usuario.Text;
            modalPopupExtenderEliminar.Show();
        }

        protected void tablaUsuarios_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            Label id_usuario = (Label)tablaUsuarios.Rows[e.NewSelectedIndex].FindControl("Label1");
            lbidUsuarioEditar.Text = id_usuario.Text;
            usuario u = ent.usuario.Find(Convert.ToInt32(id_usuario.Text));
            txtNombreEdit.Text = u.nombre;
            txtNickEdit.Text = u.nick;
            modalPopupExtenderEditar.Show();
        }

        protected void AgregarUsuario(object sender, EventArgs e)
        {
            if (txtNombre.Text != "" && txtNick.Text != "")
            {
                usuario u = new usuario
                {
                    nombre = txtNombre.Text,
                    nick = txtNick.Text
                };
                try
                {
                    ent.usuario.Add(u);
                    ent.SaveChanges();
                    tablaUsuarios.DataSource = ent.usuario.ToList();
                    tablaUsuarios.DataBind();
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalAddUsuario", "$('#modalAddUsuario').modal('hide');", true);
                    UpdatePanel1.Update();
                    lbTituloMensaje.Text = "Éxito al agregar Usuario";
                    lbMensaje.Text = "Usuario Agregado satisfactoriamente.";
                    modalPupoExtenderMensaje.Show();
                }
                catch (Exception ex)
                {
                    lbMensajeAddUsuario.Text = "Error al agregar usuario: " + ex.ToString().Substring(0, 300);
                    UpdatePanel2.Update();
                }
            }
            else
            {
                lbMensajeAddUsuario.Text = "Ingrese todos los valores.";
                UpdatePanel2.Update();
            }
        }
        protected void editarUsuario(object sender, EventArgs e)
        {
            if (txtNombreEdit.Text != "" && txtNickEdit.Text != "" && lbidUsuarioEditar.Text != "")
            {
                try
                {
                    usuario u = ent.usuario.Find(Convert.ToInt32(lbidUsuarioEditar.Text));
                    u.nombre = txtNombreEdit.Text;
                    u.nick = txtNickEdit.Text;
                    ent.SaveChanges();
                    tablaUsuarios.DataSource = ent.usuario.ToList();
                    tablaUsuarios.DataBind();
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalEditUsuario", "$('#modalEditUsuario').modal('hide');", true);
                    UpdatePanel1.Update();
                    modalPopupExtenderEditar.Hide();
                    lbTituloMensaje.Text = "Éxito al editar Usuario";
                    lbMensaje.Text = "Usuario editado satisfactoriamente.";
                    modalPupoExtenderMensaje.Show();
                }
                catch (Exception ex)
                {
                    lbTituloMensaje.Text = "Error En editar Usuario";
                    lbMensaje.Text = "Error al editar usuario: " + ex.ToString().Substring(0, 300);
                    modalPupoExtenderMensaje.Show();
                }
            }
            else
            {
                lbTituloMensaje.Text = "Error En editar Usuario";
                lbMensaje.Text = "Ingrese todos los valores";
                modalPupoExtenderMensaje.Show();
            }
        }

        protected void eliminarUsuario(object sender, EventArgs e)
        {
            if (lbidUsuarioEliminar.Text != "")
            {
                try
                {
                    usuario u = ent.usuario.Find(Convert.ToInt32(lbidUsuarioEliminar.Text));
                    ent.usuario.Remove(u);
                    ent.SaveChanges();
                    lbTituloMensaje.Text = "Usuario Eliminado";
                    lbMensaje.Text = "Usuario Eliminado satisfactoriamente";
                    modalPupoExtenderMensaje.Show();
                    tablaUsuarios.DataSource = ent.usuario.ToList();
                    tablaUsuarios.DataBind();
                }
                catch (Exception ex)
                {
                    lbTituloMensaje.Text = "Error al eliminar usuario";
                    lbMensaje.Text = "Error: " + ex.ToString().Substring(0,300);
                    modalPupoExtenderMensaje.Show();
                }
            }
        }

    }
}