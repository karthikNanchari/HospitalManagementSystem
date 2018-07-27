using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayerFactory
{
    /// <summary>
/// Patient Data access layer implemented as a part of Factory design pattern
/// </summary>
  public  class PatientDALFactory
    {
        public PatientDAL CreatePatientDAL()
        {
            return new PatientDAL();
        }
    }
}
