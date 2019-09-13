using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusgramClient.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime SendTime { get; set; }
        public DateTime ReceiveTime { get; set; }
        public DateTime ReadTime { get; set; }
        //Later
        public int MessegeType { get; set; }
        //0-text
        //1-audio
        //2-image
        //3-video
        public Music Music { get; set; }
        public string ImagePath { get; set; }
        public string VideoPath { get; set; }
        public string VoicePath { get; set; }

        public User UserIdOfWho { get; set; }
        public Chat GeneralChatId { get; set; }

    }
}
