using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Phi.MobileWebApp.Startup))]
[assembly: log4net.Config.XmlConfigurator(ConfigFile="web.config", Watch = true)]

namespace Phi.MobileWebApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
