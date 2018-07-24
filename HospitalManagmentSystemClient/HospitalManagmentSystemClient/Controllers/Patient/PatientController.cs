using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Client_Api;
using Models;

namespace HospitalManagmentSystemClient.Controllers.Patient
{
    [DoctorActionFiler]
    public class PatientController : Controller
    {
        // GET: Patient
        public ActionResult Index(int? id)
        {
            if (Convert.ToString(Session["key"]) != "patient") return RedirectToAction("Login", "Home");
           // id = 7001;

            if (id != null)
            {
                System.Web.HttpContext.Current.Session.Add("key", "patient");
                System.Web.HttpContext.Current.Session.Add("UserId", id);
                Login_Api la = new Login_Api();
                var name = la.GetUserName(id);

                System.Web.HttpContext.Current.Session.Add("UserName", name);
            }
            Admin_Api adminApi = new Admin_Api();
            NotificationsModel notificationModel = new NotificationsModel();
            notificationModel.UserRoleID = (int)Session["UserId"]; //6003
            var notifications =  adminApi.GetAllNotificationsByRole(notificationModel);

            return View("~/Views/Patient/PatientHome.cshtml", notifications);
        }

        //view  screen
        public ActionResult PatientAppointment(int patientId = 0)
        {
            if (Convert.ToString(Session["key"]) != "patient") return RedirectToAction("Login", "Home");

            AppointmentModel appModel = new AppointmentModel();
            Admin_Api adminApi = new Admin_Api();
            var model = adminApi.GetAllDoctor().ToArray();

            List<DoctorDropDown> items = new List<DoctorDropDown>();

            foreach (var m in model)
            {
                appModel.doctorDetails.Add(m);
                items.Add(new DoctorDropDown
                {
                    Value = (m.pid),
                    Text = m.firstName
                });
            }
            appModel.doctorDropDown = items;

            return View("~/Views/Patient/PatientBookAppointment.cshtml", appModel);
        }

        //ViewAppointments
        [HttpGet]
        public ActionResult GetAppointments(int? patientID = 0)
        {
            if (Convert.ToString(Session["key"]) != "patient") return RedirectToAction("Login", "Home");

            int id = (int)Session["UserId"];
            Patient_Api patientApi = new Patient_Api();

            IEnumerable<AppointmentModel> patientAppointments = patientApi.GetAppointments(id).Where(a => a.cancelled == false);

            foreach (var p in patientAppointments)
            {
                if (p.appointment_Date > DateTime.Now) {
                    p.canCancel = true;
                        }
            }

            if (patientAppointments != null)
            {
                return View("~/Views/Patient/PatientViewAppointments.cshtml", patientAppointments);
            }

            return View("~Views/Error.cshtml");
        }

        [HttpPost]
        public ActionResult GetAvailableTimings(AppointmentModel apmtModel)
        {
            Doctor_Api doctorApi = new Doctor_Api();
            apmtModel.appointment_Date = DateTime.Parse(apmtModel.timings);
            apmtModel.paitent_ID = (int)Session["UserId"];
            List<string> bookedTimings = doctorApi.GetAvailableTimings(apmtModel);

            AppointmentTimings appTimings = new AppointmentTimings();
            List<timingsValues> timings = appTimings.timingsDropDown;

            int id = (int)Session["UserId"];
            Patient_Api patientApi = new Patient_Api();

            IEnumerable<AppointmentModel> patientAppointments = patientApi.GetAppointments(id);

            List<timingsValues> displayTimings = new List<timingsValues>();
            if (bookedTimings.Count ==0)
            {
                var app = patientAppointments.Where(a => a.appointment_Date == apmtModel.appointment_Date).Select(t => t.timings).ToList();
                if (app.Count() > 0)
                {
                    List<timingsValues> t = new List<timingsValues>();
                    foreach (var a in app)
                    {
                        var aa = a.Trim('0');
                        t.Add(new timingsValues { Text= aa.Trim(':'), Value= aa.Trim(':')});
                    }
                    var i = 0;
                    foreach (var val in t)
                    {
                        displayTimings = timings.Where(d => d.Text != t.ElementAt(i).Text).ToList();
                        i++;
                    }
                }
                else
                displayTimings = timings;//.Where(t => !bookedTimings.Contains(t.Value)).ToList();

            }
            if (bookedTimings.Count != 0)
            {
                displayTimings.Add(new timingsValues { Value = "00:00", Text = "00:00" });
            }
            return Json(displayTimings, JsonRequestBehavior.AllowGet);
        }

       public ActionResult ViewEditAppointment(int id)
        {
            if (Convert.ToString(Session["key"]) != "patient") return RedirectToAction("Login", "Home");

            AppointmentModel appModel = new AppointmentModel();
            appModel.appointment_ID = id;
            Admin_Api adminApi = new Admin_Api();
            var model =  adminApi.GetAllDoctor().ToArray();

            List<DoctorDropDown> items = new List<DoctorDropDown>();

            foreach (var m in model) {
                appModel.doctorDetails.Add(m);
                items.Add(new DoctorDropDown
                {
                    Value = (m.pid),
                    Text = m.firstName
                });
            }
            appModel.doctorDropDown = items;

            return View("~/Views/Patient/PatientEditAppointment.cshtml", appModel);
        }


