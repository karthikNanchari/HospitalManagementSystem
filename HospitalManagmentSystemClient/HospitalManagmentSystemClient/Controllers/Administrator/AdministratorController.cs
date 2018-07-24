using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Models;
using Client_Api;

namespace HospitalManagmentSystemClient.Controllers.Administrator
{
    public class AdministratorController : Controller
    {
        ErrorModel error = new ErrorModel();
        public ActionResult Index(int? id)
        {
            if (Convert.ToString(Session["key"]) != "admin") return RedirectToAction("Login", "Home");

            //id = 6003;
            if (id != null)
            {
                System.Web.HttpContext.Current.Session.Add("key", "admin");
                System.Web.HttpContext.Current.Session.Add("UserId", id);
                Login_Api la = new Login_Api();
                var name = la.GetUserName(id);

                System.Web.HttpContext.Current.Session.Add("UserName", name);

            }

            return View("~/Views/Administrator/AdministratorHome.cshtml");
        }

        public ActionResult ViewPatients()
        {
            if (Convert.ToString(Session["key"]) != "admin") return RedirectToAction("Login", "Home");

            Admin_Api adminGetAllPatientsApi = new Admin_Api();
            var model = adminGetAllPatientsApi.GetAllPatients();
            if (model != null) {
                return View("~/Views/Administrator/AdminViewPatients.cshtml", model);
            }
            error.ErrorMessage = "Unable to Get Patient Details";
            return View("~/Views/Error.cshtml", error);
        }

        [HttpPost]
        public ActionResult ManagePatients(PatientModel patientUpdates)
        {
           if (Convert.ToString(Session["key"]) != "admin") return RedirectToAction("Login", "Home");

            Admin_Api adminGetAllPatients = new Admin_Api();
            var model = adminGetAllPatients.UpdatePatient(patientUpdates);

            if (model != null)
            {
                return View("~/Views/Administrator/AdminViewPatients.cshtml", model);
            }
            return View("~/Views/Error.cshtml");
        }

        public ActionResult ViewDoctors()
        {
            if (Convert.ToString(Session["key"]) != "admin") return RedirectToAction("Login", "Home");

            Admin_Api adminGetAllDoctorsApi = new Admin_Api();

            var model = adminGetAllDoctorsApi.GetAllDoctor();

            if (model != null) {

                return View("~/Views/Administrator/AdminViewDoctors.cshtml", model);
            }

            return View("~/Views/Error.cshtml");
        }

        [HttpPost]
        public ActionResult ManageDoctors(DoctorModel doctorUpdates)
        {
            if (Convert.ToString(Session["key"]) != "admin") return RedirectToAction("Login", "Home");

            Admin_Api adminApi = new Admin_Api();

            var model = adminApi.UpdateDoctor(doctorUpdates);
            if (model != null)
            {
                return View("~/Views/Administrator/AdminViewDoctors.cshtml", model);
            }
            //ErrorModel.ErrorMessage = "Unable to fetch patient Details Please Again later";
            return View("~/Views/Error.cshtml");

        }


        public ActionResult ViewNurses()
        {
            if (Convert.ToString(Session["key"]) != "admin") return RedirectToAction("Login", "Home");

            Admin_Api adminGetAllDoctorsApi = new Admin_Api();

            var model = adminGetAllDoctorsApi.GetAllNurses();

            if (model != null)
            {

                return View("~/Views/Administrator/AdminViewNurses.cshtml", model);
            }

            return View("~/Views/Error.cshtml");
        }

        public ActionResult ManageNurses(NurseModel nurseUpdates)
        {
            if (Convert.ToString(Session["key"]) != "admin") return RedirectToAction("Login", "Home");

            Admin_Api adminApi = new Admin_Api();

            var model = adminApi.UpdateNurse(nurseUpdates);
            if (model != null)
            {
                return View("~/Views/Administrator/AdminViewNurses.cshtml", model);
            }
            return View("~/Views/Error.cshtml");
        }

