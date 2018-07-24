using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Client_Api;
using Models;

namespace HospitalManagmentSystemClient.Controllers.Doctor
{
    public class DoctorController : Controller
    {

        public ActionResult Index(int? id)
        {
            if (Convert.ToString(Session["key"]) != "doctor") return RedirectToAction("Login","Home");

           // id = 5006;
            if (id != null)
            {
                System.Web.HttpContext.Current.Session.Add("key", "doctor");
                System.Web.HttpContext.Current.Session.Add("UserId", id);
                Login_Api la = new Login_Api();
                var name = la.GetUserName(id);

                System.Web.HttpContext.Current.Session.Add("UserName", name);
            }


            NotificationsModel notificationModel = new NotificationsModel();
            Admin_Api adminApi = new Admin_Api();
            notificationModel.UserRoleID = (int)Session["UserId"];//6003;
            var notifications = adminApi.GetAllNotificationsByRole(notificationModel);
            return View("~/Views/Doctor/DoctorHome.cshtml",notificationModel);
        }

        public ActionResult GetAppointments()
        {
            if (Convert.ToString(Session["key"]) != "doctor") return RedirectToAction("Login", "Home");

            int doctor_id = (int)Session["UserId"];// 5006;
            Doctor_Api doctorApi = new Doctor_Api();
            var appointments = doctorApi.GetDocApntment_Api(doctor_id);
            //var appointments = doctorApi.GetDocApntment_Api(doctor_id);

            appointments.First().filterApps.Add(new SelectListItem { Text="-Select", Value="0"});
            appointments.First().filterApps.Add(new SelectListItem { Text="Today", Value="1"});
            appointments.First().filterApps.Add(new SelectListItem { Text="Tomorrow", Value="2"});
            appointments.First().filterApps.Add(new SelectListItem { Text="All", Value="3"});
            LabIncharge_Api lApi = new LabIncharge_Api();
            var allReports = lApi.ViewPatientReports_Api(new ReportModel());

            foreach (var a in appointments)
            {
                if (allReports.Where(r => r.appointment_ID == a.appointment_ID).FirstOrDefault() != null )
                {
                    a.reportAvailable = true;
                }
                else a.reportAvailable = false;
                if (a.appointment_Date > DateTime.Now) a.canCancel = true;
            }


            if (appointments != null)
            {
                return View("~/Views/Doctor/DoctorViewAppointments.cshtml", appointments.OrderBy(a=>a.appointment_Date));
            }
            return View("~/Views/Error.cshtml");
        }

        public ActionResult GetAppointmentsByFilter(int filterID)
        {
            if (Convert.ToString(Session["key"]) != "doctor") return RedirectToAction("Login", "Home");


            int doctor_id = (int)Session["UserId"];// 5006;
            Doctor_Api doctorApi = new Doctor_Api();
            var appointments = doctorApi.GetDocApntment_Api(doctor_id);

            LabIncharge_Api lApi = new LabIncharge_Api();
            var allReports = lApi.ViewPatientReports_Api(new ReportModel());

            foreach (var a in appointments)
            {
                if (allReports.Where(r => r.appointment_ID == a.appointment_ID).FirstOrDefault() != null)
                {
                    a.reportAvailable = true;
                }
                else a.reportAvailable = false;
                int timeNow = Convert.ToInt32(DateTime.Now.Hour);
                int appTime = (Convert.ToInt32((a.timings).Substring(0, 2)));

                if (((a.appointment_Date).ToShortDateString().Equals((DateTime.Now.AddDays(1).ToShortDateString())) && (appTime > timeNow)) || (a.appointment_Date > DateTime.Now) )
                {
                    a.canCancel = true;
                }
                
            }

           

            if (filterID == 1)
            {
                appointments = appointments.Where(a => ((a.appointment_Date).ToShortDateString().Equals((DateTime.Now).ToShortDateString())));// && a.appointment_Date > DateTime.Now.AddDays(-1) && a.appointment_Date <= DateTime.Now ));

               
                appointments.First().filterApps.Add(new SelectListItem { Text = "-Select", Value = "0" });
                appointments.First().filterApps.Add(new SelectListItem { Text = "Today", Value = "1" });
                appointments.First().filterApps.Add(new SelectListItem { Text = "Tomorrow", Value = "2" });
                appointments.First().filterApps.Add(new SelectListItem { Text = "All", Value = "3" });

            }
            else if (filterID == 2)
            {


                appointments = appointments.Where(a => ((a.appointment_Date).ToShortDateString().Equals(DateTime.Now.AddDays(1).ToShortDateString())));// && a.appointment_Date > DateTime.Now));

               

                appointments.First().filterApps.Add(new SelectListItem { Text = "-Select", Value = "0" });
                appointments.First().filterApps.Add(new SelectListItem { Text = "Today", Value = "1" });
                appointments.First().filterApps.Add(new SelectListItem { Text = "Tomorrow", Value = "2" });
                appointments.First().filterApps.Add(new SelectListItem { Text = "All", Value = "3" });

            }
            //else if (id == 3)
            //{

            //}
            if (filterID == 3 || filterID ==0)
            {
                return RedirectToAction("GetAppointments");
            }

            if (appointments != null)
            {
                return View("~/Views/Doctor/DoctorViewAppointments.cshtml", appointments.OrderBy(a => a.appointment_Date));
            }
            return View("~/Views/Error.cshtml");
        }



