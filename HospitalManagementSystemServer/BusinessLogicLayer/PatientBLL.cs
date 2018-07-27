using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayerFactory;

namespace BusinessLogicLayer
{
    public class PatientBLL
    {
        //Method to insert New Patient Details, with return type PatientDalFactory 
        public PatientDALFactory InsertNewPatientBLL()
        {
            return new PatientDALFactory();
        }

        //Method to get Patient Details, with return type PatientDalFactory
        public PatientDALFactory GetPatientBLL()
        {
            return new PatientDALFactory();
        }

        //Method to update Patient Details, with return type PatientDalFactory
        public PatientDALFactory UpdatePatientBLL()
        {
            return new PatientDALFactory();
        }

        //Method to Deactivate Patient Details, with return type PatientDalFactory
        public PatientDALFactory DeActivatePatientBLL()
        {
            return new PatientDALFactory();
        }

        //Method to Activate Patient Details, with return type PatientDalFactory
        public PatientDALFactory ActivatePatientBLL()
        {
            return new PatientDALFactory();
        }

        //Method to Check Email Availability, with return type PatientDalFactory
        public PatientDALFactory CheckEmailAvailabilityBLL()
        {
            return new PatientDALFactory();
        }

        //Method to validate Patient Details, with return type PatientDalFactory
        public PatientDALFactory ValidatePatientBLL()
        {
            return new PatientDALFactory();
        }

        //Method to get security Details, with return type PatientDalFactory
        public PatientDALFactory GetSecurityDetailsBLL()
        {
            return new PatientDALFactory();

        }

        //Method to get patientAccount records, with return type PatientDalFactory
        public PatientDALFactory GetAccountDetails()
        {
            return new PatientDALFactory();
        }

        //Method to Update Account Details, with return type PatientDalFactory
        public PatientDALFactory UpdateAccountDetails()
        {
            return new PatientDALFactory();
        }

    }
}
