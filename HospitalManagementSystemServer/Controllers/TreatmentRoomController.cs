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
    /// <summary>
    /// Treatment room controller which accepts rest service calls
    /// </summary>
    public class TreatmentRoomController : ApiController
    {

        // GET: api/TreatmentRoom/5
        //Get action which accepts int input parameter and treatment business object as return type 
        public TreatmentRoomBO Get(string id)
        {
            TreatmentRoomBO treatmentRoomBO = new TreatmentRoomBO();
            treatmentRoomBO.room_ID = id;

            TreatmentRoomBLLFactory trmntroomBLLFactory = new TreatmentRoomBLLFactory();

            TreatmentRoomBO trmntBO = trmntroomBLLFactory.CreateTreatmentRoomBLL().GetTreatmentRoomBLL().CreateTreatmentRoomDAL().GetTreatmentRoom(treatmentRoomBO);
            return trmntBO;
        }

        // POST: api/TreatmentRoom
        //Post action which accepts treatment room business object as input parameter and void as return type
        public void BookTreatmentRoom(TreatmentRoomBO treatmentRoomBO)
        {
            TreatmentRoomBLLFactory trmntroomBLLFactory = new TreatmentRoomBLLFactory();

            TreatmentRoomBO trmntBO = trmntroomBLLFactory.CreateTreatmentRoomBLL().CreateTreatmentRoomBLL().CreateTreatmentRoomDAL().InsertTreatmentRoom(treatmentRoomBO);         
        }

        // PUT: api/TreatmentRoom/5
        //Put action which accepts treatment room business object as input parameter and string as return type
        [Route("api/TreatmentRoom/UpdateTreatmentRoom")]
        public string UpdateTreatmentRoom(TreatmentRoomBO trmntBO)
        {
            TreatmentRoomBLLFactory trmntroomBLLFactory = new TreatmentRoomBLLFactory();

            string result = trmntroomBLLFactory.CreateTreatmentRoomBLL().UpdateTreatmentRoomBLL().CreateTreatmentRoomDAL().UpdateTreatmentRoom(trmntBO);
            return result;
        }

        // DELETE: api/TreatmentRoom/5
        //Delete action which accpets int as input parameter and string as return type
        public string Delete(string id)
        {
            TreatmentRoomBO treatmentRoomBO = new TreatmentRoomBO();
            treatmentRoomBO.room_ID = id;

            TreatmentRoomBLLFactory trmntroomBLLFactory = new TreatmentRoomBLLFactory();

            string result = trmntroomBLLFactory.CreateTreatmentRoomBLL().DeleteTreatmentRoomBLL().CreateTreatmentRoomDAL().DeleteTreatmentRoom(treatmentRoomBO);
            return result;
        }

        [Route("api/TreatmentRoom/GetAllTreatmentRooms")]
        public IEnumerable<TreatmentRoomBO> GetAllTreatmentRooms()
        {
            TreatmentRoomBLLFactory trmntroomBLLFactory = new TreatmentRoomBLLFactory();

            return trmntroomBLLFactory.CreateTreatmentRoomBLL().DeleteTreatmentRoomBLL().CreateTreatmentRoomDAL().GetAllTreatmentRooms();
        }

    }
}
