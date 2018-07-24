﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Models
{
    public class PaymentModel
    {
        public int payment_ID { get; set; }
        public double hospital_Fee { get; set; }
        public double medicines_Fee { get; set; }
        public double total_Amount { get; set; }
        public double paid_Amount { get; set; }
        public double room_Fee { get; set; }
        public int patient_ID { get; set; }
        public int bill_ID { get; set; }
        public List<SelectListItem> billList { get; set; }
        public DateTime date { get; set; }
        public string roomID { get; set; } 

        public PaymentModel(int patient_ID,
              DateTime date, int payment_ID, double hospital_Fee,
             double medicines_Fee, double total_Amount, double paid_Amount, int bill_ID)
        {
            this.patient_ID = patient_ID;
            this.date = date;
            this.payment_ID = payment_ID;
            this.hospital_Fee = hospital_Fee;
            this.medicines_Fee = medicines_Fee;
            this.total_Amount = total_Amount;
            this.paid_Amount = paid_Amount;
            this.bill_ID = bill_ID;

        }

        public PaymentModel() {
            billList = new List<SelectListItem>();
        }
    }
}