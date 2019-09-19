using GalaSoft.MvvmLight.Messaging;
using MusgramClient.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MusgramClient.Services
{
    public class TcpConnection
    {
        private TcpClient tcpClient;
        private StreamWriter writer;
        private StreamReader reader;

        public bool RegInServer(int id, string ip, int port)
        {
            tcpClient = new TcpClient();
            if (tcpClient != null)
            {
                tcpClient.Connect(IPAddress.Parse(ip), port);
                writer = new StreamWriter(tcpClient.GetStream());
                reader = new StreamReader(tcpClient.GetStream());
                string data = id.ToString() + "~" + "connect";
                SendDataToServer(data);
                return true;
            }
            return false;
        }
        public void Listen()
        {
            Task.Run(() =>
            {
                while (true)
                {
                    try
                    {
                        var message = reader.ReadLine();
                        var data = message.Split('~')[message.Split('~').Length - 1];
                        var command = message.Split('~').Last();
                        switch(command)
                        {
                            case "new-message":

                                break;
                        }
                    }
                    catch { }
                }
            });
        }

        public void SendMessage(Message message)
        {
            string mesJson = JsonConvert.SerializeObject(message);
            string data = JsonConvert.SerializeObject(message) + "~" + "send-message";
            SendDataToServer(data);
        }
        private bool SendDataToServer(string message)
        {
            writer.AutoFlush = true;
            writer.WriteLine(message);
            return true;
        }
    }
}
