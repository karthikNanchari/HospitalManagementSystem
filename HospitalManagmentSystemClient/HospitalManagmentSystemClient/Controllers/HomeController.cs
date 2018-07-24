using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;

namespace HospitalManagmentSystemClient.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Home Page";
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Login() {

            Session["key"] = "login";
            Session["id"] = 0;
            return View("~/Views/Login/Login.cshtml");
        }

        public ActionResult Register() {

            return View();
        }

        public ActionResult SignOutUser()
        {
            Session["key"] = "login";
            Session["id"] = 0;
            return View("~/Views/Login/Login.cshtml");
        }

        public void SetViewBag()
        {

        }

        public void UnSetViewBag()
        {

        }
    }
}