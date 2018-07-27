using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject;


namespace DataAccessLayer
{
   public class NotificationsDAL
    {
        public IEnumerable<NotificationsBO> ManageNotifications_DAL(NotificationsBO manageNotifications)
        {
            try
            {
                HospitalManagementSystemDataContext ObjHmsDataContext = new HospitalManagementSystemDataContext(Utils.ConnectionString);
                if (ObjHmsDataContext.Connection.State == System.Data.ConnectionState.Closed)
                    ObjHmsDataContext.Connection.Open();

                var Notifications = ObjHmsDataContext.Notifications.SingleOrDefault(n => (n.Id == manageNotifications.ID));
               Notification newNotification = ConvertBOToNotifi(Notifications, manageNotifications);
                ObjHmsDataContext.SubmitChanges();

                return GetAllNotifications_DAL( manageNotifications);
            }
            catch
            {
                return null;
            }
        }

        public IEnumerable<NotificationsBO> GetAllNotifications_DAL(NotificationsBO manageNotifications)
        {
            try
            {
                HospitalManagementSystemDataContext ObjHmsDataContext = new HospitalManagementSystemDataContext(Utils.ConnectionString);
                if (ObjHmsDataContext.Connection.State == System.Data.ConnectionState.Closed)
                    ObjHmsDataContext.Connection.Open();

                var date7Days = DateTime.Now.AddDays(-7);
                var Notifications = ObjHmsDataContext.Notifications.Where(n => (n.Admin_ID == manageNotifications.AdminID
                 && n.Created_DateTime >= date7Days)).Select(noti => new NotificationsBO
                {
                     ID = noti.Id,
                AdminID = noti.Admin_ID,
                UserRoleID = noti.UserRole_ID,
                Notifications = noti.Notification1,
                Created_DateTime = noti.Created_DateTime,
                Expiry = noti.Expires_DateTime
                }).ToArray() ;

                return Notifications;
            }
            catch
            {
                return null;
            }
        }

        public IEnumerable<NotificationsBO> InsertNotifications_DAL(NotificationsBO newNotifications)
        {
            try
            {
                if (newNotifications.Notifications != null)
                {
                    HospitalManagementSystemDataContext ObjHmsDataContext = new HospitalManagementSystemDataContext(Utils.ConnectionString);
                    if (ObjHmsDataContext.Connection.State == System.Data.ConnectionState.Closed)
                        ObjHmsDataContext.Connection.Open();

                    Notification noti = new Notification();
                    Notification newNoti = ConvertBOToNotifi(noti, newNotifications);
                    ObjHmsDataContext.Notifications.InsertOnSubmit(newNoti);
                    ObjHmsDataContext.SubmitChanges();

                }
                return GetAllNotifications_DAL(newNotifications);
            }
            catch
            {
                return null;
            }
        }

        public IEnumerable<NotificationsBO> DeleteNotifications_DAL(NotificationsBO deleteNotifications)
        {
            try
            {
                HospitalManagementSystemDataContext ObjHmsDataContext = new HospitalManagementSystemDataContext(Utils.ConnectionString);
                if (ObjHmsDataContext.Connection.State == System.Data.ConnectionState.Closed)
                    ObjHmsDataContext.Connection.Open();

                Notification notifi =  ObjHmsDataContext.Notifications.SingleOrDefault(n => n.Id == deleteNotifications.ID);

                ObjHmsDataContext.Notifications.DeleteOnSubmit(notifi);
                ObjHmsDataContext.SubmitChanges();

                return GetAllNotifications_DAL(deleteNotifications);
            }
            catch
            {
                return null;
            }
        }

        public Notification ConvertBOToNotifi(Notification notifiaction, NotificationsBO notificationsBO)
        {
            if (notificationsBO != null)
            {
                if(notificationsBO.ID != 0)
                {
                    notifiaction.Id = notificationsBO.ID;
                }
                if (notificationsBO.AdminID != 0)
                {
                    notifiaction.Admin_ID = notificationsBO.AdminID;
                }
                if (notificationsBO.UserRoleID != 0)
                {
                    notifiaction.UserRole_ID = notificationsBO.UserRoleID;
                }
                if (notificationsBO.Notifications != null)
                {
                    notifiaction.Notification1 = notificationsBO.Notifications;
                }
                if (notificationsBO.Created_DateTime != null)
                {
                    notifiaction.Created_DateTime = notificationsBO.Created_DateTime;
                }
                if (notificationsBO.Expiry != null)
                {
                    notifiaction.Expires_DateTime = notificationsBO.Expiry;
                }
            }

            return notifiaction;
        }

        public IEnumerable<Person> GetAllUsers()
        {
            try
            {
                HospitalManagementSystemDataContext ObjHmsDataContext = new HospitalManagementSystemDataContext(Utils.ConnectionString);
                if (ObjHmsDataContext.Connection.State == System.Data.ConnectionState.Closed)
                    ObjHmsDataContext.Connection.Open();

                List<Person> personDetails = ObjHmsDataContext.Patients.Select(p => new Person
                {
                    pid = p.Patient_ID,
                    firstName = p.First_Name,

                }).ToList();

                List<Person> doctorDetails = ObjHmsDataContext.Doctors.Select(d => new Person
                {
                    pid = d.Doctor_ID,
                    firstName = d.First_Name
                }).ToList();
                foreach (var d in doctorDetails)
                { personDetails.Add(d); }

                List<Person> nurseDetails = ObjHmsDataContext.Nurses.Select(n => new Person
                {
                    pid = n.Nurse_ID,
                    firstName = n.First_Name
                }).ToList();

                foreach(var n in nurseDetails)
                {
                    personDetails.Add(n);
                }

                List<Person> inchargeDetails = ObjHmsDataContext.LabIncharges.Select(l => new Person
                {
                    pid = l.LabIncharge_ID,
                    firstName = l.First_Name
                }).ToList(); 
                foreach(var l in inchargeDetails)
                {
                    personDetails.Add(l);
                }
                return null;
            }
            catch
            {
                return null;
            }
        }

