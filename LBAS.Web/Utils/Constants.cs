using System.Configuration;

namespace LBAS.Web.Utils
{
    internal class Constants
    {
        public static string ResourceUrl = ConfigurationManager.AppSettings["ida:GraphUrl"];
        public static string ClientId = ConfigurationManager.AppSettings["ida:ClientId"];
        public static string AppKey = ConfigurationManager.AppSettings["ida:AppKey"];
        public static string TenantId = ConfigurationManager.AppSettings["ida:TenantId"];
        public static string AuthString = ConfigurationManager.AppSettings["ida:Auth"] +
                                          ConfigurationManager.AppSettings["ida:Tenant"];
        public static string ClientSecret = ConfigurationManager.AppSettings["ida:ClientSecret"];

        public static string RealUser = "Member";
        public static string VirtualUser = "Guest";
        public static string Corporation = "Corporation";
        public static string Franchise = "Franchise";
        public static string Site = "Site";
        public static string Email = "@lennoxbas.onmicrosoft.com";

        public static string Collection_State = "State";
        public static string Collection_Industry = "Industry";
    }
}