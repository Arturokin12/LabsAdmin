using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.DirectoryServices;
using System.Text;
using System.Web.Security;
using LabsAdminASP.Controlador;
using LabsAdminASP.Modelo;

namespace LabsAdminASP
{
    public partial class login : System.Web.UI.Page
    {

        private string _path;
        controladorUser controlUser = new controladorUser();
        LabsAdminEntities1 ent = new LabsAdminEntities1();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
            }
        }

        protected void loginButton_Click(object sender, EventArgs e)
        {
            lbRes.Text = "Conectando...";
            lbRes.ForeColor = System.Drawing.Color.Blue;
            lbRes.Font.Size = 12;
            
            if (txtUser.Text != "" && txtPass.Text != "")
            {
                bool auth = IsAuthenticated(txtUser.Text, txtPass.Text);

                if (auth == true)
                {
                    if (controlUser.getUsuario(txtUser.Text) != null)
                    {
                        Session["nickUser"] = txtUser.Text;
                        Response.Redirect("index.aspx");
                    }else
                    {
                        lbRes.Text = "Usted no tiene acceso al sistema, contacte al administrador.";
                    }
                }else
                {
                    lbRes.Text = "Usuario o contraseña incorrectos.";
                }
            }
        }

        public bool IsAuthenticated(string user, string pwd)
        {
            config config = ent.config.ToList().ElementAt(0);
            _path = @"LDAP://"+config.dominio;
            
            string domainAndUsername = config.dominio + @"\" + user.ToLower();

            DirectoryEntry entry = new DirectoryEntry(_path, domainAndUsername, pwd, AuthenticationTypes.Secure);

            try
            {
                //Bind to the native AdsObject to force authentication.
                object obj = entry.NativeObject;

                DirectorySearcher search = new DirectorySearcher(entry);

                //search.Filter = "(SAMAccountName=" + username + ")";
                //search.PropertiesToLoad.Add("cn");
                SearchResult result = search.FindOne();

                if (null == result)
                {
                    return false;
                }

                ////Update the new path to the user in the directory.
                //_path = result.Path;
                //_filterAttribute = (string)result.Properties["cn"][0];
                return true;
            }
            catch (Exception ex)
            {
                lbRes.ForeColor = System.Drawing.Color.Red;
                lbRes.Font.Size = 15;
                lbRes.Text = "Error Autenticando usuario. " + ex.Message;
                return false;
            }
        }

        public string GetGroups(string user)
        {
            DirectorySearcher search = new DirectorySearcher(_path);
            search.Filter = "(SAMAccountName=" + user.ToLower() + ")";
            search.PropertiesToLoad.Add("memberOf");
            StringBuilder groupNames = new StringBuilder();

            try
            {
                SearchResult result = search.FindOne();
                int propertyCount = result.Properties["memberOf"].Count;
                string dn;
                int equalsIndex, commaIndex;

                for (int propertyCounter = 0; propertyCounter < propertyCount; propertyCounter++)
                {
                    dn = (string)result.Properties["memberOf"][propertyCounter];
                    equalsIndex = dn.IndexOf("=", 1);
                    commaIndex = dn.IndexOf(",", 1);
                    if (-1 == equalsIndex)
                    {
                        return null;
                    }
                    groupNames.Append(dn.Substring((equalsIndex + 1), (commaIndex - equalsIndex) - 1));
                    groupNames.Append("|");

                }
            }
            catch (Exception ex)
            {
                lbRes.Text = "Error obtaining group names. " + ex.Message;
            }
            return groupNames.ToString();
        }
    }
}