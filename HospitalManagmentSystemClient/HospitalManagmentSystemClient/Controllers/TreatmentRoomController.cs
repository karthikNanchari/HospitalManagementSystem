using Client_Api;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HospitalManagmentSystemClient.Controllers
{
    public class TreatmentRoomController : Controller
    {
   

        public ActionResult GetAllTreatmentRooms()
        {
            TreatmentRoom_Api roomApi = new TreatmentRoom_Api();
            var model = roomApi.GetAllTreatmentRooms();
            return View("~Views/TreatmentRoom/ViewTreatmentRooms.cshtml", model);
        }

        public ActionResult BookRoomToPaitent(TreatmentRoomModel roomModel)
        {
            TreatmentRoom_Api roomApi = new TreatmentRoom_Api();
            var model = roomApi.BookTreatmentRoom(roomModel);
            return View("~Views/TreatmentRoom/ViewTreatmentRooms.cshtml", model);
        }

        public ActionResult BookRoomToPaitent()
        {
            ViewBag.AvailableRooms = new SelectList(AvailableRooms());
            return View("~Views/TreatmentRoom/ViewTreatmentRooms.cshtml");
        }

        public ActionResult UpdateTreatmentRoom(TreatmentRoomModel roomModel)
        {
            TreatmentRoom_Api roomApi = new TreatmentRoom_Api();
            var model = roomApi.UpdateTreatmentRoom(roomModel);
            return View("~Views/TreatmentRoom/ViewTreatmentRooms.cshtml", model);
        }


        public IEnumerable<string> AvailableRooms()
        {
            TreatmentRoom_Api roomApi = new TreatmentRoom_Api();
            var model = roomApi.GetAllTreatmentRooms();

           var rooms = model.Where(r => r.isBooked == false).Select(r => r.room_ID);
            return rooms;
        }


    }
}