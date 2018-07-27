using BusinessLogicLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayerFactory
{
    /* Class to provide access to Business Logic Layer,
     As a part of "Factory Design Pattern" this layer is developed.
     Gives access to Appointment related Controls   */
    public class AppointmentBLLFactory
    {
        public AppointmentBLL CreateAppointmentBLL()
        {
            return new AppointmentBLL();

        }
    }
}
