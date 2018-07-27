using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject;

namespace DataAccessLayer
{
    /*Patient Data Access layer, Using Linq to Sql to perform operations like create */
    public class PatientDAL
    {
        //Method to insert patient details into patient table, with return type patient Business object
        public PatientBO InsertPatientDetails_DAL(PatientBO patientBO)
        {
            try
            {
                using (HospitalManagementSystemDataContext objHmsDataContext = new HospitalManagementSystemDataContext(Utils.ConnectionString))
                {
                    if (objHmsDataContext.Connection.State == System.Data.ConnectionState.Closed)
                        objHmsDataContext.Connection.Open();
                    {
                        Patient patient = new Patient();
                        Patient obj_patient = ConvertBOToPatient( patient , patientBO);
                        obj_patient.IsActive = true;
                        objHmsDataContext.Patients.InsertOnSubmit(obj_patient);

                        objHmsDataContext.SubmitChanges();

                        Patient newPatient = objHmsDataContext.Patients.SingleOrDefault(pat => pat.Email_ID == patientBO.emailID);

                        PatientBO new_Patient = ConvertPatientToBO(newPatient);

                        return new_Patient;
                    }
                }
            }
            catch (Exception e)
            {
                PatientBO patient_BO = new PatientBO();

                return  patient_BO; 
            }
        }

        //Method To Validate Patient login details, with string as return type 
        public string ValidatePatient_DAL(PatientBO patientBO)
        {
            try
            {
                using (HospitalManagementSystemDataContext objHmsDataContext = new HospitalManagementSystemDataContext(Utils.ConnectionString))
                {
                    if (objHmsDataContext.Connection.State == System.Data.ConnectionState.Closed)
                        objHmsDataContext.Connection.Open() ;
                    bool validateEmail = (from patient in objHmsDataContext.Patients
                                          where ((patient.Email_ID == patientBO.emailID && patient.Password == patientBO.pwd)
                                          ||(patient.Patient_ID == patientBO.pid && patient.Password == patientBO.pwd))
                                          select patient.IsActive).SingleOrDefault();

                    return Convert.ToString(validateEmail);
                }
            }
            catch (Exception e)
            {
                return "unable to validate User please try again later";
            }
        }

        public string ValidateEmailPatient_DAL(PatientBO patientBO)
        {
            try
            {
                var validateEmail = "false";
                using (HospitalManagementSystemDataContext objHmsDataContext = new HospitalManagementSystemDataContext(Utils.ConnectionString))
                {
                    if (objHmsDataContext.Connection.State == System.Data.ConnectionState.Closed)
                        objHmsDataContext.Connection.Open();
                    validateEmail = (from patient in objHmsDataContext.Patients
                                          where ((patient.Email_ID == patientBO.emailID))
                                          select patient.Password).SingleOrDefault();

                    return Convert.ToString(validateEmail);
                }
            }
            catch (Exception e)
            {
                return "";
            }
        }

        //Method to check if emailID is already used, with string return type  
        public string CheckAvailability_DAL(PatientBO patientBO)
        {
            try
            {
                using (HospitalManagementSystemDataContext ObjHmsDataContext = new HospitalManagementSystemDataContext(Utils.ConnectionString))
                {

                    if (ObjHmsDataContext.Connection.State == System.Data.ConnectionState.Closed)
                        ObjHmsDataContext.Connection.Open();
                    var emailID = (from patients in ObjHmsDataContext.Patients
                                   select patients.Email_ID == patientBO.emailID).SingleOrDefault();

                    //If emailId = true then Email_ID  is already used
                    return emailID.ToString();
                }
            }
            catch { return "EmailID Validation failed"; }
        }

        //Method to update patient details SecurityQuestion, with string return type
        public string UpdatePatientDetails_DAL(PatientBO patientBO)

        {
            try
            {
                using (HospitalManagementSystemDataContext objHmsDataContext = new HospitalManagementSystemDataContext(Utils.ConnectionString))
                {
                    if (objHmsDataContext.Connection.State == System.Data.ConnectionState.Closed)
                        objHmsDataContext.Connection.Open();

                    Patient patient = objHmsDataContext.Patients.SingleOrDefault(pat => (pat.Patient_ID == patientBO.pid || pat.Email_ID == patientBO.emailID ));

                    Patient updatedpatient = AssignUpdates(patient, patientBO);

                    objHmsDataContext.SubmitChanges();

                    return "Successfullly Updated";
                }
            }
            catch
            {
                return "";
            }

        }

