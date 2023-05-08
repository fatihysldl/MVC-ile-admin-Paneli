using System.Web;
using System.Web.Mvc;

namespace MVC_ile_admin_Paneli
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
