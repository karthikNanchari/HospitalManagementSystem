using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject;

namespace DataAccessLayer
{
    /*Treatment Data Access layer, using Linq to Sql for performing create, update, delete and insert operations*/
    public class TreatmentDAL
    {
        //Method to insert new Treatment,with treatment business object as return type and parameter
        public TreatmentBO InsertNewTreatment_DAL(TreatmentBO treatmentBO)
        {
            try
            {
                using (HospitalManagementSystemDataContext objHmsDataContext = new HospitalManagementSystemDataContext(Utils.ConnectionString))
                {
                    if (objHmsDataContext.Connection.State == System.Data.ConnectionState.Closed)
                        objHmsDataContext.Connection.Open();

                    Treatment treatment = new Treatment();

                    objHmsDataContext.Treatments.InsertOnSubmit(ConvertBOToTreatment(treatment, treatmentBO));

                    objHmsDataContext.SubmitChanges();
                }
                return GetTreatment_DAL(treatmentBO);
            }
            catch (Exception e)
            {
                TreatmentBO treatment_BO = new TreatmentBO();

                return treatment_BO;
            }
        }

        //Method to get Treatment details,with treatment business object as return type and parameter
        public TreatmentBO GetTreatment_DAL(TreatmentBO treatmentBO)
        {
            try
            {
                using (HospitalManagementSystemDataContext objHmsDataContext = new HospitalManagementSystemDataContext(Utils.ConnectionString))
                {
                    if (objHmsDataContext.Connection.State == System.Data.ConnectionState.Closed)
                        objHmsDataContext.Connection.Open();

                    Treatment treatment = objHmsDataContext.Treatments.SingleOrDefault(trmnt => ((trmnt.Patient_ID == treatmentBO.patient_ID && trmnt.Date_time == treatmentBO.date_Time)
                                                                                || (trmnt.Treatment_ID == treatmentBO.treatment_ID)));
                    return ConvertTreatmentToBO(treatment);
                }
            }
            catch (Exception e)
            {
                TreatmentBO treatment_BO = new TreatmentBO();

                return treatment_BO;
            }
        }

        //Method to convert BO to Treatment,with treatment r return type and parameters as treatment object, treatment business object
        public Treatment ConvertBOToTreatment(Treatment treatment, TreatmentBO treatmentBO)
        {
            if (treatmentBO.treatment_ID != 0)
            {
                treatment.Treatment_ID = treatmentBO.treatment_ID;
            }

            if (treatmentBO.doctor_ID != 0)
            {
                treatment.Doctor_ID = treatmentBO.doctor_ID;
            }

            if (treatmentBO.patient_ID != 0)
            {
                treatment.Patient_ID = treatmentBO.patient_ID;
            }

            if (treatmentBO.nurse_ID != 0)
            {
                treatment.Nurse_ID = treatmentBO.nurse_ID;
            }

            if (treatmentBO.Room_ID != null)
            {
                treatment.Room_ID = treatmentBO.Room_ID;
            }

            if (treatmentBO.prescription_ID != 0)
            {
                treatment.Prescription_ID = treatmentBO.prescription_ID;
            }

            if (treatmentBO.date_Time != DateTime.MinValue)
            {
                treatment.Date_time = treatmentBO.date_Time;
            }


            if (!string.IsNullOrEmpty(treatmentBO.startTime))
            {
                treatment.Start_Time = TimeSpan.Parse(treatmentBO.startTime);
            }

            if (!string.IsNullOrEmpty(treatmentBO.endTime))
            {
                treatment.End_Time = TimeSpan.Parse(treatmentBO.endTime);
            }

            return treatment;

        }

        //Method to convert Treatment to BO
        public TreatmentBO ConvertTreatmentToBO(Treatment treatment)
        {
            TreatmentBO treatmentBO = new TreatmentBO(treatment.Treatment_ID, treatment.Patient_ID, treatment.Nurse_ID,
                                                    treatment.Prescription_ID, treatment.Room_ID, treatment.Doctor_ID,
                                                    Convert.ToString(treatment.Start_Time), Convert.ToString(treatment.End_Time));
            return treatmentBO;
        }

        //Method to Update Treatment, with string as return type and Treatment business object as parameter
        public string UpdateTreatment_DAL(TreatmentBO treatmentBO)
        {
            try
            {
                using (HospitalManagementSystemDataContext objHmsDataContext = new HospitalManagementSystemDataContext(Utils.ConnectionString))
                {
                    if (objHmsDataContext.Connection.State == System.Data.ConnectionState.Closed)
                        objHmsDataContext.Connection.Open();

                    Treatment treatment = new Treatment();
                    treatment = objHmsDataContext.Treatments.SingleOrDefault(trmnt => ((trmnt.Patient_ID == treatmentBO.patient_ID && trmnt.Date_time == treatment.Date_time)
                                                                                   || trmnt.Treatment_ID == treatmentBO.treatment_ID));

                    Treatment updatedTreatment = ConvertBOToTreatment(treatment, treatmentBO);

                    objHmsDataContext.SubmitChanges();

                    return "Successfully changes submitted";
                }
            }
            catch (Exception e)
            {
                return "Unable to update changes please try again later";
            }
        }

        //Method to delete Treatment, with string as return type and treatment business object as paratmeter
        public string DeleteTreatment_DAL(TreatmentBO treatmentBO)
        {
            try
            {
                TreatmentBO trmntBO = GetTreatment_DAL(treatmentBO);
                using (HospitalManagementSystemDataContext objHmsDataContext = new HospitalManagementSystemDataContext(Utils.ConnectionString))
                {
                    if (objHmsDataContext.Connection.State == System.Data.ConnectionState.Closed)
                        objHmsDataContext.Connection.Open();

                    Treatment treatment = new Treatment();      //objHmsDataContext.Treatments.SingleOrDefault(trmnt => (trmnt.Treatment_ID == treatmentBO.treatment_ID));

                    objHmsDataContext.Treatments.DeleteOnSubmit(ConvertBOToTreatment(treatment, trmntBO));

                    objHmsDataContext.SubmitChanges();

                    return "Treatment deleted Successfully";
                }
            }
            catch (Exception e)
            {
                return "Unable to delete treatment";
            }
        }


    }
}
