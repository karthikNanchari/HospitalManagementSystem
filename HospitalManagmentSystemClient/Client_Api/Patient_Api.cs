using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Formatting;
using Models;
using Newtonsoft.Json;

namespace Client_Api
{
    public class Patient_Api
    {
        public IEnumerable<DoctorModel> GetDoctors()
        {
            IEnumerable<DoctorModel> result = null;
            try { 

                using (var client = new HttpClient())
                {

                    client.BaseAddress = new Uri(Utils.Url());

                    client.DefaultRequestHeaders.Clear();

                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage Res = client.GetAsync(Utils.Url() + "Patient").Result;

                    if (Res.IsSuccessStatusCode)
                    {

                        var res = (Res.Content.ReadAsAsync<IEnumerable<DoctorModel>>().Result);
                        result = res;
                    }
                }
            }
            catch (Exception ex)
            {
                Utils.Logging(ex, 2);
                return result;
            }
            return result;
        }

        public IEnumerable<int> GetDoctorIDs()
        {
            IEnumerable<int> result = null;
            try
            {
                using (var client = new HttpClient())
                {

                    client.BaseAddress = new Uri(Utils.Url());

                    client.DefaultRequestHeaders.Clear();

                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage Res = client.GetAsync(Utils.Url() + "Patient/GetDoctorIDs").Result;

                    if (Res.IsSuccessStatusCode)
                    {

                        var res = (Res.Content.ReadAsAsync<IEnumerable<int>>().Result);

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

        public IEnumerable<AppointmentModel> GetAppointments(int id) {

            IEnumerable<AppointmentModel> result = null;
            try
            {
                using (var client = new HttpClient())
                {

                    client.BaseAddress = new Uri(Utils.Url());

                    client.DefaultRequestHeaders.Clear();

                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage Res = client.GetAsync(Utils.Url() + "Patient/GetAppointments" + "?id=" + id).Result;

                    if (Res.IsSuccessStatusCode)
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

        //id = patientID
        public IEnumerable<AppointmentModel> GetAppointmentsForBookedRooms(int id)
        {

            IEnumerable<AppointmentModel> result = null;
            try
            {
                using (var client = new HttpClient())
                {

                    client.BaseAddress = new Uri(Utils.Url());

                    client.DefaultRequestHeaders.Clear();

                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage Res = client.GetAsync(Utils.Url() + "Patient/GetAppointmentsForBookedRooms" + "?id=" + id).Result;

                    if (Res.IsSuccessStatusCode)
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
        public IEnumerable<int> GetPaitentsList()
        {
            IEnumerable<int> result = null;
            try
            {
                using (var client = new HttpClient())
                {

                    client.BaseAddress = new Uri(Utils.Url());

                    client.DefaultRequestHeaders.Clear();

                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage Res = client.GetAsync(Utils.Url() + "Patient/GetPaitentsList").Result;

                    if (Res.IsSuccessStatusCode)
                    {
                        var res = (Res.Content.ReadAsAsync<IEnumerable<int>>().Result);

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

        public IEnumerable<int> GetPaitentsListByDate(AppointmentModel app)
        {
            IEnumerable<int> result = null;
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Utils.Url());
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var jsonData = JsonConvert.SerializeObject(app);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(jsonData);
                    var byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    HttpResponseMessage Res = client.GetAsync(Utils.Url() + "Patient/GetPaitentsList").Result;

                    if (Res.IsSuccessStatusCode)
                    {
                        var res = (Res.Content.ReadAsAsync<IEnumerable<int>>().Result);

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

        public IEnumerable<PaymentModel> GetPatientPayments(int id)
        {

            IEnumerable<PaymentModel> result = null;
            try
            {
                using (var client = new HttpClient())
                {

                    client.BaseAddress = new Uri(Utils.Url());

                    client.DefaultRequestHeaders.Clear();

                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage Res = client.GetAsync(Utils.Url() + "Patient/GetPatientPayments" + "?id=" + id).Result;

                    if (Res.IsSuccessStatusCode)
                    {

                        var res = (Res.Content.ReadAsAsync<IEnumerable<PaymentModel>>().Result);

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


        public IEnumerable<PaymentModel> GetAllPayments()
        {

            IEnumerable<PaymentModel> result = null;
            try
            {
                using (var client = new HttpClient())
                {

                    client.BaseAddress = new Uri(Utils.Url());

                    client.DefaultRequestHeaders.Clear();

                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage Res = client.GetAsync(Utils.Url() + "Patient/GetAllPayments").Result;

                    if (Res.IsSuccessStatusCode)
                    {

                        var res = (Res.Content.ReadAsAsync<IEnumerable<PaymentModel>>().Result);

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

        //
        public IEnumerable<PaymentModel> PayPayments(PaymentModel payment)
        {
            IEnumerable<PaymentModel> payments = null;
                try
                {
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(Utils.Url());
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        var jsonData = JsonConvert.SerializeObject(payment);
                        var buffer = System.Text.Encoding.UTF8.GetBytes(jsonData);
                        var byteContent = new ByteArrayContent(buffer);
                        byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                        HttpResponseMessage Res = client.PostAsync(Utils.Url() + "Patient/PatientPayPayments", byteContent).Result;
                        if (Res.IsSuccessStatusCode)
                        {
                        payments = (Res.Content.ReadAsAsync<IEnumerable<PaymentModel>>().Result);
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

        public AppointmentModel PatientBookAppointment(AppointmentModel bookAppointment)
        {
            AppointmentModel payments = null;
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Utils.Url());
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var jsonData = JsonConvert.SerializeObject(bookAppointment);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(jsonData);
                    var byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    HttpResponseMessage Res = client.PostAsync(Utils.Url() + "Patient/PatientBookAppointment", byteContent).Result;
                    if (Res.IsSuccessStatusCode)
                    {
                        payments = (Res.Content.ReadAsAsync<AppointmentModel>().Result);
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

        public IEnumerable<PatientModel> GetPatientById(PatientModel patientModel)
        {
            IEnumerable<PatientModel> patientDetails = null;
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Utils.Url());
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var jsonData = JsonConvert.SerializeObject(patientModel);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(jsonData);
                    var byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    HttpResponseMessage Res = client.PostAsync(Utils.Url() + "Patient/GetPatientById", byteContent).Result;
                    if (Res.IsSuccessStatusCode)
                    {
                        patientDetails = (Res.Content.ReadAsAsync<IEnumerable<PatientModel>>().Result);
                        return patientDetails;
                    }
                }
                return patientDetails;
            }
            catch(Exception ex)
            {
                Utils.Logging(ex, 2);
                return patientDetails;
            }
        }


        public string PatientValidateEmail(string text)
        {
            string result = null;
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Utils.Url());
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var jsonData = JsonConvert.SerializeObject(text);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(jsonData);
                    var byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    HttpResponseMessage Res = client.PostAsync(Utils.Url() + "Patient/PatientValidateEmail", byteContent).Result;
                    if (Res.IsSuccessStatusCode)
                    {
                        result =  Res.Content.ReadAsStringAsync().Result;
                        return result;
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

        public TreatmentRoomModel ViewBookedRoom(PatientModel patient)
        {
            TreatmentRoomModel result = null;
            try
            {
                using (var client = new HttpClient())
                {

                    client.BaseAddress = new Uri(Utils.Url());

                    client.DefaultRequestHeaders.Clear();

                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage Res = client.GetAsync(Utils.Url() + "Patient/ViewBookedRoom").Result;

                    if (Res.IsSuccessStatusCode)
                    {
                        var res = (Res.Content.ReadAsAsync<TreatmentRoomModel>().Result);

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


    }
}
