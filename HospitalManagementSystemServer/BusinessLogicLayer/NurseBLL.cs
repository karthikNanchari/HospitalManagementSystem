using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayerFactory;

namespace BusinessLogicLayer
{
    public class NurseBLL
    {
        //Method to Insert New Nurse details , with return type NurseDalFactory 
        public NurseDALFactory InsertNewNurseBLL()
        {
            return new NurseDALFactory();
        }

        //Method Update Nurse Details, with return type NurseDalFactory 
        public NurseDALFactory UpdateNurseDetailsBLL()
        {
            return new NurseDALFactory();
        }

        //Method Activate Nurse Details,with return type NurseDalFactory
        public NurseDALFactory ActivateNurseBLL()
        {
            return new NurseDALFactory();
        }

        //Method DeActive Nurse Details, With return type NurseDalFactory
        public NurseDALFactory DeActivateNurseBLL()
        {
            return new NurseDALFactory();
        }

        //Method Validate Nurse, with return type NurseDalFactory
        public NurseDALFactory ValidateNurse()
        {
            return new NurseDALFactory();
        }

        //Funtion get Nurse Details, with return type Nurse Dal Factory
        public NurseDALFactory GetNurseDetails()
        {
            return new NurseDALFactory();
        }

    }
}
