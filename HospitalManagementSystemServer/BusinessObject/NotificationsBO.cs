using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject
{
   public class NotificationsBO
    {
        public int ID { get; set; }
        public int  AdminID { get; set; }
        public int UserRoleID { get; set; }
        public string Notifications { get; set; }
        public DateTime  Created_DateTime { get; set; }
        public DateTime Expiry { get; set; }
        public string UserName { get; set; }
        public string AdminName { get; set; }
    }
}
