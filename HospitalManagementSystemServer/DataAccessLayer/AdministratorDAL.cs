using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject;


namespace DataAccessLayer
{
    /*Aministrator Data Access Layer, using Linq to Sql operations like
     insert, Get, update, delete are performed*/
   public class AdministratorDAL
    {

        public IEnumerable<DoctorBO> GetAllDoctors_DAL()
        {
            try
            {
                IEnumerable<DoctorBO> allDoctorsBO = null;
                PatientDAL patientDal = new PatientDAL();
                using (HospitalManagementSystemDataContext ObjHmsDataContext = new HospitalManagementSystemDataContext(Utils.ConnectionString))
                {
                    if (ObjHmsDataContext.Connection.State == System.Data.ConnectionState.Closed)
                        ObjHmsDataContext.Connection.Open();

                    allDoctorsBO = ObjHmsDataContext.Doctors.Select(docts => new DoctorBO
                    {
                        pid = docts.Doctor_ID,
                        firstName = docts.First_Name,
                        lastName = docts.Last_Name,
                        emailID = docts.Email_ID,
                        gender = docts.Gender,
                        dateOfBirth = docts.DateOfBirth,
                        address = docts.Address,
                        phone = docts.Phone,
                        isActive = docts.IsActive,
                        drDepartment = (DoctorBO.DrDepartment)(Enum.Parse(typeof(DoctorBO.DrDepartment), docts.Department)),
                        drDesignation = (DoctorBO.DrDesignation)(Enum.Parse(typeof(DoctorBO.DrDesignation), docts.Designation)),
                    }).ToArray();
                    return allDoctorsBO;

                }
            }
            catch
            {
                IEnumerable<DoctorBO> allDoctorsBO = null;
                return allDoctorsBO;
            }
        }


        public IEnumerable<PatientBO> GetAllPatients_DAL() {
            try
            {
                IEnumerable<PatientBO> allPatientsBO = null;
                using (HospitalManagementSystemDataContext ObjHmsDataContext = new HospitalManagementSystemDataContext(Utils.ConnectionString))
                {
                    if (ObjHmsDataContext.Connection.State == System.Data.ConnectionState.Closed)
                        ObjHmsDataContext.Connection.Open();

                    allPatientsBO = ObjHmsDataContext.Patients.Select(patnts => new PatientBO
                    {
                        pid = patnts.Patient_ID,
                        firstName = patnts.First_Name,
                        lastName = patnts.Last_Name,
                        emailID = patnts.Email_ID,
                        gender = patnts.Gender,
                        dateOfBirth = patnts.DateOfBirth,
                        address = patnts.Address,
                        phone = patnts.Phone,
                        isActive = patnts.IsActive,
                        patientType = (PatientBO.PatientType)(Enum.Parse(typeof(PatientBO.PatientType), patnts.Patient_Type))
                    }).ToArray();
                    return allPatientsBO;

                }
            }
            catch
            {
                IEnumerable<PatientBO> allPatientsBO = null;
                return allPatientsBO;
            }
        }

        public IEnumerable<NurseBO> GetAllNurses_DAL()
        {
            try
            {
                IEnumerable<NurseBO> allPatientsBO = null;
                using (HospitalManagementSystemDataContext ObjHmsDataContext = new HospitalManagementSystemDataContext(Utils.ConnectionString))
                {
                    if (ObjHmsDataContext.Connection.State == System.Data.ConnectionState.Closed)
                        ObjHmsDataContext.Connection.Open();

                    allPatientsBO = ObjHmsDataContext.Nurses.Select(patnts => new NurseBO
                    {
                        pid = patnts.Nurse_ID,
                        firstName = patnts.First_Name,
                        lastName = patnts.Last_Name,
                        emailID = patnts.Email_ID,
                        gender = patnts.Gender,
                        dateOfBirth = patnts.DateOfBirth,
                        address = patnts.Address,
                        phone = patnts.Phone,
                        isActive = patnts.IsActive,
                    }).ToArray();
                    return allPatientsBO;
                }
            }
            catch
            {
                IEnumerable<NurseBO> allNursesBO = null;
                return allNursesBO;
            }
        }

