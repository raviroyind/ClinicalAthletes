using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace ClinicalAthletes
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public object AuthConfig { get; private set; }

        protected void Application_Start()
        {
            HttpConfiguration config = GlobalConfiguration.Configuration;
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            config.Formatters.JsonFormatter.UseDataContractJsonSerializer = false;

            AreaRegistration.RegisterAllAreas();
  
            GlobalConfiguration.Configure(WebApiConfig.Register);

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);


            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings
    .ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            GlobalConfiguration.Configuration.Formatters
                .Remove(GlobalConfiguration.Configuration.Formatters.XmlFormatter);

        }
        private void Application_PostAuthenticateRequest(Object sender, EventArgs e)
        {
            
        }


        void Session_Start(object sender, EventArgs e)
        {
            if (Context.User.Identity.IsAuthenticated == true)
            {
                if (Context.User != null)
                {
                    this.Context.Session.Add("USR", Context.User.Identity.Name);
                }
            }

        }
        }
    } 

