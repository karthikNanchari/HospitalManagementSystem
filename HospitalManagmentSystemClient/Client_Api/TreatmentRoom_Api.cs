using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using System.Net.Http.Headers;
using System.Net.Http;
using Newtonsoft.Json;

namespace Client_Api
{
    public class TreatmentRoom_Api
    {

        public IEnumerable<TreatmentRoomModel> GetAllTreatmentRooms()
        {
            IEnumerable<TreatmentRoomModel> result = null;
            try
            {
                using (var client = new HttpClient())
                {

                    client.BaseAddress = new Uri(Utils.Url());

                    client.DefaultRequestHeaders.Clear();

                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage Res = client.GetAsync(Utils.Url() + "TreatmentRoom/GetAllTreatmentRooms").Result;

                    if (Res.IsSuccessStatusCode)
                    {

                        var res = (Res.Content.ReadAsAsync<IEnumerable<TreatmentRoomModel>>().Result);

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

        public IEnumerable<TreatmentRoomModel> GetAllTreatmentRoomRecs()
        {
            IEnumerable<TreatmentRoomModel> result = null;
            try
            {
                using (var client = new HttpClient())
                {

                    client.BaseAddress = new Uri(Utils.Url());

                    client.DefaultRequestHeaders.Clear();

                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage Res = client.GetAsync(Utils.Url() + "TreatmentRoom/GetAllTreatmentRoomRecs").Result;

                    if (Res.IsSuccessStatusCode)
                    {

                        var res = (Res.Content.ReadAsAsync<IEnumerable<TreatmentRoomModel>>().Result);

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
        

        public IEnumerable<TreatmentRoomModel> UpdateTreatmentRoom(TreatmentRoomModel roomModel)
        {
            IEnumerable<TreatmentRoomModel> result = null;
            try
            {
                using (var client = new HttpClient())
                {

                    client.BaseAddress = new Uri(Utils.Url());
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var jsonData = JsonConvert.SerializeObject(roomModel);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(jsonData);
                    var byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    HttpResponseMessage Res = client.PostAsync(Utils.Url() + "TreatmentRoom/UpdateTreatmentRoom", byteContent).Result;

                    if (Res.IsSuccessStatusCode)
                    {

                        var res = (Res.Content.ReadAsAsync<IEnumerable<TreatmentRoomModel>>().Result);

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

        public IEnumerable<TreatmentRoomModel> BookTreatmentRoom(TreatmentRoomModel roomModel)
        {
            IEnumerable<TreatmentRoomModel> result = null;
            try
            {
                using (var client = new HttpClient())
                {

                    client.BaseAddress = new Uri(Utils.Url());
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var jsonData = JsonConvert.SerializeObject(roomModel);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(jsonData);
                    var byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    HttpResponseMessage Res = client.PostAsync(Utils.Url() + "TreatmentRoom/BookTreatmentRoom", byteContent).Result;

                    if (Res.IsSuccessStatusCode)
                    {
                        var res = (Res.Content.ReadAsAsync<IEnumerable<TreatmentRoomModel>>().Result);
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

        public IEnumerable<TreatmentRoomModel> GetAvailableRooms(TreatmentRoomModel roomModel)
        {
            IEnumerable<TreatmentRoomModel> result = null;
            try
            {
                using (var client = new HttpClient())
                {

                    client.BaseAddress = new Uri(Utils.Url());
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var jsonData = JsonConvert.SerializeObject(roomModel);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(jsonData);
                    var byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    HttpResponseMessage Res = client.PostAsync(Utils.Url() + "TreatmentRoom/GetAvailableRooms", byteContent).Result;

                    if (Res.IsSuccessStatusCode)
                    {

                        var res = (Res.Content.ReadAsAsync<IEnumerable<TreatmentRoomModel>>().Result);

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

        public TreatmentRoomModel CheckAvailabilityTreatmentRoom(TreatmentRoomModel roomModel)
        {
            TreatmentRoomModel result = null;
            try
            {
                using (var client = new HttpClient())
                {

                    client.BaseAddress = new Uri(Utils.Url());
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var jsonData = JsonConvert.SerializeObject(roomModel);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(jsonData);
                    var byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    HttpResponseMessage Res = client.PostAsync(Utils.Url() + "TreatmentRoom/CheckAvailabilityTreatmentRoom", byteContent).Result;

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


        public IEnumerable<TreatmentRoomModel> GetRoomsRecordsByPatID(PatientModel pat)
        {
            IEnumerable<TreatmentRoomModel> result = null;
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Utils.Url());
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var jsonData = JsonConvert.SerializeObject(pat);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(jsonData);
                    var byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    HttpResponseMessage Res = client.PostAsync(Utils.Url() + "TreatmentRoom/GetRoomsRecordsByPatID", byteContent).Result;

                    if (Res.IsSuccessStatusCode)
                    {

                        var res = (Res.Content.ReadAsAsync<IEnumerable<TreatmentRoomModel>>().Result);

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

        public IEnumerable<TreatmentRoomModel> GetBookedRooms(PatientModel pat)
        {
            IEnumerable<TreatmentRoomModel> result = null;
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Utils.Url());
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var jsonData = JsonConvert.SerializeObject(pat);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(jsonData);
                    var byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    HttpResponseMessage Res = client.PostAsync(Utils.Url() + "TreatmentRoom/GetBookedRooms", byteContent).Result;

                    if (Res.IsSuccessStatusCode)
                    {

                        var res = (Res.Content.ReadAsAsync<IEnumerable<TreatmentRoomModel>>().Result);

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


        public IEnumerable<TreatmentRoomModel> InsertTreatmentRoomRec(TreatmentRoomModel roomModel)
        {
            IEnumerable<TreatmentRoomModel> result = null;
            try
            {
                using (var client = new HttpClient())
                {

                    client.BaseAddress = new Uri(Utils.Url());
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var jsonData = JsonConvert.SerializeObject(roomModel);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(jsonData);
                    var byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    HttpResponseMessage Res = client.PostAsync(Utils.Url() + "TreatmentRoom/InsertTreatmentRoomRec", byteContent).Result;

                    if (Res.IsSuccessStatusCode)
                    {
                        var res = (Res.Content.ReadAsAsync<IEnumerable<TreatmentRoomModel>>().Result);
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
    }
}
