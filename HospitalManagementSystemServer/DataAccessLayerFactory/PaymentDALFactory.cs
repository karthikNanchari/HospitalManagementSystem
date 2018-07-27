using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayerFactory
{
    /// <summary>
    /// Payment data access layer implemented as a part of factory design pattern 
    /// </summary>
  public  class PaymentDALFactory
    {
        public PaymentDAL CreatePaymentDAL()
        {
            return new PaymentDAL(); 
        }
    }
}
