using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject
{
    public class TreatmentRoomRecordsBO
    {
        public string room_ID { get; set; }

        public int appointment_ID { get; set; }

        public string timings { get; set; }

        public DateTime date { get; set; }
        public DateTime DateOfJoin { get; set; }

        public bool isBooked { get; set; }

        public int patient_ID { get; set; }

        public string PatientFirstName { get; set; }

        public TreatmentRoomRecordsBO(string room_ID, int appointment_ID, string timings, DateTime date, bool isBooked, int patient_ID, string PatientFirstName)
        {
            this.room_ID = room_ID;
            this.timings = timings;
            this.date = date;
            this.isBooked = isBooked;
            this.patient_ID = patient_ID;
            this.PatientFirstName = PatientFirstName;
            this.appointment_ID = appointment_ID;
        }
        public TreatmentRoomRecordsBO(string room_ID, string timings, DateTime date, bool isBooked, int patient_ID)
        {
            this.room_ID = room_ID;
            this.timings = timings;
            this.date = date;
            this.isBooked = isBooked;
            this.patient_ID = patient_ID;
        }

        public TreatmentRoomRecordsBO()
        {
        }
    }
}
