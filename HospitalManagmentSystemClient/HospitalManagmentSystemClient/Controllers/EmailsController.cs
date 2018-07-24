using System;
using System.Web.Mvc;
using Models;
using Client_Api;
using System.Net.Mail;
using System.Collections.Generic;
using System.Linq;

namespace HospitalManagmentSystemClient.Controllers
{
    public class EmailsController : Controller
    {
        // GET: Email
        public ActionResult SendEmail()
        {
            return View();
        }


        public ActionResult SendEmailToPpl(EmailModel Model)
        {
            try
            {
                MailMessage mailMessage = new MailMessage("karthikaspdotnet@gmail.com", Model.ToEmail);
                // Specify the email body
                mailMessage.Body = Model.EMailBody;
                mailMessage.Subject = Model.EmailSubject;
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
                Utils.Logging(ex, 2);
            }
            ViewBag.SuccessEmail = "SuccessFully Sent Email";
            return RedirectToAction("SendEmail");//View("~/Views/Emails/SendEmail.cshtml");
        }

        public void SendEmails(string emailTo, string emailBody)
        {
            try
            {
                MailMessage mailMessage = new MailMessage("karthikaspdotnet@gmail.com", emailTo);
                // Specify the email body
                mailMessage.Body = emailBody;
                mailMessage.Subject = "Testing smtp";
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
                Utils.Logging(ex, 2);
            }
        }

        public void EmailToPatients()
        {
            try
            {
                Email_Api emailApi = new Email_Api();
               var patients = emailApi.GetPatientsAppointments();
                foreach (var p in patients)
                {
                    SendEmails(p.emailID,"Dear Patient you have appointment with Doctor");
                }
            }
            catch(Exception ex)
            {
                Utils.Logging(ex, 2);
            }
        }

        public void EmailBooking(AppointmentModel app)
        {
            try
            {
                Email_Api emailApi = new Email_Api();
                Appointment_Api aptms = new Appointment_Api();
                var apppointment = aptms.GetAllAppointments().Where(a => a.appointment_ID == app.appointment_ID).First();
                var patients = emailApi.GetPatientsAppointments();

                Admin_Api adminApi = new Admin_Api();
                var patDetails = (adminApi.GetAllPatients()).Where(p => p.pid == apppointment.paitent_ID).First();

                MailMessage mailMessage = new MailMessage("karthikaspdotnet@gmail.com", patDetails.emailID);
                // Specify the email body
                mailMessage.Body = "Dear Patient you have booked appointment with Doctor " + app.doctorName +
                        " Appointment ID is " + app.appointment_ID + " appointment time is " + app.timings;
                mailMessage.Subject = "Success Booking";
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
                Utils.Logging(ex, 2);
            }
        }

        public void EmailCancelledAppt(AppointmentModel app)
        {
            try
            {
                Email_Api emailApi = new Email_Api();
                Appointment_Api aptms = new Appointment_Api();
                var apppointment = aptms.GetAllAppointments().Where(a => a.appointment_ID == app.appointment_ID).First();
                var patients = emailApi.GetPatientsAppointments();

                Admin_Api adminApi = new Admin_Api();
                var patDetails = (adminApi.GetAllPatients()).Where(p => p.pid == apppointment.paitent_ID).First();

                MailMessage mailMessage = new MailMessage("karthikaspdotnet@gmail.com", patDetails.emailID);
                // Specify the email body
                mailMessage.Body = "Dear Patient youre appointment with Doctor " + apppointment.doctorName +
                        " Appointment ID is " + app.appointment_ID + " appointment time is " + apppointment.timings + "is cancelled kindly reschedule";
                mailMessage.Subject = "Appointment  cancelled ";
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
                Utils.Logging(ex, 2);
            }
        }
        //[HttpPost]
        //public ActionResult SendEmail(EmailModel email)
        //{
        //    try
        //    {
        //        //Configuring webMail class to send emails  
        //        //gmail smtp server  
        //        WebMail.SmtpServer = "smtp.gmail.com";
        //        //gmail port to send emails  
        //        WebMail.SmtpPort = 587;
        //        WebMail.SmtpUseDefaultCredentials = true;
        //        //sending emails with secure protocol  
        //        WebMail.EnableSsl = true;
        //        //EmailId used to send emails from application  
        //        WebMail.UserName = "karthikaspdotnet@gmail.com";
        //        WebMail.Password = "karthik*123";

        //        //Sender email address.  
        //        WebMail.From = "karthikaspdotnet@gmail.com";

        //        //Send email  
        //        WebMail.Send(to: email.ToEmail, subject: email.EmailSubject, body: email.EMailBody, cc: email.EmailCC, bcc: email.EmailBCC, isBodyHtml: true);
        //        ViewBag.Status = "Email Sent Successfully.";
        //    }
        //    catch (Exception ex)
        //    {
        //        Utils.Logging(ex, null);
        //        ViewBag.Status = "Problem while sending email, Please check details.";

        //    }

        //    return View();
        //}


        // [HttpPost]
        //public void SendEmail(EmailModel email)
        //{
        //    try
        //    {
        //        //Configuring webMail class to send emails  
        //        //gmail smtp server  
        //        WebMail.SmtpServer = "smtp.gmail.com";
        //        //gmail port to send emails  
        //        WebMail.SmtpPort = 587;
        //        WebMail.SmtpUseDefaultCredentials = true;
        //        //sending emails with secure protocol  
        //        WebMail.EnableSsl = true;
        //        //EmailId used to send emails from application  
        //        WebMail.UserName = "karthikaspdotnet@gmail.com";
        //        WebMail.Password = "karthik*123";

        //        //Sender email address.  
        //        WebMail.From = "karthikaspdotnet@gmail.com";

        //        //Send email  
        //        WebMail.Send(to: email.ToEmail, subject: email.EmailSubject, body: email.EMailBody, cc: email.EmailCC, bcc: email.EmailBCC, isBodyHtml: true);
        //        ViewBag.Status = "Email Sent Successfully.";
        //    }
        //    catch (Exception ex)
        //    {
        //        Utils.Logging(ex, null);
        //        ViewBag.Status = "Problem while sending email, Please check details.";

        //    }
        //}
    }
}
