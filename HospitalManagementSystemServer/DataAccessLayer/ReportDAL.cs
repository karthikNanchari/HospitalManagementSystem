using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject;

namespace DataAccessLayer
{
    /*Report Data Access Layer, using Linq to Sql to perform create, insert, update and delete operations*/
    public class ReportDAL
    {
        //Method to Insert a new report, with report business object as return type and report Business as parameter 
        public ReportBO InsertNewReport(ReportBO reportBO)
        {
            try
            {
                using (HospitalManagementSystemDataContext objHmsDataContext = new HospitalManagementSystemDataContext(Utils.ConnectionString))
                {
                    if (objHmsDataContext.Connection.State == System.Data.ConnectionState.Closed)
                        objHmsDataContext.Connection.Open();
                    //reportBO.labResult = "Blood test, No signs of Virus";

                    Report report = new Report();

                    Report newReport = ConvertBOToReport(report, reportBO);
                    

                    objHmsDataContext.Reports.InsertOnSubmit(newReport);
                    var a = objHmsDataContext.Appointments.Where(app => app.Appointment_ID == reportBO.appointment_ID).FirstOrDefault();
                    a.RequestedReport = false;
                    //objHmsDataContext.Appointments
                    objHmsDataContext.SubmitChanges();

                    return GetRecentInsertedReport(reportBO);
                }
            }
            catch (Exception e)
            {
                ReportBO rprtBO = new ReportBO();
                return rprtBO;
            }
        }

        //Method to get Report, with report Business object as return type and report Business object as parameter
        public ReportBO GetReport(ReportBO reportBO)
        {
            try
            {
                using (HospitalManagementSystemDataContext objHmsDataContext = new HospitalManagementSystemDataContext(Utils.ConnectionString))
                {
                    if (objHmsDataContext.Connection.State == System.Data.ConnectionState.Closed)
                        objHmsDataContext.Connection.Open();

                    Report report = objHmsDataContext.Reports.SingleOrDefault(rprt => (rprt.Patient_ID == reportBO.patient_ID ));

                    return ConvertReportToBO(report);
                }

            }
            catch (Exception e)
            {
                ReportBO report_BO = new ReportBO();
                return report_BO;
            }
        }


        public ReportBO GetReportsByAppt(int id)
        {
            try
            {
                using (HospitalManagementSystemDataContext objHmsDataContext = new HospitalManagementSystemDataContext(Utils.ConnectionString))
                {
                    if (objHmsDataContext.Connection.State == System.Data.ConnectionState.Closed)
                        objHmsDataContext.Connection.Open();

                    var re = objHmsDataContext.Reports.Where(rprt => (rprt.Appointment_ID == id)).ToList();
                    var reportDetails = ConvertReportToBO(re.Last());
                    reportDetails.patientFirstName = objHmsDataContext.Patients.Where(p => p.Patient_ID == reportDetails.patient_ID)
                        .Select(pat => pat.First_Name).First();
                    if(reportDetails != null)
                    {
                        reportDetails.reportRequested = objHmsDataContext.Appointments.Where(a => a.Appointment_ID == reportDetails.appointment_ID).Select(r => r.RequestedReportNotes).First();
                    }
                    return reportDetails;
                }

            }
            catch (Exception e)
            {
                ReportBO report_BO = new ReportBO();
                return report_BO;
            }
        }

        public ReportBO GetReportForID(ReportBO reportBO)
        {
            try
            {
                using (HospitalManagementSystemDataContext objHmsDataContext = new HospitalManagementSystemDataContext(Utils.ConnectionString))
                {
                    if (objHmsDataContext.Connection.State == System.Data.ConnectionState.Closed)
                        objHmsDataContext.Connection.Open();

                    Report report = objHmsDataContext.Reports.SingleOrDefault(rprt => (rprt.Patient_ID == reportBO.patient_ID
                     && rprt.Lab_Result == reportBO.labResult &&  (rprt.LabIncharge_ID == reportBO.labIncharge_ID  &&
                    rprt.Report_Time == TimeSpan.Parse(reportBO.reportTime) && rprt.Report_Date == reportBO.date)));

                    return ConvertReportToBO(report);
                }

            }
            catch (Exception e)
            {
                ReportBO report_BO = new ReportBO();
                return report_BO;
            }
        }

        public ReportBO GetRecentInsertedReport(ReportBO reportBO)
        {
            try
            {
                using (HospitalManagementSystemDataContext objHmsDataContext = new HospitalManagementSystemDataContext(Utils.ConnectionString))
                {
                    if (objHmsDataContext.Connection.State == System.Data.ConnectionState.Closed)
                        objHmsDataContext.Connection.Open();

                   

                    ReportBO rBO = objHmsDataContext.Reports.
                        Where(rt => 
                        rt.Report_Time == TimeSpan.Parse(reportBO.reportTime) &&
                        rt.Report_Date == reportBO.date &&
                        rt.Patient_ID == reportBO.patient_ID &&
                        rt.Lab_Result == reportBO.labResult
                        ).Join(objHmsDataContext.Patients,
                        r => r.Patient_ID,
                        p => p.Patient_ID,
                        (r, p) => new ReportBO { 
                        reportTime = Convert.ToString(r.Report_Time),
                        date = r.Report_Date,
                        patient_ID = r.Patient_ID,
                        labResult = r.Lab_Result,
                        report_ID = r.Report_ID,
                        patientFirstName = p.First_Name
                    }).ToArray()[0];

                    return rBO;
                }

            }
            catch (Exception e)
            {
                ReportBO report_BO = new ReportBO();
                return report_BO;
            }
        }


