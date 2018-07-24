using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class LabInchargeModel: PersonModel
    {
        public LabInchargeModel(int pid, string firstName, string lastName, string emailID, string pwd,
         DateTime dateOfBirth, string securityQn, string securityAns, string phone, string address,
         string gender, bool isActive)

            : base (pid, firstName, lastName, emailID, pwd,
            dateOfBirth, securityQn, securityAns, phone, address,
            gender, isActive)
        { }

        public LabInchargeModel() { }
    }
}
