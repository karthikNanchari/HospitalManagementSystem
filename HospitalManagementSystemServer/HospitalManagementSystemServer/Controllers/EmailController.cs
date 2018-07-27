using System.Collections.Generic;
using System.Web.Http;
using BusinessObject;
using BusinessLogicLayerFactory;
using System.Net.Mail;
using System;

namespace HospitalManagementSystemServer.Controllers
{
    public class EmailController : ApiController
    {
        [HttpGet, Route("api/Email/GetPatientsAppointments")]
        public IEnumerable<PatientBO> GetPatientsAppointments()
        {
            AdministratorBLLFactory adminBLLFactory = new AdministratorBLLFactory();

            return adminBLLFactory.CreateAdministratorBLL().GetAdminDetailsBLL().ManageEmailsDAL().GetPatientsAppointments_DAL();
        }


        public void SendForgotPasswordEmail(string emailBody,string emailID)
        {
            try
            {
                MailMessage mailMessage = new MailMessage("karthikaspdotnet@gmail.com", emailID);
                // Specify the email body
                mailMessage.Body = emailBody;
                mailMessage.Subject = "Hospital Management System";
                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
                smtpClient.Credentials = new System.Net.NetworkCredential()
                {
                    UserName = "karthikaspdotnet@gmail.com",
                    Password = "karthik*123"
                };
                smtpClient.EnableSsl = true;
                smtpClient.Send(mailMessage);
            }
            catch (Exception ex)
            {

            }
        }

        public void SendAppCancelledEmail(List<AppointmentBO> cancelledAppts)
        {
            try
            {
                foreach (var p in cancelledAppts)
                {

                    PatientBO pat = new PatientBO();
                    pat.pid = p.paitent_ID;

                    PatientBLLFactory patientBBLFactory = new PatientBLLFactory();
                    var details = (patientBBLFactory.CreatePatientBLL().GetPatientBLL().CreatePatientDAL().GetPatientDetialsByID(pat));

                    foreach (var e in details)
                    {

                        MailMessage mailMessage = new MailMessage("karthikaspdotnet@gmail.com", e.emailID);
                        // Specify the email body
                        mailMessage.Body = "Dear Patient Your appointment with doctor" + p.doctorName + "and appointment Number" + p.appointment_ID +"and date"+ p.appointment_Date +"is cancelled, Kindly reschedule it.";
                        mailMessage.Subject = "Appointment Cancelled";
                        SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
                        smtpClient.Credentials = new System.Net.NetworkCredential()
                        {
                            UserName = "karthikaspdotnet@gmail.com",
                            Password = "karthik*123"
                        };
                        smtpClient.EnableSsl = true;
                        smtpClient.Send(mailMessage);
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
