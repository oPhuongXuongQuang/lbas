using System.Web;
using System.Web.Optimization;

namespace LBAS.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            //bundles.Add(new ScriptBundle("~/bundles/jquery").Include("~/Scripts/jquery-{version}.js"));

            // Bootstrap
            bundles.Add(new StyleBundle("~/bundles/bootstrap/css").Include(
                      "~/Content/bootstrap.min.css"));
            bundles.Add(new ScriptBundle("~/bundles/bootstrap/js").Include(
                      "~/Content/dist/js/bootstrap.min.js"));

            // jQuery 2.1.4
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Content/plugins/jQuery/jQuery-2.1.4.min.js"));
            // jQuery Knob Chart
            bundles.Add(new ScriptBundle("~/bundles/knob").Include(
                        "~/Content/plugins/knob/jquery.knob.js"));

            // AdminLTE
            bundles.Add(new ScriptBundle("~/bundles/adminLTE").Include(
                        "~/Content/dist/js/app.js",
                        "~/Content/dist/js/pages/dashboard.js"));

            // DataTable
            bundles.Add(new StyleBundle("~/bundles/datatable/css").Include(
                        "~/Content/plugins/datatables/dataTables.bootstrap.css"));
            bundles.Add(new ScriptBundle("~/bundles/datatable/js").Include(
                        "~/Content/plugins/datatables/jquery.dataTables.min.js",
                        "~/Content/plugins/datatables/dataTables.bootstrap.min.js"));

            // wysihtml5
            bundles.Add(new StyleBundle("~/bundles/wysihtml5/css").Include(
                        "~/Content/plugins/bootstrap-wysihtml5/bootstrap3-wysihtml5.min.css"));
            bundles.Add(new ScriptBundle("~/bundles/wysihtml5/js").Include(
                        "~/Content/plugins/bootstrap-wysihtml5/bootstrap3-wysihtml5.all.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            

            
            bundles.Add(new StyleBundle("~/Content/styles/css").Include(
                      "~/Content/styles/login.css",
                      "~/Content/styles/header.css",
                      "~/Content/styles/left-menu.css",
                      "~/Content/styles/footer.css"));

            bundles.Add(new StyleBundle("~/Content/dist/css").Include(
          "~/Content/dist/css/AdminLTE.min.css"));

            bundles.Add(new StyleBundle("~/Content/dist/css/skins").Include(
          "~/Content/dist/css/skins/_all-skins.min.css"));
        }
    }
}
