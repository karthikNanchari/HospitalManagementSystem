using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace Client_Api
{
    public class LabIncharge_Api
    {

        public IEnumerable<ReportModel> GetAllPatientReports_Api()
        {
            IEnumerable<ReportModel> result = null;
            try
            {

                using (var client = new HttpClient())
                {

                    client.BaseAddress = new Uri(Utils.Url());

                    client.DefaultRequestHeaders.Clear();

                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage Res = client.GetAsync(Utils.Url() + "LabIncharge/GetAllPatientReports").Result;

                    if (Res.IsSuccessStatusCode)
                    {

                        var res = (Res.Content.ReadAsAsync<IEnumerable<ReportModel>>().Result);

                        result = res;

                    }
                }
                return result;
            }
            catch(Exception ex)
            {
                Utils.Logging(ex, 2);
                return result;
            }
        }

        public IEnumerable<ReportModel> GetPatientReports_Api(int patientReport)
        {
            IEnumerable<ReportModel> result = null;
            try
            {
                //int patientID = patientReport.patient_ID;
                using (var client = new HttpClient())
                {

                    client.BaseAddress = new Uri(Utils.Url());

                    client.DefaultRequestHeaders.Clear();

                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage Res = client.GetAsync(Utils.Url() + "LabIncharge/GetPatientReports?patientID=" + patientReport).Result;

                    if (Res.IsSuccessStatusCode)
                    {

                        var res = (Res.Content.ReadAsAsync<IEnumerable<ReportModel>>().Result);

                        result = res;

                    }
                }
                return result;
            }
            catch(Exception ex)
            {
                Utils.Logging(ex, 2);
                return result;
            }
        }

        public IEnumerable<ReportModel> UpdatePatientReport(ReportModel patientReport)
        {
            IEnumerable<ReportModel> result = null;
            try
            {
                using (var client = new HttpClient())
                {

                    client.BaseAddress = new Uri(Utils.Url());

                    client.DefaultRequestHeaders.Clear();

                    var jsonData = JsonConvert.SerializeObject(patientReport);

                    var buffer = System.Text.Encoding.UTF8.GetBytes(jsonData);

                    var byteContent = new ByteArrayContent(buffer);

                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    HttpResponseMessage Res =client.PostAsync(Utils.Url() + "LabIncharge/UpdatePatientReport", byteContent).Result;

                    if (Res.IsSuccessStatusCode && Res != null)
                    {

                        var res =  (Res.Content.ReadAsAsync<IEnumerable<ReportModel>>().Result);

                        result = res;

                    }
                }
                return result;
            }
            catch(Exception ex)
            {
                Utils.Logging(ex, 2);
                return result;
            }

        }


        //delete by report ID
        public void DeletePatientReport(ReportModel patientReport)
        {
            try
            {
                using (var client = new HttpClient())
                {

                    client.BaseAddress = new Uri(Utils.Url());

                    client.DefaultRequestHeaders.Clear();

                    var jsonData = JsonConvert.SerializeObject(patientReport);

                    var buffer = System.Text.Encoding.UTF8.GetBytes(jsonData);

                    var byteContent = new ByteArrayContent(buffer);

                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    HttpResponseMessage Res = client.PostAsync(Utils.Url() + "LabIncharge/DeletePatientReport", byteContent).Result;
                }
            }
            catch(Exception ex)
            {
                Utils.Logging(ex, 2);
            }

        }


        public ReportModel CreateNewReport(ReportModel patientReport)
        {
            ReportModel result = null;
            try
            {
                using (var client = new HttpClient())
                {

                    client.BaseAddress = new Uri(Utils.Url());

                    client.DefaultRequestHeaders.Clear();

                    var jsonData = JsonConvert.SerializeObject(patientReport);

                    var buffer = System.Text.Encoding.UTF8.GetBytes(jsonData);

                    var byteContent = new ByteArrayContent(buffer);

                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    HttpResponseMessage Res = client.PostAsync(Utils.Url() + "LabIncharge/CreateNewReport", byteContent).Result;

                    if (Res.IsSuccessStatusCode && Res != null)
                    {

                        var res = (Res.Content.ReadAsAsync<ReportModel>().Result);

                        result = res;

                    }
                }
                return result;
            }
            catch(Exception ex)
            {
                Utils.Logging(ex, 2);
                return result;
            }

        }

        

       public IEnumerable<AppointmentModel> ReqReport(int appID)
        {
            IEnumerable<AppointmentModel> result = null;
            try
            {
                using (var client = new HttpClient())
                {

                    client.BaseAddress = new Uri(Utils.Url());

                    client.DefaultRequestHeaders.Clear();

                    AppointmentModel appModel = new AppointmentModel();
                    appModel.appointment_ID = appID;

                    var jsonData = JsonConvert.SerializeObject(appModel);

                    var buffer = System.Text.Encoding.UTF8.GetBytes(jsonData);

                    var byteContent = new ByteArrayContent(buffer);

                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    HttpResponseMessage Res = client.PostAsync(Utils.Url() + "LabIncharge/ReqReport", byteContent).Result;

                    if (Res.IsSuccessStatusCode && Res != null)
                    {

                        var res = (Res.Content.ReadAsAsync<IEnumerable<AppointmentModel>>().Result);

                        result = res;

                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                Utils.Logging(ex, 2);
                return result;
            }

        }


        public IEnumerable<AppointmentModel> ReqNewReport(AppointmentModel appModel)
        {
            IEnumerable<AppointmentModel> result = null;
            try
            {
                using (var client = new HttpClient())
                {

                    client.BaseAddress = new Uri(Utils.Url());

                    client.DefaultRequestHeaders.Clear();

                    var jsonData = JsonConvert.SerializeObject(appModel);

                    var buffer = System.Text.Encoding.UTF8.GetBytes(jsonData);

                    var byteContent = new ByteArrayContent(buffer);

                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    HttpResponseMessage Res = client.PostAsync(Utils.Url() + "LabIncharge/ReqNewReport", byteContent).Result;

                    if (Res.IsSuccessStatusCode && Res != null)
                    {

                        var res = (Res.Content.ReadAsAsync<IEnumerable<AppointmentModel>>().Result);

                        result = res;

                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                Utils.Logging(ex, 2);
                return result;
            }

        }

        

        public IEnumerable<ReportModel> ViewPatientReports_Api(ReportModel reportModel)
        {
            IEnumerable<ReportModel> result = null;
            try
            {
                using (var client = new HttpClient())
                {

                    client.BaseAddress = new Uri(Utils.Url());

                    client.DefaultRequestHeaders.Clear();

                    var jsonData = JsonConvert.SerializeObject(reportModel);

                    var buffer = System.Text.Encoding.UTF8.GetBytes(jsonData);

                    var byteContent = new ByteArrayContent(buffer);

                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    HttpResponseMessage Res = client.PostAsync(Utils.Url() + "LabIncharge/ViewPatientReports", byteContent).Result;

                    if (Res.IsSuccessStatusCode && Res != null)
                    {

                        var res = (Res.Content.ReadAsAsync<IEnumerable<ReportModel>>().Result);

                        result = res;

                    }
                }
                return result;
            }
            catch(Exception ex)
            {
                Utils.Logging(ex, 2);
                return result;
            }
        }

        
    }
}
