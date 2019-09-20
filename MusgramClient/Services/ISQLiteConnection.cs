using MusgramClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusgramClient.Services
{
    public interface ISQLiteConnection
    {
        Task AddMessage(Message mes);
        Task<ICollection<Chat>> GetChats();
        Task AddUser(User user);
        Task AddChatMember(ChatMember member);
        Task AddChat(Chat chat);

        Task<Chat> GetChatById(int id);
        Task<bool> IfChatExist(Chat chat);
    }
}
