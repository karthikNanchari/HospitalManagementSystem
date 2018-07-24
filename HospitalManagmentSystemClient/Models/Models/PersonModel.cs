using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class PersonModel
    {
        public int pid { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string emailID { get; set; }
        public string pwd { get; set; }
        public DateTime dateOfBirth { get; set; }
        public string securityQn { get; set; }
        public string securityAns { get; set; }
        public string phone { get; set; }
        public string address { get; set; }
        public string gender { get; set; }
        public bool isActive { get; set; }

        public PersonModel(int pid, string firstName, string lastName, string emailID, string pwd,
            DateTime dateOfBirth, string securityQn, string securityAns, string phone, string address,
            string gender, bool isActive)
        {
            this.pid = pid;
            this.firstName = firstName;
            this.lastName = lastName;
            this.emailID = emailID;
            this.pwd = pwd;
            this.dateOfBirth = dateOfBirth;
            this.securityQn = this.securityQn;
            this.securityAns = securityAns;
            this.phone = phone;
            this.address = address;
            this.gender = gender;
            this.isActive = isActive;

        }

        public PersonModel() { }

    }
}