        //public NotificationsBO AddUserNameToNotification(NotificationsBO notiBO)
        //{
        //    if (notiBO.UserRoleID >4000 && notiBO.UserRoleID <4999)
        //    {
        //        NotificationsBO noti_BO = 
        //    }

        //}


        public IEnumerable<NotificationsBO> GetAllNotificationsByRole_DAL(NotificationsBO Noti)
        {
            try
            {
                HospitalManagementSystemDataContext ObjHmsDataContext = new HospitalManagementSystemDataContext(Utils.ConnectionString);
                if (ObjHmsDataContext.Connection.State == System.Data.ConnectionState.Closed)
                    ObjHmsDataContext.Connection.Open();

                var date7Days = DateTime.Now.AddDays(-7);

                List<NotificationsBO> Notifications = null;

                if (Noti.UserRoleID >= 3000 && Noti.UserRoleID < 3999)
                {
                    Notifications = ObjHmsDataContext.Notifications.Where(n => (n.Created_DateTime >= date7Days && n.UserRole_ID == Noti.UserRoleID))
                        .Join(ObjHmsDataContext.Nurses,
                        noti => noti.UserRole_ID,
                        nur => nur.Nurse_ID,
                        (noti, nur) => new NotificationsBO
                        {
                            ID = noti.Id,
                            AdminID = noti.Admin_ID,
                            UserRoleID = noti.UserRole_ID,
                            Notifications = noti.Notification1,
                            Created_DateTime = noti.Created_DateTime,
                            Expiry = noti.Expires_DateTime,
                            UserName = nur.First_Name
                        }).ToList();
                    //   Notifications = ObjHmsDataContext.Notifications.Where(n => (n.Created_DateTime >= date7Days))
                    //       .Join(ObjHmsDataContext.Nurses,  noti => noti.UserRole_ID, nur => nur.Nurse_ID,(noti, nur) => new {noti, nur })
                    //       .Join(ObjHmsDataContext.Administrators, no => no.noti.Admin_ID, ad => ad.Admin_ID, (no, ad) => new { no,ad})
                    //       .Select( m => new NotificationsBO{
                    //        ID = m.no.noti.Id,
                    //    AdminID = m.no.noti.Admin_ID,
                    //    UserRoleID = m.no.noti.UserRole_ID,
                    //    Notifications = m.no.noti.Notification1,
                    //    Created_DateTime = m.no.noti.Created_DateTime,
                    //    Expiry = m.no.noti.Expires_DateTime,
                    //    UserName = m.no.nur.First_Name,
                    //    AdminName = m.a.First_Name
                    //}).ToList();
                }
                if (Noti.UserRoleID >= 5000 && Noti.UserRoleID < 5999)
                {
                    Notifications = ObjHmsDataContext.Notifications.Where(n => (n.Created_DateTime >= date7Days && n.UserRole_ID == Noti.UserRoleID))
                        .Join(ObjHmsDataContext.Doctors,
                        noti => noti.UserRole_ID,
                        nur => nur.Doctor_ID,
                        (noti, nur) => new NotificationsBO
                        {
                            ID = noti.Id,
                            AdminID = noti.Admin_ID,
                            UserRoleID = noti.UserRole_ID,
                            Notifications = noti.Notification1,
                            Created_DateTime = noti.Created_DateTime,
                            Expiry = noti.Expires_DateTime,
                            UserName = nur.First_Name
                        }).ToList();
                }
                if (Noti.UserRoleID >= 7000 && Noti.UserRoleID < 7999)
                {
                    Notifications = ObjHmsDataContext.Notifications.Where(n => (n.Created_DateTime >= date7Days && n.UserRole_ID == Noti.UserRoleID))
                        .Join(ObjHmsDataContext.Patients,
                        noti => noti.UserRole_ID,
                        nur => nur.Patient_ID,
                        (noti, nur) => new NotificationsBO
                        {
                            ID = noti.Id,
                            AdminID = noti.Admin_ID,
                            UserRoleID = noti.UserRole_ID,
                            Notifications = noti.Notification1,
                            Created_DateTime = noti.Created_DateTime,
                            Expiry = noti.Expires_DateTime,
                            UserName = nur.First_Name
                        }).ToList();
                }
                if (Noti.UserRoleID >= 4000 && Noti.UserRoleID < 4999)
                {
                    Notifications = ObjHmsDataContext.Notifications.Where(n => (n.Created_DateTime >= date7Days && n.UserRole_ID == Noti.UserRoleID))
                        .Join(ObjHmsDataContext.LabIncharges,
                        noti => noti.UserRole_ID,
                        nur => nur.LabIncharge_ID,
                        (noti, nur) => new NotificationsBO
                        {
                            ID = noti.Id,
                            AdminID = noti.Admin_ID,
                            UserRoleID = noti.UserRole_ID,
                            Notifications = noti.Notification1,
                            Created_DateTime = noti.Created_DateTime,
                            Expiry = noti.Expires_DateTime,
                            UserName = nur.First_Name
                        }).ToList();
                }

                return Notifications;
            }
            catch
            {
                return null;
            }
        }

    }
}
