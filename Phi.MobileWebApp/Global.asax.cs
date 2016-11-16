/*
 * COPYRIGHT 2014-2016 Anton Yarkov
 * 
 * Email: anton.yarkov@gmail.com
 * Skype: optiklab
*/

namespace Phi.MobileWebApp
{
    using System.IO;
    using System.Reflection;
    using System.Web;
    using System.Web.Http;
    using System.Web.Mvc;
    using System.Web.Optimization;
    using System.Web.Routing;
    using SimpleInjector;
    using SimpleInjector.Integration.Web.Mvc;
    using SimpleInjector.Integration.WebApi;
    using Phi.Repository;
    using SimpleInjector.Diagnostics;
    using Phi.Models.Models;
    using Phi.Repository.Stores;
    using Microsoft.AspNet.Identity;
    using Phi.Models;

    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var container = ModelContainer.Instance;
            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());
            container.RegisterMvcIntegratedFilterProvider();
            
            container.GetRegistration(typeof(IUserStore<PhiUser>)).Registration.SuppressDiagnosticWarning(DiagnosticType.DisposableTransientComponent, "Reason of suppression");
            container.GetRegistration(typeof(IUserClaimStore<PhiUser>)).Registration.SuppressDiagnosticWarning(DiagnosticType.DisposableTransientComponent, "Reason of suppression");
            container.GetRegistration(typeof(IUserLoginStore<PhiUser>)).Registration.SuppressDiagnosticWarning(DiagnosticType.DisposableTransientComponent, "Reason of suppression");
            container.GetRegistration(typeof(IUserRoleStore<PhiUser>)).Registration.SuppressDiagnosticWarning(DiagnosticType.DisposableTransientComponent, "Reason of suppression");
            container.GetRegistration(typeof(IUserPasswordStore<PhiUser>)).Registration.SuppressDiagnosticWarning(DiagnosticType.DisposableTransientComponent, "Reason of suppression");
            container.GetRegistration(typeof(IUserEmailStore<PhiUser>)).Registration.SuppressDiagnosticWarning(DiagnosticType.DisposableTransientComponent, "Reason of suppression");
            container.GetRegistration(typeof(IUserProfileStore)).Registration.SuppressDiagnosticWarning(DiagnosticType.DisposableTransientComponent, "Reason of suppression");
            container.GetRegistration(typeof(IRoleStore<Role>)).Registration.SuppressDiagnosticWarning(DiagnosticType.DisposableTransientComponent, "Reason of suppression");

            container.Verify();

            // Set resolvers.
            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));

            var webApiContainer = WebApiModelContainer.Instance;
            webApiContainer.RegisterWebApiControllers(GlobalConfiguration.Configuration);

            webApiContainer.GetRegistration(typeof(IUserStore<PhiUser>)).Registration.SuppressDiagnosticWarning(DiagnosticType.DisposableTransientComponent, "Reason of suppression");
            webApiContainer.GetRegistration(typeof(IUserClaimStore<PhiUser>)).Registration.SuppressDiagnosticWarning(DiagnosticType.DisposableTransientComponent, "Reason of suppression");
            webApiContainer.GetRegistration(typeof(IUserLoginStore<PhiUser>)).Registration.SuppressDiagnosticWarning(DiagnosticType.DisposableTransientComponent, "Reason of suppression");
            webApiContainer.GetRegistration(typeof(IUserRoleStore<PhiUser>)).Registration.SuppressDiagnosticWarning(DiagnosticType.DisposableTransientComponent, "Reason of suppression");
            webApiContainer.GetRegistration(typeof(IUserPasswordStore<PhiUser>)).Registration.SuppressDiagnosticWarning(DiagnosticType.DisposableTransientComponent, "Reason of suppression");
            webApiContainer.GetRegistration(typeof(IUserEmailStore<PhiUser>)).Registration.SuppressDiagnosticWarning(DiagnosticType.DisposableTransientComponent, "Reason of suppression");
            webApiContainer.GetRegistration(typeof(IUserProfileStore)).Registration.SuppressDiagnosticWarning(DiagnosticType.DisposableTransientComponent, "Reason of suppression");
            webApiContainer.GetRegistration(typeof(IRoleStore<Role>)).Registration.SuppressDiagnosticWarning(DiagnosticType.DisposableTransientComponent, "Reason of suppression");

            webApiContainer.Verify();

            GlobalConfiguration.Configuration.DependencyResolver =
                new SimpleInjectorWebApiDependencyResolver(webApiContainer);

            log4net.Config.XmlConfigurator.Configure(new FileInfo(Server.MapPath("~/Web.config")));
        }
    }
}