        //Method to deactivate patient account, with string return type
        public string DeactivatePatient_DAL(PatientBO patientBO)
        {
            try
            {
                using (HospitalManagementSystemDataContext objHmsDataContext = new HospitalManagementSystemDataContext(Utils.ConnectionString))
                {
                    if (objHmsDataContext.Connection.State == System.Data.ConnectionState.Closed)
                        objHmsDataContext.Connection.Open();

                    Patient patient = objHmsDataContext.Patients.SingleOrDefault(id => id.Patient_ID == patientBO.pid);

                    patient.IsActive = false;

                    objHmsDataContext.SubmitChanges();

                    return "Account Deactivated Successfully";
                }
            }
            catch (Exception e)
            {
                return "Unable to deactive please try again";
            }

        }

        //Method to get patient details 
        public PatientBO GetPatientDetials_DAL(PatientBO patientBO)
        {
            try
            {

                using (HospitalManagementSystemDataContext objHmsDataContext = new HospitalManagementSystemDataContext(Utils.ConnectionString))
                {
                    if (objHmsDataContext.Connection.State == System.Data.ConnectionState.Closed)
                        objHmsDataContext.Connection.Open();

                    Patient patientInfo = objHmsDataContext.Patients.SingleOrDefault(pat => ((pat.Patient_ID == patientBO.pid || pat.Email_ID == patientBO.emailID ) && (pat.Password == patientBO.pwd)));

                    PatientBO patient_BO = ConvertPatientToBO(patientInfo);

                    return patient_BO;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        //Method to Activate Patient Account
        public string ActivatePatient_DAL(PatientBO patientBO)
        {
            try
            {
                using (HospitalManagementSystemDataContext objHmsDataContext = new HospitalManagementSystemDataContext(Utils.ConnectionString))
                {
                    if (objHmsDataContext.Connection.State == System.Data.ConnectionState.Closed)
                        objHmsDataContext.Connection.Open();

                    Patient patient1 = objHmsDataContext.Patients.SingleOrDefault(id => id.Patient_ID == patientBO.pid);

                    patient1.IsActive = true;

                    objHmsDataContext.SubmitChanges();

                    Patient patient2 = objHmsDataContext.Patients.SingleOrDefault(id => id.Patient_ID == patientBO.pid);

                    return " Account Activated";
                }
            }
            catch (Exception e)
            {

                return "Unable To Activate Account Try again later";
            }

        }

        //Convert Patient to PatientBO
        public PatientBO ConvertPatientToBO(Patient patient)
        {
            PatientBO patientBO = new PatientBO(patient.Patient_ID, patient.First_Name, patient.Last_Name, patient.Email_ID,
                                                 patient.Password, patient.DateOfBirth, patient.Security_Question, patient.Security_Answer,
                                                 patient.Phone, patient.Address, patient.Gender, patient.IsActive, ConvertStingToPatType(patient) );

            return patientBO;
        }

        //Converts PatientBO to Patient
        public Patient ConvertBOToPatient(Patient patient , PatientBO patientBO)
        {
            patient.First_Name = patientBO.firstName;
            patient.Last_Name = patientBO.lastName;
            patient.Patient_ID = patientBO.pid;
            patient.Email_ID = patientBO.emailID;
            patient.Password = patientBO.pwd;
            patient.Address = patientBO.address;
            patient.Phone = patientBO.phone;
            patient.Gender = patientBO.gender;
            patient.DateOfBirth = patientBO.dateOfBirth;
            patient.Security_Question = patientBO.securityQn;
            patient.Security_Answer = patientBO.securityAns;
            patient.Patient_Type = Convert.ToString(patientBO.patientType);
            patient.IsActive = patientBO.isActive;


            return patient;
        }

        //Funtion to Convert String to PatientType
        public PatientBO.PatientType ConvertStingToPatType(Patient patient)
        {
            return (PatientBO.PatientType)(Enum.Parse( typeof(PatientBO.PatientType) , patient.Patient_Type));
        }

        //Funtion to assign updates from patientBO to Patient
        public Patient AssignUpdates(Patient patient ,PatientBO patientBO )
        {
            if (!string.IsNullOrEmpty(patientBO.pwd))
            {
                patient.Password = patientBO.pwd;
            }

            if(!string.IsNullOrEmpty(patientBO.firstName))
            {
                patient.First_Name = patientBO.firstName;
            }

            if (!string.IsNullOrEmpty(patientBO.lastName))
            {
                patient.Last_Name = patientBO.lastName;
            }

            if (!string.IsNullOrEmpty(patientBO.address))
            {
                patient.Address = patientBO.address;
            }

            if (!string.IsNullOrEmpty(patientBO.phone))
            {
                patient.Phone = patientBO.phone;
            }

            if (!string.IsNullOrEmpty(patientBO.securityQn))
            {
                patient.Security_Question = patientBO.securityQn;
            }

            if (!string.IsNullOrEmpty(patientBO.securityAns))
            {
                patient.Security_Answer = patientBO.securityAns;
            }

            if (!string.IsNullOrEmpty(patientBO.gender))
            {
                patient.Gender = patientBO.gender;
            }

            if ((patientBO.dateOfBirth != DateTime.MinValue))
            {
                patient.DateOfBirth = patientBO.dateOfBirth;
            }

            if( !string.IsNullOrEmpty(Convert.ToString(patientBO.patientType)))
            {
                patient.Patient_Type = Convert.ToString(patientBO.patientType);
            }

            if ( patientBO.isActive != patient.IsActive)
            {
                patient.IsActive = patientBO.isActive;
            }

            return patient;
        }

        public IEnumerable<int> GetPatientsList()
        {
            try
            {
                using (HospitalManagementSystemDataContext objHmsDataContext = new HospitalManagementSystemDataContext(Utils.ConnectionString))
                {
                    if (objHmsDataContext.Connection.State == System.Data.ConnectionState.Closed)
                        objHmsDataContext.Connection.Open();

                    IEnumerable<int> patientsList = objHmsDataContext.Patients.Distinct().Select(p => p.Patient_ID).ToArray();
                    return patientsList;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public IEnumerable<int> GetPaitentsListByDate(AppointmentBO app)
        {
            try
            {
                using (HospitalManagementSystemDataContext objHmsDataContext = new HospitalManagementSystemDataContext(Utils.ConnectionString))
                {
                    if (objHmsDataContext.Connection.State == System.Data.ConnectionState.Closed)
                        objHmsDataContext.Connection.Open();

                    IEnumerable<int> patientsList = objHmsDataContext.Patients.Distinct().Select(p => p.Patient_ID).ToArray();

                   // IEnumerable<int> patients = objHmsDataContext.Appointments.Where(a =>a.Appointment_Date == app.appointment_Date && a.);

                    //foreach ()
                    //{

                    //}

                    return patientsList;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }
        

        public IEnumerable<PatientBO> GetPatientDetialsByID(PatientBO patientBO)
        {
            try
            {
                using (HospitalManagementSystemDataContext objHmsDataContext = new HospitalManagementSystemDataContext(Utils.ConnectionString))
                {
                    if (objHmsDataContext.Connection.State == System.Data.ConnectionState.Closed)
                        objHmsDataContext.Connection.Open();

                    IEnumerable<PatientBO> patientInfo = objHmsDataContext.Patients.Where(pat => (pat.Patient_ID == patientBO.pid))
                                                         .Select(p => new PatientBO {
                                                               firstName = p.First_Name ,
                                                                lastName = p.Last_Name,
                                                                address = p.Address,
                                                                phone = p.Phone,
                                                                emailID = p.Email_ID,
                                                                pwd = p.Password,
                                                                gender = p.Gender,
                                                                dateOfBirth = p.DateOfBirth,
                                                                patientType = ConvertStingToPatType(p),
                                                                pid = p.Patient_ID,
                                                                isActive = p.IsActive
                                                         }).ToArray();

                    return patientInfo;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
