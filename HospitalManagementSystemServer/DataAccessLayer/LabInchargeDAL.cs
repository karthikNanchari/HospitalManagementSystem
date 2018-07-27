using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject;

namespace DataAccessLayer
{
    /*Lab incharge Data Access Layer, using Linq to Sql to perfome create, update, insert and delete operations*/
    public class LabInchargeDAL
    {
        //Method to insert labincharge details into labincharge table, with return type lab incharge
        public LabInchargeBO InsertInchargeDetails_DAL(LabInchargeBO inchargeBO)
        {
            try
            {
                using (HospitalManagementSystemDataContext objHmsDataContext = new HospitalManagementSystemDataContext(Utils.ConnectionString))
                {
                    if (objHmsDataContext.Connection.State == System.Data.ConnectionState.Closed)
                        objHmsDataContext.Connection.Open();
                    {
                        LabIncharge labIncharge = new LabIncharge();
                        inchargeBO.isActive = true;
                        LabIncharge obj_incharge = ConvertBOToLabIncharge( labIncharge,inchargeBO);

                        objHmsDataContext.LabIncharges.InsertOnSubmit(obj_incharge);

                        objHmsDataContext.SubmitChanges();

                        LabInchargeBO labInchargeBO = GetLabInchargeDetials_DAL(inchargeBO);

                        return labInchargeBO;
                    }
                }
            }
            catch (Exception e)
            {
                LabInchargeBO labInc_BO = new LabInchargeBO();

                return labInc_BO;
            }
        }

        //Method To Validate LabIncharge login details, with string return type 
        public string ValidateLabIncharge_DAL(LabInchargeBO inchargeBO)
        {
            try
            {
                using (HospitalManagementSystemDataContext objHmsDataContext = new HospitalManagementSystemDataContext(Utils.ConnectionString))
                {
                    if (objHmsDataContext.Connection.State == System.Data.ConnectionState.Closed)
                        objHmsDataContext.Connection.Open();
                    bool validateEmail = (from LabIncharge in objHmsDataContext.LabIncharges
                                          where ((LabIncharge.Email_ID == inchargeBO.emailID && LabIncharge.Password == inchargeBO.pwd)
                                          || (LabIncharge.LabIncharge_ID == inchargeBO.pid && LabIncharge.Password == inchargeBO.pwd))
                                          select LabIncharge.IsActive).SingleOrDefault();

                    return Convert.ToString(validateEmail);
                }
            }
            catch (Exception e)
            {
                return "unable to validate User please try again later";
            }
        }

        public string ValidateEmailLabIncharge_DAL(LabInchargeBO inchargeBO)
        {
            try
            {
                var validateEmail = "false";
                using (HospitalManagementSystemDataContext objHmsDataContext = new HospitalManagementSystemDataContext(Utils.ConnectionString))
                {
                    if (objHmsDataContext.Connection.State == System.Data.ConnectionState.Closed)
                        objHmsDataContext.Connection.Open();
                    validateEmail = (from LabIncharge in objHmsDataContext.LabIncharges
                                          where ((LabIncharge.Email_ID == inchargeBO.emailID ))
                                          select LabIncharge.Password).SingleOrDefault();

                    return Convert.ToString(validateEmail);
                }
            }
            catch (Exception e)
            {
                return "";
            }
        }



        //Method to check if emailID is already used, with string return type  
        public string CheckAvailability_DAL(LabInchargeBO inchargeBO)
        {
            try
            {
                using (HospitalManagementSystemDataContext ObjHmsDataContext = new HospitalManagementSystemDataContext(Utils.ConnectionString))
                {
                    if (ObjHmsDataContext.Connection.State == System.Data.ConnectionState.Closed)
                        ObjHmsDataContext.Connection.Open();
                    var emailID = (from LabIncharges in ObjHmsDataContext.LabIncharges
                                   select LabIncharges.Email_ID == inchargeBO.emailID).SingleOrDefault();

                    //If emailId = true then Email_ID  is already used
                    return emailID.ToString();
                }
            }
            catch(Exception e)
            { return "EmailID Validation failed"; }
        }

