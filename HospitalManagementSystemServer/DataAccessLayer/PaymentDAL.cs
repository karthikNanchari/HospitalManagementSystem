using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject;

namespace DataAccessLayer
{
    /*Payment data Access layer, using Linq to Sql for performing Create, update, delete and insert operations*/
    public class PaymentDAL
    {

        public void InsertPaymentAfterBillGeneration(AccountBO accBO)
        {
            try
            {
                using (HospitalManagementSystemDataContext objHmsDataContext = new HospitalManagementSystemDataContext(Utils.ConnectionString))
                {
                    if (objHmsDataContext.Connection.State == System.Data.ConnectionState.Closed)
                        objHmsDataContext.Connection.Open();

                    objHmsDataContext.Payments.InsertOnSubmit(new Payment {
                        Patient_ID = accBO.patient_ID,
                        Hospital_Fee = Convert.ToDecimal(accBO.hospital_Fee),
                        Medicines_Fee = Convert.ToDecimal(accBO.medicines_Fee),
                        Room_Fee = accBO.room_Fee != 0 ? Convert.ToDecimal(accBO.room_Fee) :0,
                        Total_Amount = Convert.ToDecimal(accBO.hospital_Fee) + Convert.ToDecimal(accBO.medicines_Fee),
                        Paid_Amount = 0,
                        Date_Time = DateTime.Now,
                        Bill_ID = accBO.bill_ID
                    });
                    objHmsDataContext.SubmitChanges();
                }

            }
            catch (Exception ex)
            {

            }

        }
        //Method to record new payment,with return type payment Business object
        public PaymentBO CreateNewPayment(PaymentBO paymentBO)
        {
            try
            {
                using (HospitalManagementSystemDataContext objHmsDataContext = new HospitalManagementSystemDataContext(Utils.ConnectionString))
                {
                    if (objHmsDataContext.Connection.State == System.Data.ConnectionState.Closed)
                        objHmsDataContext.Connection.Open();

                    Payment payment = new Payment();

                    Payment updatedPayment = ConvertBOToPayment(payment, paymentBO);

                    objHmsDataContext.Payments.InsertOnSubmit(updatedPayment);

                    objHmsDataContext.SubmitChanges();

                    Payment newPayment = objHmsDataContext.Payments.SingleOrDefault(new_payment => (new_payment.Patient_ID == paymentBO.patient_ID || new_payment.Date_Time == paymentBO.date));

                    PaymentBO payment_BO = ConvertPaymentToBO(newPayment);

                    return payment_BO;
                }
            }
            catch (Exception e)
            {
                PaymentBO payment_BO = new PaymentBO();

                return payment_BO;
            }
        }

        //Method to update Payment Record,with string return type 
        public string UpdatePayment(PaymentBO paymentBO)
        {
            try
            {
                using (HospitalManagementSystemDataContext objHmsDataContext = new HospitalManagementSystemDataContext(Utils.ConnectionString))
                {
                    if (objHmsDataContext.Connection.State == System.Data.ConnectionState.Closed)
                        objHmsDataContext.Connection.Open();

                    Payment payment = objHmsDataContext.Payments.SingleOrDefault(pymts => (pymts.Payment_ID == paymentBO.payment_ID));

                    Payment updatedPayment = ConvertBOToPayment(payment, paymentBO);

                    objHmsDataContext.SubmitChanges();

                    return "Succesfully changes submitted";
                }
            }
            catch (Exception e)
            {
                return "Unable to submit changes please try again later";
            }
        }

        //Method to get Payment Details, with payment business object as return type
        public PaymentBO GetPayment(PaymentBO paymentBO)
        {
            try
            {
                using (HospitalManagementSystemDataContext objHmsDataContext = new HospitalManagementSystemDataContext(Utils.ConnectionString))
                {
                    if (objHmsDataContext.Connection.State == System.Data.ConnectionState.Closed)
                        objHmsDataContext.Connection.Open();

                    Payment payment = objHmsDataContext.Payments.SingleOrDefault(pymts => (pymts.Payment_ID == paymentBO.payment_ID));

                    PaymentBO payment_BO = ConvertPaymentToBO(payment);

                    return payment_BO;
                }
            }
            catch (Exception e)
            {
                PaymentBO payment_BO = new PaymentBO();

                return payment_BO;
            }
        }

        //Method to convert Payment to PaymentBO, with payment Business object as return type
        public PaymentBO ConvertPaymentToBO(Payment payment)
        {
            PaymentBO paymentBO = new PaymentBO(payment.Patient_ID, payment.Date_Time, payment.Payment_ID,
                                                    (double)payment.Hospital_Fee, (double)payment.Medicines_Fee, (double)payment.Total_Amount, (double)payment.Paid_Amount, payment.Bill_ID, (double)payment.Room_Fee);


            return paymentBO;
        }

