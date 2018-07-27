using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BusinessObject;
using BusinessLogicLayerFactory;


namespace HospitalManagementSystemServer.Controllers
{
    /// <summary>
    /// Appointment controller which Accepts rest service calls from front end
    /// </summary>
    public class AppointmentController : ApiController
    {

        // GET: api/Appointment/5
        //Get Action which accepts integer as parameter and appointment Business object as return type
        public AppointmentBO Get(int id)
        {
            AppointmentBO appBO = new AppointmentBO();
            appBO.appointment_ID = id;

            AppointmentBLLFactory appBLLFactory = new AppointmentBLLFactory();

            AppointmentBO app_BO = appBLLFactory.CreateAppointmentBLL().GetAppointment().CreateAppointmentDAL().GetAppointment(appBO);

            return app_BO;
        }

        [HttpPost, Route("api/Appointment/GetApmntByID")]
        public AppointmentBO GetApmntByID(AppointmentBO appBO)
        {
            AppointmentBLLFactory appBLLFactory = new AppointmentBLLFactory();

            AppointmentBO app_BO = appBLLFactory.CreateAppointmentBLL().GetAppointment().CreateAppointmentDAL().GetAppointment(appBO);

            return app_BO;
        }


        public IEnumerable<AppointmentBO> GetAppointment(int id)
        {
            AppointmentBO appBO = new AppointmentBO();
            appBO.paitent_ID = id;

            AppointmentBLLFactory appBLLFactory = new AppointmentBLLFactory();

            IEnumerable<AppointmentBO> app_BO = appBLLFactory.CreateAppointmentBLL().GetAppointment().CreateAppointmentDAL().GetAppointments(appBO);

            return app_BO;
        }

        public IEnumerable<AppointmentBO> GetAppointmentsForBookedRooms(int id)
        {
            AppointmentBO appBO = new AppointmentBO();
            appBO.paitent_ID = id;

            AppointmentBLLFactory appBLLFactory = new AppointmentBLLFactory();

            IEnumerable<AppointmentBO> app_BO = appBLLFactory.CreateAppointmentBLL().GetAppointment().CreateAppointmentDAL().GetAppointmentsForBookedRooms(appBO);

            return app_BO;
        }
        


        [HttpGet, Route("api/Appointment/GetAllAppointments")]
        public IEnumerable<AppointmentBO> GetAllAppointments()
        {

            AppointmentBLLFactory appBLLFactory = new AppointmentBLLFactory();

            IEnumerable<AppointmentBO> app_BO = appBLLFactory.CreateAppointmentBLL().GetAppointment().CreateAppointmentDAL().GetAllAppointments();

            return app_BO;
        }
        

        [HttpGet, Route("api/Appointment/GetAppointmentsWithNoBilling")]
        public IEnumerable<AppointmentBO> GetAppointmentsWithNoBilling()
        {
            AppointmentBLLFactory appBLLFactory = new AppointmentBLLFactory();

            IEnumerable<AppointmentBO> app_BO = appBLLFactory.CreateAppointmentBLL().GetAppointment().CreateAppointmentDAL().GetAppointmentsWithNoBilling();

            return app_BO;
        }
        

        // POST: api/Appointment
        // Post action which accepts Appointment Business object as parameter and same as return type
        public AppointmentBO BookAppointment(AppointmentBO appointmentBO)
        {
            AppointmentBLLFactory appntBLLFactory = new AppointmentBLLFactory();

            //appointmentBO.paitent_ID = 7002;
            //appointmentBO.doctor_ID = 5006;
            //appointmentBO.date = DateTime.Parse("2000/10/15");
            //appointmentBO.appointment_Date = DateTime.Parse("2010/2/3");
            //appointmentBO.timings = TimeSpan.Parse("08:00:00");

            AppointmentBO appBO = appntBLLFactory.CreateAppointmentBLL().CreateAppointmentBLL().CreateAppointmentDAL().NewAppointment(appointmentBO);

            return appBO;
        }

        [HttpPost,Route("api/Appointment/UpdateAppointment")]
        public AppointmentBO UpdateAppointment(AppointmentBO appointmentBO)
        {
            AppointmentBLLFactory appntBLLFactory = new AppointmentBLLFactory();

            appointmentBO.paitent_ID = 7002;
            appointmentBO.doctor_ID = 5006;
            appointmentBO.date = DateTime.Parse("2000/10/15");
            appointmentBO.appointment_Date = DateTime.Parse("2010/10/10");
            appointmentBO.timings = TimeSpan.Parse("08:00:00");

            AppointmentBO appBO = appntBLLFactory.CreateAppointmentBLL().CreateAppointmentBLL().CreateAppointmentDAL().UpdateApmnt(appointmentBO);

            return appBO;
        }


        // PUT: api/Appointment/5
        //Put action which accepts integer and appointment business object as parameter with void return type
        [HttpPost, Route("api/Appointment/DeleteAppointment")]
        public void DeleteAppointment(AppointmentBO appointmentBO)
        {
            
            AppointmentBLLFactory appntBLLFactory = new AppointmentBLLFactory();

            string deleteApp = appntBLLFactory.CreateAppointmentBLL().DeleteAppointmentBLL().CreateAppointmentDAL().DeleteAppointment(appointmentBO);
        }
    }
}
