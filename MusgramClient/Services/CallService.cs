using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace MusgramClient.Services
{
    public class CallService
    {
        //Подключены ли мы
        private bool connected;
        //сокет отправитель
        public Socket client;
        //поток для нашей речи
        private WaveIn input;
        //поток для речи собеседника 
        private WaveOut output;
        //буфферный поток для передачи через сеть
        private BufferedWaveProvider bufferStream;
        //Поток для прослушивания входящих сообщений
        private Thread in_thread;
        //Сокет для приёма (протокол UDP)
        public Socket listeningSocket;

        public CallService()
        {
            client = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        }

        public void Call_Acepted()
        {
            input = new WaveIn();
            //Opredelaem eqo format - 4astota diskretizacii 8000 Hz, wirina sempla - 16 bit, 
            //1 kanal - mono
            input.WaveFormat = new WaveFormat(8000, 32, 1);
            //Dobavlaem kod obrabotki naweqo qolosa, postupayuweqo na mikrafon
            input.DataAvailable += Voice_Input;
            //Создаём поток для прослушивания входящего звука
            output = new WaveOut();
            //создаём поток для буферного потока и определяем у него такой же формат как и потока с микрофона
            bufferStream = new BufferedWaveProvider(new WaveFormat(8000, 32, 1));
            //привызываем поток входящего звука к буферному потоку
            output.Init(bufferStream);
            //сокет для отправки звука
            client = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            connected = true;
            listeningSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            //Создаем поток для прослушивания 
            in_thread = new Thread(new ThreadStart(Listening));
        }

        public void Listening()
        {
            //Прослушивание по адресу 
            IPEndPoint localIP = new IPEndPoint(IPAddress.Parse("192.168.43.99"), 5555);
            listeningSocket.Bind(localIP);
            //начинаем воспроизводить входящий звук
            output.Play();
            //адрес с которого пришли данные
            EndPoint remoteIp = new IPEndPoint(IPAddress.Any, 0);
            //бесконечный цикл
            while (true)
            {
                try
                {
                    //Промежуточный буфер
                    byte[] data = new byte[65535];
                    //получение данных
                    int received = listeningSocket.ReceiveFrom(data, ref remoteIp);
                    bufferStream.AddSamples(data, 0, received);
                }
                catch (SocketException ex)
                {
                    Debug.Write(ex.Message);
                }
            }
        }

        public void Voice_Input(object sender, WaveInEventArgs e)
        {
            try
            {
                //Podklucaemsa k udalennomu adresu
                //IPEndPoint remote_point = new IPEndPoint(IPAddress.Parse(textboxIP.Text), 5555);
                //Посылаем байты полученные с микрофона на удалённый адрес
                //client.SendTo(e.Buffer, remote_point);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        private void Current_Exit(object sender, ExitEventArgs e)
        {
            connected = false;
            listeningSocket.Close();
            listeningSocket.Dispose();

            client.Close();
            client.Dispose();
            if (output != null)
            {
                output.Stop();
                output.Dispose();
                output = null;
            }
            if (input != null)
            {
                input.Dispose();
                input = null;
            }
            bufferStream = null;
        }
    }
}
