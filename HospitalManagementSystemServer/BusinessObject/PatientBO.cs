using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject
{
    /*Business object to save patient related details like id, first name, Last name, email id, password date of birth, securityquestion, security answer, phone,
     address, gender, isActive(isActive- to check is the user's account is active or not) */
    public class PatientBO : Person
    {
        //Enum to have named constants of two main types
       public enum  PatientType { InPatient, OutPatient };

        public PatientType patientType { get; set; }

        public AppointmentBO patientAppointment { get; set; }
        public PatientBO(int pid, string firstName, string lastName, string emailID, string pwd,
            DateTime dateOfBirth, string securityQn, string securityAns, string phone, string address,
            string gender, bool isActive, PatientType patientType)
            : base(pid, firstName, lastName, emailID, pwd,
            dateOfBirth, securityQn, securityAns, phone, address,
            gender, isActive)
        {
            
            this.patientType = patientType ;
            
        }

        public PatientBO()
        { }

        
    }
}
