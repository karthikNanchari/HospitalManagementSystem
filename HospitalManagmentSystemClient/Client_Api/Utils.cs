using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;

namespace Client_Api
{
    public static class Utils
    {
        public static string Url() {

            return "http://localhost:9635/api/";
        }

        public static void Logging(Exception ex, int? level)
        {
            Log.Logger = new LoggerConfiguration()
            .WriteTo.Seq("http://localhost:5341/")
            //.WriteTo.Trace(outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss }[{Level}][{SourceContext:l}]{Message}{NewLine}{Exception}")
            .CreateLogger();
            if (level == 2)
            {
                Log.Fatal(ex.StackTrace);
            }
            else if (level == null)
            {
                Log.Debug("Successfully passed action");
            }
            else if(level == 1)
            {
                Log.Warning("Warnig Test");
            }
            
            Log.CloseAndFlush();
        }

        public static void Logging(string text, int? level)
        {
            Log.Logger = new LoggerConfiguration()
            //.WriteTo.RollingFile("log-{Date}.txt", Serilog.Events.LogEventLevel.Information)
            .WriteTo.Seq("http://localhost:5341/")
            .CreateLogger();
            if (level == null)
            {
                Log.Information(text);
            }
            if (level == 1)
            {
                Log.Warning(text);
            }
            Log.CloseAndFlush();
        }


       // public static ILogger Logger { get; set; }

        
    }
}
