using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject;

namespace DataAccessLayer
{
    public class AppointmentDAL
    {
        //Method to insert new appointment, with return type appointment Business object 
        public AppointmentBO NewAppointment(AppointmentBO appointmentBO)
        {
            try
            {
                if (checkAppointemnt(appointmentBO))
                {
                    using (HospitalManagementSystemDataContext objHmsDataContext = new HospitalManagementSystemDataContext(Utils.ConnectionString))
                    {
                        if (objHmsDataContext.Connection.State == System.Data.ConnectionState.Closed)
                            objHmsDataContext.Connection.Open();

                        Appointment appointment = new Appointment();

                        Appointment appointmentUpdate = ConvertBOToAppt(appointment, appointmentBO);



                        objHmsDataContext.Appointments.InsertOnSubmit(appointmentUpdate);

                        objHmsDataContext.SubmitChanges();
                    }
                }
                else
                {
                    AppointmentBO app_BO = new AppointmentBO();
                    return app_BO;
                }
                AppointmentBO appBO = GetAppointment(appointmentBO);

                return appBO;

            }
            catch (Exception e)
            {
                AppointmentBO app_BO = new AppointmentBO();
                return app_BO;
            }
        }

        public bool checkAppointemnt(AppointmentBO apmnt)
        {
            try
            {
                using (HospitalManagementSystemDataContext objHmsDataContext = new HospitalManagementSystemDataContext(Utils.ConnectionString))
                {
                    if (objHmsDataContext.Connection.State == System.Data.ConnectionState.Closed)
                        objHmsDataContext.Connection.Open();

                    Appointment appointment = new Appointment();

                    Appointment appointmentUpdate = ConvertBOToAppt(appointment, apmnt);

                    var checkExistingAppointments = objHmsDataContext.Appointments.Select(checkApmnt => (checkApmnt.Doctor_ID == apmnt.doctor_ID &&
                    checkApmnt.Patient_ID == apmnt.paitent_ID && checkApmnt.Appointment_Date == apmnt.appointment_Date
                    && checkApmnt.Timings == apmnt.timings));
                    if (checkExistingAppointments != null)
                    {
                        return true;
                    }
                    return false;
                }
            }
            catch { return false; }
        }

        //Method to convert BO to Appointment , with return type appointmet object
        public Appointment ConvertBOToAppt(Appointment appointment, AppointmentBO appointmentBO)
        {
            try
            {

                if (appointmentBO.appointment_ID != 0)
                {
                    appointment.Appointment_ID = appointmentBO.appointment_ID;
                }

                if (appointmentBO.paitent_ID != 0)
                {
                    appointment.Patient_ID = appointmentBO.paitent_ID;
                }

                if (appointmentBO.doctor_ID != 0)
                {
                    appointment.Doctor_ID = appointmentBO.doctor_ID;
                }

                if (!string.IsNullOrEmpty(Convert.ToString(appointmentBO.timings)))
                {
                    appointment.Timings = TimeSpan.Parse(Convert.ToString(appointmentBO.timings));

                }

                if (appointmentBO.date != DateTime.MinValue)
                {
                    appointment.Date_Time = appointmentBO.date;
                }

                if (appointmentBO.appointment_Date != DateTime.MinValue)
                {
                    appointment.Appointment_Date = appointmentBO.appointment_Date;
                }
                appointment.RequestedReportNotes = appointmentBO.reqReportNotes != null ? appointmentBO.reqReportNotes : "N/A";
                return appointment;
            }
            catch (Exception e)
            {
                Appointment app = new Appointment();
                return app;
            }

        }

