using System;
using System.Web;
using System.Web.Mvc;
using System.Collections;
using System.Collections.Generic;

namespace Models
{
   public class NotificationsModel
    {
        public int ID { get; set; }
        public int AdminID { get; set; }
        public int UserRoleID { get; set; }
        public string Notifications { get; set; }
        public DateTime Created_DateTime { get; set; }
        public DateTime Expiry { get; set; }
        public string UserName { get; set; }

        public List<SelectListItem> personDetails { get; set; }
        public List<SelectListItem> notificationRoleDetails { get; set; }

        public NotificationsModel()
        {
            personDetails = new List<SelectListItem>();
            notificationRoleDetails = new List<SelectListItem>();
        }

    }
}