        public ActionResult EditAppointment(AppointmentModel appModel)
        {
            if (Convert.ToString(Session["key"]) != "patient") return RedirectToAction("Login", "Home");
            Appointment_Api apmtApi = new Appointment_Api();

           apmtApi.UpdateApnmt_Api(appModel);

            return Json("success", JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteAppointment(AppointmentModel appModel)
        {
            if (Convert.ToString(Session["key"]) != "patient") return RedirectToAction("Login", "Home");
            Appointment_Api apmtApi = new Appointment_Api();

            appModel.paitent_ID = (int)Session["UserId"];
            apmtApi.DeleteApnmt_Api(appModel);

            return Json("success", JsonRequestBehavior.AllowGet);
        }

        //post data for booking
        public ActionResult PatientBookAppointment(AppointmentModel patientAppointment)
        {
            if (Convert.ToString(Session["key"]) != "patient") return RedirectToAction("Login", "Home");

            Patient_Api patientApi = new Patient_Api();

            patientAppointment.paitent_ID = (int)Session["UserId"];
            patientAppointment.date = DateTime.Now;
           AppointmentModel model = patientApi.PatientBookAppointment(patientAppointment);


            if (model.appointment_ID != 0)
            {
               return View("~/Views/Patient/SuccessBooking.cshtml", model);
            }

            return View("~/Views/Error.cshtml");
        }

        public ActionResult GetPayments()
        {
            if (Convert.ToString(Session["key"]) != "patient") return RedirectToAction("Login", "Home");

            Patient_Api patientApi = new Patient_Api();
            var paymentsModel =  patientApi.GetPatientPayments((int)Session["UserId"]).GroupBy(g => g.bill_ID).Select(pa => pa.First()).ToList();
            PaymentModel p = new PaymentModel();
            foreach (var pay in paymentsModel)
            {
                p.billList.Add(new SelectListItem { Text=pay.bill_ID.ToString(), Value=pay.bill_ID.ToString()});
            }
            return View("~/Views/Patient/SelectBillID.cshtml", p);
        }

        public ActionResult GetPaymentsWithBillID(PaymentModel p)
        {
            if (Convert.ToString(Session["key"]) != "patient") return RedirectToAction("Login", "Home");

            Patient_Api patientApi = new Patient_Api();
            var paymentsModel = patientApi.GetPatientPayments((int)Session["UserId"]).Where(b => b.bill_ID == p.bill_ID);
            if (paymentsModel.Count() != 0) {
                ViewBag.totalAmount = paymentsModel.Last().total_Amount;
                ViewBag.paidAmount = paymentsModel.Last().paid_Amount ;
                double totalPaidAmount = 0;
                foreach (var pp in paymentsModel)
                {
                    totalPaidAmount = totalPaidAmount + pp.paid_Amount;
                }
                ViewBag.TotalPaid = totalPaidAmount;
                ViewBag.PendingAmount = ViewBag.totalAmount - totalPaidAmount;
            }
            else
            {
                return Redirect("PayPayments");
            }
            return View("~/Views/Patient/PayPayments.cshtml", paymentsModel);
        }

        public ActionResult GetPaymentsWithID(int id)
        {
            if (Convert.ToString(Session["key"]) != "patient") return RedirectToAction("Login", "Home");

            Patient_Api patientApi = new Patient_Api();

            Account_Api accApi = new Account_Api();
            AccountModel accModel = new AccountModel();
            accModel.patient_ID = (int)Session["UserId"];
            var model = accApi.GetAccountsByBillID(accModel);
            var billingID = model.Where(a =>a.appointment_ID == id).FirstOrDefault();
            if (billingID != null) {
                var paymentsModel = patientApi.GetPatientPayments((int)Session["UserId"]);
                List<PaymentModel> pay = null;
                if (paymentsModel.Count() != 0)
                {
                    var pp = paymentsModel.Where(p => p.bill_ID == billingID.bill_ID).FirstOrDefault();
                    if (pp != null)
                    {
                        pay.Add(pp);
                        return View("~/Views/Patient/PatientViewPayments.cshtml", pay);
                    }
                    ViewBag.info = "Billing is not yet generated";
                    return View("~/Views/Patient/NoReports.cshtml");
                }
                ViewBag.info = "Billing is not yet generated";
                return View("~/Views/Patient/NoReports.cshtml");
            }
            ViewBag.info = "Billing is not yet generated";
            return View("~/Views/Patient/NoReports.cshtml");
        }

        public ActionResult PayPayments()
        {
            if (Convert.ToString(Session["key"]) != "patient") return RedirectToAction("Login", "Home");

            Patient_Api patientApi = new Patient_Api();
            var paymentsModel = patientApi.GetPatientPayments((int)Session["UserId"]);
            return View("~/Views/Patient/PatientPayments.cshtml", paymentsModel);
        }

        

       // public ActionResult PatientPayPayments(PaymentModel payment)
        public ActionResult PatientPayPayments(PaymentModel payment)
        {
            if (Convert.ToString(Session["key"]) != "patient") return RedirectToAction("Login", "Home");

            Patient_Api patientApi = new Patient_Api();

            var paymentsModel = patientApi.PayPayments(payment);

            return RedirectToAction("PayPayments");
            //return View("~/Views/Patient/PatientPayments.cshtml", paymentsModel);
        }

        public ActionResult ViewManageAccount()
        {
            if (Convert.ToString(Session["key"]) != "patient") return RedirectToAction("Login", "Home");

            Patient_Api patientApi = new Patient_Api();
            PatientModel patientModel = new PatientModel();
            patientModel.pid = (int)Session["UserId"]; 
            var model = patientApi.GetPatientById(patientModel);

            return View("~/Views/Patient/ManageAccount.cshtml", model);
        }

        [HttpPost]
        public ActionResult ManageAccount(PatientModel patientModel)
        {
            if (Convert.ToString(Session["key"]) != "patient") return RedirectToAction("Login", "Home");
            Admin_Api adminApi = new Admin_Api();
            var model = adminApi.UpdatePatient(patientModel);

            var m =model.Where(p => p.pid == patientModel.pid);

            return View("~/Views/Patient/ManageAccount.cshtml", m);
        }


        [HttpPost]
        public ActionResult ValidateEmail(string text)
        {
            if (Convert.ToString(Session["key"]) != "patient") return RedirectToAction("Login", "Home");
            Patient_Api validateEmail = new Patient_Api();

            string result = validateEmail.PatientValidateEmail(text);
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        public ActionResult ViewBills()
        {
            if (Convert.ToString(Session["key"]) != "patient") return RedirectToAction("Login", "Home");
            Account_Api accApi = new Account_Api();
            AccountModel accModel = new AccountModel();
            accModel.patient_ID = (int)Session["UserId"];
            var model = accApi.GetAccountsByBillID(accModel);
            return View("~/Views/Patient/PatientViewAccount.cshtml",model);
        }

        public ActionResult ViewBillsWithID(int id)
        {
            if (Convert.ToString(Session["key"]) != "patient") return RedirectToAction("Login", "Home");
            Account_Api accApi = new Account_Api();
            AccountModel accModel = new AccountModel();
            accModel.patient_ID = (int)Session["UserId"];
            var model = accApi.GetAccountsByBillID(accModel).Where(a => a.appointment_ID == id);
            if (model.Count() != 0)
            {
                return View("~/Views/Patient/PatientViewAccount.cshtml", model);

            }
            else
            {
                ViewBag.info = "Billing is not yet generated";
                return View("~/Views/Patient/NoReports.cshtml");

            }
        }


        public ActionResult ViewBookedRoom()
        {
            if (Convert.ToString(Session["key"]) != "patient") return RedirectToAction("Login", "Home");
            Patient_Api patientApi = new Patient_Api();
            PatientModel pat = new PatientModel();
            pat.pid = (int)Session["UserId"];
            var model = patientApi.ViewBookedRoom(pat);

            return View("~/Views/Patient/ViewRoomAlloted.cshtml", model);
        }

        public ActionResult ViewReports()
        {
            if (Convert.ToString(Session["key"]) != "patient") return RedirectToAction("Login", "Home");
            Reports_Api rprt = new Reports_Api();
            PatientModel pat = new PatientModel();
            pat.pid = (int)Session["UserId"];
            var model = rprt.GetReportsByPatient(pat);
            return View("~/Views/Patient/PatientViewReports.cshtml", model);
        }

        public ActionResult ViewReportsWithID(int id)
        {
            if (Convert.ToString(Session["key"]) != "patient") return RedirectToAction("Login", "Home");
            
            Reports_Api rprt = new Reports_Api();
            PatientModel pat = new PatientModel();
            pat.pid = (int)Session["UserId"];
            List<ReportModel> m = new List<ReportModel>();
            var model = rprt.GetReportsByAppt(id);
            m.Add(model);
            if (model.appointment_ID != 0)
            {
                return View("~/Views/Patient/PatientViewReports.cshtml", m);
            }
            else
            {
                ViewBag.info = "Reports are not yet generated";
                return View("~/Views/Patient/NoReports.cshtml", m);
            }
        }

        public ActionResult SendAppointmentEmail(int appID)
        {
            PatientModel pat = new PatientModel();
            pat.pid = (int)Session["UserID"];
            Patient_Api patApi = new Patient_Api();
            var appDetails = patApi.GetAppointments((int)Session["UserId"]).Where(a => a.appointment_ID == appID).First();
            EmailsController emails = new EmailsController();
            emails.EmailBooking(appDetails);

            return RedirectToAction("GetAppointments");
        }


    }
}
