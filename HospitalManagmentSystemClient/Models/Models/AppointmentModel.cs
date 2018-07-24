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
   public class AppointmentModel
    {
        public int appointment_ID { get; set; }

        public int paitent_ID { get; set; }
        public bool requestedReport { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime appointment_Date { get; set; }

        public string timings { get; set; }

        public int doctor_ID { get; set; }

        public DateTime date { get; set; }

        public string doctorName { get; set; }

        public string patientName { get; set; }

        public List<string> doctors { get; set; }

        public List<int> doctorIDs { get; set; }

        public List<DoctorModel> doctorDetails { get; set; }

        public List<DoctorDropDown> doctorDropDown { get; set; }

        public List<SelectListItem> dropDown { get; set; }

        public string reqReportNotes { get; set; }

        public bool reportAvailable { get; set; }

        public bool canCancel { get; set; }

        public bool cancelled { get; set; }

       public List<SelectListItem> filterApps { get; set; }
        public int filterID { get; set; }

        public AppointmentModel(int appointment_ID, int patient_ID, DateTime appointment_Date,
            string timings, int doctor_ID, DateTime date, string doctorName, bool requestedReport,string reqReportNotes)
        {
            this.appointment_ID = appointment_ID;
            this.paitent_ID = patient_ID;
            this.appointment_Date = appointment_Date;
            this.timings = timings;
            this.doctor_ID = doctor_ID;
            this.date = date;
            this.doctorName = doctorName;
            this.requestedReport = requestedReport;
            this.reqReportNotes = reqReportNotes;
        }

        public AppointmentModel()
        {
            doctorDetails = new List<DoctorModel>();
            dropDown = new List<SelectListItem>();
            filterApps = new List<SelectListItem>();
        }

        public List<int> PatientList { get; set; }

    }

    public class DoctorDropDown
    {
        public string Text { get; set; }
        public int Value { get; set; }
        public List<SelectList> Docdetails { get; set; }
    }
}
