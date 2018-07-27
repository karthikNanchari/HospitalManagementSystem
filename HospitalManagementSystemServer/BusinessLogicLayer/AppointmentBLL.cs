using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayerFactory;

namespace BusinessLogicLayer
{
   public class AppointmentBLL
    {
        //Method to Create Appointment, with return type Appointment DAl Factory 
        public AppointmentDALFactory CreateAppointmentBLL()
        {
            return new AppointmentDALFactory();
        }

        //Method to Update Appointment, with return type Appointment DAl Factory
        public AppointmentDALFactory UpdateAppointmentBLL()
        {
            return new AppointmentDALFactory();
        }

        //Method to Delete Appointment, with return type Appointment DAl Factory
        public AppointmentDALFactory DeleteAppointmentBLL()
        {
            return new AppointmentDALFactory();
        }

        //Method to Get Appointment, with return type Appointment DAl Factory
        public AppointmentDALFactory GetAppointment()
        {
            return new AppointmentDALFactory();
        }

    }
}
