using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using System.Web;
using System.Web.Mvc;

namespace Library
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new AuthorizeAttribute());
        }
    }
}
