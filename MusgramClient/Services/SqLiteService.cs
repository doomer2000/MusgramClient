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
    public class SqLiteService
    {
        public void AddMessage(Message mes)
        {
            using (IDbConnection cnct = new SQLiteConnection(LoadConnectionString()))
            {
                cnct.Execute("INSERT INTO Messages VALUES " +
                    "(@Id,@Text,@SendTime," +
                    "@MessageType,@Music_Id," +
                    "@ImagePath,@VideoPath," +
                    "@VoicePath,@User_Id," +
                    "@Chat_Id)"
                    , new {mes.Id, mes.Text,SendTime = mes.SendTime.ToString(),
                    mes.MessegeType, Music_Id = mes.Music.Id,
                    mes.ImagePath,mes.VideoPath,
                    mes.VoicePath,mes.UserIdOfWho,
                    Chat_Id = mes.GeneralChatId});
            }
        }
        public List<Chat> GetChats()
        {
            using(IDbConnection cnct = new SQLiteConnection(LoadConnectionString()))
            {
                var chats = cnct.Query<Chat>("SELECT * FROM Chat", new DynamicParameters());
                return chats.ToList();
            }
        }
        public void AddUser(User user)
        {
            using (IDbConnection cnct = new SQLiteConnection(LoadConnectionString()))
            {
                cnct.Execute("INSERT INTO User " +
                    "VALUES ( @Id, @Login, @Password, @LastTimeOnline," +
                    "@IsOnline, @Mail, @MobileNum, @AvatarPath)", user);
            }
        }

        public string LoadConnectionString(string id = "MusgramCS")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }
    }
}
