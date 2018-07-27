using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject
{
    /*Business object Treatment with properties, room Id, timings
     */
    public class TreatmentRoomBO
    {
        public string room_ID { get; set; }

        public int appointment_ID { get; set; }

        public string timings { get; set; }
        public TimeSpan Timings { get; set; }

        public DateTime date { get; set; }
        public DateTime DateOfJoin { get; set; }

        public bool isBooked { get; set; }

        public int patient_ID { get; set; }

        public string PatientFirstName { get; set; }

        public int RecordID { get; set; }
        public TreatmentRoomBO(string room_ID, string timings, DateTime date, bool isBooked, int patient_ID, string PatientFirstName)
        {
            this.room_ID = room_ID;
            this.timings = timings;
            this.date = date;
            this.isBooked = isBooked;
            this.patient_ID = patient_ID;
            this.PatientFirstName = PatientFirstName;
        }
        public TreatmentRoomBO(string room_ID, string timings, DateTime date, bool isBooked, int patient_ID)
        {
            this.room_ID = room_ID;
            this.timings = timings;
            this.date = date;
            this.isBooked = isBooked;
            this.patient_ID = patient_ID;
        }

        public TreatmentRoomBO()
        {
        }
    }
}
