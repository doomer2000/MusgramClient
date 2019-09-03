using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusgramClient.Models
{
    public class ChatMsg
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime SendTime { get; set; }
        public DateTime ReceiveTime { get; set; }
        public DateTime ReadTime { get; set; }
        //Later
        public Music Music { get; set; }
        public string ImagePath { get; set; }
        public string VideoPath { get; set; }


        public virtual User UserIdOfWho { get; set; }
        public virtual GeneralChat GeneralChatId { get; set; }

    }
}