        public IEnumerable<LabInchargeBO> GetLabIncharges_DAL()
        {
            try
            {
                IEnumerable<LabInchargeBO> allPatientsBO = null;
                using (HospitalManagementSystemDataContext ObjHmsDataContext = new HospitalManagementSystemDataContext(Utils.ConnectionString))
                {
                    if (ObjHmsDataContext.Connection.State == System.Data.ConnectionState.Closed)
                        ObjHmsDataContext.Connection.Open();

                    allPatientsBO = ObjHmsDataContext.LabIncharges.Select(patnts => new LabInchargeBO
                    {
                        pid = patnts.LabIncharge_ID,
                        firstName = patnts.First_Name,
                        lastName = patnts.Last_Name,
                        emailID = patnts.Email_ID,
                        gender = patnts.Gender,
                        dateOfBirth = patnts.DateOfBirth,
                        address = patnts.Address,
                        phone = patnts.Phone,
                        isActive = patnts.IsActive,
                    }).ToArray();
                    return allPatientsBO;
                }
            }
            catch
            {
                IEnumerable<LabInchargeBO> allInchargesBO = null;
                return allInchargesBO;
            }
        }

        public IEnumerable<PatientBO> UpdatePatient_DAL(PatientBO updatePatientDetails) {
            PatientDAL patientDAL = new PatientDAL();
            var result = patientDAL.UpdatePatientDetails_DAL(updatePatientDetails);
            if(result != "")
            {
                return GetAllPatients_DAL();
            }
            //IEnumerable<PatientBO> emptyPatientsBO = ;
            return null;
        }

        public IEnumerable<DoctorBO> UpdateDoctor_DAL(DoctorBO updateDoctorDetails)
        {
            DoctorDAL doctorDAL = new DoctorDAL();
            var result = doctorDAL.UpdateDoctorDetails_DAL(updateDoctorDetails);
            if (result != "")
            {
                return GetAllDoctors_DAL();
            }
            //IEnumerable<PatientBO> emptyPatientsBO = ;
            return null;
        }

        //funtion to register new Administrator, with return type administrator Business object
        public AdministratorBO InsertAdministrator_DAL(AdministratorBO adminBO)
        {
            try
            {
                using (HospitalManagementSystemDataContext ObjHmsDataContext = new HospitalManagementSystemDataContext(Utils.ConnectionString))
                {
                    if (ObjHmsDataContext.Connection.State == System.Data.ConnectionState.Closed)
                        ObjHmsDataContext.Connection.Open();

                    Administrator admin = new Administrator();

                    Administrator admin_new = ConvertBOToAdmin( admin ,adminBO);
                    admin_new.IsActive = true;
                    ObjHmsDataContext.Administrators.InsertOnSubmit(admin_new);

                    ObjHmsDataContext.SubmitChanges();

                    AdministratorBO admin_Detials = GetAdminDetails_DAL(adminBO);

                    return admin_Detials;
                }
            }
            catch(Exception e)
            {
                AdministratorBO admin_BO = new AdministratorBO();

                return admin_BO;
            }
        }
        
        //Method to validate admin login details,with return type string 
        public string ValidateAdmin_DAL(AdministratorBO adminBO)
        {
            try 
            {
                using (HospitalManagementSystemDataContext ObjHmsDataContext = new HospitalManagementSystemDataContext(Utils.ConnectionString))
                {
                    if (ObjHmsDataContext.Connection.State == System.Data.ConnectionState.Closed)
                        ObjHmsDataContext.Connection.Open();
                    bool validation = (from Administrator in ObjHmsDataContext.Administrators
                                       where ((Administrator.Email_ID == adminBO.emailID && Administrator.Password == adminBO.pwd) ||
                                       (Administrator.Admin_ID == adminBO.pid && Administrator.Password == adminBO.pwd) ||
                                       (Administrator.Admin_ID == adminBO.pid && Administrator.Security_Question == adminBO.securityQn && Administrator.Security_Answer == adminBO.securityAns)||
                                       (Administrator.Email_ID == adminBO.emailID && Administrator.Security_Question == adminBO.securityQn && Administrator.Security_Answer == adminBO.securityAns))
                                       select Administrator.IsActive).SingleOrDefault();
                    return validation.ToString();
                }
            }
            catch{
                return "Login validation is not success";
            }

        }

