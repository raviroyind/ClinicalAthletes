using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ClinicalAthletes.Startup))]
namespace ClinicalAthletes
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
