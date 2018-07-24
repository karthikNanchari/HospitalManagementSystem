using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using Serilog;
using System.Diagnostics;

namespace HospitalManagmentSystemClient
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public static object MessageBox { get; private set; }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Session_Start(Object sender, EventArgs e)
        {
            // HttpContext.Current.Session.Add("key","login");
           // HttpContext.Current.Session.Add("key","admin");
            // HttpContext.Current.Session.Add("key","patient");
           // HttpContext.Current.Session.Add("key","doctor");
            //HttpContext.Current.Session.Add("key","nurse");
            //HttpContext.Current.Session.Add("key", "labIncharge");
        }


        //static void Main()
        //{
        //    if (AnotherInstanceExists())
        //    {
        //        MessageBox.Show("You cannot run more than one instance of this application.", "Only one instance allowed to run", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        //        return;
        //    }
        //Form1 objMyForm = new Form1();

        //objMyForm.ShowDialog();
        //    objMyForm.ShowDialog();
        //}

        public static bool AnotherInstanceExists()
        {
            Process currentRunningProcess = Process.GetCurrentProcess();
            Process[] listOfProcs = Process.GetProcessesByName(currentRunningProcess.ProcessName);
            foreach (Process proc in listOfProcs)
            {
                if ((proc.MainModule.FileName == currentRunningProcess.MainModule.FileName) && (proc.Id != currentRunningProcess.Id))
                    return true;
            }
            return false;

        }
    }
}
