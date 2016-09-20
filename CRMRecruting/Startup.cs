using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using System.Configuration;
using System.Web.Configuration;

[assembly: OwinStartupAttribute(typeof(CRMRecruting.Startup))]
[assembly: log4net.Config.XmlConfigurator(ConfigFile = "Web.config", Watch = true)]
namespace CRMRecruting
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            Configuration config = WebConfigurationManager.OpenWebConfiguration("~");
            ConnectionStringsSection connSection = (ConnectionStringsSection)config.GetSection("connectionStrings");
            connSection.SectionInformation.ProtectSection("DataProtectionConfigurationProvider");
            config.Save();
            ConfigureAuth(app);
           
        }

        }
}
