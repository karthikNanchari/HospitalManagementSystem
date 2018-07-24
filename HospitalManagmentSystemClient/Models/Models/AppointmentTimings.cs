using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
  public class AppointmentTimings
    {
        public List<string> timingsList;

        public List<timingsValues> timingsDropDown ;
        public AppointmentTimings()
        {
            timingsDropDown = new List<timingsValues>();
            timingsDropDown.Add(new timingsValues{ Value = "09:00" ,Text = "09:00" });
            timingsDropDown.Add(new timingsValues{ Value = "10:00" ,Text = "10:00" });
            timingsDropDown.Add(new timingsValues{ Value = "11:00" ,Text = "11:00" });
            timingsDropDown.Add(new timingsValues{ Value = "12:00" ,Text = "12:00" });
            timingsDropDown.Add(new timingsValues{ Value = "13:00" ,Text = "13:00" });
            timingsDropDown.Add(new timingsValues{ Value = "14:00" ,Text = "14:00" });
            timingsDropDown.Add(new timingsValues{ Value = "15:00" ,Text = "15:00" });
            timingsDropDown.Add(new timingsValues{ Value = "16:00" ,Text = "16:00" });
        }
    }

    public class timingsValues
    {
        public string Value { get; set; }
        public string Text { get; set; }
    }
}
