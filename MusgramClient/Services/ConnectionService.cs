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
        private readonly string ip;
        private readonly string port;

        public ConnectionService()
        {
            ip = "10.2.25.50";
            port = "27001";
            httpClient = new HttpClient();
        }

        public void Register(User userToRegister)
        {
            HttpClient httpClient = new HttpClient();
            HttpRequestMessage postMes = new HttpRequestMessage();
            postMes.RequestUri = new Uri($"http://{ip}:{port}/GetProfile/");
            postMes.Method = HttpMethod.Post;
            string jsonUser = JsonConvert.SerializeObject(userToRegister);
            postMes.Content = new StringContent(jsonUser);
            httpClient.SendAsync(postMes);
        }

        public User TryLogin(string login, string password)
        {
            HttpClient httpClient = new HttpClient();
            HttpRequestMessage postMes = new HttpRequestMessage();
            postMes.RequestUri = new Uri($"http://{ip}:{port}/GetProfile/");
            postMes.Method = HttpMethod.Post;
            postMes.Content = new StringContent("&goose12313&4443122");
            var a = httpClient.SendAsync(postMes).Result;
            string content = a.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<User>(content);
        }
    }
}
