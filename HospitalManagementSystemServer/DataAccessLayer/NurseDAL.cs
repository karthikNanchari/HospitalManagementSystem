using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject;
namespace DataAccessLayer
{
    /*Nurse Data Access layer, using Linq to Sql operations like create, insert, update, delete are performed*/
    public class NurseDAL
    {
        //funtion to register new nurse, with nurse BO as return type
        public NurseBO InsertNurseDetails_DAL(NurseBO nurseBO)
        {
            try
            {
                using (HospitalManagementSystemDataContext objHmsDataContext = new HospitalManagementSystemDataContext(Utils.ConnectionString))
                {
                    if (objHmsDataContext.Connection.State == System.Data.ConnectionState.Closed)
                        objHmsDataContext.Connection.Open();

                    Nurse objNurse = new Nurse();
                    nurseBO.isActive = true;
                    objHmsDataContext.Nurses.InsertOnSubmit(ConvertBoTONurse(objNurse, nurseBO));

                    objHmsDataContext.SubmitChanges();

                    Nurse nurse = objHmsDataContext.Nurses.SingleOrDefault(newnurse => (newnurse.Email_ID == nurseBO.emailID
                                                                            || newnurse.Nurse_ID == nurseBO.pid));

                    return ConvertNurseToBO(nurse);
                }
            }
            catch (Exception e)
            {
                NurseBO nurse_BO = new NurseBO();
                return nurse_BO;
            }

        }

        //funtion to validate Account Login, with return type string
        public string ValidateNurse_DAL(NurseBO nurseBO)
        {
            try
            {
                HospitalManagementSystemDataContext ObjHmsDataContext = new HospitalManagementSystemDataContext(Utils.ConnectionString);

                bool validation = (from nurse in ObjHmsDataContext.Nurses
                                   where (nurse.Nurse_ID == nurseBO.pid && nurse.Password == nurseBO.pwd)
                                   || (nurse.Email_ID == nurseBO.emailID && nurse.Password == nurseBO.pwd)
                                   select nurse.IsActive).FirstOrDefault();
                return validation.ToString();
            }
            catch (Exception e)
            {
                return "unable to process validation please try again later";
            }
        }

        public string ValidateEmailNurse_DAL(NurseBO nurseBO)
        {
            try
            {
                var validation = "false";
                HospitalManagementSystemDataContext ObjHmsDataContext = new HospitalManagementSystemDataContext(Utils.ConnectionString);

                 validation = (from nurse in ObjHmsDataContext.Nurses
                                   where (nurse.Nurse_ID == nurseBO.pid && nurse.Password == nurseBO.pwd)
                                   select nurse.Password).FirstOrDefault();
                return validation.ToString();
            }
            catch (Exception e)
            {
                return "";
            }
        }

        //funtion to check availability, with return type, string
        public string CheckAvailability(NurseBO nurseBO)
        {
            try
            {
                using (HospitalManagementSystemDataContext objHmsDataContext = new HospitalManagementSystemDataContext(Utils.ConnectionString))
                {
                    if (objHmsDataContext.Connection.State == System.Data.ConnectionState.Closed)
                        objHmsDataContext.Connection.Open();

                    bool availability = (from nurse in objHmsDataContext.Nurses
                                         select nurse.Email_ID == nurseBO.emailID).SingleOrDefault();

                    return Convert.ToString(availability);
                }
            }
            catch (Exception e)
            {
                return "Unable to chexk email Availability";
            }
        }

        //funtion to get complete details by id, with return type Nurse Business object
        public NurseBO GetNurseDetails_DAL(NurseBO nurseBO)
        {
            try
            {
                using (HospitalManagementSystemDataContext objHmsDataContext = new HospitalManagementSystemDataContext(Utils.ConnectionString))
                {
                    if (objHmsDataContext.Connection.State == System.Data.ConnectionState.Closed)
                        objHmsDataContext.Connection.Open();

                    Nurse nurseDetails = objHmsDataContext.Nurses.SingleOrDefault(nurse => ((nurse.Nurse_ID == nurseBO.pid || nurse.Email_ID == nurseBO.emailID) && (nurse.Password == nurseBO.pwd)));

                    return ConvertNurseToBO(nurseDetails);
                }
            }
            catch (Exception e)
            {
                NurseBO emptyBO = null;

                return emptyBO;
            }
        }

        //Method to updateDetails, with return type string
        public string UpdateDetails_DAL(NurseBO nurseBO)
        {
            try
            {
                using (HospitalManagementSystemDataContext ObjHmsDataContext = new HospitalManagementSystemDataContext(Utils.ConnectionString))
                {
                    if (ObjHmsDataContext.Connection.State == System.Data.ConnectionState.Closed)
                        ObjHmsDataContext.Connection.Open();
                    Nurse nurse = ObjHmsDataContext.Nurses.SingleOrDefault(nrse => (nrse.Email_ID == nurseBO.emailID
                                                                                    || nrse.Nurse_ID == nurseBO.pid));

                    Nurse nurseDetails = ConvertBoTONurse(nurse, nurseBO);

                    ObjHmsDataContext.SubmitChanges();

                    return "Successfully Updated";
                }
            }
            catch (Exception e)
            {
                return "Unable to Update Please try once again";
            }
        }

        public IEnumerable<NurseBO> UpdateNurseDetails_DAL(NurseBO nurseBO)
        {
            try
            {
                using (HospitalManagementSystemDataContext ObjHmsDataContext = new HospitalManagementSystemDataContext(Utils.ConnectionString))
                {
                    if (ObjHmsDataContext.Connection.State == System.Data.ConnectionState.Closed)
                        ObjHmsDataContext.Connection.Open();
                    Nurse nurse = ObjHmsDataContext.Nurses.SingleOrDefault(nrse => (nrse.Email_ID == nurseBO.emailID
                                                                                    || nrse.Nurse_ID == nurseBO.pid));

                    Nurse nurseDetails = ConvertBoTONurse(nurse, nurseBO);

                    ObjHmsDataContext.SubmitChanges();

                    return GetAllNurses_DAL();
                }
            }
            catch (Exception e)
            {
                return GetAllNurses_DAL();
            }
        }



