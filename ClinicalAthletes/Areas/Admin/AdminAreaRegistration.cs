using System.Web.Mvc;

namespace ClinicalAthletes.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        { 
            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { controller = "Admin", action = "Dashboard", id = UrlParameter.Optional },
                new[] { "ClinicalAthletes.Areas.Admin.Controllers" }
            ); 
        }
    }
}