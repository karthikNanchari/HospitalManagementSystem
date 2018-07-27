using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject
{
    /*Business Object to maintain Appointment details like,
     appointment id, patient id, appointment date, timings, doctor id, date*/
    public class AppointmentBO //: IEnumerable
    {
        public int appointment_ID { get; set; }

        public int paitent_ID { get; set; }

        public DateTime appointment_Date { get; set; }

        public bool requestedReport { get; set; }

        public TimeSpan timings { get; set; }

        public int doctor_ID { get; set; }

        public DateTime date { get; set; }

        public string doctorName { get; set; }

        public string patientName { get; set; }

        public string reqReportNotes { get; set; }
        public bool cancelled { get; set; }

        public AppointmentBO(int appointment_ID, int patient_ID, DateTime appointment_Date,
            TimeSpan timings, int doctor_ID, DateTime date, bool requestedReport,string reqReportNotes)

        {
            this.appointment_ID = appointment_ID;
            this.paitent_ID = patient_ID;
            this.appointment_Date = appointment_Date;
            this.timings = timings;
            this.doctor_ID = doctor_ID;
            this.date = date;
            this.requestedReport = requestedReport;
            this.reqReportNotes = reqReportNotes;
        }

        public AppointmentBO()
        { }
    }
}
