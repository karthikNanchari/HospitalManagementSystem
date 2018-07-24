using System;
using System.Collections.Generic;
using Models;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace Client_Api
{
    public class Appointment_Api
    {

        public AppointmentModel BookApnmt_Api(AppointmentModel apmnt)
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

                    HttpResponseMessage Res = client.PostAsync(Utils.Url() + "Appointment/BookAppointment", byteContent).Result;

                    if (Res.IsSuccessStatusCode)
                    {
                        newAppointment = (Res.Content.ReadAsAsync<AppointmentModel>().Result);

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

        public AppointmentModel DeleteApnmt_Api(AppointmentModel apmnt)
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

                    HttpResponseMessage Res = client.PostAsync(Utils.Url() + "Appointment/DeleteAppointment", byteContent).Result;

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

        public AppointmentModel UpdateApnmt_Api(AppointmentModel apmnt)
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

                    HttpResponseMessage Res = client.PostAsync(Utils.Url() + "Appointment/UpdateAppointment", byteContent).Result;

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


        //get Appointment by appID
        public AppointmentModel GetApnmt_Api(AppointmentModel apmnt)
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

                    HttpResponseMessage Res = client.PostAsync(Utils.Url() + "Appointment/GetApmntByID", byteContent).Result;

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

        public IEnumerable<AppointmentModel> GetAppointmentsWithNoBilling()
        {
            IEnumerable<AppointmentModel> newAppointment = null;
            try
            {

                using (var client = new HttpClient())
                {

                    client.BaseAddress = new Uri(Utils.Url());

                    client.DefaultRequestHeaders.Clear();

                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


                    HttpResponseMessage Res = client.GetAsync(Utils.Url() + "Appointment/GetAppointmentsWithNoBilling").Result;

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

        public IEnumerable<AppointmentModel> GetAllAppointments()
        {
            IEnumerable<AppointmentModel> newAppointment = null;
            try
            {

                using (var client = new HttpClient())
                {

                    client.BaseAddress = new Uri(Utils.Url());

                    client.DefaultRequestHeaders.Clear();

                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


                    HttpResponseMessage Res = client.GetAsync(Utils.Url() + "Appointment/GetAllAppointments").Result;

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
        
    }
}
