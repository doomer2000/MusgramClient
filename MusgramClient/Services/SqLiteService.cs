using MusgramClient.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Configuration;
using Dapper;
using System.Threading.Tasks;

namespace MusgramClient.Services
{
    public class SqLiteService : ISQLiteConnection
    {
        public async Task AddMessage(Message mes)
        {
            using (IDbConnection cnct = new SQLiteConnection(LoadConnectionString()))
            {
                await cnct.ExecuteAsync("INSERT INTO Messages VALUES " +
                    "(@Id,@Text,@SendTime," +
                    "@MessageType,@Music_Id," +
                    "@ImagePath,@VideoPath," +
                    "@VoicePath,@User_Id," +
                    "@Chat_Id)"
                    , new
                    {
                        mes.Id,
                        mes.Text,
                        SendTime = mes.SendTime.ToString(),
                        mes.MessegeType,
                        Music_Id = mes.Music.Id,
                        mes.ImagePath,
                        mes.VideoPath,
                        mes.VoicePath,
                        User_Id = mes.User.Id,
                        Chat_Id = mes.Chat.Id
                    });
            }
        }
        public async Task<ICollection<Chat>> GetChats()
        {
            using (IDbConnection cnct = new SQLiteConnection(LoadConnectionString()))
            {
                var chats = await cnct.QueryAsync<Chat>("SELECT * FROM Chat");
                using(IDbConnection cnct2 = new SQLiteConnection(LoadConnectionString()))
                {
                    foreach(Chat c in chats)
                    {
                        IEnumerable<ChatMember> member = await cnct2.QueryAsync<ChatMember>("SELECT * FROM ChatMember WHERE Chat_FK = @Id",c);
                        c.ChatMembers = member.ToList();
                    }
                }
                return chats.ToList();
            }
        }
        public async Task AddUser(User user)
        {
            using (IDbConnection cnct = new SQLiteConnection(LoadConnectionString()))
            {
                await cnct.ExecuteAsync("INSERT INTO User " +
                    "VALUES ( @Id, @Login, @Password, @LastTimeOnline," +
                    "@IsOnline, @Mail, @MobileNum, @AvatarPath)", user);
            }
        }
        public async Task AddChatMember(ChatMember member)
        {
            using (IDbConnection cnct = new SQLiteConnection(LoadConnectionString()))
            {
                await cnct.ExecuteAsync("INSERT INTO ChatMember " +
                    "VALUES (@Id,@Chat_FK,@User_FK)", new { member.Id, Chat_FK= member.Chat.Id, User_FK = member.User.Id });
            }
        }
        public async Task AddChat(Chat chat)
        {
            using (IDbConnection cnct = new SQLiteConnection(LoadConnectionString()))
            {
                await cnct.ExecuteAsync("INSERT INTO Chat " +
                    "VALUES (@Id,@Title,@IsPrivate,@ImagePath)", chat);
            }
        }

        public async Task<Chat> GetChatById(int id)
        {
            using (IDbConnection cnct = new SQLiteConnection(LoadConnectionString()))
            {
                var res = await cnct.QuerySingleAsync<Chat>("SELECT TOP(1) FROM Chat " +
                    "WHERE Id = @Id", id);
                return res;
            }
        }
        public async Task<bool> IfChatExist(Chat chat)
        {
            if (await GetChatById(chat.Id) != null)
            {
                return true;
            }
            return false;
        }
        private string LoadConnectionString(string id = "MusgramCS")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }
    }
}
