using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
   public class PatientModel :PersonModel
    {
        public  enum PatientType { InPatient, OutPatient };

        public  PatientType patientType { get; set; }

        public AppointmentModel patientAppointment { get; set; }

        public PatientModel(int pid, string firstName, string lastName, string emailID, string pwd,
            DateTime dateOfBirth, string securityQn, string securityAns, string phone, string address,
            string gender, bool isActive, PatientType patientType)
            : base(pid, firstName, lastName, emailID, pwd,
            dateOfBirth, securityQn, securityAns, phone, address,
            gender, isActive)
        {
            this.patientType = patientType;
        }

        public PatientModel() { }
    }
}