        public ActionResult ViewLabIncharges()
        {
            if (Convert.ToString(Session["key"]) != "admin") return RedirectToAction("Login", "Home");

            Admin_Api adminGetAllDoctorsApi = new Admin_Api();

            var model = adminGetAllDoctorsApi.GetAllLabIncharges();

            if (model != null)
            {

                return View("~/Views/Administrator/AdminViewLabIncharges.cshtml", model);
            }

            return View("~/Views/Error.cshtml");
        }

        public ActionResult ManageLabIncharges(LabInchargeModel inchargeUpdates)
        {
            if (Convert.ToString(Session["key"]) != "admin") return RedirectToAction("Login", "Home");

            Admin_Api adminApi = new Admin_Api();

            var model = adminApi.UpdateIncharges(inchargeUpdates);
            if (model != null)
            {
                return View("~/Views/Administrator/AdminViewLabIncharges.cshtml", model);
            }
            //ErrorModel.ErrorMessage = "Unable to fetch patient Details Please Again later";
            return View("~/Views/Error.cshtml");

        }


        #region Notifications

        public ActionResult ManageNotification(NotificationsModel mangeNotifications)
        {
            if (Convert.ToString(Session["key"]) != "admin") return RedirectToAction("Login", "Home");

            Admin_Api adminApi = new Admin_Api();

            mangeNotifications.AdminID = (int)Session["UserId"]; //6003;
            var notificationsModels = adminApi.ManageNotifications(mangeNotifications);
            return View("~/Views/Administrator/AdminManageNotifications.cshtml", notificationsModels);
        }

        public ActionResult SaveNotification(NotificationsModel mangeNotifications)
        {
            if (Convert.ToString(Session["key"]) != "admin") return RedirectToAction("Login", "Home");

            Admin_Api adminApi = new Admin_Api();
            IEnumerable<NotificationsModel> notificationsModels = null;
            if (!mangeNotifications.Expiry.Equals("1/1/0001 12:00:00 AM"))
            {
                mangeNotifications.AdminID = (int)Session["UserId"];
                mangeNotifications.Created_DateTime = DateTime.Now;
                notificationsModels = adminApi.InsertNotifications(mangeNotifications);
            }
            return View("~/Views/Administrator/AdminManageNotifications.cshtml", notificationsModels);
        }

        public ActionResult GetAllNotifications(NotificationsModel getNotifications)
        {
            if (Convert.ToString(Session["key"]) != "admin") return RedirectToAction("Login", "Home");

            Admin_Api adminApi = new Admin_Api();
            getNotifications.AdminID = (int)System.Web.HttpContext.Current.Session["UserId"]; //6003;
            var notificationsModels = adminApi.GetAllNotifications(getNotifications);
            return View("~/Views/Administrator/AdminManageNotifications.cshtml", notificationsModels);
        }

        public ActionResult NewNotification()
        {
            if (Convert.ToString(Session["key"]) != "admin") return RedirectToAction("Login", "Home");

            return View("~/Views/Administrator/AdminAddNotifications.cshtml");
        }

        [HttpPost]
        public ActionResult GetRoleDetails(NotificationsModel notification)
        {
            if (Convert.ToString(Session["key"]) != "admin") return RedirectToAction("Login", "Home");

            Admin_Api adminApi = new Admin_Api();
            NotificationsModel model = new NotificationsModel();
            ViewBag.dropdownModel = null;

            if (notification.Notifications.Equals("Doctors"))
            {

                var persons = adminApi.GetAllDoctor();
                foreach (var p in persons)
                {
                    model.personDetails.Add(new SelectListItem {
                        Text = p.firstName,
                        Value = Convert.ToString(p.pid)
                    });
                }
                ViewBag.dropdownModel = "Doctors";
            }
            else if (notification.Notifications.Equals("Patients"))
            {
                var persons = adminApi.GetAllPatients();
                foreach (var p in persons)
                {
                    model.personDetails.Add(new SelectListItem
                    {
                        Text = p.firstName,
                        Value = Convert.ToString(p.pid)
                    });
                }
                ViewBag.dropdownModel = "Patients";

            }
            else if (notification.Notifications.Equals("Nurses"))
            {
                var persons = adminApi.GetAllNurses();
                foreach (var p in persons)
                {
                    model.personDetails.Add(new SelectListItem
                    {
                        Text = p.firstName,
                        Value = Convert.ToString(p.pid)
                    });
                }
                ViewBag.dropdownModel = "Nurses";
            }
            else if (notification.Notifications.Equals("LabIncharges"))
            {
                var persons = adminApi.GetAllLabIncharges();
                foreach (var p in persons)
                {
                    model.personDetails.Add(new SelectListItem
                    {
                        Text = p.firstName,
                        Value = Convert.ToString(p.pid)
                    });
                }
                ViewBag.dropdownModel = "LabIncharges";
            }

            return View("~/Views/Administrator/AdminAddNotifications.cshtml", model);
        }

