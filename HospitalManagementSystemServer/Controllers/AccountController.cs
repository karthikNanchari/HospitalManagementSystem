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
    /// Account controller used to accepet Rest service calls from front end
    /// </summary>
    public class AccountController : ApiController
    {
        // GET: api/Account/5
        //Get Action to retrieve Account details, Account business object as return type and int as parameter
        public AccountBO Get(int id)
        {
            AccountBLLFactory accBLLFactory = new AccountBLLFactory();

            AccountBO acc_BO = new AccountBO();

            acc_BO.generatedDate_Time = DateTime.Parse("2222/10/10");
            acc_BO.paidDate_Time = DateTime.Parse("2222/10/10");
            acc_BO.patient_ID = 7002;
            acc_BO.total_Amount = 1234.26;
            acc_BO.paid_Amount = 1234.309;
            acc_BO.payment_ID = 3;
            acc_BO.account_ID = id;

            AccountBO account_BO = accBLLFactory.createAccountBLL().GetAccountDetails().CreateAccountDAL().GetAccountDetails_DAL(acc_BO);

            return account_BO;
        }

        // POST: api/Account
        // Post Action, accepts Account buisness object as parameter with void as return type 
        public void Post([FromBody]AccountBO accBO)
        {
            AccountBO acc_BO = new AccountBO();

            acc_BO.generatedDate_Time = DateTime.Parse("2222/10/10");
            acc_BO.paidDate_Time = DateTime.Parse("2222/10/10");
            acc_BO.patient_ID = 7002;
            acc_BO.payment_ID = 3;
            acc_BO.total_Amount = 1234.26;
            acc_BO.paid_Amount = 1234.309;


            AccountBLLFactory accBLLFactory = new AccountBLLFactory();

            AccountBO newAcc = accBLLFactory.createAccountBLL().CreateAccountDetails().CreateAccountDAL().InsertAccountDetails_DAL(acc_BO);

        }

        // PUT: api/Account/5
        //Put action accepts integer and account business object as parameter, void as return type
        public void Put(int id, [FromBody]AccountBO acc_BO)
        {
            AccountBO accBO = new AccountBO();
            acc_BO.generatedDate_Time = DateTime.Parse("2222/10/10");
            acc_BO.paidDate_Time = DateTime.Parse("2222/10/10");
            accBO.patient_ID = 7002;
            accBO.payment_ID = 3;
            accBO.total_Amount = 4321.26;
            accBO.paid_Amount = 4321.309;

            AccountBLLFactory accBLLFactory = new AccountBLLFactory();

            string updateAcc = accBLLFactory.createAccountBLL().UpdateAccountDetails().CreateAccountDAL().UpdateAccountDetails_DAL(accBO);
        }

        // DELETE: api/Account/5
        //Delete action, which accepts integer as parameter and void as return type
        public void Delete(int id)
        {
            AccountBO acc_BO = new AccountBO();

            acc_BO.generatedDate_Time = DateTime.Parse("2222/10/10");
            acc_BO.paidDate_Time = DateTime.Parse("2222/10/10");
            acc_BO.patient_ID = 7002;
            acc_BO.total_Amount = 4321.26;
            acc_BO.paid_Amount = 4321.309;
            acc_BO.payment_ID = 3;
            acc_BO.account_ID = 9005;
            AccountBLLFactory accBLLFactory = new AccountBLLFactory();

            string deleteAcc = accBLLFactory.createAccountBLL().DeleteAccountDetails().CreateAccountDAL().DeleteAccount_DAL(acc_BO);
        }
    }
}