        public ActionResult ViewAppointmentsToCancel()
        {
            if (Convert.ToString(Session["key"]) != "doctor") return RedirectToAction("Login", "Home");

            int doctor_id = (int)Session["UserId"];// 5006;
            Doctor_Api doctorApi = new Doctor_Api();
            var appointments = doctorApi.GetDocApntment_Api(doctor_id);

            if (appointments != null)
            {
                return View("~/Views/Doctor/DoctorCancelAppts.cshtml", appointments);
            }
            return View("~/Views/Error.cshtml");
        }


        public ActionResult GetAppointmentsByDate(DateTime test)
        {
            if (Convert.ToString(Session["key"]) != "doctor") return RedirectToAction("Login", "Home");

            int doctor_id = (int)Session["UserId"];// 5006;
            Doctor_Api doctorApi = new Doctor_Api();
            var appointments = doctorApi.GetDocApntment_Api(doctor_id).Where(a =>a.appointment_Date == test);

            //ViewBag.appts = appointments.Select(a => a.appointment_ID);
            TempData["Date"] = test;
            if (appointments != null)
            {
                return View("~/Views/Doctor/DoctorCancelAppts.cshtml", appointments);
            }
            return View("~/Views/Error.cshtml");
        }

        public ActionResult cancelAppointment(int id)
        {
            if (Convert.ToString(Session["key"]) != "doctor") return RedirectToAction("Login", "Home");

            Doctor_Api doctorApi = new Doctor_Api();
            AppointmentModel app = new AppointmentModel();
            app.appointment_ID = id;
            app.doctor_ID = (int)Session["UserId"];
            var appointments = doctorApi.cancelAppointment(app);
            EmailsController email = new EmailsController();
            email.EmailCancelledAppt(app);

            return RedirectToAction("GetAppointments");
          // return View("~/Views/Doctor/DoctorViewAppointments.cshtml", appointments);
        }

        public ActionResult cancelAllAppointmentByDate()
        {
            if (Convert.ToString(Session["key"]) != "doctor") return RedirectToAction("Login", "Home");

            Doctor_Api doctorApi = new Doctor_Api();
            AppointmentModel app = new AppointmentModel();
            //app.appointment_ID = id;
            app.appointment_Date = Convert.ToDateTime(TempData["Date"].ToString());
            app.doctor_ID = (int)Session["UserId"];
            var appointments = doctorApi.cancelAllAppointmentByDate(app);

            return RedirectToAction("GetAppointments");
            //return View("~/Views/Doctor/DoctorViewAppointments.cshtml", appointments);
        }

        public ActionResult GetReports()
        {
            if (Convert.ToString(Session["key"]) != "doctor") return RedirectToAction("Login", "Home");

            return View("~/Views/Doctor/DoctorManageReports.cshtml");
        }

        public ActionResult ViewAllReportsByDoctor()
        {
            if (Convert.ToString(Session["key"]) != "doctor") return RedirectToAction("Login", "Home");

            Patient_Api patientApi = new Patient_Api();
            ViewBag.GetPatientsList = new SelectList(patientApi.GetPaitentsList());

            LabIncharge_Api inchargeApi = new LabIncharge_Api();
            var reportsModel = inchargeApi.GetAllPatientReports_Api();

            return View("~/Views/Doctor/DoctorManageReports.cshtml", reportsModel);
        }

        public ActionResult ViewPatients()
        {
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
            return View("~/Views/Doctor/ViewPatientList.cshtml",docapts);
        }


