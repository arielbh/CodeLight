using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using CodeValue.CodeLight.Server.Providers;
using Patients.Server.DAL;

namespace Patients.Web
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            ProviderConfiguration.Register("Patient", new PatientProvider());


        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}