using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayerFactory;

namespace BusinessLogicLayer
{
    public class PaymentBLL
    {
        //Method to Create Payment i.e to insert payment details, with return type Payment DalFactory
        public PaymentDALFactory CreatePaymentBLL()
        {
            return new PaymentDALFactory();
        }

        //Method to update Payment details, with return type payment Dal factory
        public PaymentDALFactory UpdatePaymentBLL()
        {
            return new PaymentDALFactory();
        }

        //Method to Get payment details, with return type Payment DAl Factory
        public PaymentDALFactory GetPaymentBLL()
        {
            return new PaymentDALFactory();
        }

        //Method to Delete payment details, with return type Payment DAl Factory
        public PaymentDALFactory DeletePaymentBLL()
        {
            return new PaymentDALFactory();
        }

    }
}
