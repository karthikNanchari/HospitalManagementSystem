using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Models
{
    public class LoginModel
    {
        public int id { get; set; }
        public string pwd { get; set; }
        public string userRole { get; set; }
        public static string UserRole { get; set; }
    }
}
