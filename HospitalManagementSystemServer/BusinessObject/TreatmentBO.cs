using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject
{
    /*Business object Treatment,with properties treatment id, patient id, 
     * nurse id, prescription id, room id, doctor id, date time, start time, end time */
    public class TreatmentBO
    {

        public int treatment_ID { get; set; }

        public int patient_ID { get; set; }

        public int nurse_ID { get; set; }

        public int prescription_ID { get; set; }

        public string Room_ID { get; set; }

        public int doctor_ID { get; set; }

        public DateTime date_Time { get; set; }

        public string startTime { get; set; }

        public string endTime { get; set; }


        public TreatmentBO(int treatment_ID, int patient_ID, int nurse_ID,
            int prescription_ID, string Room_ID, int doctor_ID, string startTime, string endTime)
        {
        }

        public TreatmentBO()
        {
        }
    }
}
