﻿using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MusgramClient.Models;
using System.Threading.Tasks;
using MusgramClient.Services;
using GalaSoft.MvvmLight.Messaging;

namespace MusgramClient.ViewModel
{
    public class ChatVM : ViewModelBase
    {


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

        private User userTSMessage;
        public User UserTSMessage
        {
            get
            {
                return userTSMessage;
            }
            set
            {
                userTSMessage = value;
                RaisePropertyChanged();
            }
        }

        public ChatVM(IConnection connection)
        {
            connectionService = connection;
            Messenger.Default.Register<User>(this, "User", (u) => CurrentUser = u);
            Messenger.Default.Send<string>("", "ready");
            Messenger.Default.Register<Message>(this, "Message", (m) =>newMessageGeted(m));
            //CurrentUser.UserFriends = connectionService.GetFriends();
            //List<Срф> members = new List<User>()
            //{
            //    new User(){Id=currentUser.Id},
            //    new User(){Id=2},
            //    new User(){Id=3}
            //};
            //
            //Chat chat = new Chat()
            //{
            //    Title = "Test",
            //    IsPrivate = false,
            //    ChatMembers = members
            //};
            //connectionService.CreateChat(chat);
        }
        private void newMessageGeted(Message message)
        {
            
        }
    }
}
