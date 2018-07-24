using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Hangfire;
using Hangfire.MemoryStorage;
using HospitalManagmentSystemClient.Controllers.Administrator;
using HospitalManagmentSystemClient.Controllers;
using Models;
using System.Web;

[assembly: OwinStartup(typeof(HospitalManagmentSystemClient.Startup))]

namespace HospitalManagmentSystemClient
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            GlobalConfiguration.Configuration.UseMemoryStorage();

           // app.UseHangfireDashboard();
           // EmailsController email = new EmailsController();
           // //services.AddHangfire(x => x.Use);
           // BackgroundJob.Enqueue(() => email.SendEmail());
           //// RecurringJob.AddOrUpdate(() => Console.WriteLine("Test Hangfire"), Cron.Minutely);
           // RecurringJob.AddOrUpdate(() => email.EmailToPatients(), Cron.MinuteInterval(5));
           // app.UseHangfireServer();

        }
    }
}
