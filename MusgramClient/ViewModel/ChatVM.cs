using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MusgramClient.Models;
using System.Threading.Tasks;

namespace MusgramClient.ViewModel
{
    public class ChatVM : ViewModelBase
    {
        public User CurrentUser;

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
                return UserTSMessage;
            }
            set
            {
                userTSMessage = value;
                RaisePropertyChanged();
            }
        }

    }
}