        //Method to convert PaymentBO to Payment
        public Payment ConvertBOToPayment(Payment payment, PaymentBO paymentBO)
        {

            if (paymentBO.payment_ID != 0)
            {
                payment.Payment_ID = paymentBO.payment_ID;
            }

            if (paymentBO.patient_ID != 0)
            {
                payment.Patient_ID = paymentBO.patient_ID;

            }

            if (paymentBO.hospital_Fee != 0)
            {
                payment.Hospital_Fee = (decimal)paymentBO.hospital_Fee;
            }

            if (paymentBO.medicines_Fee != 0)
            {
                payment.Medicines_Fee = (decimal)paymentBO.medicines_Fee;
            }

            if (paymentBO.total_Amount != 0)
            {
                payment.Total_Amount = (decimal)paymentBO.total_Amount;
            }

            if (paymentBO.paid_Amount != 0)
            {
                payment.Paid_Amount = (decimal)paymentBO.paid_Amount;
            }

            if (paymentBO.date != DateTime.MinValue)
            {
                payment.Date_Time = paymentBO.date;
            }

            return payment;
        }

        //Method to delete Payment
        public string DeletePayment(PaymentBO paymentBO)
        {
            try
            {
                using (HospitalManagementSystemDataContext objHmsDataContext = new HospitalManagementSystemDataContext(Utils.ConnectionString))
                {
                    if (objHmsDataContext.Connection.State == System.Data.ConnectionState.Closed)
                        objHmsDataContext.Connection.Open();

                    Payment payment = objHmsDataContext.Payments.SingleOrDefault(pymnts => (pymnts.Payment_ID == paymentBO.payment_ID));

                    Payment delPayment = ConvertBOToPayment(payment, paymentBO);

                    objHmsDataContext.Payments.DeleteOnSubmit(delPayment);

                    objHmsDataContext.SubmitChanges();

                    return "successfully payment deleted";

                }
            }
            catch (Exception e)
            {
                return "unable to delete payment please try again later";
            }
        }


        public IEnumerable<PaymentBO> GetPatientPayments(int patientID)
        {
            try
            {
                using (HospitalManagementSystemDataContext objHmsDataContext = new HospitalManagementSystemDataContext(Utils.ConnectionString))
                {
                    if (objHmsDataContext.Connection.State == System.Data.ConnectionState.Closed)
                        objHmsDataContext.Connection.Open();

                    IEnumerable<PaymentBO> payment_BO = objHmsDataContext.Payments
                        .Where(pymts => pymts.Patient_ID == patientID)
                        .Join(objHmsDataContext.Accounts,
                        p => p.Bill_ID,
                        a => a.Bill_ID,
                        (p,a) => new PaymentBO {
                            payment_ID = p.Payment_ID,
                            patient_ID = p.Patient_ID,
                            bill_ID = p.Bill_ID,
                            medicines_Fee = Convert.ToDouble(p.Medicines_Fee),
                            hospital_Fee = Convert.ToDouble(p.Hospital_Fee),
                            room_Fee = p.Room_Fee != null? Convert.ToDouble(p.Room_Fee) :0,  
                            paid_Amount = Convert.ToDouble(p.Paid_Amount),
                            total_Amount = Convert.ToDouble(p.Total_Amount),
                            date = p.Date_Time,
                            appointmentID = a.Appointment_ID
                        }).ToArray();

                    foreach(var p in payment_BO)
                    {
                        var room = objHmsDataContext.TreatmentRoom_Records.Where(a => a.Appointment_ID == p.appointmentID)
                                                             .Select(t => new TreatmentRoomBO
                                                             {
                                                                 isBooked =  t.IsBooked ,
                                                                  room_ID = t.Room_ID,
                                                                  DateOfJoin = t.DateOfJoin,
                                                                 RecordID = t.Record_ID
                                                             }).OrderByDescending(r =>r.RecordID);
                        //var room = objHmsDataContext.TreatmentRoom_Records.Where(a => a.Appointment_ID == p.appointmentID).Select(t => t.Room_ID).LastOrDefault();
                        if (room.Count() >0)
                        {
                            p.roomID = room.FirstOrDefault().isBooked != false ? room.First().room_ID : "N/A";
                        }
                    }

                    return payment_BO;
                }
            }
            catch (Exception e)
            {
                IEnumerable<PaymentBO> payment_BO = null;

                return payment_BO;
            }
        }

