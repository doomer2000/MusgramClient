using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using MusgramClient.Models;
using Newtonsoft.Json;

namespace MusgramClient.Services
{
    public class ConnectionService : IConnection
    {
        private HttpClient httpClient;

        public ConnectionService()
        {
            httpClient = new HttpClient();
        }

        public void Register(User userToRegister)
        {
            HttpClient httpClient = new HttpClient();
            HttpRequestMessage getMes = new HttpRequestMessage();
            getMes.RequestUri = new Uri("http://10.2.25.31:27001/GetProfile/");
            getMes.Method = HttpMethod.Post;
            string jsonUser = JsonConvert.SerializeObject(userToRegister);
            getMes.Content = new StringContent(jsonUser);
        }

        public User TryLogin(string login, string password)
        {
            HttpClient httpClient = new HttpClient();
            HttpRequestMessage getMes = new HttpRequestMessage();
            getMes.RequestUri = new Uri("http://10.2.25.31:27001/GetProfile/");
            getMes.Method = HttpMethod.Post;
            getMes.Content = new StringContent("&goose12313&4443122");
            var a = httpClient.SendAsync(getMes).Result;
            string content = a.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<User>(content);
        }
    }
}
