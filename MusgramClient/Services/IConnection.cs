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
        User TryLogin(string login, string password);
        void Register(User userToRegister);
        User GetUserProfile(User user);
    }
}
