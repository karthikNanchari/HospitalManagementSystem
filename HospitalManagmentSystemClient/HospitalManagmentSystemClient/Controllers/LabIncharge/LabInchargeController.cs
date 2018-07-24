using System;

using System.Web.Mvc;
using Models;
using Client_Api;
using Serilog;
using System.Collections.Generic;

namespace HospitalManagmentSystemClient.Controllers.LabIncharge
{
    public class LabInchargeController : Controller
    {
     
        public ActionResult Index(int? id)
        {
            if (Convert.ToString(Session["key"]) != "labIncharge") return RedirectToAction("Login", "Home");

            if (id != null)
            {
                System.Web.HttpContext.Current.Session.Add("key", "labIncharge");
                System.Web.HttpContext.Current.Session.Add("UserId", id);
                Login_Api la = new Login_Api();
                var name = la.GetUserName(id);

                System.Web.HttpContext.Current.Session.Add("UserName", name);
            }
            LabIncharge_Api inchargeApi = new LabIncharge_Api();
            var reportsModel = inchargeApi.GetAllPatientReports_Api();

            if (reportsModel != null)
            {
                return View("~/Views/LabIncharge/LabInchargeHome.cshtml", reportsModel);
            }
            return View("~/Views/Error.cshtml");
        }

        //[HttpPost]
        public ActionResult GetPatientReports(int id)
        {
            if (Convert.ToString(Session["key"]) != "labIncharge") return RedirectToAction("Login", "Home");

            LabIncharge_Api inchargeApi = new LabIncharge_Api();

            var reportsModel = inchargeApi.GetPatientReports_Api(id);

            if (reportsModel != null)
            {
                return View("~/Views/LabIncharge/LabInchargeViewReports.cshtml", reportsModel);
            }
            // ErrorModel.ErrorMessage = "Unable to fetch Patient Reports Please try again";
            return View("~/Views/Error.cshtml");
        }


        public ActionResult ViewPatientReports()
        {
            if (Convert.ToString(Session["key"]) != "labIncharge") return RedirectToAction("Login", "Home");

            ReportModel reportModel = new ReportModel();
            reportModel.labIncharge_ID = (int)Session["UserId"];

            LabIncharge_Api inchargeApi = new LabIncharge_Api();
            var reportsModel = inchargeApi.ViewPatientReports_Api(reportModel);

            if (reportsModel != null)
            {
                return View("~/Views/LabIncharge/LabInchargeViewReports.cshtml", reportsModel);
            }
            // ErrorModel.ErrorMessage = "Unable to fetch Patient Reports Please try again";
            return View("~/Views/Error.cshtml");
        }


        public ActionResult EditPatientReports(ReportModel patientReport)
        {
            if (Convert.ToString(Session["key"]) != "labIncharge") return RedirectToAction("Login", "Home");

            LabIncharge_Api inchargeApi = new LabIncharge_Api();
            patientReport.date = DateTime.Now;

            var reportsModel = inchargeApi.UpdatePatientReport(patientReport);

            if (reportsModel != null)
            {
                return View("~/Views/LabIncharge/LabInchargeViewReports.cshtml", reportsModel);
            }


            // ErrorModel.ErrorMessage = "Unable to fetch Patient Reports Please try again";
            return View("~/Views/Error.cshtml");
        }

        public ActionResult DeletePatientReport(ReportModel ReportModel)
        {

            LabIncharge_Api inchargeApi = new LabIncharge_Api();

             inchargeApi.DeletePatientReport(ReportModel);

            return Json("success", JsonRequestBehavior.AllowGet);
        }


        public ActionResult ViewCreateNewReport()
        {
            if (Convert.ToString(Session["key"]) != "labIncharge") return RedirectToAction("Login", "Home");

            Patient_Api patientApi = new Patient_Api();
            //ViewBag.GetPatientsList = new SelectList(patientApi.GetPaitentsList());
            Admin_Api adminApi = new Admin_Api();
            var pats = adminApi.GetAllPatients();
            ReportModel repts = new ReportModel();

            foreach (var d in pats)
            {
                repts.dropDown.Add(new SelectListItem
                {
                    Text = d.firstName,
                    Value = Convert.ToString(d.pid)
                });
            }

            //ViewBag.GetPatientsList = docapts;
            return View("~/Views/LabIncharge/LabInchargeCreateReport.cshtml", repts);

        }

        public ActionResult CreateNewReport(ReportModel newReport)
        {
            if (Convert.ToString(Session["key"]) != "labIncharge") return RedirectToAction("Login", "Home");

            LabIncharge_Api inchargeApi = new LabIncharge_Api();
            newReport.labIncharge_ID = (int)Session["UserId"];

            if (newReport.patient_ID != 0 && newReport.reportTime != "-Select" && newReport.date != null && newReport.labResult != null)
            {
                List<ReportModel> reportsModel = new List<ReportModel>();
                newReport.reportTime = Convert.ToString(DateTime.Now.TimeOfDay);
                newReport.date = DateTime.Now;
                var model = inchargeApi.CreateNewReport(newReport);
                reportsModel.Add(model);

                if (reportsModel.Count != 0)
                {
                    return View("~/Views/LabIncharge/LabInchargeViewReports.cshtml", reportsModel);
                }
                else
                {
                    return RedirectToAction("ViewCreateNewReport");
                }
            }
            else
            {
                return RedirectToAction("ViewCreateNewReport");
            }
        }


        public ActionResult GetAppointments(int id)
        {
            if (Convert.ToString(Session["key"]) != "labIncharge") return RedirectToAction("Login", "Home");
            Patient_Api patientApi = new Patient_Api();

            IEnumerable<AppointmentModel> patientAppointments = patientApi.GetAppointments(id);
            ReportModel rptmOld = new ReportModel();
            //if (patientAppointments)
            {
                foreach (var a in patientAppointments)
                {
                    try
                    {
                        if (a.requestedReport)
                        {
                            rptmOld.appointmentIntIDs.Add(a.appointment_ID);
                        }
                    }
                    catch (Exception ex)
                    {
                        string test = ex.ToString();
                    }
                }
            }
            LabIncharge_Api inchargeApi = new LabIncharge_Api();
            var reportsModel = inchargeApi.GetPatientReports_Api(id);

            ReportModel rptmNew = new ReportModel();

            foreach (var a in reportsModel)
            {
                {
                    rptmNew.appointmentIntIDs.Add(a.appointment_ID);
                }
            }
            foreach (var a in rptmNew.appointmentIntIDs)
            {
                    rptmOld.appointmentIntIDs.Remove(a);
            }

            foreach (var a in rptmOld.appointmentIntIDs)
            {
                rptmOld.appointmentIDs.Add(new SelectListItem { Text = a.ToString(), Value= a.ToString() });
            }

            return Json(rptmOld.appointmentIDs, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetReqReportNotes(int aptId)
        {
            if (Convert.ToString(Session["key"]) != "labIncharge") return RedirectToAction("Login", "Home");
            Patient_Api patientApi = new Patient_Api();

            Appointment_Api appApi = new Appointment_Api();
            AppointmentModel appModel = new AppointmentModel();
            appModel.appointment_ID = aptId;
            var appDetails = appApi.GetApnmt_Api(appModel);

            return Json(appDetails.reqReportNotes, JsonRequestBehavior.AllowGet);
        }
    }

}
