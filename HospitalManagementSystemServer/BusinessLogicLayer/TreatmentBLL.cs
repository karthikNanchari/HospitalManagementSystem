using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayerFactory;
namespace BusinessLogicLayer
{
  public  class TreatmentBLL
    {
        //Method to create Treatment details i.e insert, with return type Treatment DAl Factory
        public TreatmentDALFactory CreateTreatmentBLL()
        {
            return new TreatmentDALFactory();
        }

        //Method to Get Treatment details, with return type Treatment DAl Factory
        public TreatmentDALFactory GetTreatmentBLL()
        {
            return new TreatmentDALFactory();
        }

        //Method to Update Treatment details, with return type Treatment DAl Factory
        public TreatmentDALFactory UpdateTreatmentBLL()
        {
            return new TreatmentDALFactory();
        }

        //Method to Delete Treatment details, with return type Treatment DAl Factory
        public TreatmentDALFactory DeleteTreatmentBLL()
        {
            return new TreatmentDALFactory();
        }

    }
}
