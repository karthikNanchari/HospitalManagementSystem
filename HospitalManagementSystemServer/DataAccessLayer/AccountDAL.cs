using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject;
namespace DataAccessLayer
{
    /*Account data access layer, using Linq to Sql,  operations are performed */
    public class AccountDAL
    {

        //Method to insert AccountDetails, with return type of Account Business object
        public AccountBO InsertAccountDetails_DAL(AccountBO accountBO)
        {
            try
            {
                using (HospitalManagementSystemDataContext objHmsDataContext = new HospitalManagementSystemDataContext(Utils.ConnectionString))
                {
                    if (objHmsDataContext.Connection.State == System.Data.ConnectionState.Closed)
                        objHmsDataContext.Connection.Open();

                    Account account = new Account();

                    //checks for booked room
                    var allroomsBooked = objHmsDataContext.TreatmentRoom_Records.Where(a => a.Appointment_ID == accountBO.appointment_ID).ToList();
                    if (allroomsBooked.Count() > 1)
                    {
                        var roomBl = objHmsDataContext.TreatmentRoom_Records.Where(a => a.Appointment_ID == accountBO.appointment_ID)
                            .Select(r => new TreatmentRoomRecordsBO{
                                room_ID = r.Room_ID,
                                appointment_ID = r.Appointment_ID,
                                date = r.DateOfJoin,
                                isBooked = r.IsBooked,
                                patient_ID = r.Patient_ID,
                                timings = Convert.ToString(r.Timings)
                            });
                        var roomBill = objHmsDataContext.TreatmentRoom_Records.Where(a => a.Appointment_ID == accountBO.appointment_ID)
                            .Select(r => new TreatmentRoomBO {  room_ID= r.Room_ID, isBooked = r.IsBooked , DateOfJoin = r.DateOfJoin}).OrderByDescending(r =>r.DateOfJoin);
                            //!= null ? 50 : 0;
                        accountBO.room_Fee = roomBill.First().isBooked ==false?0:50;
                        accountBO.total_Amount = accountBO.medicines_Fee + accountBO.hospital_Fee + accountBO.room_Fee;

                        Account updatedAccount = ConvertBOToAccount(account, accountBO);

                        objHmsDataContext.Accounts.InsertOnSubmit(updatedAccount);

                        objHmsDataContext.SubmitChanges();
                    }
                    else
                    {
                        var roomBill = objHmsDataContext.TreatmentRoom_Records.Where(a => a.Appointment_ID == accountBO.appointment_ID).Select(r => r.Room_ID).Count() != 0 ? 50 : 0;
                        accountBO.room_Fee = roomBill;
                        accountBO.total_Amount = accountBO.medicines_Fee + accountBO.hospital_Fee + accountBO.room_Fee;

                        Account updatedAccount = ConvertBOToAccount(account, accountBO);

                        objHmsDataContext.Accounts.InsertOnSubmit(updatedAccount);

                        objHmsDataContext.SubmitChanges();
                    }
                }

                AccountBO account_BO = GetAccountDetailsForBiiling_DAL(accountBO);

                PaymentDAL paymentDal = new PaymentDAL();
                paymentDal.InsertPaymentAfterBillGeneration(account_BO);
                return account_BO;
            }catch (Exception ex)
            {
                AccountBO accBo = new AccountBO();
                return accBo;
            }

        }

