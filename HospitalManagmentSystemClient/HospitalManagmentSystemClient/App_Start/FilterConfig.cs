using HospitalManagmentSystemClient.Controllers;
using System.Web;
using System.Web.Mvc;

namespace HospitalManagmentSystemClient
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }

    
}