        public IEnumerable<PaymentBO> PatientPayPayments(PaymentBO paymentBO)
        {
            try
            {
                using (HospitalManagementSystemDataContext objHmsDataContext = new HospitalManagementSystemDataContext(Utils.ConnectionString))
                {
                    if (objHmsDataContext.Connection.State == System.Data.ConnectionState.Closed)
                        objHmsDataContext.Connection.Open();
                    var p = GetPatientPayments(paymentBO.patient_ID).Last().paid_Amount;
                   
                    InsertPatientPayments(paymentBO);
                    //paymentBO.paid_Amount = paymentBO.paid_Amount + p;
                    var pp = GetPatientPayments(paymentBO.patient_ID);
                    var paidTotal = pp.Where(d => d.bill_ID == paymentBO.bill_ID).Select(h => h.paid_Amount).ToList().Sum();
                    AccountDAL acc = new AccountDAL();
                    AccountBO accBO = new AccountBO();
                    accBO.patient_ID = paymentBO.patient_ID;
                    accBO.bill_ID = paymentBO.bill_ID;
                    accBO.paid_Amount = paidTotal;
                    accBO.generatedDate_Time = DateTime.Now;
                    acc.UpdateAccountDetails_DAL(accBO);
                    return pp;
                }
            }
            catch (Exception e)
            {
                IEnumerable<PaymentBO> payment_BO = null;

                return payment_BO;
            }
        }

        public void InsertPatientPayments(PaymentBO paymentBO)
        {
            try
            {
                using (HospitalManagementSystemDataContext objHmsDataContext = new HospitalManagementSystemDataContext(Utils.ConnectionString))
                {
                    if (objHmsDataContext.Connection.State == System.Data.ConnectionState.Closed)
                        objHmsDataContext.Connection.Open();

                    objHmsDataContext.Payments.InsertOnSubmit(new Payment
                    {
                        Patient_ID = paymentBO.patient_ID,
                        Total_Amount = Convert.ToDecimal(paymentBO.total_Amount),
                        Hospital_Fee = Convert.ToDecimal(paymentBO.hospital_Fee),
                        Medicines_Fee = Convert.ToDecimal(paymentBO.medicines_Fee),
                        Room_Fee = paymentBO.room_Fee != 0 ? Convert.ToDecimal(paymentBO.room_Fee) : 0,
                        Paid_Amount = Convert.ToDecimal(paymentBO.paid_Amount),
                        Date_Time = DateTime.Now,
                        Bill_ID = paymentBO.bill_ID
                    });
                    objHmsDataContext.SubmitChanges();
                    //return GetPatientPayments(paymentBO.patient_ID);
                }
            }
            catch (Exception e)
            {
               // IEnumerable<PaymentBO> payment_BO = null;

                //return payment_BO;
            }
        }


        public IEnumerable<PaymentBO> GetAllPayments()
        {
            try
            {
                using (HospitalManagementSystemDataContext objHmsDataContext = new HospitalManagementSystemDataContext(Utils.ConnectionString))
                {
                    if (objHmsDataContext.Connection.State == System.Data.ConnectionState.Closed)
                        objHmsDataContext.Connection.Open();

                    IEnumerable<PaymentBO> payment_BO = objHmsDataContext.Payments
                        .Join(objHmsDataContext.Accounts,
                        p => p.Bill_ID,
                        a => a.Bill_ID,
                        (p, a) => new PaymentBO
                        {
                            payment_ID = p.Payment_ID,
                            patient_ID = p.Patient_ID,
                            bill_ID = p.Bill_ID,
                            medicines_Fee = Convert.ToDouble(p.Medicines_Fee),
                            hospital_Fee = Convert.ToDouble(p.Hospital_Fee),
                            room_Fee = p.Room_Fee != null ? Convert.ToDouble(p.Room_Fee) : 0,
                            paid_Amount = Convert.ToDouble(p.Paid_Amount),
                            total_Amount = Convert.ToDouble(p.Total_Amount),
                            date = p.Date_Time,
                            appointmentID = a.Appointment_ID
                        }).ToArray();

                    foreach (var p in payment_BO)
                    {
                        var room = objHmsDataContext.TreatmentRoom_Records.Where(a => a.Appointment_ID == p.appointmentID).Select(t => t.Room_ID).FirstOrDefault();
                        p.roomID = room != null ? room : "N/A";
                    }

                    return payment_BO;
                }
            }
            catch (Exception e)
            {
                IEnumerable<PaymentBO> payment_BO = null;

                return payment_BO;
            }
        }
    }
}
