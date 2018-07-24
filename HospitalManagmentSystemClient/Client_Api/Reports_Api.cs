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
    public class Reports_Api
    {
        public IEnumerable<ReportModel> GetAllReports()
        {
            IEnumerable<ReportModel> result = null;
            try
            {
                using (var client = new HttpClient())
                {

                    client.BaseAddress = new Uri(Utils.Url());

                    client.DefaultRequestHeaders.Clear();

                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage Res = client.GetAsync(Utils.Url() + "Report/GetAllReports").Result;

                    if (Res.IsSuccessStatusCode)
                    {

                        var res = (Res.Content.ReadAsAsync<IEnumerable<ReportModel>>().Result);

                        result = res;
                        Utils.Logging(Environment.StackTrace, null);
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
        
        public IEnumerable<ReportModel> GetReportsByDoctor(DoctorModel doctor)
        {
            IEnumerable<ReportModel> result = null;
            try
            {
                using (var client = new HttpClient())
                {

                    client.BaseAddress = new Uri(Utils.Url());
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var jsonData = JsonConvert.SerializeObject(doctor);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(jsonData);
                    var byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    HttpResponseMessage Res = client.GetAsync(Utils.Url() + "Report/GetReportsByDoctor").Result;

                    if (Res.IsSuccessStatusCode)
                    {

                        var res = (Res.Content.ReadAsAsync<IEnumerable<ReportModel>>().Result);

                        result = res;
                        Utils.Logging(Environment.StackTrace, null);
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

        public IEnumerable<ReportModel> GetReportsByPatient(PatientModel patient)
        {
            IEnumerable<ReportModel> result = null;
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Utils.Url());
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var jsonData = JsonConvert.SerializeObject(patient);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(jsonData);
                    var byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    HttpResponseMessage Res = client.PostAsync(Utils.Url() + "Report/GetAllReportsByPatient", byteContent).Result;

                    if (Res.IsSuccessStatusCode)
                    {
                        var res = (Res.Content.ReadAsAsync<IEnumerable<ReportModel>>().Result);
                        result = res;
                        Utils.Logging(Environment.StackTrace, null);
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

        public ReportModel GetReportsByAppt(int id)
        {
            ReportModel result = null;
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Utils.Url());
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    AppointmentModel app = new AppointmentModel();
                    app.appointment_ID = id;
                    var jsonData = JsonConvert.SerializeObject(app);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(jsonData);
                    var byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    HttpResponseMessage Res = client.PostAsync(Utils.Url() + "Report/GetReportsByAppt", byteContent).Result;

                    if (Res.IsSuccessStatusCode)
                    {
                        var res = (Res.Content.ReadAsAsync<ReportModel>().Result);
                        result = res;
                        Utils.Logging(Environment.StackTrace, null);
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


        public IEnumerable<ReportModel> UpdateReports(ReportModel reportModel)
        {
            IEnumerable<ReportModel> payments = null;
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Utils.Url());
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var jsonData = JsonConvert.SerializeObject(reportModel);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(jsonData);
                    var byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    HttpResponseMessage Res = client.PostAsync(Utils.Url() + "LabIncharge/UpdatePatientReport", byteContent).Result;
                    if (Res.IsSuccessStatusCode)
                    {
                        payments = (Res.Content.ReadAsAsync<IEnumerable<ReportModel>>().Result);
                        return payments;
                    }
                }
                return payments;
            }
            catch (Exception ex)
            {
                Utils.Logging(ex, 2);
                return payments;
            }
        }


        public IEnumerable<ReportModel> CreateReport(ReportModel reportModel)
        {
            IEnumerable<ReportModel> payments = null;
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Utils.Url());
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var jsonData = JsonConvert.SerializeObject(reportModel);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(jsonData);
                    var byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    HttpResponseMessage Res = client.PostAsync(Utils.Url() + "Report/UpdateReport", byteContent).Result;
                    if (Res.IsSuccessStatusCode)
                    {
                        payments = (Res.Content.ReadAsAsync<IEnumerable<ReportModel>>().Result);
                        return payments;
                    }
                }
                return payments;
            }
            catch (Exception ex)
            {
                Utils.Logging(ex, 2);
                return payments;
            }
        }


    }
}