        //Method to get Payment by Patient, with return type of Account Business object
        public AccountBO GetAccountDetailsForBiiling_DAL(AccountBO accountBO)
        {
            try
            {
                using (HospitalManagementSystemDataContext objHmsDataContext = new HospitalManagementSystemDataContext(Utils.ConnectionString))
                {
                    if (objHmsDataContext.Connection.State == System.Data.ConnectionState.Closed)
                        objHmsDataContext.Connection.Open();

                    AccountBO accountDetails = objHmsDataContext.Accounts.Where(account => account.Patient_ID == accountBO.patient_ID
                                                && account.Generated_Date_Time == accountBO.generatedDate_Time && account.Total_Amount == (decimal)accountBO.total_Amount)
                                                .Select(a => new AccountBO {
                                                    bill_ID = a.Bill_ID,
                                                    total_Amount = Convert.ToDouble(a.Total_Amount),
                                                    paid_Amount = Convert.ToDouble(a.Paid_Amount),
                                                    medicines_Fee = Convert.ToDouble(a.Medicines_Fee),
                                                    hospital_Fee = Convert.ToDouble(a.Hospital_Fee),
                                                    room_Fee = a.Room_Fee != null ? Convert.ToDouble(a.Room_Fee) : 0,
                                                    patient_ID = a.Patient_ID,
                                                    appointment_ID = a.Appointment_ID,
                                                      generatedDate_Time = a.Generated_Date_Time
                                                }).First();

                        return accountDetails;
                }
            }
            catch (Exception e)
            {
                AccountBO appBO = new AccountBO();
                return appBO;
            }
        }

        //Method to get Payment by Patient, with return type of Account Business object
        public AccountBO GetAccountDetails_DAL(AccountBO accountBO)
        {
            try
            {
                using (HospitalManagementSystemDataContext objHmsDataContext = new HospitalManagementSystemDataContext(Utils.ConnectionString))
                {
                    if (objHmsDataContext.Connection.State == System.Data.ConnectionState.Closed)
                        objHmsDataContext.Connection.Open();

                    Account accountDetails = objHmsDataContext.Accounts.SingleOrDefault(account => account.Bill_ID == accountBO.bill_ID);

                    if (accountDetails != null)
                    {
                        AccountBO acount_BO = ConvertAccountToBO(accountDetails);

                        return acount_BO;
                    }
                    else
                    {
                        AccountBO account_BO = new AccountBO();
                        return account_BO;
                    }
                }
            }
            catch(Exception e)
            {
                AccountBO appBO = new AccountBO();
                return appBO;
            }
        }

        public IEnumerable<AccountBO> GetAllAccounts_DAL()
        {
            try
            {
                using (HospitalManagementSystemDataContext objHmsDataContext = new HospitalManagementSystemDataContext(Utils.ConnectionString))
                {
                    if (objHmsDataContext.Connection.State == System.Data.ConnectionState.Closed)
                        objHmsDataContext.Connection.Open();

                    IEnumerable<AccountBO> accountDetails = objHmsDataContext.Accounts.Join(objHmsDataContext.Patients,
                        a => a.Patient_ID,
                        p => p.Patient_ID,
                        (a, p) => new AccountBO {
                            patient_ID = a.Patient_ID,
                            bill_ID = a.Bill_ID,
                            payment_ID = a.Payment_ID,
                            total_Amount = Convert.ToDouble(a.Total_Amount),
                            paid_Amount = Convert.ToDouble(a.Paid_Amount),
                            medicines_Fee = Convert.ToDouble(a.Medicines_Fee),
                            hospital_Fee = Convert.ToDouble(a.Hospital_Fee),
                            room_Fee = a.Room_Fee!= null? Convert.ToDouble(a.Room_Fee):0 ,
                            generatedDate_Time = a.Generated_Date_Time,
                            patientFirstName = p.First_Name,
                            appointment_ID = a.Appointment_ID
                        }).ToArray();

                    foreach (var a in accountDetails)
                    {
                       // a.room_ID = objHmsDataContext.Treatment_Rooms.Where(t => t.Appointment_ID == a.appointment_ID).Select(t => t.Room_ID).FirstOrDefault();
                        a.room_ID = objHmsDataContext.TreatmentRoom_Records.Where(t => t.Appointment_ID == a.appointment_ID).Select(t => t.Room_ID).ToArray().LastOrDefault();
                    }

                    return accountDetails;
                }
            }
            catch (Exception e)
            {
                IEnumerable<AccountBO> accountBO = null;
                return accountBO;
            }
        }

