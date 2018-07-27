using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayerFactory
{
    /// <summary>
    /// Doctor data Access layer Factory implemented as a part of factory design pattern
    /// </summary>
  public  class DoctorDALFactory
    {
        public DoctorDAL CreateDoctorDAL()
        {
            return new DoctorDAL();
        }
    }
}