        public ActionResult ViewRoleNoti(string role)
        {
            if (role != null && role != "-Select")
            {
                if (Convert.ToString(Session["key"]) != "admin") return RedirectToAction("Login", "Home");
                Admin_Api adminApi = new Admin_Api();
                NotificationsModel model = new NotificationsModel();
                ViewBag.dropdownModel = null;
                model.UserRoleID = Convert.ToInt32(role);
                var notis = adminApi.GetAllNotificationsByRole(model);
                ViewBag.dropdownModel = "true";
                return View("~/Views/Administrator/AdminManageNotifications.cshtml", notis);
            }
            return View("~/Views/Administrator/AdminManageNotifications.cshtml");
        }


        public ActionResult DeleteNotifications(NotificationsModel notification)
        {
            if (Convert.ToString(Session["key"]) != "admin") return RedirectToAction("Login", "Home");

            Admin_Api adminApi = new Admin_Api();
            var model = adminApi.DeleteNotifications(notification);
            return View("~/Views/Administrator/AdminManageNotifications.cshtml", model);
        }
        #endregion


        #region Accounts
        public ActionResult ViewAccounts()
        {
            if (Convert.ToString(Session["key"]) != "admin") return RedirectToAction("Login", "Home");

            Admin_Api adminApi = new Admin_Api();
            var model = adminApi.GetAllAccounts();
            return View("~/Views/Administrator/AdminViewAccounts.cshtml", model);
        }

        [HttpPost]
        public ActionResult GetAccountsByDropDown(AccountModel accountModel)
        {
           if (Convert.ToString(Session["key"]) != "admin") return RedirectToAction("Login", "Home");

            Admin_Api adminApi = new Admin_Api();
            var model = adminApi.GetAllAccounts();
            if (accountModel.account_ID == 1)
            {
                ViewBag.AccountDropDrown = new SelectList(model.Select(a => a.account_ID));
            }
            else if (accountModel.account_ID == 2)
            {
                ViewBag.AccountDropDrown = new SelectList(model.Select(a => a.patient_ID));
            }
            else if (accountModel.account_ID == 3)
            {
                ViewBag.AccountDropDrown = new SelectList(model.Select(a => a.payment_ID));
            }
            return View("~/Views/Administrator/AdminViewAccounts.cshtml");
        }

        [HttpPost]
        public ActionResult ViewAccountsByID(AccountModel accountModel)
        {
            if (Convert.ToString(Session["key"]) != "admin") return RedirectToAction("Login", "Home");

            Admin_Api adminApi = new Admin_Api();
            IEnumerable<AccountModel> model = null;
            if (accountModel.account_ID != 0)
                model = adminApi.GetAccountsByID(accountModel);
            return View("~/Views/Administrator/AdminViewAccounts.cshtml", model);
        }

        [HttpPost]
        public ActionResult SaveAccountDetail(AccountModel accountModel)
        {
            if (Convert.ToString(Session["key"]) != "admin") return RedirectToAction("Login", "Home");

            Admin_Api adminApi = new Admin_Api();
            var model = adminApi.SaveAccountDetail(accountModel);
            return View("~/Views/Administrator/AdminViewAccounts.cshtml", model);
        }
        #endregion