        //get all account details based on account ID
        public IEnumerable<AccountBO> GetAccountsByID_DAL(AccountBO accountBO)
        {
            try
            {
                using (HospitalManagementSystemDataContext objHmsDataContext = new HospitalManagementSystemDataContext(Utils.ConnectionString))
                {
                    if (objHmsDataContext.Connection.State == System.Data.ConnectionState.Closed)
                        objHmsDataContext.Connection.Open();

                    IEnumerable<AccountBO> accountDetails = objHmsDataContext.Accounts.Where(a =>a.Bill_ID ==  accountBO.bill_ID || a.Patient_ID == accountBO.patient_ID ||
                                                                            a.Payment_ID == accountBO.payment_ID).Select(a => new AccountBO
                    {
                        patient_ID = a.Patient_ID,
                        bill_ID = a.Bill_ID,
                        payment_ID = a.Payment_ID,
                        total_Amount = Convert.ToDouble(a.Total_Amount),
                        generatedDate_Time = a.Generated_Date_Time, 
                       // paidDate_Time = a.Paid_Date_Time,
                        hospital_Fee = Convert.ToDouble(a.Hospital_Fee),
                        medicines_Fee = Convert.ToDouble(a.Medicines_Fee),
                        room_Fee = a.Room_Fee!= null? Convert.ToDouble(a.Room_Fee):0 ,
                        paid_Amount = Convert.ToDouble(a.Paid_Amount),
                        appointment_ID = a.Appointment_ID
                    }).ToArray();
                    return accountDetails;
                }
            }
            catch (Exception e)
            {
                IEnumerable<AccountBO> accountDetails = null;
                return accountDetails;
            }
        }

        public IEnumerable<AccountBO> AccountUpdateDetails_DAL(AccountBO accountBO)
        {
            try
            {
                using (HospitalManagementSystemDataContext objHmsDataContext = new HospitalManagementSystemDataContext(Utils.ConnectionString))
                {

                    if (objHmsDataContext.Connection.State == System.Data.ConnectionState.Closed)
                        objHmsDataContext.Connection.Open();

                    Account account = objHmsDataContext.Accounts.SingleOrDefault(accountDetails => (accountDetails.Bill_ID == accountBO.bill_ID));

                    Account updatedAccount = ConvertBOToAccount(account, accountBO);

                    objHmsDataContext.SubmitChanges();

                    return GetAllAccounts_DAL();
                }
            }
            catch (Exception e)
            {
                IEnumerable<AccountBO> accounts = null;
                return accounts;
            }
        }


        //Method to update AccountDetails, with return type string
        public string UpdateAccountDetails_DAL(AccountBO accountBO)
        {
            try
            {
                using (HospitalManagementSystemDataContext objHmsDataContext = new HospitalManagementSystemDataContext(Utils.ConnectionString))
                {

                    if (objHmsDataContext.Connection.State == System.Data.ConnectionState.Closed)
                        objHmsDataContext.Connection.Open();
                    Account account = null;
                    if (accountBO.payment_ID != 0)
                    account =objHmsDataContext.Accounts.Where(accountDetails => (accountDetails.Patient_ID == accountBO.patient_ID && accountDetails.Payment_ID == accountBO.payment_ID)
                    ).First();
                    else
                        account = objHmsDataContext.Accounts.Where(accountDetails => 
                                              (accountDetails.Patient_ID == accountBO.patient_ID && accountDetails.Bill_ID == accountBO.bill_ID)).First();

                    Account updatedAccount = ConvertBOToAccount(account, accountBO);

                    objHmsDataContext.SubmitChanges();

                    return "Account updated successfully";
                }
            }
            catch(Exception e)
            {
                return "Unable to update account details please try again later ";
            }
        }

