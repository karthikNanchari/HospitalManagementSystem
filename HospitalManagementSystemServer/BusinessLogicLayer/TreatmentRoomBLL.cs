using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayerFactory;

namespace BusinessLogicLayer
{
    public class TreatmentRoomBLL
    {
        //Method to Create Treatment room details i.e insert, with return type Treatment DAl Factory
        public TreatmentRoomDALFactory CreateTreatmentRoomBLL()
        {
            return new TreatmentRoomDALFactory();
        }

        //Method to update Treatment room details, with return type Treatment DAl Factory
        public TreatmentRoomDALFactory UpdateTreatmentRoomBLL()
        {
            return new TreatmentRoomDALFactory();
        }

        //Method to get Treatment room details, with return type Treatment DAl Factory
        public TreatmentRoomDALFactory GetTreatmentRoomBLL()
        {
            return new TreatmentRoomDALFactory();
        }

        //Method to Delete Treatment room details, with return type Treatment DAl Factory
        public TreatmentRoomDALFactory DeleteTreatmentRoomBLL()
        {
            return new TreatmentRoomDALFactory();
        }
    }
}