        //used when user forgot password
        public string ValidateEmailAdmin_DAL(string adminEmail)
        {
            try
            {
                var validation = "false";
                using (HospitalManagementSystemDataContext ObjHmsDataContext = new HospitalManagementSystemDataContext(Utils.ConnectionString))
                {
                    if (ObjHmsDataContext.Connection.State == System.Data.ConnectionState.Closed)
                        ObjHmsDataContext.Connection.Open();
                    validation = (from Administrator in ObjHmsDataContext.Administrators
                                       where ((Administrator.Email_ID == adminEmail)) 
                                       select Administrator.Password).SingleOrDefault();
                    return validation.ToString();
                }
            }
            catch
            {
                return "";
            }

        }

        //funtion to check availability of email, with return type string 
        public string CheckAvailability(AdministratorBO adminBO)
        {
            using (HospitalManagementSystemDataContext objHmsDataContext = new HospitalManagementSystemDataContext(Utils.ConnectionString))
            { if (objHmsDataContext.Connection.State == System.Data.ConnectionState.Closed)
                    objHmsDataContext.Connection.Open();

                bool availability = (from admin in objHmsDataContext.Administrators
                                     select admin.Email_ID == adminBO.emailID).SingleOrDefault();        

                return Convert.ToString( availability);
            }
        }

        //Method to update Admin details, with return type string 
        public string UpdateAdminDetails_DAL(AdministratorBO adminBO)
        {
            try
            {

                using (HospitalManagementSystemDataContext ObjHmsDataContext = new HospitalManagementSystemDataContext(Utils.ConnectionString))
                {
                    if (ObjHmsDataContext.Connection.State == System.Data.ConnectionState.Closed)
                        ObjHmsDataContext.Connection.Open();

                    Administrator adminDetails = ObjHmsDataContext.Administrators.SingleOrDefault(admin => (admin.Email_ID == adminBO.emailID
                                                                                                            || admin.Admin_ID == adminBO.pid )); 

                    Administrator adminUpdated = ConvertBOToAdmin(adminDetails,adminBO);

                    ObjHmsDataContext.SubmitChanges();


                    return "Successfully Updated";
                }
            }
            catch
            {
                return "Unable to update please try again later";
            }

        }

        //funtion to get complete admin details by ID, with return type Administrator Business object
        public AdministratorBO GetAdminDetails_DAL(AdministratorBO adminBO)
        {
            try
            {
                using (HospitalManagementSystemDataContext ObjHmsDataContext = new HospitalManagementSystemDataContext(Utils.ConnectionString))
                {
                    if (ObjHmsDataContext.Connection.State == System.Data.ConnectionState.Closed)
                        ObjHmsDataContext.Connection.Open();

                    var adminDetails = ObjHmsDataContext.Administrators.SingleOrDefault(admin => ((admin.Admin_ID == adminBO.pid || admin.Email_ID == adminBO.emailID) && (admin.Password == adminBO.pwd)));

                    return ConvertAdminToBO(adminDetails);
                }
            }catch(Exception e)
            {
                AdministratorBO admin_BO = null; 

                return admin_BO;
            }
        }


        public AdministratorBO GetAdminDetailsByID_DAL(AdministratorBO adminBO)
        {
            try
            {
                using (HospitalManagementSystemDataContext ObjHmsDataContext = new HospitalManagementSystemDataContext(Utils.ConnectionString))
                {
                    if (ObjHmsDataContext.Connection.State == System.Data.ConnectionState.Closed)
                        ObjHmsDataContext.Connection.Open();

                    var adminDetails = ObjHmsDataContext.Administrators.SingleOrDefault(admin => ((admin.Admin_ID == adminBO.pid || admin.Email_ID == adminBO.emailID)));

                    return ConvertAdminToBO(adminDetails);
                }
            }
            catch (Exception e)
            {
                AdministratorBO admin_BO = null;

                return admin_BO;
            }
        }

