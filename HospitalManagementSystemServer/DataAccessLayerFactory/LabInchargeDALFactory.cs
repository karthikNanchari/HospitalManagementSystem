using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayerFactory
{
    /// <summary>
    /// Lab incharge Data access layer factory implemented as a part of fatory design pattern
    /// </summary>
  public  class LabInchargeDALFactory
    {
        public LabInchargeDAL CreateLabInchargeDAL()
        {
            return new LabInchargeDAL(); 
        }

    }
}
