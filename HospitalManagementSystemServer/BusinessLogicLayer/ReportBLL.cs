using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayerFactory;

namespace BusinessLogicLayer
{
    public class ReportBLL
    {
        //Method to Create Report, with return type Report Dal Factory
        public ReportDALFactory CreateReportBLL()
        {
            return new ReportDALFactory();
        }

        //Method to Get Report, with return type Report Dal Factory
        public ReportDALFactory GetReportBLL()
        {
            return new ReportDALFactory();
        }

        //Method to update Report, with return type Report Dal Factory
        public ReportDALFactory UpdateReportBLL()
        {
            return new ReportDALFactory();
        }

        //Method to Delete Report, with return type Report Dal Factory
        public ReportDALFactory DeleteReportBLL()
        {
            return new ReportDALFactory();
        }


    }
}
