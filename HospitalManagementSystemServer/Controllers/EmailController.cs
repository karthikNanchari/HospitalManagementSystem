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



    }
}
