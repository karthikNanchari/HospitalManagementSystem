using System;
using System.Collections.Generic;
using Models;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;


namespace Client_Api
{
    public class Account_Api
    {
        public IEnumerable<AccountModel> GetAccountsByBillID(AccountModel accountModel)
        {
            IEnumerable<AccountModel> result = null;
            try
            {
                using (var client = new HttpClient())
                {

                    client.BaseAddress = new Uri(Utils.Url());

                    client.DefaultRequestHeaders.Clear();

                    var jsonData = JsonConvert.SerializeObject(accountModel);

                    var buffer = System.Text.Encoding.UTF8.GetBytes(jsonData);

                    var byteContent = new ByteArrayContent(buffer);

                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    HttpResponseMessage Res = client.PostAsync(Utils.Url() + "Account/GetAccountsByBillID", byteContent).Result;

                    if (Res.IsSuccessStatusCode && Res != null)
                    {
                        var res = (Res.Content.ReadAsAsync<IEnumerable<AccountModel>>().Result);
                        result = res;
                    }
                }
                return result;
            }
            catch
            {
                return result;
            }
        }



    }
}