        //Method to get appointment details, with return type appointmet Business object
        public AppointmentBO GetAppointment(AppointmentBO appointmentBO)
        {
            try
            {
                using (HospitalManagementSystemDataContext objHmsDataContext = new HospitalManagementSystemDataContext(Utils.ConnectionString))
                {
                    if (objHmsDataContext.Connection.State == System.Data.ConnectionState.Closed)
                        objHmsDataContext.Connection.Open();
                    AppointmentBO apmntBO = null;
                    if (appointmentBO.appointment_Date != DateTime.MinValue)
                    {
                         apmntBO = objHmsDataContext.Appointments.Where(apmnt => ((apmnt.Patient_ID == appointmentBO.paitent_ID && apmnt.Date_Time == appointmentBO.date) ||
                  (apmnt.Patient_ID == appointmentBO.paitent_ID && apmnt.Appointment_Date == appointmentBO.appointment_Date))
                  || (apmnt.Appointment_ID == appointmentBO.appointment_ID))
                  .Join(objHmsDataContext.Patients,
                  a => a.Patient_ID,
                  p => p.Patient_ID,
                  (a, p) => new AppointmentBO
                  {
                      appointment_ID = a.Appointment_ID,
                      patientName = p.First_Name,
                      paitent_ID = a.Patient_ID,
                      appointment_Date = a.Appointment_Date,
                      timings = a.Timings,
                      doctor_ID = a.Doctor_ID,
                      reqReportNotes = a.RequestedReportNotes != null ? a.RequestedReportNotes : "N/A"
                  }).First();
                    }
                    else
                    {
                        apmntBO = objHmsDataContext.Appointments.Where(apmnt =>(apmnt.Appointment_ID == appointmentBO.appointment_ID))
                        .Join(objHmsDataContext.Patients,
                        a => a.Patient_ID,
                        p => p.Patient_ID,
                        (a, p) => new AppointmentBO
                        {
                            appointment_ID = a.Appointment_ID,
                            patientName = p.First_Name,
                            paitent_ID = a.Patient_ID,
                            appointment_Date = a.Appointment_Date,
                            timings = a.Timings,
                            doctor_ID = a.Doctor_ID,
                            reqReportNotes = a.RequestedReportNotes != null ? a.RequestedReportNotes : "N/A"
                        }).First();
                    }

                    apmntBO.doctorName = objHmsDataContext.Doctors.Where(d => d.Doctor_ID == apmntBO.doctor_ID).Select(dn =>dn.First_Name).FirstOrDefault();
                    return apmntBO;
                }
            }
            catch (Exception e)
            {
                AppointmentBO app_BO = new AppointmentBO();
                return app_BO;
            }
        }


        public AppointmentBO GetAppointmentByID(AppointmentBO appointmentBO)
        {
            try
            {
                using (HospitalManagementSystemDataContext objHmsDataContext = new HospitalManagementSystemDataContext(Utils.ConnectionString))
                {
                    if (objHmsDataContext.Connection.State == System.Data.ConnectionState.Closed)
                        objHmsDataContext.Connection.Open();

                    AppointmentBO apmntBO = objHmsDataContext.Appointments.Where(apmnt => (apmnt.Appointment_ID == appointmentBO.appointment_ID))
                    .Join(objHmsDataContext.Patients,
                    a => a.Patient_ID,
                    p => p.Patient_ID,
                    (a, p) => new AppointmentBO
                    {
                        appointment_ID = a.Appointment_ID,
                        patientName = p.First_Name,
                        paitent_ID = a.Patient_ID,
                        appointment_Date = a.Appointment_Date,
                        timings = a.Timings,
                        doctor_ID = a.Doctor_ID,

                    }).First();

                    return apmntBO;
                }
            }
            catch (Exception e)
            {
                AppointmentBO app_BO = new AppointmentBO();
                return app_BO;
            }
        }

        public IEnumerable<AppointmentBO> GetAppointmentsWithNoBilling()
        {
            try
            {
                using (HospitalManagementSystemDataContext objHmsDataContext = new HospitalManagementSystemDataContext(Utils.ConnectionString))
                {
                    if (objHmsDataContext.Connection.State == System.Data.ConnectionState.Closed)
                        objHmsDataContext.Connection.Open();
                    List<int> appts = objHmsDataContext.Appointments.Where(a=> a.Cancelled == false).Select(a => a.Appointment_ID).ToList();
                    List<int> accBO = objHmsDataContext.Accounts.Select(a => a.Appointment_ID).ToList();
                    appts = appts.Except(accBO).ToList();

                    List<AppointmentBO> result = new List<AppointmentBO>();
                    AppointmentBO app = new AppointmentBO();
                    foreach(var a in appts)
                    {
                        app.appointment_ID = a;
                        var r = GetAppointmentByID(app);
                        result.Add(r);
                    }

                    return result;
                }
            }
            catch (Exception e)
            {
                IEnumerable<AppointmentBO> app_BO = null;
                return app_BO;
            }
        }

