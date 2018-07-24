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
    public class Doctor_Api
    {

        public IEnumerable<AppointmentModel> GetDocApntment_Api(int id) {

            IEnumerable<AppointmentModel> result = null;
            try
            {
                using (var client = new HttpClient())
                {

                    DoctorModel doc = new DoctorModel();
                    doc.pid = id;
                    client.BaseAddress = new Uri(Utils.Url());

                    client.DefaultRequestHeaders.Clear();

                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var jsonData = JsonConvert.SerializeObject(doc);

                    var buffer = System.Text.Encoding.UTF8.GetBytes(jsonData);

                    var byteContent = new ByteArrayContent(buffer);

                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    HttpResponseMessage Res = client.PostAsync(Utils.Url() + "Doctor/GetDoctorAppointments", byteContent).Result;

                    if (Res.IsSuccessStatusCode)
                    {

                        var res = (Res.Content.ReadAsAsync<IEnumerable<AppointmentModel>>().Result);

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

        public AppointmentModel BookDocApnmt_Api(AppointmentModel apmnt)
        {
            AppointmentModel newAppointment = null;
            try
            {
                
                using (var client = new HttpClient())
                {

                    client.BaseAddress = new Uri(Utils.Url());

                    client.DefaultRequestHeaders.Clear();

                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var jsonData = JsonConvert.SerializeObject(apmnt);

                    var buffer = System.Text.Encoding.UTF8.GetBytes(jsonData);

                    var byteContent = new ByteArrayContent(buffer);

                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    HttpResponseMessage Res = client.PostAsync(Utils.Url() + "Doctor/BookAppointment", byteContent).Result;

                    if (Res.IsSuccessStatusCode)
                    {
                         newAppointment = (Res.Content.ReadAsAsync<AppointmentModel>().Result);

                        return newAppointment;
                    }
                }
                return newAppointment;
            }
            catch(Exception ex)
            {
                Utils.Logging(ex, 2);
                return newAppointment;
            }

        }


        public IEnumerable<AppointmentModel> cancelAppointment(AppointmentModel apmnt)
        {
            IEnumerable<AppointmentModel> newAppointment = null;
            try
            {

                using (var client = new HttpClient())
                {

                    client.BaseAddress = new Uri(Utils.Url());

                    client.DefaultRequestHeaders.Clear();

                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var jsonData = JsonConvert.SerializeObject(apmnt);

                    var buffer = System.Text.Encoding.UTF8.GetBytes(jsonData);

                    var byteContent = new ByteArrayContent(buffer);

                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    HttpResponseMessage Res = client.PostAsync(Utils.Url() + "Doctor/cancelAppointment", byteContent).Result;

                    if (Res.IsSuccessStatusCode)
                    {
                        newAppointment = (Res.Content.ReadAsAsync<IEnumerable<AppointmentModel>>().Result);

                        return newAppointment;
                    }
                }
                return newAppointment;
            }
            catch (Exception ex)
            {
                Utils.Logging(ex, 2);
                return newAppointment;
            }

        }


        public IEnumerable<AppointmentModel> cancelAllAppointmentByDate(AppointmentModel apmnt)
        {
            IEnumerable<AppointmentModel> newAppointment = null;
            try
            {

                using (var client = new HttpClient())
                {

                    client.BaseAddress = new Uri(Utils.Url());

                    client.DefaultRequestHeaders.Clear();

                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var jsonData = JsonConvert.SerializeObject(apmnt);

                    var buffer = System.Text.Encoding.UTF8.GetBytes(jsonData);

                    var byteContent = new ByteArrayContent(buffer);

                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    HttpResponseMessage Res = client.PostAsync(Utils.Url() + "Doctor/cancelAllAppointmentByDate", byteContent).Result;

                    if (Res.IsSuccessStatusCode)
                    {
                        //    newAppointment = (Res.Content.ReadAsAsync<IEnumerable<AppointmentModel>>().Result);

                        //    return newAppointment;
                    }
                }
                return newAppointment;
            }
            catch (Exception ex)
            {
                Utils.Logging(ex, 2);
                return newAppointment;
            }

        }
        


        public List<string> GetAvailableTimings(AppointmentModel appointmentModel)
        {
            List<string> result = null;
            try
            {
                using (var client = new HttpClient())
                {

                    client.BaseAddress = new Uri(Utils.Url());

                    client.DefaultRequestHeaders.Clear();

                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var jsonData = JsonConvert.SerializeObject(appointmentModel);

                    var buffer = System.Text.Encoding.UTF8.GetBytes(jsonData);

                    var byteContent = new ByteArrayContent(buffer);

                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    HttpResponseMessage Res = client.PostAsync(Utils.Url() + "Doctor/GetAvailableTimings", byteContent).Result;

                    if (Res.IsSuccessStatusCode)
                    {

                        var res = (Res.Content.ReadAsAsync<IEnumerable<string>>().Result);

                        result = res.ToList();

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

        public List<string> GetAvailableTimingsForPatient(AppointmentModel appointmentModel)
        {
            List<string> result = null;
            try
            {
                using (var client = new HttpClient())
                {

                    client.BaseAddress = new Uri(Utils.Url());

                    client.DefaultRequestHeaders.Clear();

                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var jsonData = JsonConvert.SerializeObject(appointmentModel);

                    var buffer = System.Text.Encoding.UTF8.GetBytes(jsonData);

                    var byteContent = new ByteArrayContent(buffer);

                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    HttpResponseMessage Res = client.PostAsync(Utils.Url() + "Doctor/GetAvailableTimingsForPatient", byteContent).Result;

                    if (Res.IsSuccessStatusCode)
                    {

                        var res = (Res.Content.ReadAsAsync<IEnumerable<string>>().Result);

                        result = res.ToList();

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

        public IEnumerable<DoctorModel> GetDoctorById(DoctorModel doctorModel)
        {
            IEnumerable<DoctorModel> patientDetails = null;
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Utils.Url());
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var jsonData = JsonConvert.SerializeObject(doctorModel);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(jsonData);
                    var byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    HttpResponseMessage Res = client.PostAsync(Utils.Url() + "Doctor/GetDoctorById", byteContent).Result;
                    if (Res.IsSuccessStatusCode)
                    {
                        patientDetails = (Res.Content.ReadAsAsync<IEnumerable<DoctorModel>>().Result);
                        return patientDetails;
                    }
                }
                return patientDetails;
            }
            catch (Exception ex)
            {
                Utils.Logging(ex, 2);
                return patientDetails;
            }
        }
    }
}
