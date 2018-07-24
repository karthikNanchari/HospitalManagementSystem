using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Models.ViewModels
{
    public class DataModel {

        public AccountModel account { get; set; }

        public AdministratorModel admin { get; set; }

        public AppointmentModel appointment { get; set; }

        public DoctorModel doctor { get; set; }

        public LabInchargeModel labincharge { get; set; }

        public LoginModel login { get; set; }

        public NurseModel nurse { get; set; }

        public PatientModel patient { get; set; }

        public PaymentModel payment { get; set; }

        public PersonModel person { get; set; }

        public ReportModel report { get; set; }

        public TreatmentModel treatment { get; set; }

        public EmailModel email { get; set; }

        public NotificationsModel notificationModel { get; set; }

    }

    public class EmailModel
    {

        //[DataType(DataType.EmailAddress), Display(Name = "To")]
        //[Required]
        public string ToEmail { get; set; }

        [Display(Name = "Body")]
        [DataType(DataType.MultilineText)]
        public string EMailBody { get; set; }

        [Display(Name = "Subject")]
        public string EmailSubject { get; set; }

        [DataType(DataType.EmailAddress)]
        [Display(Name = "CC")]
        public string EmailCC { get; set; }

        [DataType(DataType.EmailAddress)]
        [Display(Name = "BCC")]
        public string EmailBCC { get; set; }
    }

    public class AccountModel
    {
        public int account_ID { get; set; }
        public int patient_ID { get; set; }
        public int payment_ID { get; set; }
        public double total_Amount { get; set; }
        public double paid_Amount { get; set; }
        public DateTime date_Time { get; set; }
        public AccountModel(int account_ID, int patient_ID, int payment_ID, double total_Amount,
                 double paid_Amount, DateTime date_Time)

        {
            this.account_ID = account_ID;
            this.patient_ID = patient_ID;
            this.payment_ID = payment_ID;
            this.total_Amount = total_Amount;
            this.paid_Amount = paid_Amount;
            this.date_Time = date_Time;
        }
    }

    public class AdministratorModel : PersonModel
    {
        public AdministratorModel(int pid, string firstName, string lastName, string emailID, string pwd,
                    DateTime dateOfBirth, string securityQn, string securityAns, string phone, string address,
                    string gender, bool isActive)
        : base(pid, firstName, lastName, emailID, pwd,
            dateOfBirth, securityQn, securityAns, phone, address,
            gender, isActive)
        { }
    }

    public class AppointmentModel
    {
        public int appointment_ID { get; set; }

        public int paitent_ID { get; set; }

        public DateTime appointment_Date { get; set; }

        public string timings { get; set; }

        public int doctor_ID { get; set; }

        public DateTime date { get; set; }

        public List<string> doctors { get; set; }

        public AppointmentModel(int appointment_ID, int patient_ID, DateTime appointment_Date,
            string timings, int doctor_ID, DateTime date)

        {
            this.appointment_ID = appointment_ID;
            this.paitent_ID = patient_ID;
            this.appointment_Date = appointment_Date;
            this.timings = timings;
            this.doctor_ID = doctor_ID;
            this.date = date;

        }

    }

    public class DoctorModel : PersonModel
    {
        //Enum to have constants like department of doctor and designation of doctor
        //public enum DrDesignation { GeneralManager, Surgeon, Senior, Junior };
        //public enum DrDepartment { Radiologist, Orthopedics, Dental }

        //public DrDesignation drDesignation { get; set; }
        //public DrDepartment drDepartment { get; set; }

        public DoctorModel(int pid, string firstName, string lastName, string emailID, string pwd,
            DateTime dateOfBirth, string securityQn, string securityAns, string phone, string address,
            string gender, bool isActive, DrDesignation drDesignation, DrDepartment drDepartment)

        //: base(pid, firstName, lastName, emailID, pwd,
        //dateOfBirth, securityQn, securityAns, phone, address,
        //gender, isActive)
        {

            //this.drDesignation = drDesignation;
            //this.drDepartment = drDepartment;
        }
    }

    public class LabInchargeModel : PersonModel
    {
        public LabInchargeModel(int pid, string firstName, string lastName, string emailID, string pwd,
         DateTime dateOfBirth, string securityQn, string securityAns, string phone, string address,
         string gender, bool isActive)

            : base(pid, firstName, lastName, emailID, pwd,
            dateOfBirth, securityQn, securityAns, phone, address,
            gender, isActive)
        { }

    }

    public class LoginModel
    {
        public int id { get; set; }
        public string pwd { get; set; }
    }

    public class NurseModel : PersonModel
    {
        public NurseModel(int pid, string firstName, string lastName, string emailID, string pwd,
           DateTime dateOfBirth, string securityQn, string securityAns, string phone, string address,
           string gender, bool isActive)

            : base(pid, firstName, lastName, emailID, pwd,
            dateOfBirth, securityQn, securityAns, phone, address,
            gender, isActive)
        { }
    }

    public class PatientModel : PersonModel
    {
        // public enum PatientType { InPatient, OutPatient };

        // public PatientType patientType { get; set; }

        public PatientModel() { }

        public PatientModel(int pid, string firstName, string lastName, string emailID, string pwd,
            DateTime dateOfBirth, string securityQn, string securityAns, string phone, string address,
            string gender, bool isActive, PatientType patientType)
        //: base(pid, firstName, lastName, emailID, pwd,
        //dateOfBirth, securityQn, securityAns, phone, address,
        //gender, isActive)
        {

            //this.patientType = patientType;

        }
    }

    public class PaymentModel
    {
        public int payment_ID { get; set; }
        public double hospital_Fee { get; set; }
        public double medicines_Fee { get; set; }
        public double total_Amount { get; set; }
        public double paid_Amount { get; set; }
        public int patient_ID { get; set; }
        public DateTime date { get; set; }

        public PaymentModel(int patient_ID,
              DateTime date, int payment_ID, double hospital_Fee,
             double medicines_Fee, double total_Amount, double paid_Amount)
        {
            this.patient_ID = patient_ID;
            this.date = date;
            this.payment_ID = payment_ID;
            this.hospital_Fee = hospital_Fee;
            this.medicines_Fee = medicines_Fee;
            this.total_Amount = total_Amount;
            this.paid_Amount = paid_Amount;
        }
    }

    public class PersonModel
    {
        public int pid { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string emailID { get; set; }
        public string pwd { get; set; }
        public DateTime dateOfBirth { get; set; }
        public string securityQn { get; set; }
        public string securityAns { get; set; }
        public string phone { get; set; }
        public string address { get; set; }
        public string gender { get; set; }
        public bool isActive { get; set; }

        public enum PatientType { InPatient, OutPatient };
        public enum DrDesignation { GeneralManager, Surgeon, Senior, Junior };
        public enum DrDepartment { Radiologist, Orthopedics, Dental }

        public PatientType patientType { get; set; }
        public DrDesignation drDesignation { get; set; }
        public DrDepartment drDepartment { get; set; }

        public PersonModel(int pid, string firstName, string lastName, string emailID, string pwd,
            DateTime dateOfBirth, string securityQn, string securityAns, string phone, string address,
            string gender, bool isActive)
        {
            this.pid = pid;
            this.firstName = firstName;
            this.lastName = lastName;
            this.emailID = emailID;
            this.pwd = pwd;
            this.dateOfBirth = dateOfBirth;
            this.securityQn = this.securityQn;
            this.securityAns = securityAns;
            this.phone = phone;
            this.address = address;
            this.gender = gender;
            this.isActive = isActive;

        }

        public PersonModel(int pid, string firstName, string lastName, string emailID, string pwd,
          DateTime dateOfBirth, string securityQn, string securityAns, string phone, string address,
          string gender, bool isActive, PatientType patientType)
        {
            this.pid = pid;
            this.firstName = firstName;
            this.lastName = lastName;
            this.emailID = emailID;
            this.pwd = pwd;
            this.dateOfBirth = dateOfBirth;
            this.securityQn = this.securityQn;
            this.securityAns = securityAns;
            this.phone = phone;
            this.address = address;
            this.gender = gender;
            this.isActive = isActive;
            this.patientType = patientType;

        }

        public PersonModel(int pid, string firstName, string lastName, string emailID, string pwd,
      DateTime dateOfBirth, string securityQn, string securityAns, string phone, string address,
      string gender, bool isActive, DrDesignation drDesignation, DrDepartment drDepartment)
        {
            this.pid = pid;
            this.firstName = firstName;
            this.lastName = lastName;
            this.emailID = emailID;
            this.pwd = pwd;
            this.dateOfBirth = dateOfBirth;
            this.securityQn = this.securityQn;
            this.securityAns = securityAns;
            this.phone = phone;
            this.address = address;
            this.gender = gender;
            this.isActive = isActive;
            this.drDesignation = drDesignation;
            this.drDepartment = drDepartment;

        }

        public PersonModel() { }

    }

    public class ReportModel
    {
        public int report_ID { get; set; }

        public int labIncharge_ID { get; set; }

        public string labResult { get; set; }

        public string reportTime { get; set; }

        public int patient_ID { get; set; }

        public DateTime date { get; set; }

        public ReportModel(int report_ID, int labIncharge_ID, string labResult, string reportTime,
            int patient_ID, DateTime date)

        {
            this.report_ID = report_ID;
            this.labIncharge_ID = labIncharge_ID;
            this.labResult = labResult;
            this.reportTime = reportTime;
            this.patient_ID = patient_ID;
            this.date = date;

        }
    }

    public class TreatmentRoomModel
    {
        public int room_ID { get; set; }

        public string timings { get; set; }

        public DateTime date { get; set; }

        public TreatmentRoomModel(int room_ID, string timings, DateTime date)
        {
            this.room_ID = room_ID;
            this.timings = timings;
            this.date = date;
        }
    }


}
