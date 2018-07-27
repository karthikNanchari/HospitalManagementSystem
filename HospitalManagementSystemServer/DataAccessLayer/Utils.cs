using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    /// <summary>
    /// Utils class used to save connectionstring, so that it can be accessed 
    /// </summary>
    public static class Utils
    {
            public static string ConnectionString
        {
            get {
               // return @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\nkaru\OneDrive\Documents\Visual Studio 2017\Projects\HospitalManagementSystemServer\DataAccessLayer\HospitalManagementSystem.mdf;Integrated Security=True;user instance = false";
                return @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\nkaru\Desktop\Karthik\Project\Hospital Management System Server\HospitalManagementSystemServer\DataAccessLayer\HospitalManagementSystem.mdf;Integrated Security=True; user instance = false";
            }
        }
    }
}
