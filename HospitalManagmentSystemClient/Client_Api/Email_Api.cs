using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Client_Api
{
    public class Email_Api
    {

        public IEnumerable<PatientModel> GetPatientsAppointments()
        {
            IEnumerable<PatientModel> result = null;
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Utils.Url());
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage Res = client.GetAsync(Utils.Url() + "Email/GetPatientsAppointments").Result;
                    if (Res.IsSuccessStatusCode)
                    {
                        var res = (Res.Content.ReadAsAsync<IEnumerable<PatientModel>>().Result);
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



    }
}