        //Method to update LabIncharge details SecurityQuestion, with string return type
        public string UpdateLabInchargeDetails_DAL(LabInchargeBO inchargeBO)
        {
            try
            {
                using (HospitalManagementSystemDataContext objHmsDataContext = new HospitalManagementSystemDataContext(Utils.ConnectionString))
                {
                    if (objHmsDataContext.Connection.State == System.Data.ConnectionState.Closed)
                        objHmsDataContext.Connection.Open();
                   

                    LabIncharge labIncharge = objHmsDataContext.LabIncharges.SingleOrDefault( pat => (pat.LabIncharge_ID == inchargeBO.pid || pat.Email_ID == inchargeBO.emailID));

                    LabIncharge updaterLabIncharge = ConvertBOToLabIncharge(labIncharge ,inchargeBO);

                    objHmsDataContext.SubmitChanges();

                    return "Successfullly Updated";
                }
            }
            catch(Exception e)
            {
                return "Unable to update Please try once again";
            }

        }

        public IEnumerable<LabInchargeBO> UpdateLabIncharge_DAL(LabInchargeBO inchargeBO)
        {
            try
            {
                using (HospitalManagementSystemDataContext objHmsDataContext = new HospitalManagementSystemDataContext(Utils.ConnectionString))
                {
                    if (objHmsDataContext.Connection.State == System.Data.ConnectionState.Closed)
                        objHmsDataContext.Connection.Open();


                    LabIncharge labIncharge = objHmsDataContext.LabIncharges.SingleOrDefault(pat => (pat.LabIncharge_ID == inchargeBO.pid || pat.Email_ID == inchargeBO.emailID));

                    LabIncharge updaterLabIncharge = ConvertBOToLabIncharge(labIncharge, inchargeBO);

                    objHmsDataContext.SubmitChanges();

                    return GetAllLabInchargeDetials_DAL();
                }
            }
            catch (Exception e)
            {
                return GetAllLabInchargeDetials_DAL();
            }

        }





        //Method to deactivate LabIncharge account, with string return type
        public string DeactivateLabIncharge_DAL(LabInchargeBO inchargeBO)
        {
            try
            {
                using (HospitalManagementSystemDataContext objHmsDataContext = new HospitalManagementSystemDataContext(Utils.ConnectionString))
                {
                    if (objHmsDataContext.Connection.State == System.Data.ConnectionState.Closed)
                        objHmsDataContext.Connection.Open();

                    LabIncharge LabIncharge = objHmsDataContext.LabIncharges.SingleOrDefault(id => (id.LabIncharge_ID == inchargeBO.pid || id.Email_ID == inchargeBO.emailID));

                    LabIncharge.IsActive = false;

                    objHmsDataContext.SubmitChanges();

                    return "Account Deactivated Successfully";
                }
            }
            catch (Exception e)
            {
                return "Unable to deactive please try again";
            }

        }

