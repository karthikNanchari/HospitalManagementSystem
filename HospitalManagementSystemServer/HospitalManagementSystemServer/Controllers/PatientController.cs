using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Runtime.Serialization;
using BusinessObject;
using BusinessLogicLayerFactory;
using System.Web.Http.Cors;

namespace HospitalManagementSystemServer.Controllers
{
    public class PatientController : ApiController
    {

        //[EnableCors(origins: "*", headers: "*", methods: "*")]
        public IEnumerable<DoctorBO> GetDoctorsList()
        {
            DoctorController doctorCntrlr = new DoctorController();
            return doctorCntrlr.GetListDoctors();
        }

        public IEnumerable<AppointmentBO> GetAppointments(int id)
        {
            AppointmentController appCntrlr = new AppointmentController();
            return appCntrlr.GetAppointment(id);
        }

        // GET: api/Patient/7001
        //Get action which accepts int and string as parameters and patient business object as return type
        public PatientBO Get(int id, string userPwd)
        {
            PatientBO patient_BO = new PatientBO();
            patient_BO.pid = id;
            patient_BO.pwd = userPwd;
            PatientBLLFactory patientBBLFactory = new PatientBLLFactory();
            patient_BO = patientBBLFactory.CreatePatientBLL().GetPatientBLL().CreatePatientDAL().GetPatientDetials_DAL(patient_BO);

            return patient_BO;
        }

        // POST: api/Patient
        // post action which accepts patient business object as input parameter and same as return type
        //public PatientBO Post(PatientBO patientDetails)
        //{
        //    PatientBO patient_BO = new PatientBO();

        //    PatientBLLFactory patientBLLFactory = new PatientBLLFactory();

        //    PatientBO patientID = patientBLLFactory.CreatePatientBLL().InsertNewPatientBLL().CreatePatientDAL().InsertPatientDetails_DAL(patientDetails);

        //    return patientID;
        //}

        //[EnableCors(origins: "*", headers: "*", methods: "*")]
        // [System.Web.Http.HttpPost]
        [HttpPost]
        public string RegisterNewUser(PatientBO patientDetails)
        {
            PatientBO patient_BO = new PatientBO();

            PatientBLLFactory patientBLLFactory = new PatientBLLFactory();

            PatientBO patientID = patientBLLFactory.CreatePatientBLL().InsertNewPatientBLL().CreatePatientDAL().InsertPatientDetails_DAL(patientDetails);
            if (patientID.pid != 0)
            {
                return ("Your new User ID " + Convert.ToString(patientID.pid) + "- Login Using this UserID");
            }
            return ("Email ID is already Used by other User, try using other EmailID");
        }

        // PUT: api/Patient/5
        //Put action with int and patient Business object as input parameter, string as return type
        public string Put(int id, [FromBody]PatientBO patientUpdates)
        {
            PatientBLLFactory patientBLLFactory = new PatientBLLFactory();

            string result = patientBLLFactory.CreatePatientBLL().UpdatePatientBLL().CreatePatientDAL().UpdatePatientDetails_DAL(patientUpdates);

            return result;
        }

        // DELETE: api/Patient/5
        //Delete action which accpets patient business object as input parameter and string as return type
        public string Delete(PatientBO details)
        {
            PatientBLLFactory patientBLLFactory = new PatientBLLFactory();

            PatientBO patientBO = new PatientBO();
            patientBO.pid = 7002;
            patientBO.emailID = "shlok@gmail.com";
            patientBO.pwd = "karthik123";

            string deactivateResult = patientBLLFactory.CreatePatientBLL().DeActivatePatientBLL().CreatePatientDAL().DeactivatePatient_DAL(patientBO);

            string activateResult = patientBLLFactory.CreatePatientBLL().ActivatePatientBLL().CreatePatientDAL().ActivatePatient_DAL(patientBO);

            string validateResult = patientBLLFactory.CreatePatientBLL().ValidatePatientBLL().CreatePatientDAL().ValidatePatient_DAL(patientBO);

            string checkAvailability = patientBLLFactory.CreatePatientBLL().CheckEmailAvailabilityBLL().CreatePatientDAL().CheckAvailability_DAL(patientBO);

            return "";
        }

        [HttpGet, Route("api/Patient/GetPaitentsList")]
        public IEnumerable<int> GetPaitentsList()
        {
            PatientBLLFactory patientBBLFactory = new PatientBLLFactory();
            var patientsList = patientBBLFactory.CreatePatientBLL().GetPatientBLL().CreatePatientDAL().GetPatientsList();

            return patientsList;
        }