        //funtion to Deactivate Acocunt, with return type string
        public string DeActivateAccount_DAL(NurseBO nurseBO)
        {
            try
            {
                using (HospitalManagementSystemDataContext ObjHmsDataContext = new HospitalManagementSystemDataContext(Utils.ConnectionString))
                {
                    if (ObjHmsDataContext.Connection.State == System.Data.ConnectionState.Closed)
                        ObjHmsDataContext.Connection.Open();

                    Nurse nurse_DeActivate = ObjHmsDataContext.Nurses.SingleOrDefault(nrse => (nrse.Email_ID == nurseBO.emailID
                                                                                       || nrse.Nurse_ID == nurseBO.pid));

                    nurse_DeActivate.IsActive = false;

                    ObjHmsDataContext.SubmitChanges();

                    return "Nurse Account DeActivated";
                }
            }
            catch (Exception e)
            {
                return "Unable to Deactivate Nurse Account Please try again later";
            }
        }

        //funtion to activate Account, with return type string
        public string ActivateAccount_DAL(NurseBO nurseBO)
        {
            try
            {
                using (HospitalManagementSystemDataContext ObjHmsDataContext = new HospitalManagementSystemDataContext(Utils.ConnectionString))
                {
                    if (ObjHmsDataContext.Connection.State == System.Data.ConnectionState.Closed)
                        ObjHmsDataContext.Connection.Open();

                    Nurse nurse_Activate = ObjHmsDataContext.Nurses.SingleOrDefault(nrse => (nrse.Email_ID == nurseBO.emailID
                                                                            || nrse.Nurse_ID == nurseBO.pid));


                    nurse_Activate.IsActive = true;

                    ObjHmsDataContext.SubmitChanges();

                    return "Successfully Account Activated";
                }
            }
            catch (Exception e)
            {
                return "Unable to activate Account, please try once again";
            }

        }

        public IEnumerable<NurseBO> GetAllNurses_DAL()
        {
            try
            {
                using (HospitalManagementSystemDataContext objHmsDataContext = new HospitalManagementSystemDataContext(Utils.ConnectionString))
                {
                    if (objHmsDataContext.Connection.State == System.Data.ConnectionState.Closed)
                        objHmsDataContext.Connection.Open();

                    IEnumerable<NurseBO> nurseDetails = objHmsDataContext.Nurses
                        .Select(n => new NurseBO
                        { pid = n.Nurse_ID,
                        firstName = n.First_Name,
                        lastName = n.Last_Name,
                        pwd = n.Password,
                        address = n.Address,
                        emailID = n.Email_ID,
                        dateOfBirth = n.DateOfBirth,
                        gender = n.Gender,
                        isActive = n.IsActive,
                        phone = n.Phone,
                        securityAns = n.Security_Answer,
                        securityQn = n.Security_Question
                    } ).ToArray();

                    return nurseDetails;
                }
            }
            catch
            {
                return null;
            }
        }


        //Method to convert NurseBO to Nurse 
        public Nurse ConvertBoTONurse(Nurse objNurse, NurseBO nurseBO)
        {

            if (nurseBO.pid != 0)
            {
                objNurse.Nurse_ID = nurseBO.pid;
            }

            if (!string.IsNullOrEmpty(nurseBO.firstName))
            {
                objNurse.First_Name = nurseBO.firstName;
            }

            if (!string.IsNullOrEmpty(nurseBO.lastName))
            {
                objNurse.Last_Name = nurseBO.lastName;
            }

            if (!string.IsNullOrEmpty(nurseBO.lastName))
            {
                objNurse.Email_ID = nurseBO.emailID;
            }

            if (!string.IsNullOrEmpty(nurseBO.pwd))
            {
                objNurse.Password = nurseBO.pwd;
            }

            if (nurseBO.dateOfBirth != DateTime.MinValue)
            {
                objNurse.DateOfBirth = nurseBO.dateOfBirth;
            }

            if (!string.IsNullOrEmpty(nurseBO.phone))
            {
                objNurse.Phone = nurseBO.phone;
            }

            if (!string.IsNullOrEmpty(nurseBO.gender))
            {
                objNurse.Gender = nurseBO.gender;
            }

            if (!string.IsNullOrEmpty(nurseBO.address))
            {
                objNurse.Address = nurseBO.address;
            }

            if (!string.IsNullOrEmpty(nurseBO.securityQn))
            {
                objNurse.Security_Question = nurseBO.securityQn;
            }

            if (!string.IsNullOrEmpty(nurseBO.securityAns))
            {
                objNurse.Security_Answer = nurseBO.securityAns;
            }

            {
                objNurse.IsActive = nurseBO.isActive;
            }

            return objNurse;

        }

        //funtion to convert Nurse to NurseBO
        public NurseBO ConvertNurseToBO(Nurse nurse)
        {
            NurseBO nurseBO = new NurseBO(nurse.Nurse_ID, nurse.First_Name, nurse.Last_Name, nurse.Email_ID,
                                           nurse.Password, nurse.DateOfBirth, nurse.Security_Question, nurse.Security_Answer,
                                           nurse.Phone, nurse.Address, nurse.Gender, nurse.IsActive);

            if (!string.IsNullOrEmpty(nurse.Security_Question))
            {
                nurseBO.securityQn = nurse.Security_Question;
            }

            return nurseBO;

        }

    }
}