        public IEnumerable<AppointmentBO> GetAppointmentsForBookedRooms(AppointmentBO appointmentBO)
        {
            try
            {
                using (HospitalManagementSystemDataContext objHmsDataContext = new HospitalManagementSystemDataContext(Utils.ConnectionString))
                {
                    if (objHmsDataContext.Connection.State == System.Data.ConnectionState.Closed)
                        objHmsDataContext.Connection.Open();

                    var ap = objHmsDataContext.Appointments.Where(app => app.Patient_ID == appointmentBO.paitent_ID).Join(objHmsDataContext.Doctors,
                        a => a.Doctor_ID,
                        d => d.Doctor_ID,
                        (a, d) => new AppointmentBO
                        {
                            doctor_ID = a.Doctor_ID,
                            appointment_Date = a.Appointment_Date,
                            timings = a.Timings,
                            appointment_ID = a.Appointment_ID,
                            doctorName = d.First_Name,
                            paitent_ID = a.Patient_ID,
                            requestedReport = a.RequestedReport,
                            cancelled = a.Cancelled
                        }).ToList();
                    var appp = objHmsDataContext.TreatmentRoom_Records.Select(t => t.Appointment_ID);
                    foreach (var p in appp)
                    {
                        ap = ap.Where(a => a.appointment_ID != p).ToList();
                    }
                    return ap;
                }
            }
            catch (Exception e)
            {
                throw new Exception("Unable to get appointments");
            }
        }
        


        public IEnumerable<AppointmentBO> GetAppointments(AppointmentBO appointmentBO)
        {
            try
            {
                using (HospitalManagementSystemDataContext objHmsDataContext = new HospitalManagementSystemDataContext(Utils.ConnectionString))
                {
                    if (objHmsDataContext.Connection.State == System.Data.ConnectionState.Closed)
                        objHmsDataContext.Connection.Open();

                    var ap = objHmsDataContext.Appointments.Where(app => app.Patient_ID == appointmentBO.paitent_ID).Join(objHmsDataContext.Doctors,
                        a => a.Doctor_ID ,
                        d => d.Doctor_ID ,
                        (a, d) => new AppointmentBO
                        {
                            doctor_ID = a.Doctor_ID,
                            appointment_Date = a.Appointment_Date,
                            timings = a.Timings,
                            appointment_ID = a.Appointment_ID,
                            doctorName = d.First_Name,
                            paitent_ID = a.Patient_ID,
                            requestedReport = a.RequestedReport,
                           cancelled = a.Cancelled,
                           reqReportNotes = a.RequestedReportNotes
                        }).ToList();

                    foreach(var p in ap)
                    {
                        var patientDetails = objHmsDataContext.Patients.Where(pat => pat.Patient_ID == p.paitent_ID)
                            .Select(pp => new PatientBO {
                                 firstName = pp.First_Name 
                            });
                        p.patientName = patientDetails.First().firstName;
                    }
                    return ap;
                }
            }
            catch (Exception e)
            {
                throw new Exception("Unable to get appointments");
            }
        }

        public IEnumerable<AppointmentBO> GetAllAppointments()
        {
            try
            {
                using (HospitalManagementSystemDataContext objHmsDataContext = new HospitalManagementSystemDataContext(Utils.ConnectionString))
                {
                    if (objHmsDataContext.Connection.State == System.Data.ConnectionState.Closed)
                        objHmsDataContext.Connection.Open();

                    var ap = objHmsDataContext.Appointments.Join(objHmsDataContext.Doctors,
                        a => a.Doctor_ID,
                        d => d.Doctor_ID,
                        (a, d) => new AppointmentBO
                        {
                            doctor_ID = a.Doctor_ID,
                            appointment_Date = a.Appointment_Date,
                            timings = a.Timings,
                            appointment_ID = a.Appointment_ID,
                            doctorName = d.First_Name,
                            paitent_ID = a.Patient_ID,
                            requestedReport = a.RequestedReport
                        }).ToList();

                    foreach (var p in ap)
                    {
                        var patientDetails = objHmsDataContext.Patients.Where(pat => pat.Patient_ID == p.paitent_ID)
                            .Select(pp => new PatientBO
                            {
                                firstName = pp.First_Name
                            });
                        p.patientName = patientDetails.First().firstName;
                    }
                    return ap;
                }
            }
            catch (Exception e)
            {
                throw new Exception("Unable to get appointments");
            }
        }

