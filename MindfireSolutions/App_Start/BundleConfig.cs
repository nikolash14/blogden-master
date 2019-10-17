using System.Web.Optimization;

namespace MindfireSolutions.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            ScriptBundle scriptBndl = new ScriptBundle("~/Bundles/JS");
            scriptBndl.Include(
                                "~/scripts/modernizr-2.6.2.js",
                                "~/scripts/Scripts.js",
                                "~/scripts/jquery-1.10.2.js",
                                "~/scripts/bootstrap.js",
                                "~/scripts/Script.js"
                              );

            bundles.Add(scriptBndl);

            StyleBundle stylebndl = new StyleBundle("~/Bundles/CSS");
            stylebndl.Include(
                                "~/Content/bootstrap.css",
                                "~/Content/Site.css",
                                "~/Content/Header.css",
                                "~/Content/DashBoard.css",
                                "~/Content/FooterStyle.css",
                                "~/Content/Profile.css",
                                "~/Content/Sidebar.css",
                                "~/Content/ModalStyle.css"
                );
            bundles.Add(stylebndl);
        }
    }
}