        [HttpPost, Route("api/Patient/GetPaitentsListByDate")]
        public IEnumerable<int> GetPaitentsListByDate(AppointmentBO app)
        {
            PatientBLLFactory patientBBLFactory = new PatientBLLFactory();
            var patientsList = patientBBLFactory.CreatePatientBLL().GetPatientBLL().CreatePatientDAL().GetPatientsList();

            return patientsList;
        }

        [Route("api/Patient/GetDoctorIDs")]
        public IEnumerable<int> GetDoctorIDs()
        {

            DoctorBLLFactory getDoctors = new DoctorBLLFactory();

            var val = getDoctors.CreateDoctorBLL().GetDoctorsListBLL().CreateDoctorDAL().GetDoctorIDs_DAL();
            return val;
        }

        [Route("api/Patient/GetPatientPayments")]
        public IEnumerable<PaymentBO> GetPatientPayments(int id)
        {
            PaymentBO paymentBO = new PaymentBO();

            paymentBO.payment_ID = id;

            PaymentBLLFactory paymentBLLFactory = new PaymentBLLFactory();

            IEnumerable<PaymentBO> payment_BO = paymentBLLFactory.CreatePaymentBLL().GetPaymentBLL().CreatePaymentDAL().GetPatientPayments(id);

            return payment_BO;

        }

        [Route("api/Patient/PatientPayPayments")]
        public IEnumerable<PaymentBO> PatientPayPayments(PaymentBO payment_BO)
        {
            PaymentBO paymentBO = new PaymentBO();

            //paymentBO.payment_ID = id;

            PaymentBLLFactory paymentBLLFactory = new PaymentBLLFactory();

            IEnumerable<PaymentBO> paymt_BO = paymentBLLFactory.CreatePaymentBLL().GetPaymentBLL().CreatePaymentDAL().PatientPayPayments(payment_BO);

            return paymt_BO;

        }

        [HttpGet,Route("api/Patient/GetAllPayments")]
        public IEnumerable<PaymentBO> GetAllPayments( )
        {

            PaymentBLLFactory paymentBLLFactory = new PaymentBLLFactory();

            IEnumerable<PaymentBO> paymt_BO = paymentBLLFactory.CreatePaymentBLL().GetPaymentBLL().CreatePaymentDAL().GetAllPayments();

            return paymt_BO;

        }
        

        [Route("api/Patient/PatientBookAppointment")]
        public AppointmentBO PatientBookAppointment(AppointmentBO bookAppointment)
        {
            AppointmentBLLFactory appBLLFactory = new AppointmentBLLFactory();

            AppointmentBO newAppointment = appBLLFactory.CreateAppointmentBLL().GetAppointment().CreateAppointmentDAL().BookAppointment(bookAppointment);

            return newAppointment;
        }

        [HttpPost, Route("api/Patient/GetPatientById")]
        public IEnumerable<PatientBO> GetPatientById(PatientBO patientBO)
        {
            PatientBLLFactory patientBBLFactory = new PatientBLLFactory();
            return patientBBLFactory.CreatePatientBLL().GetPatientBLL().CreatePatientDAL().GetPatientDetialsByID(patientBO);

        }

        [HttpPost, Route("api/Patient/PatientValidateEmail")]
        public string PatientValidateEmail(string text)
        {
            PatientBLLFactory patient = new PatientBLLFactory();
            PatientBO pat = new PatientBO();
            pat.emailID = text;
            return patient.CreatePatientBLL().ValidatePatientBLL().CreatePatientDAL().ValidateEmailPatient_DAL(pat);

        }


        [HttpPost, Route("api/Patient/ViewBookedRoom")]
        public IEnumerable<TreatmentRoomBO> ViewBookedRoom(PatientBO pat)
        {
            TreatmentRoomBLLFactory trmntroomBLLFactory = new TreatmentRoomBLLFactory();
            return trmntroomBLLFactory.CreateTreatmentRoomBLL().DeleteTreatmentRoomBLL().CreateTreatmentRoomDAL().GetBookedRooms(pat);
        }

        [HttpGet, Route("api/Patient/GetAppointmentsForBookedRooms")]
        public IEnumerable<AppointmentBO> GetAppointmentsForBookedRooms(int id)
        {
            AppointmentController appCntrlr = new AppointmentController();
            return appCntrlr.GetAppointmentsForBookedRooms(id);
        }
        

    }
}
