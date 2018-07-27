using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BusinessLogicLayerFactory;
using BusinessObject;

namespace HospitalManagementSystemServer.Controllers
{
    public class NurseController : ApiController
    {
        // GET: api/Nurse/5
        //Get action which accepts int and string as parameters, with nurse business object as return type
        public NurseBO Get(int id , string pwd)
        {
            NurseBLLFactory nurseBLLFactory = new NurseBLLFactory();

            NurseBO nurseBO = new NurseBO();
            nurseBO.pid = id;
            nurseBO.pwd = pwd;

            nurseBO = nurseBLLFactory.CreateNurseBLL().GetNurseDetails().CreateNurseDAL().GetNurseDetails_DAL(nurseBO);
            return nurseBO;

        }

        // POST: api/Nurse
        //Post action which accepts nurse buisness object as parameter and void as return type
        public void Post([FromBody]NurseBO nurseBO)
        {
            NurseBLLFactory nurseBLLFactory = new NurseBLLFactory();

            NurseBO nurse_BO = nurseBLLFactory.CreateNurseBLL().InsertNewNurseBLL().CreateNurseDAL().InsertNurseDetails_DAL(nurseBO);
        }

        [HttpPost, Route("api/Nurse/RegisterNewNurse")]
        public string RegisterNewNurse(NurseBO nurseBO)
        {
            NurseBLLFactory nurseBLLFactory = new NurseBLLFactory();

            NurseBO nurse_BO = nurseBLLFactory.CreateNurseBLL().InsertNewNurseBLL().CreateNurseDAL().InsertNurseDetails_DAL(nurseBO);
            if(nurse_BO.pid != 0)
            {
                return ("Your new User ID " + Convert.ToString(nurse_BO.pid) + " - Login Using this UserID");
            }
            return ("Email ID is already Used by other User, try using other EmailID");
        }


        // PUT: api/Nurse/5
        //Put action, which accepts int and nurse Business object as parameters and void as return type
        public void Put(int id, [FromBody]NurseBO nurseBO)
        {

            NurseBLLFactory nurseBLLFactory = new NurseBLLFactory();

            string nurse = nurseBLLFactory.CreateNurseBLL().UpdateNurseDetailsBLL().CreateNurseDAL().UpdateDetails_DAL(nurseBO);

            string activateNurse = nurseBLLFactory.CreateNurseBLL().ActivateNurseBLL().CreateNurseDAL().ActivateAccount_DAL(nurseBO);

            string deactivateNurse = nurseBLLFactory.CreateNurseBLL().DeActivateNurseBLL().CreateNurseDAL().DeActivateAccount_DAL(nurseBO);

        }




    }
}
