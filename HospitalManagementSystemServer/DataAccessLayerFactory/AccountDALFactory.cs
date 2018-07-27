using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayerFactory
{
    /// <summary>
    /// Account DataAccess layer factory, implemented as a part of factory design pattern
    /// </summary>
  public  class AccountDALFactory
    {
        public AccountDAL CreateAccountDAL()
        {
            return new AccountDAL();
        }
    }
}