        //Method to get LabIncharge details 
        public LabInchargeBO GetLabInchargeDetials_DAL(LabInchargeBO inchargeBO)
        {
            try
            {

                using (HospitalManagementSystemDataContext objHmsDataContext = new HospitalManagementSystemDataContext(Utils.ConnectionString))
                {
                    if (objHmsDataContext.Connection.State == System.Data.ConnectionState.Closed)
                        objHmsDataContext.Connection.Open();

                    LabIncharge LabInchargeInfo = objHmsDataContext.LabIncharges.SingleOrDefault(incharge => ((incharge.LabIncharge_ID == inchargeBO.pid || incharge.Email_ID == inchargeBO.emailID) && (incharge.Password == inchargeBO.pwd)));

                    LabInchargeBO incharge_BO = ConvertLabInchargeToBO(LabInchargeInfo);

                    return incharge_BO;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }


        public IEnumerable<LabInchargeBO> GetAllLabInchargeDetials_DAL()
        {
            try
            {
                using (HospitalManagementSystemDataContext objHmsDataContext = new HospitalManagementSystemDataContext(Utils.ConnectionString))
                {
                    if (objHmsDataContext.Connection.State == System.Data.ConnectionState.Closed)
                        objHmsDataContext.Connection.Open();

                    IEnumerable<LabInchargeBO> LabInchargeInfo = objHmsDataContext.LabIncharges.Select(incharge => new LabInchargeBO

                    { pid = incharge.LabIncharge_ID,
                        firstName = incharge.First_Name,
                        lastName = incharge.Last_Name,
                        pwd = incharge.Password,
                        address = incharge.Address,
                        emailID = incharge.Email_ID,
                        dateOfBirth = incharge.DateOfBirth,
                        gender = incharge.Gender,
                        isActive = incharge.IsActive,
                        phone = incharge.Phone,
                        securityAns = incharge.Security_Answer,
                        securityQn = incharge.Security_Question
                    }).ToArray();
                    return LabInchargeInfo;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }



        //Method to Activate LabIncharge Account
        public string ActivateLabIncharge_DAL(LabInchargeBO inchargeBO)
        {
            try
            {
                using (HospitalManagementSystemDataContext objHmsDataContext = new HospitalManagementSystemDataContext(Utils.ConnectionString))
                {
                    if (objHmsDataContext.Connection.State == System.Data.ConnectionState.Closed)
                        objHmsDataContext.Connection.Open();

                    LabIncharge LabIncharge1 = objHmsDataContext.LabIncharges.SingleOrDefault(id => (id.LabIncharge_ID == inchargeBO.pid || id.Email_ID == inchargeBO.emailID));

                    LabIncharge1.IsActive = true;

                    objHmsDataContext.SubmitChanges();

                    LabIncharge LabIncharge2 = objHmsDataContext.LabIncharges.SingleOrDefault(id => id.LabIncharge_ID == inchargeBO.pid);

                    return " Account Activated";
                }
            }
            catch (Exception e)
            {

                return "Unable To Activate Account Try again later";
            }

        }

        //Convert LabIncharge to BO
        public LabInchargeBO ConvertLabInchargeToBO(LabIncharge LabIncharge)
        {
            try
            {
                LabInchargeBO inchargeBO = new LabInchargeBO(LabIncharge.LabIncharge_ID, LabIncharge.First_Name, LabIncharge.Last_Name, LabIncharge.Email_ID,
                                                     LabIncharge.Password, LabIncharge.DateOfBirth, LabIncharge.Security_Question, LabIncharge.Security_Answer,
                                                     LabIncharge.Phone, LabIncharge.Address, LabIncharge.Gender, LabIncharge.IsActive);

                if (!string.IsNullOrEmpty(inchargeBO.securityQn))
                {
                    LabIncharge.Security_Question = inchargeBO.securityQn;
                }

                return inchargeBO;
            }
            catch(Exception e)
            {
                LabInchargeBO   incharge_BO = new LabInchargeBO();

                return incharge_BO;
            }
        }

        //Convert BO to LabIncharge
        public LabIncharge ConvertBOToLabIncharge(LabIncharge labIncharge, LabInchargeBO inchargeBO)
        {

            if (inchargeBO.pid != 0)
            {
                labIncharge.LabIncharge_ID = inchargeBO.pid;
            }

            if (!string.IsNullOrEmpty(inchargeBO.firstName))
            {
                labIncharge.First_Name = inchargeBO.firstName;
            }

            if (!string.IsNullOrEmpty(inchargeBO.lastName))
            {
                labIncharge.Last_Name = inchargeBO.lastName;
            }

            if (!string.IsNullOrEmpty(inchargeBO.emailID))
            {
                labIncharge.Email_ID = inchargeBO.emailID;
            }

            if (!string.IsNullOrEmpty(inchargeBO.pwd))
            {
                labIncharge.Password = inchargeBO.pwd;
            }

            if (inchargeBO.dateOfBirth != DateTime.MinValue)
            {
                labIncharge.DateOfBirth = inchargeBO.dateOfBirth;
            }

            if (!string.IsNullOrEmpty(inchargeBO.phone))
            {
                labIncharge.Phone = inchargeBO.phone;
            }

            if (!string.IsNullOrEmpty(inchargeBO.address))
            {
                labIncharge.Address = inchargeBO.address;
            }

            if (!string.IsNullOrEmpty(inchargeBO.securityQn))
            {
                labIncharge.Security_Question = inchargeBO.securityQn;
            }

            if (!string.IsNullOrEmpty(inchargeBO.securityAns))
            {
                labIncharge.Security_Answer = inchargeBO.securityAns;
            }

            if (!string.IsNullOrEmpty(inchargeBO.gender))
            {
                labIncharge.Gender = inchargeBO.gender;
            }      

            {
                labIncharge.IsActive = inchargeBO.isActive;
            }

            return labIncharge;
        }

    }
}