        #region Billing
        public ActionResult ViewAddAccountDetail()
        {
            if (Convert.ToString(Session["key"]) != "admin") return RedirectToAction("Login", "Home");

            Admin_Api adminApi = new Admin_Api();
            var patients = adminApi.GetAllPatients();

            List<SelectListItem> dropDownpatients = new List<SelectListItem>();

            foreach (var p in patients)
            {
                dropDownpatients.Add(new SelectListItem
                {
                    Text = p.firstName,
                    Value = Convert.ToString(p.pid)
                });
            }

            AccountModel model = new AccountModel();
            model.patientdropDown = dropDownpatients;
            model.AppointmentIDDropDown.Add(new SelectListItem { Text="-Select-", Value="-Select-"});
            return View("~/Views/Administrator/AdminAddViewBilling.cshtml", model);
        }

        public ActionResult ViewAppointments(int patientID)
        {
            int id = (int)Session["UserId"];
            Patient_Api patientApi = new Patient_Api();

            IEnumerable<AppointmentModel> patientAppointments = patientApi.GetAppointmentsForBookedRooms(patientID).Where(a =>a.appointment_Date < DateTime.Now);
            //IEnumerable<AppointmentModel> patientAppointments = patientApi.GetAppointmentsForBookedRooms(patientID);

            List<SelectListItem> appts = new List<SelectListItem>();
            foreach (var a in patientAppointments)
            {
                appts.Add(new SelectListItem { Text =a.appointment_ID.ToString(), Value= a.appointment_ID.ToString() });
            }
            return Json(appts, JsonRequestBehavior.AllowGet);
        }

     
        