        //Method to convert Appointment to AppointmentBO, with return type appointmet Business object
        public AppointmentBO ConvertApmntToBO(Appointment apmnt)
        {
            try
            {
                AppointmentBO apmntBO = new AppointmentBO(apmnt.Appointment_ID, apmnt.Patient_ID, apmnt.Appointment_Date,
                                                            apmnt.Timings, apmnt.Doctor_ID, apmnt.Date_Time, apmnt.RequestedReport, apmnt.RequestedReportNotes);
                return apmntBO;
            }
            catch (Exception e)
            {
                AppointmentBO appBO = new AppointmentBO();
                return appBO;
            }
        }

        //Method to update Appointment, with return type string 
        public AppointmentBO UpdateApmnt(AppointmentBO appointmentBO)
        {
            try
            {
                using (HospitalManagementSystemDataContext objHmsDataContext = new HospitalManagementSystemDataContext(Utils.ConnectionString))
                {
                    if (objHmsDataContext.Connection.State == System.Data.ConnectionState.Closed)
                        objHmsDataContext.Connection.Open();

                    Appointment apmnt = objHmsDataContext.Appointments.SingleOrDefault(appmt => (appmt.Appointment_ID == appointmentBO.appointment_ID ||
                                                                    (appmt.Patient_ID == appointmentBO.paitent_ID && appmt.Appointment_Date == appointmentBO.appointment_Date)));

                    Appointment updateApmnt = ConvertBOToAppt(apmnt, appointmentBO);

                    objHmsDataContext.SubmitChanges();

                }
                return GetAppointment(appointmentBO);
            }
            catch (Exception e)
            {
                throw new Exception("unable to update appointment now");
                //return "Unable to update appointment please try again later";
            }
        }

        //Method to delete Appointment, with return type string
        public string DeleteAppointment(AppointmentBO apmntBO)
        {
            try
            {
                using (HospitalManagementSystemDataContext objHmsDataContext = new HospitalManagementSystemDataContext(Utils.ConnectionString))
                {
                    if (objHmsDataContext.Connection.State == System.Data.ConnectionState.Closed)
                        objHmsDataContext.Connection.Open();

                    Appointment apmnt = objHmsDataContext.Appointments.SingleOrDefault(appmnt => (appmnt.Appointment_ID == apmntBO.appointment_ID &&
                                                                                                                appmnt.Patient_ID == apmntBO.paitent_ID));
                    Appointment converted_Apmnt = ConvertBOToAppt(apmnt, apmntBO);

                    objHmsDataContext.Appointments.DeleteOnSubmit(converted_Apmnt);

                    objHmsDataContext.SubmitChanges();

                    return "Appointment Deleted successfully";
                }
            }
            catch (Exception e)
            {
                return "Unable to Delete Appointment Please try again later";
            }
        }

        public AppointmentBO BookAppointment(AppointmentBO appointmentBO)
        {
            try
            {
                if (checkAppointemnt(appointmentBO))
                {
                    using (HospitalManagementSystemDataContext objHmsDataContext = new HospitalManagementSystemDataContext(Utils.ConnectionString))
                    {
                        if (objHmsDataContext.Connection.State == System.Data.ConnectionState.Closed)
                            objHmsDataContext.Connection.Open();

                        Appointment app = new Appointment();
                        Appointment newAppointment = ConvertBOToAppt(app, appointmentBO);
                        objHmsDataContext.Appointments.InsertOnSubmit(newAppointment);
                        objHmsDataContext.SubmitChanges();
                    }
                }
                else
                {
                    AppointmentBO app_BO = null;
                    return app_BO;
                }
                AppointmentBO appBO = GetAppointment(appointmentBO);
                return appBO;
            }
            catch (Exception e)
            {
                AppointmentBO app_BO = new AppointmentBO();
                return app_BO;
            }

        }
    }
}
