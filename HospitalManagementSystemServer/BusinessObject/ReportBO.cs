using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject
{
    /*Business Object to save report details like report id,
     * labincharge id, lab results, report, patient id, date*/

    public class ReportBO 
    {
        public int report_ID { get; set; }

        public int appointment_ID { get; set; }

        public int labIncharge_ID { get; set; }

        public string labResult { get; set; }

        public string reportTime { get; set; }

        public int patient_ID { get; set; }

        public DateTime date { get; set; }

        public string patientFirstName { get; set; }

        public string reportRequested { get; set; }

        public ReportBO(int report_ID, int labIncharge_ID ,string labResult, string reportTime,
            int patient_ID, DateTime date, int appointment_ID)
            
        {
            this.report_ID = report_ID;
            this.labIncharge_ID = labIncharge_ID;
            this.labResult = labResult;
            this.reportTime = reportTime;
            this.patient_ID = patient_ID;
            this.date = date;
            this.appointment_ID = appointment_ID;

        }

        public ReportBO()
        {
        }
    }
}