        //Method to delete records in Account table, with return type of Account Business object
        public string DeleteAccount_DAL(AccountBO accountBO)
        {
            try
            {
                using (HospitalManagementSystemDataContext objHmsDataContext = new HospitalManagementSystemDataContext(Utils.ConnectionString))
                {
                    if (objHmsDataContext.Connection.State == System.Data.ConnectionState.Closed)
                        objHmsDataContext.Connection.Open();

                    Account account = objHmsDataContext.Accounts.SingleOrDefault(acnt => ((acnt.Patient_ID == accountBO.patient_ID && acnt.Payment_ID == accountBO.payment_ID)
                                                                                                || acnt.Bill_ID == accountBO.bill_ID));

                    objHmsDataContext.Accounts.DeleteOnSubmit(account);
                    objHmsDataContext.SubmitChanges();

                    return "Successfully Account record deleted";
                }
            }
            catch(Exception e)
            {
                return "Unable to delete now please try again later";
            }
        }

        public IEnumerable<AccountBO> DeleteAccountDetail_DAL(AccountBO accountBO)
        {
            try
            {
                using (HospitalManagementSystemDataContext objHmsDataContext = new HospitalManagementSystemDataContext(Utils.ConnectionString))
                {
                    if (objHmsDataContext.Connection.State == System.Data.ConnectionState.Closed)
                        objHmsDataContext.Connection.Open();

                    Account account = objHmsDataContext.Accounts.Where(acnt => (acnt.Bill_ID == accountBO.bill_ID)).SingleOrDefault();

                    objHmsDataContext.Accounts.DeleteOnSubmit(account);
                    objHmsDataContext.SubmitChanges();

                    return GetAllAccounts_DAL();
                }
            }
            catch (Exception e)
            {
                IEnumerable<AccountBO> result = null;
                return result;
            }
        }
        
        //Method to convert BO to Account, with return type of Account Business object
        public Account ConvertBOToAccount(Account account  , AccountBO accountBO )
        {

            if (accountBO.patient_ID != 0)
            {
                account.Patient_ID = accountBO.patient_ID;
            }

            if (accountBO.payment_ID != 0)
            {
                account.Payment_ID = accountBO.payment_ID;
            }

            if (accountBO.total_Amount != 0)
            {
                account.Total_Amount = (decimal)(accountBO.total_Amount);
            }

            if (accountBO.paid_Amount != 0)
            {
                account.Paid_Amount = (decimal)(accountBO.paid_Amount);
            }

            if (accountBO.medicines_Fee != 0)
            {
                account.Medicines_Fee = (decimal)(accountBO.medicines_Fee);
            }

            if (accountBO.hospital_Fee != 0)
            {
                account.Hospital_Fee = (decimal)(accountBO.hospital_Fee);
            }

            if (accountBO.generatedDate_Time != DateTime.MinValue)
            {
                account.Generated_Date_Time = accountBO.generatedDate_Time;
            }
           
            if (accountBO.bill_ID != 0)
            {
                account.Bill_ID = accountBO.bill_ID;
            }
            if (accountBO.appointment_ID != 0)
            {
                account.Appointment_ID = accountBO.appointment_ID;
            }
            if(accountBO.room_Fee != 0)
            {
                account.Room_Fee = (decimal)(accountBO.room_Fee);
            }
            
            return account;
        }

        //Method to convert Account to BO, with return type of Account Business object
        public AccountBO ConvertAccountToBO(Account account)
        {
            AccountBO accountBO = new AccountBO( account.Patient_ID, account.Bill_ID, account.Payment_ID,
                                                Convert.ToDouble(account.Total_Amount), Convert.ToDouble( account.Paid_Amount), 
                                                Convert.ToDouble(account.Medicines_Fee), Convert.ToDouble(account.Hospital_Fee), account.Generated_Date_Time,(account.Room_Fee != null ? Convert.ToDouble(account.Room_Fee) : 0));
             return accountBO;
        }

      

    }
}
