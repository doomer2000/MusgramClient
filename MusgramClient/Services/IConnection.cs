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
        Task<bool> CreateChat(Chat chatTC);
        User TryLogin(string login, string password);
        void Register(User userToRegister);

        //ICollection<User> GetFriends();
        User GetUserProfile(User user);
        bool SendMessage(Message chatMsg);
    }
}
