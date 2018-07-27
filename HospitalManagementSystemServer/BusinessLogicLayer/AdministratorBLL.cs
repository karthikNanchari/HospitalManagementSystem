using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayerFactory;
namespace BusinessLogicLayer
{
   public class AdministratorBLL
    {

        public AdministratorDALFactory GetAllPatientsBLL() {
            return new AdministratorDALFactory();

        }

        public AdministratorDALFactory UpdatePatientDetailsBLL()
        {
            return new AdministratorDALFactory();
        }
        public AdministratorDALFactory ManageNotificationsBLL()
        {
            return new AdministratorDALFactory();
        }

        public AccountDALFactory ManageAccountsBLL()
        {
            return new AccountDALFactory();
        }
        //Method to insert New Admin , with return type AdminstratorDalFactory
        public AdministratorDALFactory InsertNewAdminBLL()
        {
            return new AdministratorDALFactory();
        }

        //Method to Update Admin Details , with return type AdminstratorDalFactory
        public AdministratorDALFactory UpdateAdminDetailsBLL()
        {
            return new AdministratorDALFactory();
        }

        //Method to Activate Admin , with return type AdminstratorDalFactory
        public AdministratorDALFactory ActivateAdminBLL()
        {
            return new AdministratorDALFactory();
        }

        //Method to DeActivate Admin when requested , with return type AdminstratorDalFactory
        public AdministratorDALFactory DeActivateAdminBLL()
        {
            return new AdministratorDALFactory();
        }

        //Method to Get Admin Details , with return type AdminstratorDalFactory
        public AdministratorDALFactory GetAdminDetailsBLL()
        {
            return new AdministratorDALFactory();
        }

        //Method to Validate Admin , with return type AdminstratorDalFactory
        public AdministratorDALFactory ValidateAdminBLL()
        {
            return new AdministratorDALFactory();
        }

        public AdministratorDALFactory ManageNotificationBLL()
        {
            return new AdministratorDALFactory();
        }
    }
}
