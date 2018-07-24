using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
namespace Models
{
    public class AccountModel
    {
        public static string test {get;set;}
        public int account_ID { get; set; }
        public int patient_ID { get; set; }
        public int bill_ID { get; set; }
        public int payment_ID { get; set; }
        public double total_Amount { get; set; }
        public double paid_Amount { get; set; }
        public double medicines_Fee { get; set; }
        public double hospital_Fee { get; set; }
        public double room_Fee { get; set; }

        public int appointment_ID { get; set; }
        public string room_ID { get; set; }
        public DateTime appointment_Date { get; set; }


        public DateTime generatedDate_Time { get; set; }
        public DateTime paidDate_Time { get; set; }
        public List<SelectListItem> patientdropDown { get; set; }
        public List<SelectListItem> AppointmentIDDropDown { get; set; }
        
        public string PatientFirstName { get; set; }


        public AccountModel(int account_ID, int patient_ID, int bill_ID,int payment_ID, double total_Amount,
                 double paid_Amount,double  medicines_Fee, double hospital_Fee, DateTime generatedDate_Time ,
                 DateTime paidDate_Time, string PatientFirstName, int appointment_ID, string room_ID)

        {
            this.account_ID = account_ID;
            this.patient_ID = patient_ID;
            this.bill_ID = bill_ID;
            this.payment_ID = payment_ID;
            this.total_Amount = total_Amount;
            this.paid_Amount = paid_Amount;
            this.medicines_Fee = medicines_Fee;
            this.hospital_Fee = hospital_Fee;
            this.generatedDate_Time = generatedDate_Time;
            this.paidDate_Time = paidDate_Time;
            this.PatientFirstName = PatientFirstName;
            this.appointment_ID = appointment_ID;
            this.room_ID = room_ID;
        }

        public AccountModel() {
            AppointmentIDDropDown = new List<SelectListItem>();
        }
    }

    //public class PatientModel
}
