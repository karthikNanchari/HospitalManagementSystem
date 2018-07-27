using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayerFactory
{
    /// <summary>
    /// Treatment Data access layer implemented as a part of factory desgin pattern
    /// </summary>
  public  class TreatmentDALFactory
    {
        public TreatmentDAL CreateTreatmentDAL()
        {
            return new TreatmentDAL();
        }
    }
}
