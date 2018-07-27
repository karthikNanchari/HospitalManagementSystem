using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    /*Doctor Data Access layer, using Linq to Sql insert , delete, update, create operations are performed*/
    public class DoctorDAL
    {
        //funtion to register new doctor, with return type doctor business object 
        public DoctorBO InsertNewDoctor_DAL(DoctorBO doctorBO)
        {
            try
            {
                Doctor doctor = new Doctor();
                Doctor doctor_new = ConvertBOToDoctor( doctor,doctorBO);

                using (HospitalManagementSystemDataContext ObjHmsDataContext = new HospitalManagementSystemDataContext(Utils.ConnectionString))
                {
                    if (ObjHmsDataContext.Connection.State == System.Data.ConnectionState.Closed)
                        ObjHmsDataContext.Connection.Open();
                    doctor_new.IsActive = true;
                    ObjHmsDataContext.Doctors.InsertOnSubmit(doctor_new);
                    ObjHmsDataContext.SubmitChanges();

                    DoctorBO doctor_Details = GetDoctorDetails_DAL(doctorBO);


                    if (doctor_Details != null)
                    {
                        return doctor_Details ;
                    }
                    else
                    {
                        DoctorBO doctor_BO = new DoctorBO();
                        return doctor_BO;
                    }
                }
            }
            catch(Exception e)
            {
                DoctorBO doctor_BO = new DoctorBO();
                return doctor_BO ;
            }
        }

        //Method to get doctor details, with return type doctor business object  
        public DoctorBO GetDoctorDetails_DAL(DoctorBO doctorBO)
        {
            try
            {
                using (HospitalManagementSystemDataContext ObjHmsDataContext = new HospitalManagementSystemDataContext(Utils.ConnectionString))
                {
                    if (ObjHmsDataContext.Connection.State == System.Data.ConnectionState.Closed)
                        ObjHmsDataContext.Connection.Open();

                    Doctor doctor_Details = null;
                    //if (doctorBO.emailID != null)
                    {
                         doctor_Details = ObjHmsDataContext.Doctors.Where(id => (id.Email_ID == doctorBO.emailID || id.Doctor_ID == doctorBO.pid)).FirstOrDefault();
                    }
                    //else {
                        //doctor_Details = ObjHmsDataContext.Doctors.
                   // }
                    DoctorBO doctor_BO = ConvertDoctorToBO(doctor_Details);

                    return doctor_BO;
                }
            }
            catch(Exception e)
            {
                DoctorBO doctor_Empty = new DoctorBO();
                return doctor_Empty;
            }
        }

        public IEnumerable<DoctorBO> GetDoctorsList_DAL() {

            try
            {
                using (HospitalManagementSystemDataContext ObjHmsDataContext = new HospitalManagementSystemDataContext(Utils.ConnectionString))
                {
                    if (ObjHmsDataContext.Connection.State == System.Data.ConnectionState.Closed)
                        ObjHmsDataContext.Connection.Open();

                  var val = ObjHmsDataContext.Doctors.Select(x => new DoctorBO {firstName = x.First_Name, drDesignation = (DoctorBO.DrDesignation)ConvertStringToEnumDesign(x.Designation), drDepartment = (DoctorBO.DrDepartment)ConvertStringToEnumDept(x.Department) }).ToArray();
                    return val;
                }

            }
            catch (Exception ex) {
                throw new Exception("Unable to Fetch Doctors List");
            }
        }

        public IEnumerable<DoctorBO> GetDoctorById(DoctorBO docBO)
        {

            try
            {
                using (HospitalManagementSystemDataContext ObjHmsDataContext = new HospitalManagementSystemDataContext(Utils.ConnectionString))
                {
                    if (ObjHmsDataContext.Connection.State == System.Data.ConnectionState.Closed)
                        ObjHmsDataContext.Connection.Open();

                    var val = ObjHmsDataContext.Doctors.Where(d =>d.Doctor_ID == docBO.pid)
                        .Select(x => new DoctorBO
                        {
                            firstName = x.First_Name,
                            lastName = x.Last_Name,
                            gender = x.Gender,
                            address = x.Address,
                            isActive = x.IsActive,
                            dateOfBirth = x.DateOfBirth,
                            pid = x.Doctor_ID,
                            emailID = x.Email_ID,
                            pwd = x.Password,
                            phone = x.Phone,
                            drDesignation = (DoctorBO.DrDesignation)ConvertStringToEnumDesign(x.Designation),
                            drDepartment = (DoctorBO.DrDepartment)ConvertStringToEnumDept(x.Department) }).ToList();
                    return val;
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Unable to Fetch Doctors List");
            }
        }
        


        public IEnumerable<int> GetDoctorIDs_DAL()
        {
            try
            {
                using (HospitalManagementSystemDataContext ObjHmsDataContext = new HospitalManagementSystemDataContext(Utils.ConnectionString))
                {
                    if (ObjHmsDataContext.Connection.State == System.Data.ConnectionState.Closed)
                        ObjHmsDataContext.Connection.Open();

                    IEnumerable<int> doctorsList = ObjHmsDataContext.Doctors.Distinct().Select(d => d.Doctor_ID).ToArray();
                    return doctorsList;
                }

            }
            catch (Exception ex)
            {
                IEnumerable<int> doctorsList = null;
                return doctorsList;
            }
        }


        public List<string> GetAvailableTimings(AppointmentBO appointmentBo)
        {
            try
            {
                using (HospitalManagementSystemDataContext ObjHmsDataContext = new HospitalManagementSystemDataContext(Utils.ConnectionString))
                {
                    if (ObjHmsDataContext.Connection.State == System.Data.ConnectionState.Closed)
                        ObjHmsDataContext.Connection.Open();

                    IEnumerable<AppointmentBO> doctorsAppointmentsList = ObjHmsDataContext.Appointments.Where(a => a.Appointment_Date == appointmentBo.appointment_Date && a.Doctor_ID == appointmentBo.doctor_ID && a.Cancelled == false)
                                                                    .Select(t => new AppointmentBO {
                                                                    timings = t.Timings}).ToList();

                    List<string> timingsList = new List<string>();
                    foreach (var d in doctorsAppointmentsList) {
                        timingsList.Add(d.timings.ToString(@"hh\:mm"));
                    }

                    return timingsList;
                }
            }
            catch (Exception ex)
            {
                List<string> timingsList = null;
                return timingsList;
            }
        }

        public List<string> GetAvailableTimingsForPatient(AppointmentBO appointmentBo)
        {
            try
            {
                using (HospitalManagementSystemDataContext ObjHmsDataContext = new HospitalManagementSystemDataContext(Utils.ConnectionString))
                {
                    if (ObjHmsDataContext.Connection.State == System.Data.ConnectionState.Closed)
                        ObjHmsDataContext.Connection.Open();

                    IEnumerable<AppointmentBO> doctorsAppointmentsList = ObjHmsDataContext.Appointments.Where(a => a.Appointment_Date == appointmentBo.appointment_Date
                    && a.Patient_ID == appointmentBo.paitent_ID && a.Cancelled == false )
                                                                    .Select(t => new AppointmentBO
                                                                    {
                                                                        timings = t.Timings
                                                                    }).ToList();

                    List<string> timingsList = new List<string>();
                    foreach (var d in doctorsAppointmentsList)
                    {
                        timingsList.Add(d.timings.ToString(@"hh\:mm"));
                    }

                    return timingsList;
                }
            }
            catch (Exception ex)
            {
                List<string> timingsList = null;
                return timingsList;
            }
        }


        public IEnumerable<AppointmentBO> GetDocAppointments(int doctorId = 0)
        {
            try
            {
                using (HospitalManagementSystemDataContext ObjHmsDataContext = new HospitalManagementSystemDataContext(Utils.ConnectionString))
                {
                    if (ObjHmsDataContext.Connection.State == System.Data.ConnectionState.Closed)
                        ObjHmsDataContext.Connection.Open();
                    IEnumerable<AppointmentBO> docAptmntBo = null;
                    if (doctorId != 0)
                    {
                        docAptmntBo = ObjHmsDataContext.Appointments
                            .Where(apmnt => (apmnt.Doctor_ID == doctorId) && (apmnt.Cancelled == false))
                            .Join(ObjHmsDataContext.Patients,
                            a => a.Patient_ID,
                            p => p.Patient_ID,
                            (a,p) => new AppointmentBO { 
                             paitent_ID = a.Patient_ID,
                            appointment_Date = a.Appointment_Date,
                            timings = (a.Timings),
                            appointment_ID = a.Appointment_ID,
                            date = a.Date_Time,
                            patientName = p.First_Name,
                            requestedReport = a.RequestedReport

                        }).ToArray();
                    }
                    return docAptmntBo;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to get doctor appointments");
            }

        }

        public IEnumerable<AppointmentBO> cancelAppointment(AppointmentBO appBO)
        {
            try
            {
                using (HospitalManagementSystemDataContext ObjHmsDataContext = new HospitalManagementSystemDataContext(Utils.ConnectionString))
                {
                    if (ObjHmsDataContext.Connection.State == System.Data.ConnectionState.Closed)
                        ObjHmsDataContext.Connection.Open();
                    Appointment app = ObjHmsDataContext.Appointments.Where(a => a.Appointment_ID == appBO.appointment_ID).FirstOrDefault();

                    app.Cancelled = true;
                    ObjHmsDataContext.SubmitChanges();
                    return GetDocAppointments(appBO.doctor_ID);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to get doctor appointments");
            }

        }

        public IEnumerable<AppointmentBO> cancelAllAppointmentByDate(AppointmentBO appBO)
        {
            try
            {
                using (HospitalManagementSystemDataContext ObjHmsDataContext = new HospitalManagementSystemDataContext(Utils.ConnectionString))
                {
                    if (ObjHmsDataContext.Connection.State == System.Data.ConnectionState.Closed)
                        ObjHmsDataContext.Connection.Open();
                    IEnumerable<Appointment> app = ObjHmsDataContext.Appointments.Where(a => a.Appointment_Date == appBO.appointment_Date);
                    List<AppointmentBO> patientAppts = app
                        .Join(ObjHmsDataContext.Doctors,
                        a => a.Doctor_ID,
                        d => d.Doctor_ID,
                        (a,d) =>  new AppointmentBO
                    {
                        appointment_ID = a.Appointment_ID,
                        paitent_ID = a.Patient_ID,
                        appointment_Date = a.Appointment_Date,
                        timings = a.Timings,
                        doctorName = d.First_Name,
                    }).ToList();

                    foreach (var a in app) {
                        a.Cancelled = true;
                    }
                    ObjHmsDataContext.SubmitChanges();

                    return patientAppts;
                  //  return GetDocAppointments(appBO.doctor_ID);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to get doctor appointments");
            }

        }


        //Method to update doctor details,with return type string
        public string UpdateDoctorDetails_DAL(DoctorBO doctorBO)
        {
            try
            {
                using (HospitalManagementSystemDataContext ObjHmsDataContext = new HospitalManagementSystemDataContext(Utils.ConnectionString))
                {
                    
                    if (ObjHmsDataContext.Connection.State == System.Data.ConnectionState.Closed)
                        ObjHmsDataContext.Connection.Open();

                    Doctor doctor = ObjHmsDataContext.Doctors.SingleOrDefault(dctr => dctr.Email_ID == doctorBO.emailID
                                                                        || dctr.Doctor_ID == doctorBO.pid);
                    Doctor doctor_Details = ConvertBOToDoctor(doctor, doctorBO);

                    ObjHmsDataContext.SubmitChanges();

                    return "Successfully updated";
                }
            }
            catch
            {
                return "Unable to update Doctor Details please try again later";
            }
        }

        //Method to Check Availability of EmailID, with return type string
        public string CheckAvailability_DAL(DoctorBO doctorBO)
        {
            try
            {
                using (HospitalManagementSystemDataContext ObjHmsDataContext = new HospitalManagementSystemDataContext(Utils.ConnectionString))
                {
                    if (ObjHmsDataContext.Connection.State == System.Data.ConnectionState.Closed)
                        ObjHmsDataContext.Connection.Open();
                    var checkAvailablity = (from doctors in ObjHmsDataContext.Doctors
                                            select doctors.Email_ID == doctorBO.emailID
                                            ).SingleOrDefault();

                    return checkAvailablity.ToString();
                }
            }
            catch
            {
                return "Unable to check availability";
            }
        }

        //Method to validate, with return type string
        public string ValidateDoctor_DAL(DoctorBO doctorBO)
        {
            try
            {
                using (HospitalManagementSystemDataContext ObjHmsDataContext = new HospitalManagementSystemDataContext(Utils.ConnectionString))
                {

                    if (ObjHmsDataContext.Connection.State == System.Data.ConnectionState.Closed)
                        ObjHmsDataContext.Connection.Open();

                    bool validation = ObjHmsDataContext.Doctors.Where(d => (d.Doctor_ID == doctorBO.pid && d.Password == doctorBO.pwd)).Select(d => d.IsActive).SingleOrDefault();
                    return Convert.ToString(validation);
                }
            }
            catch(Exception ex)
            {
                return "Unable to Validate now please try again later";
            }

        }

        public string ValidateEmailDoctor_DAL(DoctorBO doctorBO)
        {
            try
            {
                var validation = "false";
                using (HospitalManagementSystemDataContext ObjHmsDataContext = new HospitalManagementSystemDataContext(Utils.ConnectionString))
                {

                    if (ObjHmsDataContext.Connection.State == System.Data.ConnectionState.Closed)
                        ObjHmsDataContext.Connection.Open();

              validation = (from doctors in ObjHmsDataContext.Doctors
                                       where ((doctors.Email_ID == doctorBO.emailID))
                                       select doctors.Password).SingleOrDefault();

                    return Convert.ToString(validation);
                }
            }
            catch (Exception ex)
            {
                return "";
            }

        }

        //Method to DeActive Doctor Account, with string
        public string DeActivate_DAL(DoctorBO doctorBO)
        {
            try
            {
                using (HospitalManagementSystemDataContext ObjHmsDataContext = new HospitalManagementSystemDataContext(Utils.ConnectionString))
                {
                    if (ObjHmsDataContext.Connection.State == System.Data.ConnectionState.Closed)
                        ObjHmsDataContext.Connection.Open();
                    ObjHmsDataContext.SubmitChanges();

                    Doctor doctor = ObjHmsDataContext.Doctors.SingleOrDefault(dctr => (dctr.Email_ID == doctorBO.emailID 
                                                                                      || dctr.Doctor_ID == doctorBO.pid));
                    
                    doctor.IsActive = false;

                    ObjHmsDataContext.SubmitChanges();

                    return "Account DeActivated successfully";
                }
            }
            catch
            {
                return "Unable to DeActivate Account please try again later ";
            }

        }

        //Method to Activate Doctor Account, with return type string
        public string Activate_DAL(DoctorBO doctorBO)
        {
            try
            {
                using (HospitalManagementSystemDataContext ObjHmsDataContext = new HospitalManagementSystemDataContext(Utils.ConnectionString))
                {
                    if (ObjHmsDataContext.Connection.State == System.Data.ConnectionState.Closed)
                        ObjHmsDataContext.Connection.Open();

                    Doctor doctor = ObjHmsDataContext.Doctors.SingleOrDefault(dctr => (dctr.Email_ID == doctorBO.emailID || dctr.Doctor_ID == doctorBO.pid));

                    doctor.IsActive = true;

                    ObjHmsDataContext.SubmitChanges();

                    return "Account Activated Successfully";
                }
            }
            catch(Exception e)
            {
                return "Unable to Activate Account";
            }

        }

        //Method To convert Doctor to DoctorBO
        public DoctorBO ConvertDoctorToBO(Doctor doctor)
        {
            try
            {
                DoctorBO doctorBO = new DoctorBO(doctor.Doctor_ID, doctor.First_Name, doctor.Last_Name, doctor.Email_ID,
                                                doctor.Password, doctor.DateOfBirth, doctor.Security_Question, doctor.Security_Answer,
                                                doctor.Phone, doctor.Address, doctor.Gender, doctor.IsActive, (DoctorBO.DrDesignation)ConvertStringToEnumDesign(doctor.Designation),
                                                (DoctorBO.DrDepartment)ConvertStringToEnumDept(doctor.Department));

                if(!string.IsNullOrEmpty(doctor.Security_Question) && !string.IsNullOrEmpty(doctorBO.securityQn) )
                {
                    doctorBO.securityQn = doctor.Security_Question;
                }

                return doctorBO;

            }
            catch(Exception e)
            {
                DoctorBO doctor_BO = new DoctorBO();
                return doctor_BO;
            }
        }

        //funtion to convert DoctorBO to Doctor
        public Doctor ConvertBOToDoctor(Doctor doctor, DoctorBO doctorBO)
        {
            if (doctorBO.pid != 0)
            {
                doctor.Doctor_ID = doctorBO.pid;
            }

            if (!string.IsNullOrEmpty(doctorBO.firstName))
            {
                doctor.First_Name = doctorBO.firstName;
            }

            if (!string.IsNullOrEmpty(doctorBO.lastName))
            {
                doctor.Last_Name = doctorBO.lastName;
            }

            if (!string.IsNullOrEmpty(Convert.ToString(doctorBO.drDepartment)))
            {
                doctor.Department = Convert.ToString(doctorBO.drDepartment);
            }

            if (!string.IsNullOrEmpty(Convert.ToString(doctorBO.drDesignation)))
            {
                doctor.Designation = Convert.ToString(doctorBO.drDesignation);
            }

            if (!string.IsNullOrEmpty(doctorBO.emailID))
            {
                doctor.Email_ID = doctorBO.emailID;
            }

            if (!string.IsNullOrEmpty(doctorBO.pwd))
            {
                doctor.Password = doctorBO.pwd;
            }

            if (!string.IsNullOrEmpty(doctorBO.gender))
            {
                doctor.Gender = doctorBO.gender;
            }

            if (doctorBO.dateOfBirth != DateTime.MinValue)
            {
                doctor.DateOfBirth = doctorBO.dateOfBirth;
            }

            if (!string.IsNullOrEmpty(doctorBO.phone))
            {
                doctor.Phone = doctorBO.phone;
            }

            if (!string.IsNullOrEmpty(doctorBO.address))
            {
                doctor.Address = doctorBO.address;
            }

            if (!string.IsNullOrEmpty(doctorBO.securityQn))
            {
                doctor.Security_Question = doctorBO.securityQn;
            }

            if (!string.IsNullOrEmpty(doctorBO.securityAns))
            {
                doctor.Security_Answer = doctorBO.securityAns;
            }

            {
                doctor.IsActive = doctorBO.isActive;
            }


            return doctor;
        }

        //funtion to convert string to department enum 
        public Enum ConvertStringToEnumDept(string doctorDept)
        {
            try
            {
                return (DoctorBO.DrDepartment)Enum.Parse(typeof(DoctorBO.DrDepartment), doctorDept);
            }
            catch(Exception e)
            {
                return (DoctorBO.DrDepartment)Enum.Parse(typeof(DoctorBO.DrDepartment), doctorDept);
            }
        }

        //Funtion to convert string to designation enum
        public Enum ConvertStringToEnumDesign(string doctorDesign)
        {
            try
            {
                return (DoctorBO.DrDesignation)Enum.Parse(typeof(DoctorBO.DrDesignation), doctorDesign);
            }
            catch(Exception e)
            {
                return (DoctorBO.DrDesignation)Enum.Parse(typeof(DoctorBO.DrDesignation), doctorDesign);
            }
        }

    
    }
}
