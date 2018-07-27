using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayerFactory
{
    /// <summary>
    /// Treatment Room data access layer implemented as a part of factory design pattern
    /// </summary>
  public  class TreatmentRoomDALFactory
    {
        public TreatmentRoomDAL CreateTreatmentRoomDAL()
        {
            return new TreatmentRoomDAL(); 
        }
    }
}
