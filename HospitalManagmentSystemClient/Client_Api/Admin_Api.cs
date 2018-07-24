using System;
using System.Collections.Generic;
using Models;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;



namespace Client_Api
{
    public class Admin_Api
    {

        public IEnumerable<PatientModel> GetAllPatients()
        {

            IEnumerable<PatientModel> result = null;
            try
            {
                using (var client = new HttpClient())
                {

                    client.BaseAddress = new Uri(Utils.Url());

                    client.DefaultRequestHeaders.Clear();

                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage Res = client.GetAsync(Utils.Url() + "Administrator/GetAllPatients").Result;

                    if (Res.IsSuccessStatusCode)
                    {

                        var res = (Res.Content.ReadAsAsync<IEnumerable<PatientModel>>().Result);

                        result = res;
                        Utils.Logging(Environment.StackTrace,null);
                    }
                }
                return result;
            }
            catch(Exception ex)
            {
                Utils.Logging(ex,2);
                return result;
            }
        }

        public IEnumerable<PatientModel> UpdatePatient(PatientModel updatedPatient)
        {
            IEnumerable<PatientModel> result = null;
            try
            {
                using (var client = new HttpClient())
                {

                    client.BaseAddress = new Uri(Utils.Url());

                    client.DefaultRequestHeaders.Clear();

                    var jsonData = JsonConvert.SerializeObject(updatedPatient);

                    var buffer = System.Text.Encoding.UTF8.GetBytes(jsonData);

                    var byteContent = new ByteArrayContent(buffer);

                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    HttpResponseMessage Res = client.PostAsync(Utils.Url() + "Administrator/UpdatePatient", byteContent).Result;

                    if (Res.IsSuccessStatusCode && Res != null)
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

        public IEnumerable<DoctorModel> GetAllDoctor()
        {
            IEnumerable<DoctorModel> result = null;
            try
            {
                using (var client = new HttpClient())
                {

                    client.BaseAddress = new Uri(Utils.Url());

                    client.DefaultRequestHeaders.Clear();

                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage Res = client.GetAsync(Utils.Url() + "Administrator/GetAllDoctors").Result;

                    if (Res.IsSuccessStatusCode)
                    {

                        var res = (Res.Content.ReadAsAsync<IEnumerable<DoctorModel>>().Result);

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

        public IEnumerable<DoctorModel> UpdateDoctor(DoctorModel updatedDoctors)
        {
            IEnumerable<DoctorModel> result = null;
            try
            {
                using (var client = new HttpClient())
                {

                    client.BaseAddress = new Uri(Utils.Url());

                    client.DefaultRequestHeaders.Clear();

                    var jsonData = JsonConvert.SerializeObject(updatedDoctors);

                    var buffer = System.Text.Encoding.UTF8.GetBytes(jsonData);

                    var byteContent = new ByteArrayContent(buffer);

                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    HttpResponseMessage Res = client.PostAsync(Utils.Url() + "Administrator/UpdateDoctor", byteContent).Result;

                    if (Res.IsSuccessStatusCode && Res != null)
                    {

                        var res = (Res.Content.ReadAsAsync<IEnumerable<DoctorModel>>().Result);

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


        public IEnumerable<NurseModel> GetAllNurses()
        {
            IEnumerable<NurseModel> result = null;
            try
            {
                using (var client = new HttpClient())
                {

                    client.BaseAddress = new Uri(Utils.Url());

                    client.DefaultRequestHeaders.Clear();

                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage Res = client.GetAsync(Utils.Url() + "Nurse/GetAllNurses").Result;

                    if (Res.IsSuccessStatusCode)
                    {

                        var res = (Res.Content.ReadAsAsync<IEnumerable<NurseModel>>().Result);

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

        public IEnumerable<NurseModel> UpdateNurse(NurseModel updatedNurse)
        {
            IEnumerable<NurseModel> result = null;
            try
            {
                using (var client = new HttpClient())
                {

                    client.BaseAddress = new Uri(Utils.Url());

                    client.DefaultRequestHeaders.Clear();

                    var jsonData = JsonConvert.SerializeObject(updatedNurse);

                    var buffer = System.Text.Encoding.UTF8.GetBytes(jsonData);

                    var byteContent = new ByteArrayContent(buffer);

                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    HttpResponseMessage Res = client.PostAsync(Utils.Url() + "Nurse/UpdateNurse", byteContent).Result;

                    if (Res.IsSuccessStatusCode && Res != null)
                    {

                        var res = (Res.Content.ReadAsAsync<IEnumerable<NurseModel>>().Result);

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

        public IEnumerable<LabInchargeModel> GetAllIncharges()
        {
            IEnumerable<LabInchargeModel> result = null;
            try
            {
                using (var client = new HttpClient())
                {

                    client.BaseAddress = new Uri(Utils.Url());

                    client.DefaultRequestHeaders.Clear();

                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage Res = client.GetAsync(Utils.Url() + "LabIncharge/GetAllLabInchargeDetials").Result;

                    if (Res.IsSuccessStatusCode)
                    {

                        var res = (Res.Content.ReadAsAsync<IEnumerable<LabInchargeModel>>().Result);

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

        public IEnumerable<LabInchargeModel> UpdateIncharges(LabInchargeModel updatedDoctors)
        {
            IEnumerable<LabInchargeModel> result = null;
            try
            {
                using (var client = new HttpClient())
                {

                    client.BaseAddress = new Uri(Utils.Url());

                    client.DefaultRequestHeaders.Clear();

                    var jsonData = JsonConvert.SerializeObject(updatedDoctors);

                    var buffer = System.Text.Encoding.UTF8.GetBytes(jsonData);

                    var byteContent = new ByteArrayContent(buffer);

                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    HttpResponseMessage Res = client.PostAsync(Utils.Url() + "LabIncharge/UpdateLabIncharge", byteContent).Result;

                    if (Res.IsSuccessStatusCode && Res != null)
                    {

                        var res = (Res.Content.ReadAsAsync<IEnumerable<LabInchargeModel>>().Result);

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


        public IEnumerable<NotificationsModel> ManageNotifications(NotificationsModel manageNotifications)
        {
            IEnumerable<NotificationsModel> result = null;
            try
            {
                using (var client = new HttpClient())
                {

                    client.BaseAddress = new Uri(Utils.Url());

                    client.DefaultRequestHeaders.Clear();

                    var jsonData = JsonConvert.SerializeObject(manageNotifications);

                    var buffer = System.Text.Encoding.UTF8.GetBytes(jsonData);

                    var byteContent = new ByteArrayContent(buffer);

                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    HttpResponseMessage Res = client.PostAsync(Utils.Url() + "Administrator/ManageNotifications", byteContent).Result;

                    if (Res.IsSuccessStatusCode && Res != null)
                    {
                        var res = (Res.Content.ReadAsAsync<IEnumerable<NotificationsModel>>().Result);
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

        //need to pass AdminID in parameter// sample 6003 
        public IEnumerable<NotificationsModel> GetAllNotifications(NotificationsModel Notifications)
        {
            IEnumerable<NotificationsModel> result = null;
            try
            {
                using (var client = new HttpClient())
                {

                    client.BaseAddress = new Uri(Utils.Url());

                    client.DefaultRequestHeaders.Clear();

                    var jsonData = JsonConvert.SerializeObject(Notifications);

                    var buffer = System.Text.Encoding.UTF8.GetBytes(jsonData);

                    var byteContent = new ByteArrayContent(buffer);

                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    HttpResponseMessage Res = client.PostAsync(Utils.Url() + "Administrator/GetAllNotifications", byteContent).Result;

                    if (Res.IsSuccessStatusCode && Res != null)
                    {
                        var res = (Res.Content.ReadAsAsync<IEnumerable<NotificationsModel>>().Result);
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

        public IEnumerable<NotificationsModel> GetAllNotificationsByRole(NotificationsModel manageNotifications)
        {
            IEnumerable<NotificationsModel> result = null;
            try
            {
                using (var client = new HttpClient())
                {

                    client.BaseAddress = new Uri(Utils.Url());

                    client.DefaultRequestHeaders.Clear();

                    var jsonData = JsonConvert.SerializeObject(manageNotifications);

                    var buffer = System.Text.Encoding.UTF8.GetBytes(jsonData);

                    var byteContent = new ByteArrayContent(buffer);

                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    HttpResponseMessage Res = client.PostAsync(Utils.Url() + "Administrator/GetAllNotificationsByRole", byteContent).Result;

                    if (Res.IsSuccessStatusCode && Res != null)
                    {
                        var res = (Res.Content.ReadAsAsync<IEnumerable<NotificationsModel>>().Result);
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

        public IEnumerable<NotificationsModel> InsertNotifications(NotificationsModel manageNotifications)
        {
            IEnumerable<NotificationsModel> result = null;
            try
            {
                using (var client = new HttpClient())
                {

                    client.BaseAddress = new Uri(Utils.Url());

                    client.DefaultRequestHeaders.Clear();

                    var jsonData = JsonConvert.SerializeObject(manageNotifications);

                    var buffer = System.Text.Encoding.UTF8.GetBytes(jsonData);

                    var byteContent = new ByteArrayContent(buffer);

                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    HttpResponseMessage Res = client.PostAsync(Utils.Url() + "Administrator/InsertNotifications", byteContent).Result;

                    if (Res.IsSuccessStatusCode && Res != null)
                    {
                        var res = (Res.Content.ReadAsAsync<IEnumerable<NotificationsModel>>().Result);
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

        public IEnumerable<NotificationsModel> DeleteNotifications(NotificationsModel manageNotifications)
        {
            IEnumerable<NotificationsModel> result = null;
            try
            {
                using (var client = new HttpClient())
                {

                    client.BaseAddress = new Uri(Utils.Url());

                    client.DefaultRequestHeaders.Clear();

                    var jsonData = JsonConvert.SerializeObject(manageNotifications);

                    var buffer = System.Text.Encoding.UTF8.GetBytes(jsonData);

                    var byteContent = new ByteArrayContent(buffer);

                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    HttpResponseMessage Res = client.PostAsync(Utils.Url() + "Administrator/DeleteNotifications", byteContent).Result;

                    if (Res.IsSuccessStatusCode && Res != null)
                    {
                        var res = (Res.Content.ReadAsAsync<IEnumerable<NotificationsModel>>().Result);
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

        public List<PersonModel> GetAllPersonDetails()
        {
            List<PersonModel> result = null;
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Utils.Url());
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage Res = client.GetAsync(Utils.Url() + "Administrator/GetAllPersonDetails").Result;

                    if (Res.IsSuccessStatusCode && Res != null)
                    {
                        var res = (Res.Content.ReadAsAsync<List<PersonModel>>().Result);
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

        public IEnumerable<LabInchargeModel> GetAllLabIncharges()
        {
            IEnumerable<LabInchargeModel> result = null;
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Utils.Url());
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage Res = client.GetAsync(Utils.Url() + "Administrator/GetAllLabIncharges").Result;
                    if (Res.IsSuccessStatusCode)
                    {
                        var res = (Res.Content.ReadAsAsync<IEnumerable<LabInchargeModel>>().Result);
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


        //to get all Billing related details
        public IEnumerable<AccountModel> GetAllAccounts()
        {
            IEnumerable<AccountModel> result = null;
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Utils.Url());
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage Res = client.GetAsync(Utils.Url() + "Administrator/GetAllAccounts").Result;
                    if (Res.IsSuccessStatusCode)
                    {
                        var res = (Res.Content.ReadAsAsync<IEnumerable<AccountModel>>().Result);
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

        public IEnumerable<AccountModel> GetAccountsByID(AccountModel accountModel)
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

                    HttpResponseMessage Res = client.PostAsync(Utils.Url() + "Administrator/GetAccountsByID", byteContent).Result;

                    if (Res.IsSuccessStatusCode && Res != null)
                    {
                        var res = (Res.Content.ReadAsAsync<IEnumerable<AccountModel>>().Result);
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

        public IEnumerable<AccountModel> SaveAccountDetail(AccountModel accountModel)
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

                    HttpResponseMessage Res = client.PostAsync(Utils.Url() + "Administrator/SaveAccountDetail", byteContent).Result;

                    if (Res.IsSuccessStatusCode && Res != null)
                    {
                        var res = (Res.Content.ReadAsAsync<IEnumerable<AccountModel>>().Result);
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

        public IEnumerable<AccountModel> GetAccountsByDropDown(AccountModel accountModel)
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

                    HttpResponseMessage Res = client.PostAsync(Utils.Url() + "Administrator/GetAccountsByDropDown", byteContent).Result;

                    if (Res.IsSuccessStatusCode && Res != null)
                    {
                        var res = (Res.Content.ReadAsAsync<IEnumerable<AccountModel>>().Result);
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

        public IEnumerable<AccountModel> DeleteAccountDetail(AccountModel accountModel)
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

                    HttpResponseMessage Res = client.PostAsync(Utils.Url() + "Administrator/DeleteAccountDetail", byteContent).Result;

                    if (Res.IsSuccessStatusCode && Res != null)
                    {
                        var res = (Res.Content.ReadAsAsync<IEnumerable<AccountModel>>().Result);
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

        public AccountModel AddAccountDetail(AccountModel accountModel)
        {
            AccountModel result = null;
            try
            {
                using (var client = new HttpClient())
                {

                    client.BaseAddress = new Uri(Utils.Url());
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var jsonData = JsonConvert.SerializeObject(accountModel);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(jsonData);
                    var byteContent = new ByteArrayContent(buffer);

                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    HttpResponseMessage Res = client.PostAsync(Utils.Url() + "Administrator/AddAccountDetail", byteContent).Result;

                    if (Res.IsSuccessStatusCode && Res != null)
                    {
                        var res = (Res.Content.ReadAsAsync<AccountModel>().Result);
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
