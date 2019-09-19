using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Messaging;
using MusgramClient.Models;
using Newtonsoft.Json;

namespace MusgramClient.Services
{
    public class ConnectionService : IConnection
    {
        private SqLiteService sqLiteConnection;
        private TcpConnection tcpConnection;

        private HttpClient httpClient;
        private readonly string ip;
        private readonly string port;

        public ConnectionService()
        {
            tcpConnection = new TcpConnection();
            sqLiteConnection = new SqLiteService();
            ip = "192.168.0.110";
            port = "27001";
            httpClient = new HttpClient();
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

        public bool CreateChat(Chat chatTC)
        {
            HttpRequestMessage postMes = new HttpRequestMessage();
            postMes.RequestUri = new Uri($"http://{ip}:{port}/CreateChat/");
            postMes.Method = HttpMethod.Post;
            string jsonUser = JsonConvert.SerializeObject(chatTC);
            postMes.Content = new StringContent(jsonUser);
            httpClient.SendAsync(postMes);
            return true;
        }

        public User TryLogin(string login, string password)
        {
            HttpRequestMessage postMes = new HttpRequestMessage();
            postMes.RequestUri = new Uri($"http://{ip}:{port}/TryLogin/");
            postMes.Method = HttpMethod.Post;
            postMes.Content = new StringContent($"&{login}&{password}");
            var a = httpClient.SendAsync(postMes).Result;
            string content = a.Content.ReadAsStringAsync().Result;
            //tcpClient.Connect(IPAddress.Parse(ip), int.Parse(port));
            User loggined = JsonConvert.DeserializeObject<User>(content);
            if (null == loggined)
            {
                throw new Exception();
            }
            else
            {
                tcpConnection.RegInServer(loggined.Id, ip, 8080);
                Task.Run(() =>
                {
                    while (true)
                    {
                        tcpConnection.Listen();
                    }
                });
                sqLiteConnection.AddUser(loggined);
                loggined.Password.Remove(loggined.Password.Length - 9);
                return loggined;
            }
        }

        //private string getLocalIPAddress()
        //{
        //    var host = Dns.GetHostEntry(Dns.GetHostName());
        //    foreach (var ip in host.AddressList)
        //    {
        //        if (ip.AddressFamily == AddressFamily.InterNetwork)
        //        {
        //            return ip.ToString();
        //        }
        //    }
        //    return null;
        //}
    }
}
