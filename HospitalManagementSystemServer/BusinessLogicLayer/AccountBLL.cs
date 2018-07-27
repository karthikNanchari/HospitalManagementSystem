using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayerFactory;
namespace BusinessLogicLayer
{
    public class AccountBLL
    {
        //Method to get AccountDetails , with return type AccountDalFactory
        public AccountDALFactory GetAccountDetails()
        {
            return new AccountDALFactory();
        }

        //Method to update Account Details , with return type AccountDalFactory
        public AccountDALFactory UpdateAccountDetails()
        {
            return new AccountDALFactory();
        }

        //Method to Delete Account Details , with return type AccountDalFactory
        public AccountDALFactory DeleteAccountDetails()
        {
            return new AccountDALFactory();
        }

        //Method tp Create Account Details , with return type AccountDalFactory
        public AccountDALFactory CreateAccountDetails()
        {
            return new AccountDALFactory();
        }

    }
}
