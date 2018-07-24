using Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Client_Api
{
    public class Login_Api
    {

        public  int AuthenticateUser(LoginModel loginDetails)
        {
            loginDetails.id = 7001;
            loginDetails.pwd = "haryy007";

            int result = -1;
            try
            {
                using (var client = new HttpClient())
                {

                    client.BaseAddress = new Uri(Utils.Url());

                    client.DefaultRequestHeaders.Clear();

                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage Res = client.GetAsync(Utils.Url() + "UserLogin/Authenticate" + "?id=" + Convert.ToString(loginDetails.id) + "&pwd=" + loginDetails.pwd).Result;

                    if (Res.IsSuccessStatusCode)
                    {

                        result = Convert.ToInt32(Res.Content.ReadAsStringAsync().Result);
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


        public string GetUserName(int? id)
        {
            string result = "";
            try
            {
                using (var client = new HttpClient())
                {

                    client.BaseAddress = new Uri(Utils.Url());

                    client.DefaultRequestHeaders.Clear();

                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage Res = client.GetAsync(Utils.Url() + "UserLogin/GetUserName" + "?id=" + Convert.ToString(id) ).Result;

                    if (Res.IsSuccessStatusCode)
                    {
                        result = (Res.Content.ReadAsStringAsync().Result);
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