        //Method to convert BO to Report
        public Report ConvertBOToReport(Report report, ReportBO reportBO)
        {
            if (reportBO.report_ID != 0)
            {
                report.Report_ID = reportBO.report_ID;
            }

            if (reportBO.labIncharge_ID != 0)
            {
                report.LabIncharge_ID = reportBO.labIncharge_ID;
            }

            if (reportBO.patient_ID != 0)
            {
                report.Patient_ID = reportBO.patient_ID;
            }

            if (!string.IsNullOrEmpty(reportBO.labResult))
            {
                report.Lab_Result = reportBO.labResult;
            }

            if (!string.IsNullOrEmpty(reportBO.reportTime))
            {
                report.Report_Time = TimeSpan.Parse((reportBO.reportTime));
            }

            if (reportBO.date != DateTime.MinValue)
            {
                report.Report_Date = reportBO.date;
            }

            if(reportBO.appointment_ID != 0)
            {
                report.Appointment_ID = reportBO.appointment_ID;
            }
            return report;
        }

        //Method to convert Report to Business object
        public ReportBO ConvertReportToBO(Report report)
        {
            ReportBO reportBO = new ReportBO(report.Report_ID, report.LabIncharge_ID, report.Lab_Result,
                                                Convert.ToString(report.Report_Time), report.Patient_ID, report.Report_Date, report.Appointment_ID);

            return reportBO;
        }

        //Method to update Report, with string return type and report business object as parameter
        public IEnumerable<ReportBO> UpdatePatientReport_DAL(ReportBO reportBO)
        {

            try
            {
                using (HospitalManagementSystemDataContext objHmsDataContext = new HospitalManagementSystemDataContext(Utils.ConnectionString))
                {
                    if (objHmsDataContext.Connection.State == System.Data.ConnectionState.Closed)
                        objHmsDataContext.Connection.Open();

                    Report report = objHmsDataContext.Reports.SingleOrDefault(rprt => ( rprt.Report_ID == reportBO.report_ID));

                    Report updatedReport = ConvertBOToReport(report, reportBO);

                    objHmsDataContext.SubmitChanges();

                    return GetPatientReports_DAL(reportBO.patient_ID);
                }
            }
            catch 
            {
                return null;
            }
        }

        //Method to delete Report, with string return type and report business object as parameter
        public string DeleteRepoprt(ReportBO reportBO)
        {
            try
            {
                using (HospitalManagementSystemDataContext objHmsDataContext = new HospitalManagementSystemDataContext(Utils.ConnectionString))
                {
                    if (objHmsDataContext.Connection.State == System.Data.ConnectionState.Closed)
                        objHmsDataContext.Connection.Open();

                    Report report = objHmsDataContext.Reports.SingleOrDefault(rprt => (rprt.Report_ID == reportBO.report_ID));

                    objHmsDataContext.Reports.DeleteOnSubmit(report);

                    objHmsDataContext.SubmitChanges();

                    return "Report Deleted successfully";
                }
            }
            catch (Exception e)
            {
                return "Unable to delete report Please try again Later";
            }
        }

        public void DeletePatientReport(ReportBO reportBO)
        {
            try
            {
                using (HospitalManagementSystemDataContext objHmsDataContext = new HospitalManagementSystemDataContext(Utils.ConnectionString))
                {
                    if (objHmsDataContext.Connection.State == System.Data.ConnectionState.Closed)
                        objHmsDataContext.Connection.Open();

                    Report report = objHmsDataContext.Reports.SingleOrDefault(rprt => (rprt.Report_ID == reportBO.report_ID));

                    objHmsDataContext.Reports.DeleteOnSubmit(report);

                    objHmsDataContext.SubmitChanges();

                }
            }
            catch (Exception e)
            {
            }
        }