        public ActionResult ViewReportsByPatient(AppointmentModel app)
        {
            if (Convert.ToString(Session["key"]) != "doctor") return RedirectToAction("Login", "Home");


            LabIncharge_Api inchargeApi = new LabIncharge_Api();
            var reportsModel = inchargeApi.GetPatientReports_Api(app.paitent_ID);

            return View("~/Views/Doctor/DoctorManageReports.cshtml", reportsModel);
        }


        public ActionResult ViewAllReportsByPatient(int patientID)
        {
            if (Convert.ToString(Session["key"]) != "doctor") return RedirectToAction("Login", "Home");

            Reports_Api reports_Api = new Reports_Api();
            PatientModel patient = new PatientModel();
            patient.pid = patientID;

            var model = reports_Api.GetReportsByPatient(patient);

            return View("~/Views/Doctor/DoctorManageReports.cshtml", model);
        }

        public ActionResult UpdateReport(ReportModel report)
        {
            if (Convert.ToString(Session["key"]) != "doctor") return RedirectToAction("Login", "Home");

            Reports_Api reports_Api = new Reports_Api();

            var model = reports_Api.UpdateReports(report);

            return View("~/Views/Doctor/DoctorManageReports.cshtml", model);
        }

        public ActionResult NewReport()
        {
            if (Convert.ToString(Session["key"]) != "doctor") return RedirectToAction("Login", "Home");

            Doctor_Api doctorApi = new Doctor_Api();
            var appointments = doctorApi.GetDocApntment_Api((int)Session["UserId"]).Where(a =>a.appointment_Date < DateTime.Now);

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

            return View("~/Views/Doctor/RequestReport.cshtml", docapts);
        }

        public ActionResult CreateReport(ReportModel report)
        {
            if (Convert.ToString(Session["key"]) != "doctor") return RedirectToAction("Login", "Home");

            LabIncharge_Api inchargeApi = new LabIncharge_Api();
            report.labIncharge_ID = (int)Session["UserId"]; //4001;

            var reportsModel = inchargeApi.CreateNewReport(report);

            return ViewAllReportsByPatient(report.patient_ID);
        }