        //funtion to deactive Admin account, with return type string
        public string DeActivateAdmin_DAL(AdministratorBO adminBO)
        {
            try
            {
                HospitalManagementSystemDataContext ObjHmsDataContext = new HospitalManagementSystemDataContext(Utils.ConnectionString);
                if (ObjHmsDataContext.Connection.State == System.Data.ConnectionState.Closed)
                    ObjHmsDataContext.Connection.Open();

                Administrator admin_Details = ObjHmsDataContext.Administrators.SingleOrDefault(admin => (admin.Email_ID == adminBO.emailID 
                                                                                    || admin.Admin_ID == adminBO.pid));

                admin_Details.IsActive = false;
                
                ObjHmsDataContext.SubmitChanges();

                return "Successfully Deactivated";
            }
            catch
            {
                return "Unable to Deactivate Admin Account";
            }

        }

        //funtion to activate Admin account, with return type string
        public string ActivateAdmin_DAL(AdministratorBO adminBO)
        {
            try
            {
                using(HospitalManagementSystemDataContext ObjHmsDataContext = new HospitalManagementSystemDataContext(Utils.ConnectionString))
                {
                    if (ObjHmsDataContext.Connection.State == System.Data.ConnectionState.Closed)
                        ObjHmsDataContext.Connection.Open();

                    Administrator admin_Details = ObjHmsDataContext.Administrators.SingleOrDefault(admin => (admin.Email_ID == adminBO.emailID ||
                                                                                        admin.Admin_ID == adminBO.pid));
                    admin_Details.IsActive = true;

                    ObjHmsDataContext.SubmitChanges();
                    return "Successfully Activated";
                }
            }
            catch(Exception e) {

                return "Unable to Activate Account please try again later";
            }

        }

        //Method to convert  Administrator to AdministratorBO, with return type Administrator Business object
        public AdministratorBO ConvertAdminToBO(Administrator admin)
        {
            AdministratorBO adminBO = new AdministratorBO(admin.Admin_ID, admin.First_Name, admin.Last_Name, admin.Email_ID,
                                                            admin.Password, admin.DateOfBirth, admin.Security_Question, admin.Security_Answer,
                                                            admin.Phone, admin.Address, admin.Gender, admin.IsActive);

            if(string.IsNullOrEmpty(adminBO.securityQn) )
            {
                adminBO.securityQn = admin.Security_Question;
            }
            return adminBO;
        }

        //funtion to convert AdministratorBO to Administrator, with return type Administrator
        public Administrator ConvertBOToAdmin( Administrator admin  ,AdministratorBO adminBO)
        {
            if (!string.IsNullOrEmpty(adminBO.firstName))
            {
                admin.First_Name = adminBO.firstName;
            }

            if (!string.IsNullOrEmpty(adminBO.lastName)) {
                admin.Last_Name = adminBO.lastName;
            }

            if (!string.IsNullOrEmpty(adminBO.lastName)) {
                admin.Email_ID = adminBO.emailID;
            }

            if (!string.IsNullOrEmpty(adminBO.pwd))
            {
                admin.Password = adminBO.pwd;
            }

            if (adminBO.dateOfBirth != DateTime.MinValue)
            {
                admin.DateOfBirth = adminBO.dateOfBirth;
            }

            if (!string.IsNullOrEmpty(adminBO.phone))
            {
                admin.Phone = adminBO.phone;
            }

            if (!string.IsNullOrEmpty(adminBO.address))
            {
                admin.Address = adminBO.address;
            }

            if (!string.IsNullOrEmpty(adminBO.securityQn))
            {
                admin.Security_Question = adminBO.securityQn;
            }

            if (!string.IsNullOrEmpty(adminBO.securityAns))
            {
                admin.Security_Answer = adminBO.securityAns;
            }

            if(!string.IsNullOrEmpty(adminBO.gender))
            {
                admin.Gender = adminBO.gender;
            }


            return admin;

        }

    }
}
