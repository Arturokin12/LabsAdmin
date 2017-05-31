using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.UI;

namespace LabsAdminASP
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            ScriptManager.ScriptResourceMapping.AddDefinition("jquery",
                    new ScriptResourceDefinition
                    {
                        Path = "~/vendors/jquery/dist/jquery.min.js",
                        DebugPath = "~/vendors/jquery/dist/jquery.min.js"
                    }
            );
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            Session["nickUser"] = "";
            Session["id_laboratorio"] = "";
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}