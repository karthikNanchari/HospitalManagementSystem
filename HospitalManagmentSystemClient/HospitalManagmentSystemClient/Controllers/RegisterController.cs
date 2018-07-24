using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;
using Client_Api;

namespace HospitalManagmentSystemClient.Controllers
{
    public class RegisterController : Controller
    {
       // [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        // GET: Register
        [HttpPost]
        //[HttpGet]
        public ActionResult RegisterUser(PersonModel newRegisterDetails)
        {
            RegisterNewUser_Api newUser = new RegisterNewUser_Api();

            string result = newUser.RegisterUser(newRegisterDetails);

            return View("~/Login/Register.cshtml");
        }


    }
}


