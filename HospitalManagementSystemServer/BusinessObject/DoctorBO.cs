using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject
{
    /*Business Object to maintain doctor details like,
     id, first name, Last name, email id, password date of birth, securityquestion, security answer, phone,
     address, gender, isActive(isActive- to check is the user's account is active or not)*/

    public class DoctorBO : Person
    {
        //Enum to have constants like department of doctor and designation of doctor
        public enum DrDesignation { GeneralManager, Surgeon, Senior, Junior }
        public enum DrDepartment { Radiologist, Orthopedics, Dental }

        public DrDesignation drDesignation { get; set; }
        public DrDepartment drDepartment { get; set; }

        public DoctorBO(int pid, string firstName, string lastName, string emailID, string pwd,
            DateTime dateOfBirth, string securityQn, string securityAns, string phone, string address,
            string gender, bool isActive, DrDesignation drDesignation, DrDepartment drDepartment)

            : base(pid, firstName, lastName, emailID, pwd,
            dateOfBirth, securityQn, securityAns, phone, address,
            gender, isActive)
        {

            this.drDesignation = drDesignation;
            this.drDepartment = drDepartment;
        }

        public DoctorBO()
        {
        }
    }
}
