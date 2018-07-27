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
    public class LabInchargeController : ApiController
    {

        // GET: api/LabIncharge/5
        //Get Action which accepts int and string parameter, return type lab incharge business object
        public LabInchargeBO Get(int id, string pwd)
        {
            LabInchargeBLLFactory inchargeBLLFactory = new LabInchargeBLLFactory();

            LabInchargeBO incharge_BO = new LabInchargeBO();
            incharge_BO.pid = id;
            incharge_BO.pwd = pwd;

            LabInchargeBO inchargeBO = inchargeBLLFactory.CreateLabInchargeBLL().GetLabInchargeBLL().CreateLabInchargeDAL().GetLabInchargeDetials_DAL(incharge_BO);

            return inchargeBO;
        }

        // POST: api/LabIncharge
        //Post action, which accepts Labincharge business object as parameter and same as return type
        public LabInchargeBO Post([FromBody]LabInchargeBO inchargeBO)
        {
            LabInchargeBLLFactory inchargeBLLFactory = new LabInchargeBLLFactory();

            LabInchargeBO updates = inchargeBLLFactory.CreateLabInchargeBLL().UpdateLabInchargeBLL().CreateLabInchargeDAL().InsertInchargeDetails_DAL(inchargeBO);

            return updates;
        }

        [HttpPost,Route("api/LabIncharge/RegisterNewIncharge")]
        public string RegisterNewIncharge([FromBody]LabInchargeBO inchargeBO)
        {
            LabInchargeBLLFactory inchargeBLLFactory = new LabInchargeBLLFactory();

            LabInchargeBO updates = inchargeBLLFactory.CreateLabInchargeBLL().UpdateLabInchargeBLL().CreateLabInchargeDAL().InsertInchargeDetails_DAL(inchargeBO);
            if (updates.pid != 0)
            {
                return ("Your new User ID " + Convert.ToString(updates.pid) + "- Login Using this UserID");
            }
            return ("Email ID is already Used by other User, try using other EmailID");
        }

        // PUT: api/LabIncharge/5
        //Put action which accepts int and labincharge business object as parameter, void as return type
        public void Put(int id, [FromBody]LabInchargeBO inchargeBO)
        {
            LabInchargeBLLFactory inchargeBLLFactory = new LabInchargeBLLFactory();

            string updates = inchargeBLLFactory.CreateLabInchargeBLL().UpdateLabInchargeBLL().CreateLabInchargeDAL().UpdateLabInchargeDetails_DAL(inchargeBO);

            string Activate = inchargeBLLFactory.CreateLabInchargeBLL().ActivateLabInchargeBLL().CreateLabInchargeDAL().ActivateLabIncharge_DAL(inchargeBO);


            string validate = inchargeBLLFactory.CreateLabInchargeBLL().ValidateLabInchargeBLL().CreateLabInchargeDAL().ValidateLabIncharge_DAL(inchargeBO);

            string Deactive = inchargeBLLFactory.CreateLabInchargeBLL().DeActivateLabInchargeBLL().CreateLabInchargeDAL().DeactivateLabIncharge_DAL(inchargeBO);

            string isavailable = inchargeBLLFactory.CreateLabInchargeBLL().IsAvailableEmailBLL().CreateLabInchargeDAL().CheckAvailability_DAL(inchargeBO);
        }

        [HttpGet, Route("api/LabIncharge/GetPatientReports")]
        public IEnumerable<ReportBO> GetPatientReports(int patientID) {
            LabInchargeBLLFactory inchargeBLLFactory = new LabInchargeBLLFactory();

            return inchargeBLLFactory.CreateLabInchargeBLL().GetPatientReportsBLL().CreateReportDAL().GetPatientReports_DAL(patientID);
        }

        [HttpGet, Route("api/LabIncharge/GetAllPatientReports")]
        public IEnumerable<ReportBO> GetAllPatientReports() {

            LabInchargeBLLFactory inchargeBLLFactory = new LabInchargeBLLFactory();

            return inchargeBLLFactory.CreateLabInchargeBLL().GetPatientReportsBLL().CreateReportDAL().GetAllPatientsReports_DAL();
        }

        [Route("api/LabIncharge/UpdatePatientReport")]
        public IEnumerable<ReportBO> UpdatePatientReport(ReportBO patientReport)
        {
            LabInchargeBLLFactory inchargeBLLFactory = new LabInchargeBLLFactory();

            return inchargeBLLFactory.CreateLabInchargeBLL().GetPatientReportsBLL().CreateReportDAL().UpdatePatientReport_DAL(patientReport);

        }


        [Route("api/LabIncharge/CreateNewReport")]
        public ReportBO CreateNewReport(ReportBO patientReport)
        {
            LabInchargeBLLFactory inchargeBLLFactory = new LabInchargeBLLFactory();

            return inchargeBLLFactory.CreateLabInchargeBLL().GetPatientReportsBLL().CreateReportDAL().InsertNewReport(patientReport);

        }


        [HttpGet, Route("api/LabIncharge/ViewAllReportsByDoctor")]
        public IEnumerable<ReportBO> ViewAllReportsByDoctor(DoctorBO doctor)
        {

            LabInchargeBLLFactory inchargeBLLFactory = new LabInchargeBLLFactory();

            return inchargeBLLFactory.CreateLabInchargeBLL().GetPatientReportsBLL().CreateReportDAL().GetAllPatientsReports_DAL();
        }

        





    }
}
