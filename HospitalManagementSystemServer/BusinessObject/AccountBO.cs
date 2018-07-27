using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject
{
    /*Class which is used to operate on Account, with properties like
     Account Id, Patient Id, Payment Id, Total Amount, Paid Amount, date time*/
    public class AccountBO
    {
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

        public DateTime generatedDate_Time { get; set; }
        public DateTime paidDate_Time { get; set; }
        public string patientFirstName { get; set; }

        public AccountBO( int patient_ID, int bill_ID, int payment_ID, double total_Amount,
                 double paid_Amount, double medicines_Fee , double hospital_Fee, DateTime generatedDate_Time, string patientFirstName, int appointment_ID, string room_ID)

        {
            this.patient_ID = patient_ID;
            this.payment_ID = payment_ID;
            this.total_Amount = total_Amount;
            this.paid_Amount = paid_Amount;
            this.medicines_Fee = medicines_Fee;
            this.hospital_Fee = hospital_Fee;
            this.generatedDate_Time = generatedDate_Time;
            this.patientFirstName = patientFirstName;
            this.appointment_ID = appointment_ID;
            this.room_ID = room_ID;
        }


        public AccountBO(int patient_ID, int bill_ID, int payment_ID, double total_Amount,
                 double paid_Amount, double medicines_Fee, double hospital_Fee, DateTime generatedDate_Time,double room_Fee)

        {
            this.patient_ID = patient_ID;
            this.payment_ID = payment_ID;
            this.total_Amount = total_Amount;
            this.paid_Amount = paid_Amount;
            this.medicines_Fee = medicines_Fee;
            this.hospital_Fee = hospital_Fee;
            this.generatedDate_Time = generatedDate_Time;
            this.room_Fee = room_Fee;
        }

        public AccountBO()
        { }

    }
}
