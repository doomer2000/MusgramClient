using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MusgramClient.Models;
using System.Threading.Tasks;
using MusgramClient.Services;
using GalaSoft.MvvmLight.Messaging;
using Newtonsoft.Json;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using System.Collections.ObjectModel;

namespace MusgramClient.ViewModel
{
    public class ChatVM : ViewModelBase
    {
        private ObservableCollection<Chat> userChats;
        public ObservableCollection<Chat> UserChats
        {
            get
            {
                return userChats;
            }
            set
            {
                userChats = value;
                RaisePropertyChanged();
            }
        }

        private ISQLiteConnection sQLite;

        private User currentUser;
        public User CurrentUser
        {
            get
            {
                return currentUser;
            }
            set
            {
                currentUser = value;
                RaisePropertyChanged();
            }
        }

        private IConnection connectionService;

        private string messegeTS;
        public string MessegeTS
        {
            get
            {
                return messegeTS;
            }
            set
            {
                messegeTS = value;
                RaisePropertyChanged();
            }
        }

        private Chat chatTSMessage;
        public Chat ChatTSMessage
        {
            get
            {
                return chatTSMessage;
            }
            set
            {
                chatTSMessage = value;
                RaisePropertyChanged();
            }
        }

        public ChatVM(IConnection connection,ISQLiteConnection sQLiteConnection)
        {
            sQLite = sQLiteConnection;
            connectionService = connection;
            Messenger.Default.Register<User>(this, "User", (u) => CurrentUser = u);
            Messenger.Default.Send<string>("", "ready");
            Messenger.Default.Register<Message>(this, "Message", (m) => newMessageGeted(m));
            //Chats = sq

            //connectionService.CreateChat(chat);

            //connectionService.SendMessage(new Message() { SendTime = DateTime.Now,Text = "Hello world!", User = new User() { Id = 2 },Chat=chat,MessegeType=0});
        }

        private ICommand sendMessage;
        public ICommand SendMessage => sendMessage ?? (sendMessage = new RelayCommand(() =>
        {
            
        }));
        private void newMessageGeted(Message message)
        {
            
        }
    }
}
