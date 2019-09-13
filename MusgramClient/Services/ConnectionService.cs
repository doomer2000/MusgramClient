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
        private TcpClient tcpClient;
        private TcpListener tcpListener;

        public ConnectionService()
        {
            ip = "192.168.31.48";
            port = "27001";
            httpClient = new HttpClient();
            tcpClient = new TcpClient();
            //tcpListener = new TcpListener();
        }

        public ICollection<User> GetFriends(int id)
        {
            HttpRequestMessage getMes = new HttpRequestMessage();
            getMes.RequestUri = new Uri($"http://{ip}:{port}/GetFriends/{id}");
            getMes.Method = HttpMethod.Get;
            var a = httpClient.SendAsync(getMes).Result;
            string content = a.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<ICollection<User>>(content);
        }

        public bool SendMessage(Message chatMsg)
        {
            HttpRequestMessage postMes = new HttpRequestMessage();
            postMes.RequestUri = new Uri($"http://{ip}:{port}/SendMessage/");
            postMes.Method = HttpMethod.Post;
            string jsonUser = JsonConvert.SerializeObject(chatMsg);
            postMes.Content = new StringContent($"{jsonUser}");
            var a = httpClient.SendAsync(postMes).Result;
            string content = a.Content.ReadAsStringAsync().Result;
            return true;
        }

        public void Register(User userToRegister)
        {
            HttpRequestMessage postMes = new HttpRequestMessage();
            postMes.RequestUri = new Uri($"http://{ip}:{port}/Registration/");
            postMes.Method = HttpMethod.Post;
            string jsonUser = JsonConvert.SerializeObject(userToRegister);
            postMes.Content = new StringContent(jsonUser);
            httpClient.SendAsync(postMes);
        }

        public User GetUserProfile(User user)
        {
            HttpRequestMessage postMes = new HttpRequestMessage();
            postMes.RequestUri = new Uri($"http://{ip}:{port}/{user.Id.ToString()}");
            postMes.Method = HttpMethod.Post;
            postMes.Content = null;
            var a = httpClient.SendAsync(postMes).Result;
            string content = a.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<User>(content);
        }

        public bool CreateChat(MyChat chatTC)
        {
            HttpRequestMessage postMes = new HttpRequestMessage();
            postMes.RequestUri = new Uri($"http://{ip}:{port}/CreateChat/");
            postMes.Method = HttpMethod.Post;
            string jsonUser = JsonConvert.SerializeObject(chatTC);
            postMes.Content = new StringContent(jsonUser);
            httpClient.SendAsync(postMes);
            
        }

        public User TryLogin(string login, string password)
        {
            HttpRequestMessage postMes = new HttpRequestMessage();
            postMes.RequestUri = new Uri($"http://{ip}:{port}/TryLogin/");
            postMes.Method = HttpMethod.Post;
            postMes.Content = new StringContent($"&{login}&{password}&{getLocalIPAddress()}");
            var a = httpClient.SendAsync(postMes).Result;
            string content = a.Content.ReadAsStringAsync().Result;
            //tcpClient.Connect(IPAddress.Parse(ip), int.Parse(port));
            if(JsonConvert.DeserializeObject<User>(content).Id == 0)
            {
                throw new Exception();
            }
            else return JsonConvert.DeserializeObject<User>(content);
        }

        private string getLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            return null;
        }
    }
}
