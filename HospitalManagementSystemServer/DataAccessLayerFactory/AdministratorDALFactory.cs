using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayerFactory
{
    /// <summary>
    /// Administrator Data access layer implemented as a part of facatory design pattern
    /// </summary>
  public  class AdministratorDALFactory
    {
        public AdministratorDAL CreateAdminstratorDAL()
        {
            return new AdministratorDAL();
        }

        public NotificationsDAL ManageNotificationDAL()
        {
            return new NotificationsDAL();
        }

        public EmailDAL ManageEmailsDAL()
        {
            return new EmailDAL();
        }
    }
}
