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
    public partial class Perfil : System.Web.UI.Page
    {
        controladorUser cont = new controladorUser();
        ControladorPass contPass = new ControladorPass();
        LabsAdminEntities1 ent = new LabsAdminEntities1();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                usuario u = ent.usuario.Find(3);
                lbid_usuario.Text = u.id_usuario + "";
                txtNick.Text = u.nick;
                txtNombre.Text = u.nombre;
                imgUsuario.ImageUrl = u.imagen;
            }
        }

        protected void btGuardarUser_Click(object sender, EventArgs e)
        {
            if (lbid_usuario.Text != "")
            {
                try
                {
                    int id = Convert.ToInt32(lbid_usuario.Text);
                    usuario u = ent.usuario.Find(id);
                    u.nombre = txtNombre.Text;
                    u.nick = txtNick.Text;
                    if (ent.SaveChanges() > 0)
                    {
                        lbRes.Text = "Datos guardados";
                    }else
                    {
                        lbRes.Text = "Ningún cambio realizado.";
                    }
                }
                catch (Exception ex)
                {
                    lbRes.Text = "Error al guardar datos: \n"+ex.ToString().Substring(0,200);
                }
            }
        }
        

        protected void btCambiarImagen_Click(object sender, EventArgs e)
        {
            modalPupoExtenderPopup1.Show();
            UpdatePanel1.Update();
        }

        protected void btSubirImagen_Click(object sender, EventArgs e)
        {
            try
            {
                string savePath = Server.MapPath("~/images/usuarios/");

                string fileName = FileUpload1.FileName;

                // Create the path and file name to check for duplicates.
                string pathToCheck = savePath + fileName;

                // Create a temporary file name to use for checking duplicates.
                string tempfileName = "";

                // Check to see if a file already exists with the
                // same name as the file to upload.        
                if (System.IO.File.Exists(pathToCheck))
                {
                    int counter = 2;
                    while (System.IO.File.Exists(pathToCheck))
                    {
                        // if a file with this name already exists,
                        // prefix the filename with a number.
                        tempfileName = counter.ToString() + fileName;
                        pathToCheck = savePath + tempfileName;
                        counter++;
                    }

                    fileName = tempfileName;

                    // Notify the user that the file name was changed.
                    lbRes.Text = "A file with the same name already exists." +
                        "<br />Your file was saved as " + fileName;
                }
                else
                {
                    // Notify the user that the file was saved successfully.
                    lbRes.Text = "Your file was uploaded successfully.";
                }
                FileUpload1.SaveAs(Server.MapPath("~/images/usuarios/") + fileName);
                int id = Convert.ToInt32(lbid_usuario.Text);
                usuario u = ent.usuario.Find(id);
                u.imagen = "images/usuarios/" + fileName;
                if (ent.SaveChanges() > 0)
                {
                    modalPupoExtenderPopup1.Hide();
                    lbRes.Text = "Imagen cambiada satisfactoriamente";
                    imgUsuario.ImageUrl = u.imagen;
                    UpdatePanel1.Update();
                }
                else
                {
                    modalPupoExtenderPopup1.Hide();
                    lbRes.Text = "Error guardar imagen en base de datos.";
                    UpdatePanel1.Update();
                }
            }
            catch (Exception ex)
            {
                modalPupoExtenderPopup1.Hide();
                lbRes.Text = "Error al cambiar imagen. \n" + ex.ToString().Substring(0, 200);
                UpdatePanel1.Update();
            }
        }

        protected void btPass_Click(object sender, EventArgs e)
        {
            if (txtPass.Text != "" && txtPass2.Text != "")
            {
                if (txtPass2.Text == txtPass.Text)
                {
                    if (lbid_usuario.Text != "")
                    {
                        int id = Convert.ToInt32(lbid_usuario.Text);
                        usuario u = ent.usuario.Find(id);
                        string pass = contPass.Encrypt(txtPass2.Text);
                        u.pass = pass;
                        if (ent.SaveChanges() > 0)
                        {
                            lbRes.Text = "Contraseña establecida satisfactoriamente.";
                            UpdatePanel1.Update();
                            UpdatePanel3.Update();
                        }
                        else
                        {
                            lbResPass.Text = "Contraseña no establecida.";
                            UpdatePanel2.Update();
                        }
                    }
                }else
                {
                    lbResPass.Text = "Las contraseñas no coinciden";
                    UpdatePanel2.Update();
                }
            }else
            {
                lbResPass.Text = "Ingrese la contraseña";
                UpdatePanel2.Update();
            }
        }

        protected void btModalPass_Click(object sender, EventArgs e)
        {
            ModalPopupCambiarPass.Show();
            UpdatePanel3.Update();
        }
    }
}