using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web;
using System.Web.Mvc;

namespace Models
{
   public class ReportModel
    {
        public int report_ID { get; set; }

        public int appointment_ID { get; set; }

        public int labIncharge_ID { get; set; }

        public string labResult { get; set; }

        public string reportTime { get; set; }

        public int patient_ID { get; set; }

        public string patientFirstName { get; set; }

        public DateTime date { get; set; }

        public List<SelectListItem> dropDown { get; set; }

        public List<SelectListItem> appointmentIDs { get; set; }

        public List<int> appointmentIntIDs { get; set; }

        public string reportRequested { get; set; }

        public ReportModel(int report_ID, int labIncharge_ID, string labResult, string reportTime,
            int patient_ID, DateTime date, string patientFirstName)

        {
            this.report_ID = report_ID;
            this.labIncharge_ID = labIncharge_ID;
            this.labResult = labResult;
            this.reportTime = reportTime;
            this.patient_ID = patient_ID;
            this.date = date;
            this.patientFirstName = patientFirstName;
            dropDown = new List<SelectListItem>();
            appointmentIntIDs = new List<int>();
        }

        public ReportModel() {
            dropDown = new List<SelectListItem>();
            appointmentIDs = new List<SelectListItem>();
            appointmentIntIDs = new List<int>();
        }
    }
}
