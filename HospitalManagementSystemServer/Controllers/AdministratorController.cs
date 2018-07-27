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
    /// Administrator Controller which accepts Rest service calls from front end
    /// </summary>
    public class AdministratorController : ApiController
    {
        [HttpGet, Route("api/Administrator/GetAllPatients")]
        public IEnumerable<PatientBO> GetAllPatients() {

            AdministratorBLLFactory adminBLLFactory = new AdministratorBLLFactory();
           var allPatients = adminBLLFactory.CreateAdministratorBLL().GetAllPatientsBLL().CreateAdminstratorDAL().GetAllPatients_DAL();
            return allPatients;
        }

        [HttpGet, Route("api/Administrator/GetAllDoctors")]
        public IEnumerable<DoctorBO> GetAllDoctors() {
            AdministratorBLLFactory adminBLLFactory = new AdministratorBLLFactory();
            var allDoctors = adminBLLFactory.CreateAdministratorBLL().GetAllPatientsBLL().CreateAdminstratorDAL().GetAllDoctors_DAL();
            return allDoctors;
        }

        [HttpGet, Route("api/Administrator/GetAllNurses")]
        public IEnumerable<NurseBO> GetAllNurses()
        {
            AdministratorBLLFactory adminBLLFactory = new AdministratorBLLFactory();
            var allNurses = adminBLLFactory.CreateAdministratorBLL().GetAllPatientsBLL().CreateAdminstratorDAL().GetAllNurses_DAL();
            return allNurses;
        }

        [HttpGet, Route("api/Administrator/GetAllLabIncharges")]
        public IEnumerable<LabInchargeBO> GetAllLabIncharges()
        {
            AdministratorBLLFactory adminBLLFactory = new AdministratorBLLFactory();
            var allLabIncharges = adminBLLFactory.CreateAdministratorBLL().GetAllPatientsBLL().CreateAdminstratorDAL().GetLabIncharges_DAL();
            return allLabIncharges;
        }


        // GET: api/Administrator/5
        //Get action which accepts integer and string as parameters, administrator business object as return type
        public AdministratorBO Get(int id, string pwd)
        {
            AdministratorBO adminBO = new AdministratorBO();

            adminBO.pid = id;
            adminBO.pwd = pwd;

            AdministratorBLLFactory adminBLLFactory = new AdministratorBLLFactory();

            return adminBLLFactory.CreateAdministratorBLL().GetAdminDetailsBLL().CreateAdminstratorDAL().GetAdminDetails_DAL(adminBO);
        }

        // POST: api/Administrator
        //Post action, which accepts Administrator business object as parameter and void return type 
        public void Post([FromBody]AdministratorBO adminBO)
        {
            AdministratorBLLFactory adminBLLFactory = new AdministratorBLLFactory();

            AdministratorBO admin_BO = adminBLLFactory.CreateAdministratorBLL().InsertNewAdminBLL().CreateAdminstratorDAL().InsertAdministrator_DAL(adminBO);
        }

        [HttpPost, Route("api/Administrator/RegisterNewAdmin")]
        public string RegisterNewAdmin(AdministratorBO adminBO)
        {
            AdministratorBLLFactory adminBLLFactory = new AdministratorBLLFactory();

            AdministratorBO admin_BO = adminBLLFactory.CreateAdministratorBLL().InsertNewAdminBLL().CreateAdminstratorDAL().InsertAdministrator_DAL(adminBO);
            if (admin_BO.pid != 0)
            {
                return ("Your new User ID " + Convert.ToString(admin_BO.pid) + "- Login Using this UserID");
            }
            return ("Email ID is already Used by other User, try using other EmailID");
        }


        // PUT: api/Administrator/5
        //Put action which accepts integer and administrator as parameter and void return type
        public void Put(int id, [FromBody]AdministratorBO adminBO)
        {

            AdministratorBLLFactory adminBLLFactory = new AdministratorBLLFactory();

            string Update = adminBLLFactory.CreateAdministratorBLL().UpdateAdminDetailsBLL().CreateAdminstratorDAL().UpdateAdminDetails_DAL(adminBO);

            string activate = adminBLLFactory.CreateAdministratorBLL().ActivateAdminBLL().CreateAdminstratorDAL().ActivateAdmin_DAL(adminBO);

            string deactivate = adminBLLFactory.CreateAdministratorBLL().DeActivateAdminBLL().CreateAdminstratorDAL().DeActivateAdmin_DAL(adminBO);

        }

        //[HttpPost]
        [Route("api/Administrator/UpdatePatient")]
        public IEnumerable<PatientBO> UpdatePatient(PatientBO updatePatient) {

            AdministratorBLLFactory adminBLLFactory = new AdministratorBLLFactory();
            return adminBLLFactory.CreateAdministratorBLL().UpdatePatientDetailsBLL().CreateAdminstratorDAL().UpdatePatient_DAL(updatePatient);

        }

        [Route("api/Administrator/UpdateDoctor")]
        public IEnumerable<DoctorBO> UpdateDoctor(DoctorBO updateDoctor)
        {
            AdministratorBLLFactory adminBLLFactory = new AdministratorBLLFactory();
            return adminBLLFactory.CreateAdministratorBLL().UpdatePatientDetailsBLL().CreateAdminstratorDAL().UpdateDoctor_DAL(updateDoctor);
        }

        [ HttpPost, Route("api/Administrator/ManageNotifications")]
        public IEnumerable<NotificationsBO> ManageNotifications(NotificationsBO manageNotifications)
        {
            AdministratorBLLFactory adminBLLFactory = new AdministratorBLLFactory();
            return adminBLLFactory.CreateAdministratorBLL().ManageNotificationsBLL().ManageNotificationDAL().ManageNotifications_DAL(manageNotifications);
        }

        [HttpPost ,Route("api/Administrator/GetAllNotifications")]
        public IEnumerable<NotificationsBO> GetAllNotifications(NotificationsBO manageNotifications)
        {
            AdministratorBLLFactory adminBLLFactory = new AdministratorBLLFactory();
            return adminBLLFactory.CreateAdministratorBLL().ManageNotificationsBLL().ManageNotificationDAL().GetAllNotifications_DAL(manageNotifications);
        }

        [Route("api/Administrator/InsertNotifications")]
        public IEnumerable<NotificationsBO> InsertNotifications(NotificationsBO manageNotifications)
        {
            AdministratorBLLFactory adminBLLFactory = new AdministratorBLLFactory();
            return adminBLLFactory.CreateAdministratorBLL().ManageNotificationsBLL().ManageNotificationDAL().InsertNotifications_DAL(manageNotifications);
        }

        [HttpPost , Route("api/Administrator/DeleteNotifications")]
        public IEnumerable<NotificationsBO> DeleteNotifications(NotificationsBO deleteNotifications)
        {
            AdministratorBLLFactory adminBLLFactory = new AdministratorBLLFactory();
            return adminBLLFactory.CreateAdministratorBLL().ManageNotificationsBLL().ManageNotificationDAL().DeleteNotifications_DAL(deleteNotifications);
        }

        [Route("api/Administrator/GetAllAccounts")]
        public IEnumerable<AccountBO> GetAllAccounts()
        {
            AdministratorBLLFactory adminBLLFactory = new AdministratorBLLFactory();
            return adminBLLFactory.CreateAdministratorBLL().ManageAccountsBLL().CreateAccountDAL().GetAllAccounts_DAL();
        }

        [ HttpPost,Route("api/Administrator/GetAccountsByID")]
        public IEnumerable<AccountBO> GetAccountsByID(AccountBO accountBO)
        {
            AdministratorBLLFactory adminBLLFactory = new AdministratorBLLFactory();
            return adminBLLFactory.CreateAdministratorBLL().ManageAccountsBLL().CreateAccountDAL().GetAccountsByID_DAL(accountBO);
        }

        [HttpPost, Route("api/Administrator/SaveAccountDetail")]
        public IEnumerable<AccountBO> SaveAccountDetail(AccountBO accountBO)
        {
            AdministratorBLLFactory adminBLLFactory = new AdministratorBLLFactory();
            return adminBLLFactory.CreateAdministratorBLL().ManageAccountsBLL().CreateAccountDAL().AccountUpdateDetails_DAL(accountBO);
        }
        


        [HttpPost, Route("api/Administrator/DeleteAccountDetail")]
        public IEnumerable<AccountBO> DeleteAccountDetail(AccountBO accountBO)
        {
            AdministratorBLLFactory adminBLLFactory = new AdministratorBLLFactory();
            return adminBLLFactory.CreateAdministratorBLL().ManageAccountsBLL().CreateAccountDAL().DeleteAccountDetail_DAL(accountBO);
        }


    }
}
