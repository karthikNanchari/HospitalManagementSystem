using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace HospitalManagmentSystemClient.Controllers
{
    public class DoctorActionFiler: ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (Convert.ToString(HttpContext.Current.Session["key"]) != "doctor")
                HttpContext.Current.Session["key"] = "login";
            base.OnActionExecuting(actionContext);
        }
    }
}