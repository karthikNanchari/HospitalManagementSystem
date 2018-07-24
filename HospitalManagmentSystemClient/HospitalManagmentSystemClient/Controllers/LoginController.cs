using Client_Api;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HospitalManagmentSystemClient.Controllers
{
    public class LoginController : Controller
    {
        [HttpGet]
        public ViewResult Login() {

            return View();
        }
       
        //[HttpGet]
        public ActionResult Login(LoginModel loginDetails)
        {
            ViewBag.Message = "Your application description page.";

            Login_Api lapi = new Login_Api();

            // var result = Convert.ToInt32(lapi.AuthenticateUser(loginDetails));
            var result = 3;
            if (result == 0)
            {
                return View();
            }
            if (result == 1)
            {
                ViewBag.UserRole = "nurse";
                return View("~/Views/Nurse/NurseHome.cshtml");
            }
            else if(result == 2)
            {
                ViewBag.User = "labincharge";
                return View("~/Views/LabIncharge/LabInchargeHome.cshtml");
            }
            else if (result == 3)
            {
                ViewBag.UserRole = "doctor";
                return View("~/Views/Doctor/DoctorHome.cshtml");
            }
            else if (result == 4)
            {
                ViewBag.UserRole = "admin";
                return View("~/Views/Administrator/AdministratorHome.cshtml");
            }
            else if (result == 5)
            {
                ViewBag.UserRole = "patient";
                return View("~/Views/Patient/PatientHome.cshtml");
            }

            return View();
        }


        public void SetSessions(LoginModel loginDetails)
        {
            Session["key"] = loginDetails.userRole;
            Session["id"] = loginDetails.id;
            //return Json( ,JsonRequestBehavior.AllowGet);
        } 

    }
}
