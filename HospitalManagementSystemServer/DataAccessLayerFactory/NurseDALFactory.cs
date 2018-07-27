using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayerFactory
{
    /// <summary>
    /// Nurse Data Access layer implemented as a part of Factory design pattern
    /// </summary>
  public  class NurseDALFactory
    {
        public NurseDAL CreateNurseDAL()
        {
            return new NurseDAL(); 
        }
    }
}
