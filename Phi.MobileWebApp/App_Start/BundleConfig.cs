/*
 * COPYRIGHT 2014-2016 Anton Yarkov
 * 
 * Email: anton.yarkov@gmail.com
 * Skype: optiklab
*/

namespace Phi.MobileWebApp
{
    using System.Web.Optimization;

    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/jquery-{version}.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                "~/Scripts/jquery.unobtrusive*",
                "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/knockout").Include(
                "~/Scripts/knockout-{version}.js",
                "~/Scripts/knockout.validation.js"));

            bundles.Add(new ScriptBundle("~/bundles/app").Include(
                "~/Scripts/sammy-{version}.min.js",
                "~/Scripts/app/common.js",
                "~/Scripts/app/app.datamodel.js",
                "~/Scripts/app/app.viewmodel.js",
                "~/Scripts/app/profile.viewmodel.js",
                //"~/Scripts/app/home.viewmodel.js",
                "~/Scripts/app/_run.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/Scripts/bootstrap.min.js",
                "~/Scripts/html5shiv.js",
                "~/Scripts/respond.min.js"));

#if DEBUG
            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/font-awesome.min.css",
                      "~/Content/flaticon.css",
                      "~/Content/docs.css",
                      "~/Content/site.css",
                      "~/Content/styles.css",
                      "~/Content/fontface.css"
                      ));
#else
            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/cleverwearer.css",
                      "~/Content/styles.css",
                      "~/Content/fontface.css"));
#endif

            // Set EnableOptimizations to false for debugging. For more information,
            // visit http://go.microsoft.com/fwlink/?LinkId=301862
            BundleTable.EnableOptimizations = true;
        }

        public static void RegisterMainViewBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/mainview").Include("~/Scripts/MainView.js"));
            bundles.Add(new StyleBundle("~/Content/mainview").Include("~/Content/jquery-ui.min.css"));

            BundleTable.EnableOptimizations = true;
        }
        public static void RegisterProfileBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/profile").Include("~/Content/profile.css", "~/Content/jquery-ui.min.css"));
            bundles.Add(new ScriptBundle("~/bundles/profile").Include("~/Scripts/Profile.js"));

            BundleTable.EnableOptimizations = true;
        }

        public static void RegisterToolsBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/tools").Include("~/Content/jquery-ui.min.css"));
            bundles.Add(new ScriptBundle("~/bundles/tools").Include("~/Scripts/ToolsScripts.js"));

            BundleTable.EnableOptimizations = true;
        }
    }
}
