using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject;



namespace DataAccessLayer
{
   public class EmailDAL
    {
        public IEnumerable<PatientBO> GetPatientsAppointments_DAL()
        {
            try
            {
                using (HospitalManagementSystemDataContext objHmsDataContext = new HospitalManagementSystemDataContext(Utils.ConnectionString))
                {
                    if (objHmsDataContext.Connection.State == System.Data.ConnectionState.Closed)
                        objHmsDataContext.Connection.Open();

                    //var check_appointmentDate = DateTime.Now.AddDays(2);
                    var check_appointmentDate = DateTime.Now;
                    var app = (objHmsDataContext.Appointments.Where(a => a.Appointment_Date >= check_appointmentDate).Select(a => a.Patient_ID)).ToArray();
                    IEnumerable<PatientBO> patients = null;
                    if (app.Count() != 0)
                    {
                        foreach (var a in app)
                        {
                            patients = (objHmsDataContext.Patients.Where(p => p.Patient_ID == a)).Select(p => new PatientBO {
                                emailID = p.Email_ID,
                                firstName = p.First_Name
                            }).ToArray();
                        }
                    }
                    return patients;
                }
            }
            catch (Exception e)
            {
                IEnumerable<PatientBO> patients = null;
                return patients;
            }
        }

       

        }
    }
