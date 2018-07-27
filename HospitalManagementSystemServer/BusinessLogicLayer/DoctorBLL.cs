using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayerFactory;


namespace BusinessLogicLayer
{
    public class DoctorBLL
    {
        //Method to register new doctor, with return type Doctor Dal Factory
        public DoctorDALFactory InsertNewDoctorBLL()
        {
            return new DoctorDALFactory();
        }

        public DoctorDALFactory GetDoctorsListBLL()
        {
            return new DoctorDALFactory();
        }

        //Method to update doctor details, with return type Doctor Dal Factory
        public DoctorDALFactory UpdateDoctorDetailsBLL()
        {
            return new DoctorDALFactory();
        }

        //Method to Get doctor details, with return type Doctor Dal Factory
        public DoctorDALFactory GetDoctorDetailsBLL()
        {
            return new DoctorDALFactory();
        }

        //Method to deactivate Doctor account, with return type Doctor Dal Factory
        public DoctorDALFactory DeActivateDoctorBLL()
        {
            return new DoctorDALFactory();
        }

        //Method to activate Doctor account, with return type Doctor Dal Factory
        public DoctorDALFactory ActivateDoctorBLL()
        {
            return new DoctorDALFactory();
        }

        public DoctorDALFactory GetDocAppointments() {
            return new DoctorDALFactory();
        } 

        //Method to validate Doctor , with return type Doctor Dal Factory
        public DoctorDALFactory ValidateDoctorBLL()
        {
            return new DoctorDALFactory();
        }

    }
}
