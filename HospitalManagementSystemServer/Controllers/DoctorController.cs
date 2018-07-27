using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BusinessObject;
using BusinessLogicLayerFactory;
using System.Web.Http.Cors;

namespace HospitalManagementSystemServer.Controllers
{
    public class DoctorController : ApiController
    {
        public DoctorBO GetDocDetails(int id, string pwd)
        {
            DoctorBO doctorBO = new DoctorBO();
            doctorBO.pid = id;
            doctorBO.pwd = pwd;

            DoctorBLLFactory getDoctorDetails = new DoctorBLLFactory();

            doctorBO = getDoctorDetails.CreateDoctorBLL().GetDoctorDetailsBLL().CreateDoctorDAL().GetDoctorDetails_DAL(doctorBO);

            return doctorBO;
        }

        //[Route("Doctor/{doctorId}/getDocApmnts")]
        [HttpGet]
        public IEnumerable<AppointmentBO> GetDoctorAppointments(int doctorId) {
            DoctorBLLFactory docBLLFactory = new DoctorBLLFactory();
            return docBLLFactory.CreateDoctorBLL().GetDocAppointments().CreateDoctorDAL().GetDocAppointments(doctorId);
        }


        public void InsertNewDoctor(DoctorBO doctorBO)
        {
            DoctorBLLFactory insertDoctor = new DoctorBLLFactory();

            DoctorBO newDoctor = insertDoctor.CreateDoctorBLL().InsertNewDoctorBLL().CreateDoctorDAL().InsertNewDoctor_DAL(doctorBO);

        }


        [HttpPost, Route("api/Doctor/RegisterNewDoctor")]
        public string RegisterNewDoctor(DoctorBO doctorBO)
        {
            DoctorBLLFactory insertDoctor = new DoctorBLLFactory();

            DoctorBO newDoctor = insertDoctor.CreateDoctorBLL().InsertNewDoctorBLL().CreateDoctorDAL().InsertNewDoctor_DAL(doctorBO);
            if (newDoctor.pid != 0)
            {
                return ("Your new User ID " + Convert.ToString(newDoctor.pid) + "- Login Using this UserID");
            }
            return ("Email ID is already Used by other User, try using other EmailID");

        }

        [Route("api/Doctor/BookAppointment")]
        public AppointmentBO BookAppointment(AppointmentBO apmntBO)
        {
            AppointmentController apmntCntrl = new AppointmentController();
            return apmntCntrl.BookAppointment(apmntBO);
        }


        public IEnumerable<DoctorBO> GetListDoctors() {

            DoctorBLLFactory getDoctors = new DoctorBLLFactory();

            var val = getDoctors.CreateDoctorBLL().GetDoctorsListBLL().CreateDoctorDAL().GetDoctorsList_DAL();
            return val;
        } 

        public void Put(int id, [FromBody]DoctorBO doctorBO)
        {

            DoctorBLLFactory doctorBLLFactory = new DoctorBLLFactory();
            string updateDoctor = doctorBLLFactory.CreateDoctorBLL().UpdateDoctorDetailsBLL().CreateDoctorDAL().UpdateDoctorDetails_DAL(doctorBO);

            string deactivate = doctorBLLFactory.CreateDoctorBLL().DeActivateDoctorBLL().CreateDoctorDAL().DeActivate_DAL(doctorBO);

            string activate = doctorBLLFactory.CreateDoctorBLL().ActivateDoctorBLL().CreateDoctorDAL().Activate_DAL(doctorBO);
        }

        [HttpPost, Route("api/Doctor/GetAvailableTimings")]
        public List<string> GetAvailableTimings(AppointmentBO appointmentBO)
        {
            DoctorBLLFactory getDoctors = new DoctorBLLFactory();

            var val = getDoctors.CreateDoctorBLL().GetDoctorsListBLL().CreateDoctorDAL().GetAvailableTimings(appointmentBO);
            return val;
        }

    }
}