        public ActionResult GetAppointmentsWithNoBilling(int patientID)
        {
            //int id = (int)Session["UserId"];
            Appointment_Api app = new Appointment_Api();
            IEnumerable<int> patientAppointments = app.GetAppointmentsWithNoBilling().Where(a => (a.paitent_ID == patientID && a.appointment_Date < DateTime.Now)).Select(a =>a.appointment_ID); //apts with no billing
            TreatmentRoom_Api trApi = new TreatmentRoom_Api();
            PatientModel patModel = new PatientModel();
            patModel.pid = patientID;
            var AptWithRoomInFuture = trApi.GetBookedRooms(patModel).Where(t => t.date > DateTime.Now).Select(t =>t.appointment_ID); //apts with rooms in future
            patientAppointments = patientAppointments.Except(AptWithRoomInFuture);
          

            List<SelectListItem> appts = new List<SelectListItem>();
            foreach (var a in patientAppointments)
            {
                appts.Add(new SelectListItem { Text = a.ToString(), Value = a.ToString() });
            }
            return Json(appts, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult AddAccountDetail(AccountModel accModel)
        {
            if (Convert.ToString(Session["key"]) != "admin") return RedirectToAction("Login", "Home");

            accModel.generatedDate_Time = DateTime.Now;
            accModel.paid_Amount = 0;
            Admin_Api adminApi = new Admin_Api();
            adminApi.AddAccountDetail(accModel);

            var model = adminApi.GetAllAccounts();

            return View("~/Views/Administrator/AdminViewBilling.cshtml", model.OrderByDescending(m => m.bill_ID));
        }

        public ActionResult ViewBilling()
        {
            if (Convert.ToString(Session["key"]) != "admin") return RedirectToAction("Login", "Home");
            Admin_Api adminApi = new Admin_Api();
            var model = adminApi.GetAllAccounts();

            return View("~/Views/Administrator/AdminViewBilling.cshtml", model.OrderByDescending(m =>m.bill_ID));
        }


        [HttpPost]
        public ActionResult DeleteAccountDetail(AccountModel accountModel)
        {
            if (Convert.ToString(Session["key"]) != "admin") return RedirectToAction("Login", "Home");

            Admin_Api adminApi = new Admin_Api();
            var model = adminApi.DeleteAccountDetail(accountModel);
            return View("~/Views/Administrator/AdminViewBilling.cshtml", model);
        }


        #endregion


        #region Payments

        public ActionResult ViewPatientsForPayments()
        {
            if (Convert.ToString(Session["key"]) != "admin") return RedirectToAction("Login", "Home");


            Admin_Api adminApi = new Admin_Api();
            var patients = adminApi.GetAllPatients();

            List<SelectListItem> dropDownpatients = new List<SelectListItem>();

            foreach (var p in patients)
            {
                dropDownpatients.Add(new SelectListItem
                {
                    Text = p.firstName,
                    Value = Convert.ToString(p.pid)
                });
            }
            AccountModel model = new AccountModel();
            model.patientdropDown = dropDownpatients;

            if (model != null)
            {
                return View("~/Views/Administrator/AdminViewPatientsForPayments.cshtml", model);
            }
            error.ErrorMessage = "Unable to Get Patient Details";
            return View("~/Views/Error.cshtml", error);
        }



        public ActionResult GetPayments(AccountModel account)
        {
            if (Convert.ToString(Session["key"]) != "admin") return RedirectToAction("Login", "Home");

            Patient_Api patientApi = new Patient_Api();
            var d = TempData["id"];
            TempData.Keep("id");
            if (account.patient_ID != 0)
            {
                TempData["id"] = account.patient_ID;
             
            }
            if (account.patient_ID != 0)
            { 
            var paymentsModel = patientApi.GetPatientPayments(account.patient_ID);
            return View("~/Views/Administrator/AdminViewPayments.cshtml", paymentsModel.OrderBy(p => p.bill_ID));
            }
            else
            {
                var paymentsModel = patientApi.GetPatientPayments((int)d);
                return View("~/Views/Administrator/AdminViewPayments.cshtml", paymentsModel.OrderBy(p => p.bill_ID));
            }
        }
        

        public ActionResult EditPayments(PaymentModel payment)
        {
            if (Convert.ToString(Session["key"]) != "admin") return RedirectToAction("Login", "Home");

            Patient_Api patientApi = new Patient_Api();

            var paymentsModel = patientApi.PayPayments(payment);

            return View("~/Views/Administrator/AdminViewPayments.cshtml", paymentsModel.OrderBy(p => p.bill_ID));
        }

        public ActionResult ViewForPayments()
        {
            if (Convert.ToString(Session["key"]) != "admin") return RedirectToAction("Login", "Home");

            Admin_Api adminGetAllPatientsApi = new Admin_Api();
            var model = adminGetAllPatientsApi.GetAllPatients();
            AppointmentModel app = new AppointmentModel();
            foreach (var p in model)
            {
                app.dropDown.Add(new SelectListItem { Text= p.firstName, Value= Convert.ToString(p.pid)});
            }
            if (model != null)
            {
                return View("~/Views/Administrator/ViewPatientsForPayments.cshtml", app);
            }
            error.ErrorMessage = "Unable to Get Patient Details";
            return View("~/Views/Error.cshtml", error);
        }

        public ActionResult SelectPatientsForPayments(AppointmentModel app)
        {
            if (Convert.ToString(Session["key"]) != "admin") return RedirectToAction("Login", "Home");

            Patient_Api patientApi = new Patient_Api();
            var paymentsModel = patientApi.GetPatientPayments(app.paitent_ID).GroupBy(g => g.bill_ID).Select(pa => pa.First()).ToList();
            PaymentModel p = new PaymentModel();
            foreach (var pay in paymentsModel)
            {
                p.billList.Add(new SelectListItem { Text = pay.bill_ID.ToString(), Value = pay.bill_ID.ToString() });
            }
            return View("~/Views/Administrator/AdminSelectBillID.cshtml", p);
        }
        
        public ActionResult GetPaymentsWithBillID(PaymentModel p)
        {
            if (Convert.ToString(Session["key"]) != "admin") return RedirectToAction("Login", "Home");

            Patient_Api patientApi = new Patient_Api();
            var paymentsModel = patientApi.GetAllPayments().Where(b => b.bill_ID == p.bill_ID);
            if (paymentsModel.Count() != 0)
            {
                ViewBag.totalAmount = paymentsModel.Last().total_Amount;
                ViewBag.paidAmount = paymentsModel.Last().paid_Amount;
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
                return Redirect("ViewPatientsForPayments");
            }
            return View("~/Views/Administrator/AdminPayPayments.cshtml", paymentsModel);
        }

        public ActionResult GetPaymentsWithID(int id)
        {
            if (Convert.ToString(Session["key"]) != "admin") return RedirectToAction("Login", "Home");

            Patient_Api patientApi = new Patient_Api();

            Account_Api accApi = new Account_Api();
            AccountModel accModel = new AccountModel();
            accModel.patient_ID = (int)Session["UserId"];
            var model = accApi.GetAccountsByBillID(accModel);
            var billingID = model.Where(a => a.appointment_ID == id).FirstOrDefault();
            if (billingID != null)
            {
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

        public ActionResult AdminPayPayments(PaymentModel payment)
        {
            if (Convert.ToString(Session["key"]) != "admin") return RedirectToAction("Login", "Home");

            Patient_Api patientApi = new Patient_Api();

            var paymentsModel = patientApi.PayPayments(payment);

            return RedirectToAction("ViewPatientsForPayments");
            //return View("~/Views/Patient/PatientPayments.cshtml", paymentsModel);
        }


        #endregion


        #region rooms
        public ActionResult ViewRooms()
        {
            if (Convert.ToString(Session["key"]) != "admin") return RedirectToAction("Login", "Home");

            TreatmentRoom_Api roomApi = new TreatmentRoom_Api();
            var model = roomApi.GetAllTreatmentRoomRecs();

           Patient_Api patientApi = new Patient_Api();
            var p = patientApi.GetPaitentsList();

            foreach (var r in model)
            {
                if (r.date > DateTime.Now) { r.canEdit = true; }
            }
            foreach (var m in model)
            {
                m.patientList = p;
            }
            return View("~/Views/Administrator/AdminViewRooms.cshtml", model);
        }

        public ActionResult BookRooms(TreatmentRoomModel roomModel)
        {
            if (Convert.ToString(Session["key"]) != "admin") return RedirectToAction("Login", "Home");

            TreatmentRoom_Api tr_Api = new TreatmentRoom_Api();
            roomModel.isBooked = true;
            roomModel.timings = "09:00";
             tr_Api.InsertTreatmentRoomRec(roomModel);

            TreatmentRoom_Api roomApi = new TreatmentRoom_Api();
            PatientModel pat = new PatientModel();
            pat.pid = roomModel.patient_ID;
            var model = roomApi.GetRoomsRecordsByPatID(pat);

            Patient_Api patientApi = new Patient_Api();
            var p = patientApi.GetPaitentsList();

            foreach (var r in model)
            {
                if (r.date > DateTime.Now) { r.canEdit = true; }
            }

            foreach (var m in model)
            {
                m.patientList = p;
            }
            return View("~/Views/Administrator/ViewRoomBooking.cshtml", model);
        }



        public ActionResult viewNewBooking()
        {
            if (Convert.ToString(Session["key"]) != "admin") return RedirectToAction("Login", "Home");

            Admin_Api adminApi = new Admin_Api();
            var patients = adminApi.GetAllPatients();

            TreatmentRoomModel trModel = new TreatmentRoomModel();

            foreach (var p in patients)
            {
                trModel.patientDropDown.Add(new SelectListItem {
                    Text = p.firstName,
                    Value = Convert.ToString(p.pid)
                });
            }

            TreatmentRoom_Api roomApi = new TreatmentRoom_Api();
            var bookedRooms = roomApi.GetAllTreatmentRooms().Where(t => t.isBooked == true).Select(t =>t.patient_ID);
            AppointmentTimings timings = new AppointmentTimings();
            trModel.appointmentTimings = timings.timingsDropDown;

            return View("~/Views/Administrator/AdminBookRoom.cshtml", trModel);
        }

        public ActionResult ViewPatRoomBooking()
        {
            if (Convert.ToString(Session["key"]) != "admin") return RedirectToAction("Login", "Home");
            Admin_Api adminApi = new Admin_Api();
            var patients = adminApi.GetAllPatients();

            List<SelectListItem> dropDownpatients = new List<SelectListItem>();

            foreach (var p in patients)
            {
                dropDownpatients.Add(new SelectListItem
                {
                    Text = p.firstName,
                    Value = Convert.ToString(p.pid)
                });
            }
            AccountModel model = new AccountModel();
            model.patientdropDown = dropDownpatients;

            if (model != null)
            {
                return View("~/Views/Administrator/AdminViewPatientsForRoom.cshtml", model);
            }
            return RedirectToAction("ViewRooms") ;
        }


        public ActionResult GetRoomsRecordsByPatID(AccountModel acc)
        {
            if (Convert.ToString(Session["key"]) != "admin") return RedirectToAction("Login", "Home");

            TreatmentRoom_Api roomApi = new TreatmentRoom_Api();
            PatientModel pat = new PatientModel();
            pat.pid = acc.patient_ID;
            var model = roomApi.GetRoomsRecordsByPatID(pat);

            Patient_Api patientApi = new Patient_Api();
            var p = patientApi.GetPaitentsList();

            foreach (var r in model)
            {
                if (r.date > DateTime.Now) { r.canEdit = true; }
            }

            foreach (var m in model)
            {
                m.patientList = p;
            }
            return View("~/Views/Administrator/ViewRoomBooking.cshtml", model.OrderBy(m => m.date));
        }

        public ActionResult ViewAppointmentsForRoom(int patientID)
        {
            var response = GetApptsForRooms(patientID);
            //int id = (int)Session["UserId"];
            //Patient_Api patientApi = new Patient_Api();
            //Appointment_Api app = new Appointment_Api();

            //IEnumerable<AppointmentModel> patientAppointments = patientApi.GetAppointmentsForBookedRooms(patientID).Where(a => a.appointment_Date > DateTime.Now && a.cancelled == false); //apts with no rooms
            //IEnumerable<AppointmentModel> patAptsNoBilling = app.GetAppointmentsWithNoBilling().Where(a => (a.paitent_ID == patientID && a.appointment_Date > DateTime.Now && a.cancelled == false)); //not billed apts

            //List<int> aptsInt = new List<int>();
            //foreach (var a in patientAppointments)
            //{
            //    aptsInt.Add(a.appointment_ID);
            //}

            //List<int> aptsIntNoBilling = new List<int>();
            //foreach (var a in patAptsNoBilling)
            //{
            //    aptsIntNoBilling.Add(a.appointment_ID);
            //}

            //PatientModel patModel = new PatientModel();
            //patModel.pid = patientID;
            //TreatmentRoom_Api trApi = new TreatmentRoom_Api();
            //var bookedAppointments = trApi.GetBookedRooms(patModel); //apts with room booked

            //List<int> bookedaptsInt = new List<int>();
            //foreach (var a in bookedAppointments)
            //{
            //    bookedaptsInt.Add(a.appointment_ID);
            //}

            //IEnumerable<int> finalApts = aptsIntNoBilling.Concat(aptsInt);//aptsIntNoBilling.Except(bookedaptsInt).ToList();
            //finalApts = finalApts.Except(bookedaptsInt).ToList();


            //List<SelectListItem> appts = new List<SelectListItem>();
            //foreach (var a in finalApts)
            //{
            //    appts.Add(new SelectListItem { Text = a.ToString(), Value = a.ToString() });
            //}
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult fillPatientList(string text)
        {
            Appointment_Api app = new Appointment_Api();
            TreatmentRoom_Api roomApi = new TreatmentRoom_Api();
            var model = roomApi.GetAllTreatmentRoomRecs().Where(t => (t.date == DateTime.Parse(text) && t.isBooked == true)).GroupBy(g => g.patient_ID).Select(s => s.First()).ToList();
            var allAptmts = app.GetAllAppointments();

            var apps = allAptmts.Where(a => a.appointment_Date > DateTime.Now).GroupBy(g => g.paitent_ID).Select(s => s.First()).ToList();

            TreatmentRoomModel trModelPats = new TreatmentRoomModel();

            TreatmentRoomModel trModel = new TreatmentRoomModel();

            //var allApts = app.GetAllAppointments();

            foreach (var p in model)
            {
                trModelPats.patientDropDown.Add(new SelectListItem
                {
                    Text = p.patientFirstName,
                    Value = Convert.ToString(p.patient_ID)
                });
            }

            foreach (var p in apps)
            {
                        trModel.patientDropDown.Add(new SelectListItem
                        {
                            Text = p.patientName,
                            Value = Convert.ToString(p.paitent_ID)
                        });
            }

            trModel.patientDropDown = (from t in trModel.patientDropDown
                                      where trModelPats.patientDropDown.All(o => o.Text != t.Text) select t).ToList();
            List<SelectListItem> patientList = new List<SelectListItem>();
            foreach (var p in trModel.patientDropDown)
            {
                patientList.Add(p);
            }
             
            foreach (var t in trModel.patientDropDown)
            {
                if ((GetApptsForRooms(Convert.ToInt32(t.Value))).Count() == 0)
                {
                    patientList.Remove(t);
                }
            }

            return Json(patientList, JsonRequestBehavior.AllowGet);
        }


        //  public ActionResult UpdateRooms(TreatmentRoomModel roomModel)
        public ActionResult UpdateRooms(TreatmentRoomModel roomModel)
        {
            if (Convert.ToString(Session["key"]) != "admin") return RedirectToAction("Login", "Home");

            //IEnumerable<TreatmentRoomModel> model = null;
            TreatmentRoom_Api roomApi = new TreatmentRoom_Api();

            TreatmentRoomModel trModel = new TreatmentRoomModel();
            trModel.room_ID = roomModel.room_ID;
            trModel.isBooked = false;
                roomApi.UpdateTreatmentRoom(roomModel);
            return RedirectToAction("ViewRooms");
            //model = 
            //return View("~/Views/Administrator/AdminViewRooms.cshtml", model);
        }

        [HttpPost]
        public JsonResult GetAvailableRooms(string text)
        {
            TreatmentRoom_Api roomApi = new TreatmentRoom_Api();

            TreatmentRoomModel room = new TreatmentRoomModel();
            room.date = DateTime.Parse(text);
            IEnumerable<string> availableRooms = roomApi.GetAvailableRooms(room).Select(r => r.room_ID);

            var ra = room.roomList;

            var displayRooms = new List<RoomValues>();
            try
            {
                displayRooms = ra.Where(r => availableRooms.Contains(r.Value)).ToList();
            }
            catch (Exception ex)
            {
            }
            return Json(displayRooms, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult CheckAvailabilityTreatmentRoom(TreatmentRoomModel roomModel)
        {
            TreatmentRoom_Api roomApi = new TreatmentRoom_Api();
            var roomDetail =  roomApi.CheckAvailabilityTreatmentRoom(roomModel);
            if(roomDetail != null)
            {
                return Json(roomDetail.timings, JsonRequestBehavior.AllowGet);
            }
            return Json(roomDetail, JsonRequestBehavior.AllowGet);
        }

        //Used to get aptmts for patients with no room booking 
        public List<SelectListItem> GetApptsForRooms(int patientID)
        {
            Patient_Api patientApi = new Patient_Api();
            Appointment_Api app = new Appointment_Api();

            IEnumerable<AppointmentModel> patientAppointments = patientApi.GetAppointmentsForBookedRooms(patientID).Where(a => a.appointment_Date > DateTime.Now && a.cancelled == false); //apts with no rooms
            IEnumerable<AppointmentModel> patAptsNoBilling = app.GetAppointmentsWithNoBilling().Where(a => (a.paitent_ID == patientID && a.appointment_Date > DateTime.Now && a.cancelled == false)); //not billed apts

            List<int> aptsInt = new List<int>();
            foreach (var a in patientAppointments)
            {
                aptsInt.Add(a.appointment_ID);
            }

            List<int> aptsIntNoBilling = new List<int>();
            foreach (var a in patAptsNoBilling)
            {
                aptsIntNoBilling.Add(a.appointment_ID);
            }

            PatientModel patModel = new PatientModel();
            patModel.pid = patientID;
            TreatmentRoom_Api trApi = new TreatmentRoom_Api();
            var bookedAppointments = trApi.GetBookedRooms(patModel); //apts with room booked

            List<int> bookedaptsInt = new List<int>();
            foreach (var a in bookedAppointments)
            {
                bookedaptsInt.Add(a.appointment_ID);
            }

            IEnumerable<int> finalApts = aptsIntNoBilling.Concat(aptsInt);//aptsIntNoBilling.Except(bookedaptsInt).ToList();
            finalApts = finalApts.Except(bookedaptsInt).ToList();


            List<SelectListItem> appts = new List<SelectListItem>();
            foreach (var a in finalApts)
            {
                appts.Add(new SelectListItem { Text = a.ToString(), Value = a.ToString() });
            }
            return appts;
        }


        #endregion

    }
}
