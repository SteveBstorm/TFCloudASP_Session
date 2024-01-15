using DAL.Entities;
using DAL.Interfaces;
using DAL.Tools;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class UserRepositoryAPI : IUserRepository
    {
        private string urlAPI = "https://localhost:7152/api/";

        private HttpClient client;
        private APIRequester requester;

        public UserRepositoryAPI()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(urlAPI);
            requester = new APIRequester(client);
        }
        public void Create(User user)
        {
            string json = JsonConvert.SerializeObject(user);
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

            using (HttpResponseMessage message = client.PostAsync("user/register", content).Result)
            {
                message.EnsureSuccessStatusCode();
            }
        }

        public bool Delete(User user)
        {
            using (HttpResponseMessage message = client.DeleteAsync("user/"+ user.Id).Result)
            {
                if(message.IsSuccessStatusCode)
                {
                    return true;
                }
                return false;
            }
        }

        public IEnumerable<User> GetAll(string token)
        {
            //using(HttpResponseMessage message = client.GetAsync("user").Result)
            //{
            //    message.EnsureSuccessStatusCode();
            //    List<User> users = new List<User>();

            //    string json = message.Content.ReadAsStringAsync().Result;
            //    users = JsonConvert.DeserializeObject<List<User>>(json);

            //    return users;
            //}

            return requester.Get<IEnumerable<User>>("user", token);
        }

        public User? GetByEmail(string email)
        {
            //using (HttpResponseMessage message = client.GetAsync("user/getUserByEmail/"+email).Result)
            //{
            //    if(message.IsSuccessStatusCode)
            //    {
            //        string json = message.Content.ReadAsStringAsync().Result;
            //        return JsonConvert.DeserializeObject<User>(json);
            //    }
            //    return null;
            //}

            return requester.Get<User>("user/getUserByEmail/" + email, "");
        }

        public User? GetById(int id)
        {
            //using (HttpResponseMessage message = client.GetAsync("user/" + id).Result)
            //{
            //    if (message.IsSuccessStatusCode)
            //    {
            //        string json = message.Content.ReadAsStringAsync().Result;
            //        return JsonConvert.DeserializeObject<User>(json);
            //    }
            //    return null;
            //}
            return requester.Get<User>("user/" + id, "");
        }

        public bool Update(UpdatePasswordDAL user)
        {
            string json = JsonConvert.SerializeObject(user);
            HttpContent content= new StringContent(json, Encoding.UTF8, "application/json");

            using (HttpResponseMessage message = client.PatchAsync("user/updatepwd/" + user.Id, content).Result)
            {
                if (message.IsSuccessStatusCode)
                {
                    return true;
                }
                return false;
            }
        }

        public string Login(string email, string password)
        {
            string jsonLogin = JsonConvert.SerializeObject(new {email, password});
            HttpContent content = new StringContent(jsonLogin, Encoding.UTF8, "application/json");

            string token = "";

            using (HttpResponseMessage message = client.PostAsync("user/login", content).Result)
            {
                if (message.IsSuccessStatusCode)
                    token = message.Content.ReadAsStringAsync().Result;
                return token;
            }
        }
    }
}
