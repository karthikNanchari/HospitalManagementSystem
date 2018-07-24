using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Models
{
    public class DoctorModel : PersonModel
    {
        //Enum to have constants like department of doctor and designation of doctor
        public enum DrDesignation { GeneralManager, Surgeon, Senior, Junior };
        public enum DrDepartment { Radiologist, Orthopedics, Dental }

        public DrDesignation drDesignation { get; set; }
        public DrDepartment drDepartment { get; set; }

        public List<SelectListItem> dropDownDoc { get; set; }

        public DoctorModel(int pid, string firstName, string lastName, string emailID, string pwd,
            DateTime dateOfBirth, string securityQn, string securityAns, string phone, string address,
            string gender, bool isActive, DrDesignation drDesignation, DrDepartment drDepartment)

            : base(pid, firstName, lastName, emailID, pwd,
            dateOfBirth, securityQn, securityAns, phone, address,
            gender, isActive)
        {

            this.drDesignation = drDesignation;
            this.drDepartment = drDepartment;
        }

        public DoctorModel() {
            dropDownDoc = new List<SelectListItem>();
        }
    }
}
