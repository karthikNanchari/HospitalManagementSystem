using DataAccessLayer; 
    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayerFactory
{
    /// <summary>
    /// Appointment Data Access Layer implemented as a part of Factory design pattern
    /// </summary>
  public  class AppointmentDALFactory
    {
        public AppointmentDAL CreateAppointmentDAL()
        {
            return new AppointmentDAL();
        }
    }
}
