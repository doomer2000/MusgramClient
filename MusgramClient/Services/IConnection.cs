using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusgramClient.Models;

namespace MusgramClient.Services
{
    public interface IConnection
    {
        bool CreateChat(MyChat chatTC);
        User TryLogin(string login, string password);
        void Register(User userToRegister);

        //ICollection<User> GetFriends();
        User GetUserProfile(User user);
        bool SendMessage(Message chatMsg);
    }
}
