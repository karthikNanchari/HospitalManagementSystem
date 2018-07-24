using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
   public class TreatmentModel
    {
        public int treatment_ID { get; set; }

        public int patient_ID { get; set; }

        public int nurse_ID { get; set; }

        public int prescription_ID { get; set; }

        public int Room_ID { get; set; }

        public int doctor_ID { get; set; }

        public DateTime date_Time { get; set; }

        public string startTime { get; set; }

        public string endTime { get; set; }


        public TreatmentModel(int treatment_ID, int patient_ID, int nurse_ID,
            int prescription_ID, int Room_ID, int doctor_ID, string startTime, string endTime)
        {
        }

        public TreatmentModel() { }
    }
}