        public ActionResult ViewAppointmentsForReport(int patientID)
        {
            int id = (int)Session["UserId"];
            Patient_Api patientApi = new Patient_Api();

            IEnumerable<AppointmentModel> patientAppointments = patientApi.GetAppointments(patientID).Where(a=>(a.appointment_Date < DateTime.Now
            && a.reqReportNotes == "N/A" && a.requestedReport == false && a.doctor_ID == id && a.cancelled == false));
            List<int> patapts = new List<int>();
            foreach(var a in patientAppointments)
            {
                patapts.Add(a.appointment_ID);
            }

            PatientModel patient = new PatientModel();
            patient.pid = patientID;

            Reports_Api reports_Api = new Reports_Api();
            var allReports = reports_Api.GetReportsByPatient(patient);
            List<int> patrptapts = new List<int>();
            foreach (var a in allReports)
            {
                patrptapts.Add(a.appointment_ID);
            }

            {
                patapts = patapts.Except(patrptapts).ToList();
            }
            List<SelectListItem> patReports = new List<SelectListItem>();
            foreach (var a in patapts)
            {
                patReports.Add(new SelectListItem { Text = a.ToString(), Value = a.ToString() });
            }
            return Json(patReports, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ReqReport(int id)
        {
            if (Convert.ToString(Session["key"]) != "doctor") return RedirectToAction("Login", "Home");

            LabIncharge_Api inchargeApi = new LabIncharge_Api();

            var reportsModel = inchargeApi.ReqReport(id);

            return RedirectToAction("GetAppointments");
        }


        public ActionResult ReqNewReport(AppointmentModel appModel)
        {
            if (Convert.ToString(Session["key"]) != "doctor") return RedirectToAction("Login", "Home");

            LabIncharge_Api inchargeApi = new LabIncharge_Api();

            var reportsModel = inchargeApi.ReqNewReport(appModel);

            return RedirectToAction("GetAppointments");
        }

        public ActionResult ViewReportsWithID(int id)
        {
            if (Convert.ToString(Session["key"]) != "doctor") return RedirectToAction("Login", "Home");

            Reports_Api rprt = new Reports_Api();
            PatientModel pat = new PatientModel();
            pat.pid = (int)Session["UserId"];
            List<ReportModel> m = new List<ReportModel>();
            var model = rprt.GetReportsByAppt(id);
            m.Add(model);
            if (model.report_ID != 0)
            {
                return View("~/Views/Doctor/DoctorManageReports.cshtml", m);
            }
            else
                return View("~/Views/Doctor/ReportAvailability.cshtml");
        }

        public ActionResult BookNewAppointment(AppointmentModel apmnt = null)
        {
            if (Convert.ToString(Session["key"]) != "doctor") return RedirectToAction("Login", "Home");

            if (apmnt.timings != null)
            {
                Doctor_Api doc_Api = new Doctor_Api();
                apmnt.doctor_ID = (int)Session["UserId"]; //5006;
                apmnt.date = DateTime.Now; //booking date
                var newBooking = doc_Api.BookDocApnmt_Api(apmnt);
                List<AppointmentModel> newAppointment = new List<AppointmentModel>();
                    newBooking.canCancel = true;
                newAppointment.Add(newBooking);
                if (newBooking != null)
                    return View("~/Views/Doctor/DoctorViewAppointments.cshtml", newAppointment);
                else
                    return View("~/Views/Error.cshtml");
            }
            else
            {
                AppointmentModel appModel = new AppointmentModel();
                Admin_Api adminApi = new Admin_Api();
                var patients = adminApi.GetAllPatients();

                foreach (var p in patients)
                {
                    appModel.dropDown.Add(new SelectListItem {
                        Text = p.firstName,
                        Value = Convert.ToString(p.pid)
                    });
                }

                return View("~/Views/Doctor/DoctorBookAppointment.cshtml", appModel);
            }
        }


        [HttpPost]
        public ActionResult GetAvailableAppointments(string text)
        {
            if (Convert.ToString(Session["key"]) != "doctor") return RedirectToAction("Login", "Home");

            if (text != null)
            {
                Doctor_Api doc_Api = new Doctor_Api();
                var newBooking = doc_Api.GetDocApntment_Api((int)Session["UserId"]);
                if (newBooking != null)
                    return View("~/Views/Doctor/DoctorBookAppointment.cshtml", newBooking);
                else
                    return View("~/Views/Error.cshtml");
            }
            else
            {
                AppointmentModel appModel = new AppointmentModel(); 
                Patient_Api patientApi = new Patient_Api();
                appModel.PatientList =  patientApi.GetPaitentsList().ToList();

                return View("~/Views/Doctor/DoctorBookAppointment.cshtml",appModel);
            }
        }

        [HttpPost]
        public JsonResult GetAvailableTimings(AppointmentModel aappModel)
        {
            Doctor_Api doctorApi = new Doctor_Api();
            aappModel.doctor_ID = (int)Session["UserId"]; 
            List<string> bookedTimings = doctorApi.GetAvailableTimingsForPatient(aappModel); //gets booked timings for patient doc and time
            var t = doctorApi.GetDocApntment_Api((int)Session["UserId"]).Where(a => a.paitent_ID == aappModel.paitent_ID && a.appointment_Date == aappModel.appointment_Date).Select( ap => ap.timings).ToList();
            AppointmentTimings appTimings = new AppointmentTimings();
            List<timingsValues> timings = appTimings.timingsDropDown;

            List<string> allBookedtimings = doctorApi.GetAvailableTimings(aappModel); //gets all booked timings for date and doctor

            List<timingsValues> displayTimings = new List<timingsValues>();

            if (bookedTimings.Count() == 0) {
                if (allBookedtimings.Count() == 0)
                    displayTimings = timings;
                else {
                    displayTimings = timings;

                    foreach (var d in allBookedtimings) {
                        displayTimings = displayTimings.Where(a => a.Text != d.ToString()).ToList();
                        //displayTimings = timings.Except(allBookedtimings);
                    }
                }
            }
            if(bookedTimings.Count !=0)
            {
                if (t.Count() > 0)
                {
                    displayTimings.Add(new timingsValues { Value = "00:00", Text = "00:00" });
                }else
                    displayTimings = timings.Where(d => d.Text != bookedTimings.First()).ToList();

            }
            return Json(displayTimings, JsonRequestBehavior.AllowGet);
        }


        public ActionResult ViewAccountDetails()
        {
            if (Convert.ToString(Session["key"]) != "doctor") return RedirectToAction("Login", "Home");

            
            DoctorModel docModel = new DoctorModel();
            docModel.pid = (int)Session["UserId"]; //5006;
            Doctor_Api docApi = new Doctor_Api();
            var doc = docApi.GetDoctorById(docModel);
            return View("~/Views/Doctor/ViewAccountDetails.cshtml", doc);
        }

        public ActionResult ManageAccount(DoctorModel doctor)
        {
            Admin_Api adminApi = new Admin_Api();
            var model = adminApi.UpdateDoctor(doctor);

            return View("~/Views/Doctor/ViewAccountDetails.cshtml", model);
        }

    }
}
