using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Client_Api;
using Models;


namespace HospitalManagmentSystemClient.Controllers.Nurse
{
    public class NurseController : Controller
    {
        // GET: Nurse
        public ActionResult Index(int? id)
        {
            if (Convert.ToString(Session["key"]) != "nurse") return RedirectToAction("Login", "Home");
            if (id != null)
            {
                System.Web.HttpContext.Current.Session.Add("key", "nurse");
                System.Web.HttpContext.Current.Session.Add("UserId", id);
                Login_Api la = new Login_Api();
                var name = la.GetUserName(id);

                System.Web.HttpContext.Current.Session.Add("UserName", name);
            }
            Admin_Api adminApi = new Admin_Api();
            NotificationsModel notificationModel = new NotificationsModel();
            notificationModel.UserRoleID = (int)Session["UserId"]; //6003
            var notifications = adminApi.GetAllNotificationsByRole(notificationModel);
            return View("~/Views/Nurse/NurseHome.cshtml", notifications);
        }

        public ActionResult SelectDoctors()
        {
            if (Convert.ToString(Session["key"]) != "nurse") return RedirectToAction("Login", "Home");

           AppointmentModel doctorApts = new AppointmentModel();

            Admin_Api adminApi = new Admin_Api();
            var docs = adminApi.GetAllDoctor();

            foreach (var d in docs)
            {
                doctorApts.dropDown.Add(new SelectListItem
                    { Text = d.firstName,
                Value =Convert.ToString(d.pid)});
            }

            return View("~/Views/Nurse/ViewDoctorsList.cshtml", doctorApts);
        } 


        public ActionResult ViewDoctorAppointments(AppointmentModel person)
        {
            if (Convert.ToString(Session["key"]) != "nurse") return RedirectToAction("Login", "Home");

            Doctor_Api doctorApi = new Doctor_Api();
            if (person.doctor_ID != 0)
            {
                var doctorApmnts = doctorApi.GetDocApntment_Api(person.doctor_ID).Where(a => (a.cancelled == false && a.appointment_Date > DateTime.Now));
                return View("~/Views/Nurse/NurseViewDoctorAppointments.cshtml", doctorApmnts);
            }
            else
            {
                AppointmentModel doctorApts = new AppointmentModel();

                Admin_Api adminApi = new Admin_Api();
                var docs = adminApi.GetAllDoctor();

                foreach (var d in docs)
                {
                    doctorApts.dropDown.Add(new SelectListItem
                    {
                        Text = d.firstName,
                        Value = Convert.ToString(d.pid)
                    });
                }

                return View("~/Views/Nurse/ViewDoctorsList.cshtml", doctorApts);
            }
          }


        public ActionResult SelectPatients()
        {
            if (Convert.ToString(Session["key"]) != "nurse") return RedirectToAction("Login", "Home");

            Admin_Api adminApi = new Admin_Api();
            var pats = adminApi.GetAllPatients();
            AppointmentModel docapts = new AppointmentModel();

            foreach (var d in pats)
            {
                docapts.dropDown.Add(new SelectListItem
                {
                    Text = d.firstName,
                    Value = Convert.ToString(d.pid)
                });
            }

            return View("~/Views/Nurse/ViewPatientList.cshtml", docapts);
        }

        public ActionResult ViewPatientAppointments(AppointmentModel person)
        {
            if (Convert.ToString(Session["key"]) != "nurse") return RedirectToAction("Login", "Home");
            Patient_Api patientApi = new Patient_Api();
            var patientApmnts = patientApi.GetAppointments(person.paitent_ID).Where(a => (a.cancelled == false && a.appointment_Date > DateTime.Now));
            return View("~/Views/Nurse/NurseViewPatientAppointments.cshtml", patientApmnts);
        }


        public ActionResult ViewRooms()
        {
            if (Convert.ToString(Session["key"]) != "nurse") return RedirectToAction("Login", "Home");

            TreatmentRoom_Api roomApi = new TreatmentRoom_Api();
            var model = roomApi.GetAllTreatmentRooms();

            Patient_Api patientApi = new Patient_Api();
            var p = patientApi.GetPaitentsList();

            foreach (var m in model)
            {
                m.patientList = p;
            }

            return View("~/Views/Nurse/NurseViewRooms.cshtml", model);
        }
    }
}