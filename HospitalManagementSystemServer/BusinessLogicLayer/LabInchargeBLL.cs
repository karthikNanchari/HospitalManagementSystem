using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayerFactory;

namespace BusinessLogicLayer
{
    public class LabInchargeBLL
    {
        //Method to insert new Lab incharge, with return type Lab incharge Dal Factory
        public LabInchargeDALFactory InsertNewLabInchargeBLL()
        {
            return new LabInchargeDALFactory();
        }

        public ReportDALFactory GetPatientReportsBLL() {

            return new ReportDALFactory();
        }
        //Method to update Lab incharge, with return type Lab incharge Dal Factory
        public LabInchargeDALFactory UpdateLabInchargeBLL()
        {
            return new LabInchargeDALFactory();
        }

        //Method to Deactivate lab incharge, with return type Lab incharge Dal Factory
        public LabInchargeDALFactory DeActivateLabInchargeBLL()
        {
            return new LabInchargeDALFactory();
        }

        //Method to activate Lab incharge, with return type Lab incharge Dal Factory
        public LabInchargeDALFactory ActivateLabInchargeBLL()
        {
            return new LabInchargeDALFactory();
        }

        //Method to validate Lab incharge, with return type Lab incharge Dal Factory
        public LabInchargeDALFactory ValidateLabInchargeBLL()
        {
            return new LabInchargeDALFactory();
        }

        //Method to get Lab incharge, with return type Lab incharge Dal Factory
        public LabInchargeDALFactory GetLabInchargeBLL()
        {
            return new LabInchargeDALFactory();
        }

        //Method to check availability of email for registration, with return type Lab incharge Dal Factory
        public LabInchargeDALFactory IsAvailableEmailBLL()
        {
            return new LabInchargeDALFactory();
        }


    }
}
