using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BusinessLogicLayerFactory;
using System.Web.Http.Cors;

namespace HospitalManagementSystemServer.Controllers
{
    public class UserLoginController : ApiController
    {
        // GET api/<controller>/5
        [System.Web.Http.HttpGet]
        public int Authenticate(int id, string pwd)
        {
            int response = -1;
            if (id > 3000 && id < 4000)
            {
                NurseBO nurseBo = new NurseBO();
                nurseBo.pid = id;
                nurseBo.pwd = pwd;

                NurseBLLFactory validatenurseBLLFactory = new NurseBLLFactory();
                string validateNurse = validatenurseBLLFactory.CreateNurseBLL().ValidateNurse().CreateNurseDAL().ValidateNurse_DAL(nurseBo);
                if (validateNurse.Equals("True"))
                {
                    response = 1;
                }
                else if (validateNurse.Equals("False")){
                    response = 0;
                }
            }
            else if (id > 4000 && id < 5000)
            {
               
                LabInchargeBO inchargeBo = new LabInchargeBO();
                inchargeBo.pid = id;
                inchargeBo.pwd = pwd;

                LabInchargeBLLFactory validateInchargeBLLFacotry = new LabInchargeBLLFactory();
                string validateIncharge = validateInchargeBLLFacotry.CreateLabInchargeBLL().ValidateLabInchargeBLL().CreateLabInchargeDAL().ValidateLabIncharge_DAL(inchargeBo);
                if (validateIncharge.Equals("True"))
                {
                    response = 2;
                }
                else if (validateIncharge.Equals("False"))
                {
                    response = 0;
                }
            }
            else if (id > 5000 && id < 6000)
            {
                DoctorBO doctorBO = new DoctorBO();
                doctorBO.pid = id;
                doctorBO.pwd = pwd;

                DoctorBLLFactory doctorBLLFactory = new DoctorBLLFactory();
                string validateDoctor = doctorBLLFactory.CreateDoctorBLL().ValidateDoctorBLL().CreateDoctorDAL().ValidateDoctor_DAL(doctorBO);
                if (validateDoctor.Equals("True"))
                {
                    response = 3;
                }
                else if (validateDoctor.Equals("False"))
                {
                    response = 0;
                }
            }
            else if (id > 6000 && id < 7000)
            {

                AdministratorBO adminBO = new AdministratorBO();
                adminBO.pid = id;
                adminBO.pwd = pwd;

                AdministratorBLLFactory adminBLLFactory = new AdministratorBLLFactory();
                string validateAdmin = adminBLLFactory.CreateAdministratorBLL().ValidateAdminBLL().CreateAdminstratorDAL().ValidateAdmin_DAL(adminBO);
                if (validateAdmin.Equals("True"))
                {
                    response = 4;
                }
                else if (validateAdmin.Equals("False"))
                {
                    response = 0;
                }
            }
            else if (id > 7000 && id < 8000)
            {
                PatientBO patientBO = new PatientBO();
                patientBO.pid = id;
                patientBO.pwd = pwd;

                PatientBLLFactory patientBLLFactory = new PatientBLLFactory();
                string  validatePatient = patientBLLFactory.CreatePatientBLL().ValidatePatientBLL().CreatePatientDAL().ValidatePatient_DAL(patientBO);
                if (validatePatient.Equals("True"))
                {
                    response = 5;
                }
                else if (validatePatient.Equals("False"))
                {
                    response = 0;
                }
            }
            
            return response;
        }

        //[System.Web.Http.HttpGet]
        //[EnableCors(origins: "*", headers: "*", methods: "*")]
       // public string UserForgotPassword(int pid,string emailID)
       [HttpPost]
        public string UserForgotPassword(Person _person)
        {
            var result = "";
            if(_person.pid == 1)
            {
                AdministratorBLLFactory admin = new AdministratorBLLFactory();
                AdministratorBO adminBO = new AdministratorBO();
                adminBO.emailID = _person.emailID;

                 result = admin.CreateAdministratorBLL().ValidateAdminBLL().CreateAdminstratorDAL().ValidateEmailAdmin_DAL(_person.emailID);
              
            }
            else if (_person.pid == 2)
            {
                PatientBLLFactory patient = new PatientBLLFactory();
                PatientBO patientBO = new PatientBO();
                patientBO.emailID = _person.emailID;

                 result = patient.CreatePatientBLL().ValidatePatientBLL().CreatePatientDAL().ValidateEmailPatient_DAL(patientBO);

            }
            else if(_person.pid == 3)
            {
                DoctorBLLFactory doctor = new DoctorBLLFactory();
                DoctorBO doctorBO = new DoctorBO();
                doctorBO.emailID = _person.emailID;
                result = doctor.CreateDoctorBLL().ValidateDoctorBLL().CreateDoctorDAL().ValidateEmailDoctor_DAL(doctorBO);
            }
            else if (_person.pid == 4)
            {
                NurseBLLFactory nurse = new NurseBLLFactory();
                NurseBO nurseBO = new NurseBO();
                nurseBO.emailID = _person.emailID;
                result = nurse.CreateNurseBLL().ValidateNurse().CreateNurseDAL().ValidateNurse_DAL(nurseBO);

            }
            else if (_person.pid == 5)
            {
                LabInchargeBLLFactory incharge = new LabInchargeBLLFactory();
                LabInchargeBO inchargeBO = new LabInchargeBO();
                inchargeBO.emailID = _person.emailID;

                result = incharge.CreateLabInchargeBLL().ValidateLabInchargeBLL().CreateLabInchargeDAL().ValidateLabIncharge_DAL(inchargeBO);
            }

            if ((result) != "false" && result != "" && result != null)
            {
                EmailController email = new EmailController();
                email.SendForgotPasswordEmail(result, _person.emailID);
                return "Sent Password to Email";
            }
            else 
            return "User Do not exist";
        }
    }
}