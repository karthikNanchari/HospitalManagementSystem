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
    public class TreatmentController : ApiController
    {
        // GET: api/Treatment/5
        //Get action which accepts int as input parameter and treatment business object as return type
        public TreatmentBO Get(int id)
        {
            TreatmentBO treatmentBO = new TreatmentBO();

            TreatmentBLLFactory trmntBLLFactory = new TreatmentBLLFactory();

            TreatmentBO trmntBO = trmntBLLFactory.CreateTreatmentBLL().GetTreatmentBLL().CreateTreatmentDAL().GetTreatment_DAL(treatmentBO);

            return trmntBO;

        }

        // POST: api/Treatment
        //Post action which accepts treatment business object as input parameter and void as return type
        public void Post([FromBody]TreatmentBO trmnt)
        {
            TreatmentBO treatmentBO = new TreatmentBO();
            treatmentBO.doctor_ID = 5006;
            treatmentBO.patient_ID = 7002;
            treatmentBO.nurse_ID = 3004;

            TreatmentBLLFactory trmntBLLFactory = new TreatmentBLLFactory();

            TreatmentBO trmntBO = trmntBLLFactory.CreateTreatmentBLL().CreateTreatmentBLL().CreateTreatmentDAL().InsertNewTreatment_DAL(treatmentBO);

        }

        // PUT: api/Treatment/5
        //Put action which accepts int and string as input parameter with void as return type
        public void Put(int id, [FromBody]string value)
        {
            TreatmentBO treatmentBO = new TreatmentBO();

            TreatmentBLLFactory trmntBLLFactory = new TreatmentBLLFactory();

            string updates = trmntBLLFactory.CreateTreatmentBLL().UpdateTreatmentBLL().CreateTreatmentDAL().UpdateTreatment_DAL(treatmentBO);
        }

        // DELETE: api/Treatment/5
        //Deleter action with int as input parameter and void as return type
        public void Delete(int id)
        {
            TreatmentBO treatmentBO = new TreatmentBO();

            TreatmentBLLFactory trmntBLLFactory = new TreatmentBLLFactory();

            string dleTreatment = trmntBLLFactory.CreateTreatmentBLL().DeleteTreatmentBLL().CreateTreatmentDAL().DeleteTreatment_DAL(treatmentBO);

        }
    }
}
