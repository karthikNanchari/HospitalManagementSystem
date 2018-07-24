using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Windows;

namespace Client_Api
{
    public class RegisterNewUser_Api
    {

        string Baseurl = "http://localhost:9635/api/";

        public string RegisterUser(PersonModel NewRegistrationDetails)
        {
            try
            {
                using (var client = new HttpClient())
                {

                    client.BaseAddress = new Uri(Baseurl);

                    client.DefaultRequestHeaders.Clear();

                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var jsonData = JsonConvert.SerializeObject(NewRegistrationDetails);

                    var buffer = System.Text.Encoding.UTF8.GetBytes(jsonData);

                    var byteContent = new ByteArrayContent(buffer);

                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    HttpResponseMessage Res = client.PostAsync(Baseurl + "Patient/RegisterNewUser", byteContent).Result;

                    if (Res.IsSuccessStatusCode)
                    {
                        return Res.Content.ReadAsStringAsync().Result;

                    }
                }
                return "Internal error please try after some time";
            }catch(Exception ex)
            {
                Utils.Logging(ex,2);
                return "Internal error please try after some time";
            }
        }

    }
}
