using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject
{
    /*Business Object to maintain Administrator, with properties like
     id, first name, Last name, email id, password date of birth, securityquestion, security answer, phone,
     address, gender, isActive(isActive- to check is the user's account is active or not)*/
    public class AdministratorBO : Person
    {
        public AdministratorBO(int pid, string firstName, string lastName, string emailID, string pwd,
            DateTime dateOfBirth, string securityQn, string securityAns, string phone, string address,
            string gender, bool isActive)
        :base(pid, firstName, lastName, emailID, pwd,
            dateOfBirth, securityQn, securityAns, phone, address,
            gender, isActive)
        { }
        public AdministratorBO()
        { }
    }
}
