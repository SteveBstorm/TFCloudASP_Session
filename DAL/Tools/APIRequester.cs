using DAL.Entities;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Tools
{
    public class APIRequester
    {
        private HttpClient _client;

        public APIRequester(HttpClient client)
        {
            _client = client;
        }

        public TResult Get<TResult>(string route, string token)
        {
            _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            using (HttpResponseMessage message = _client.GetAsync(route).Result)
            {
                message.EnsureSuccessStatusCode();
                
                    string json = message.Content.ReadAsStringAsync().Result;
                    return JsonConvert.DeserializeObject<TResult>(json);
                
                
            }
        }
    }
}
