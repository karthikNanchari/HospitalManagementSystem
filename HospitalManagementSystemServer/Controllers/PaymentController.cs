using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BusinessObject;
using BusinessLogicLayerFactory;

namespace HospitalManagementSystemServer.Controllers
{
    /// <summary>
    /// Payment controller, accepts rest service calls 
    /// </summary>
    public class PaymentController : ApiController
    {

        // GET: api/Payment/5
        //Get action which accepts int as parameter and payment business object as return type
        public PaymentBO Get(int id)
        {
            PaymentBO paymentBO = new PaymentBO();

            paymentBO.payment_ID = id;

            PaymentBLLFactory paymentBLLFactory = new PaymentBLLFactory();

            PaymentBO payment_BO = paymentBLLFactory.CreatePaymentBLL().GetPaymentBLL().CreatePaymentDAL().GetPayment(paymentBO);

            return payment_BO;

        }

        // POST: api/Payment
        //Post action which accepts payment business object with void return type
        public void Post([FromBody]PaymentBO paymentBO)
        {
            PaymentBLLFactory paymentBLLFactory = new PaymentBLLFactory();

            PaymentBO payment_BO = new PaymentBO();
            payment_BO.patient_ID = 7002;
            payment_BO.hospital_Fee = 1234.32;
            payment_BO.medicines_Fee = 5646.21;
            payment_BO.paid_Amount = 1234.21;
            payment_BO.total_Amount = 4325.12;


            PaymentBO paymt_BO = paymentBLLFactory.CreatePaymentBLL().CreatePaymentBLL().CreatePaymentDAL().CreateNewPayment(payment_BO);

        }

        // PUT: api/Payment/5
        //Put action which accepts payment business object as input parameter and void return type
        public void Put(int id, [FromBody]PaymentBO paymentBO)
        {
            //sample objects are created to check functionality
            PaymentBO payment_BO = new PaymentBO();
            payment_BO.patient_ID = 7002;
            payment_BO.hospital_Fee = 1111.32;
            payment_BO.medicines_Fee = 1111.21;
            payment_BO.paid_Amount = 1111.21;
            payment_BO.total_Amount = 1111.12;

            PaymentBLLFactory paymentBLLFactory = new PaymentBLLFactory();

            string updatedPayment = paymentBLLFactory.CreatePaymentBLL().UpdatePaymentBLL().CreatePaymentDAL().UpdatePayment(payment_BO);

            //sample objects are created to check functionality
            PaymentBO paymt_BO = new PaymentBO();
            paymt_BO.patient_ID = 7002;
            paymt_BO.hospital_Fee = 1234.32;
            paymt_BO.medicines_Fee = 5646.21;
            paymt_BO.paid_Amount = 1234.21;
            paymt_BO.total_Amount = 4325.12;

            PaymentBO paymnt_BO = paymentBLLFactory.CreatePaymentBLL().CreatePaymentBLL().CreatePaymentDAL().CreateNewPayment(paymt_BO);


        }

        // DELETE: api/Payment/5
        //Delete action which accepts int as input parameter and void as return type
        public void Delete(int id)
        {
            PaymentBO paymt_BO = new PaymentBO();
            paymt_BO.patient_ID = 7002;
            paymt_BO.hospital_Fee = 1234.32;
            paymt_BO.medicines_Fee = 5646.21;
            paymt_BO.paid_Amount = 1234.21;
            paymt_BO.total_Amount = 4325.12;
            paymt_BO.payment_ID = id;

            PaymentBLLFactory paymentBLLFactory = new PaymentBLLFactory();
            string deletePayment = paymentBLLFactory.CreatePaymentBLL().DeletePaymentBLL().CreatePaymentDAL().DeletePayment(paymt_BO);

        }
    }
}