        public IEnumerable<ReportBO> ReqReport(int appId)
        {
            try
            {
                using (HospitalManagementSystemDataContext objHmsDataContext = new HospitalManagementSystemDataContext(Utils.ConnectionString))
                {
                    if (objHmsDataContext.Connection.State == System.Data.ConnectionState.Closed)
                        objHmsDataContext.Connection.Open();

                    Appointment appt = objHmsDataContext.Appointments.SingleOrDefault(rprt => (rprt.Appointment_ID == appId));
                    //.Select( r => r.RequestedReport = r.Re);
                    appt.RequestedReport = true;
                   // objHmsDataContext.Appointments.(appt);

                    objHmsDataContext.SubmitChanges();

                   return GetAllPatientsReports_DAL();
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public IEnumerable<ReportBO> ReqNewReport(AppointmentBO  appBO)
        {
            try
            {
                using (HospitalManagementSystemDataContext objHmsDataContext = new HospitalManagementSystemDataContext(Utils.ConnectionString))
                {
                    if (objHmsDataContext.Connection.State == System.Data.ConnectionState.Closed)
                        objHmsDataContext.Connection.Open();

                    Appointment appt = objHmsDataContext.Appointments.SingleOrDefault(rprt => (rprt.Appointment_ID == appBO.appointment_ID));
                    appt.RequestedReport = true;
                    appt.RequestedReportNotes = appBO.reqReportNotes;
                    objHmsDataContext.SubmitChanges();




                    return GetAllPatientsReports_DAL();
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }
        


        public IEnumerable<ReportBO> GetPatientReports_DAL(int patientID)
        {
            try
            {
                using (HospitalManagementSystemDataContext objHmsDataContext = new HospitalManagementSystemDataContext(Utils.ConnectionString))
                {
                    if (objHmsDataContext.Connection.State == System.Data.ConnectionState.Closed)
                        objHmsDataContext.Connection.Open();

                    IEnumerable<ReportBO> Reports = objHmsDataContext.Reports.Where(rprt => (rprt.Patient_ID == patientID)).
                        Join(objHmsDataContext.Patients,
                        r => r.Patient_ID,
                        p => p.Patient_ID,
                        (r,p) => new ReportBO 
                        { report_ID = r.Report_ID,
                          patient_ID = r.Patient_ID,
                          labIncharge_ID = r.LabIncharge_ID,
                          labResult = r.Lab_Result,
                          reportTime = Convert.ToString(r.Report_Time),
                          patientFirstName = p.First_Name,
                          appointment_ID = r.Appointment_ID,
                          date = r.Report_Date
                        }).ToArray();


                    foreach (var r in Reports)
                    {
                        r.reportRequested = objHmsDataContext.Appointments.Where(a => a.Appointment_ID == r.appointment_ID).Select(ap => ap.RequestedReportNotes).FirstOrDefault();
                    }
                    return Reports;
                }

            }
            catch 
            {
                IEnumerable<ReportBO> report_BO = null;
                return report_BO;
            }

        }

        public IEnumerable<ReportBO> GetAllPatientsReports_DAL()
        {
            try
            {
                using (HospitalManagementSystemDataContext objHmsDataContext = new HospitalManagementSystemDataContext(Utils.ConnectionString))
                {
                    if (objHmsDataContext.Connection.State == System.Data.ConnectionState.Closed)
                        objHmsDataContext.Connection.Open();


                    var Reports = objHmsDataContext.Reports
                        .Select(patientReports => new ReportBO {
                            report_ID = patientReports.Report_ID,
                              patient_ID = patientReports.Patient_ID,
                              labResult = patientReports.Lab_Result,
                              reportTime = Convert.ToString(patientReports.Report_Time),
                              date = patientReports.Report_Date}).GroupBy(p => p.patient_ID).First();


                    foreach (var rr in Reports)
                    {
                        rr.reportRequested = objHmsDataContext.Appointments.Where(a => a.Appointment_ID == rr.appointment_ID).Select(ap => ap.RequestedReportNotes).FirstOrDefault();
                    }
                    var r = objHmsDataContext.ExecuteCommand("select Report_ID, Patient_ID,LabIncharge_ID,Lab_Result,Report_Time,Report_Date from ( select *, row_Number() over(partition by Patient_ID order by Report_ID) as rnum from Report) source where rnum = 1; ");
                    return Reports;
                }
            }
            catch(Exception ex)
            {
                IEnumerable<ReportBO> report_BO = null;
                return report_BO;
            }
        }


        //View by Incharge ID
        public IEnumerable<ReportBO> ViewAllReports(ReportBO reportBO)
        {
            try
            {
                using (HospitalManagementSystemDataContext objHmsDataContext = new HospitalManagementSystemDataContext(Utils.ConnectionString))
                {
                    if (objHmsDataContext.Connection.State == System.Data.ConnectionState.Closed)
                        objHmsDataContext.Connection.Open();

                    IEnumerable<ReportBO> report = objHmsDataContext.Reports.Join(objHmsDataContext.Patients, 
                        p => p.Patient_ID,
                        r => r.Patient_ID,
                        (r,p) => new ReportBO {
                            appointment_ID = r.Appointment_ID,
                            patient_ID = r.Patient_ID,
                            labResult = r.Lab_Result,
                            date = r.Report_Date,
                            report_ID = r.Report_ID,
                            reportTime = Convert.ToString(r.Report_Time),
                            patientFirstName = p.First_Name 
                        }).ToList();

                    return report;
                }

            }
            catch (Exception e)
            {
                IEnumerable<ReportBO> report_BO = null;
                return report_BO;
            }
        }


    }
}
