using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;


namespace Models
{
    public class TreatmentRoomModel
    {
        public string room_ID { get; set; }

        public int appointment_ID { get; set; }

        public string timings { get; set; }

        public DateTime date { get; set; }

        public int patient_ID { get; set; }

        public bool isBooked { get; set; }

        public List<RoomValues> roomList { get; set; }

        public IEnumerable<int> patientList { get; set; }

        public List<SelectListItem> patientDropDown { get; set; }

        public List<SelectListItem> appoitmentDropDown { get; set; }

        public List<timingsValues> appointmentTimings { get; set; }

        public bool canEdit { get; set; }

        public string patientFirstName { get; set; }

        public TreatmentRoomModel(string room_ID, int appointment_ID,string timings, DateTime date,int patient_ID, bool isBooked, string patientFirstName)
        {
            this.room_ID = room_ID;
            this.timings = timings;
            this.date = date;
            this.patient_ID = patient_ID;
            this.isBooked = isBooked;
            this.patientFirstName = patientFirstName;
            this.appointment_ID = appointment_ID;
        }

        public TreatmentRoomModel()
        {
            patientDropDown = new List<SelectListItem>();
            roomList = new List<RoomValues>();
            roomList.Add(new RoomValues{ Value="R001", Text= "R001" });
            roomList.Add(new RoomValues{ Value="R002", Text= "R002" });
            roomList.Add(new RoomValues{ Value="R003", Text= "R003" });
            roomList.Add(new RoomValues{ Value="R004", Text= "R004" });
            roomList.Add(new RoomValues{ Value="R005", Text= "R005" });
            roomList.Add(new RoomValues{ Value="R006", Text= "R006" });
            roomList.Add(new RoomValues{ Value="R007", Text= "R007" });
            roomList.Add(new RoomValues{ Value="R008", Text= "R008" });
            roomList.Add(new RoomValues{ Value="R009", Text= "R009" });
            roomList.Add(new RoomValues{ Value="R010", Text= "R010" });
            appoitmentDropDown = new List<SelectListItem>();
        }
    }

    public class RoomValues
    {
        public string Value { get; set; }
        public string Text { get; set; }
    }
}
