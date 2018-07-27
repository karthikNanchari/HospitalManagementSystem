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
    public class ReportController : ApiController
    {

        // GET: api/Report/5
        //Get action which accepts int as input parameter and report business object as return type
        public ReportBO Get(int id)
        {
            ReportBO reportBO = new ReportBO();
            reportBO.report_ID = id;

            ReportBLLFactory rprtBLLFactory = new ReportBLLFactory();

            ReportBO rprtBO = rprtBLLFactory.CreateReportBLL().GetReportBLL().CreateReportDAL().GetReport(reportBO);


            return rprtBO;
        }

        // POST: api/Report
        //Post action which accepts report business object as input parameter and void as return type
        public void Post([FromBody]ReportBO reportBO)
        {
            ReportBO report_BO = new ReportBO();

            report_BO.labIncharge_ID = 4001;
            report_BO.patient_ID = 7002;
            report_BO.reportTime = "08:08:08";
            report_BO.date = DateTime.Parse("2017/08/15");

            ReportBLLFactory rprtBLLFactory = new ReportBLLFactory();

            ReportBO rprt = rprtBLLFactory.CreateReportBLL().CreateReportBLL().CreateReportDAL().InsertNewReport(report_BO);

        }

        // PUT: api/Report/5
        //Put action which accepts int and report business object as input parameter and void as return type
        public void Put(int id, [FromBody]ReportBO reportBO)
        {
            ReportBO report_BO = new ReportBO();

            report_BO.labIncharge_ID = 4001;
            report_BO.patient_ID = 7002;
            report_BO.reportTime = "10:10:10";
            report_BO.date = DateTime.Parse("2010/10/10");

            ReportBLLFactory rprtBLLFactory = new ReportBLLFactory();

            var rprt = rprtBLLFactory.CreateReportBLL().UpdateReportBLL().CreateReportDAL().UpdatePatientReport_DAL(report_BO);

        }

        // DELETE: api/Report/5
        //Delete action which accepts int as input parameter and void as return type
        public void Delete(int id)
        {
            ReportBO report_BO = new ReportBO();

            report_BO.report_ID = id;

            ReportBLLFactory rprtBLLFactory = new ReportBLLFactory();

            string rprt = rprtBLLFactory.CreateReportBLL().DeleteReportBLL().CreateReportDAL().DeleteRepoprt(report_BO);

        }

    }
}
