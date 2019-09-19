using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusgramClient.Models
{
    public class UnreadMessage
    {
        public int Id { get; set; }
        public Message Message { get; set; }
        public User UserWS { get; set; }
        public int UserTW_Id { get; set; }
        public User UserTW { get; set; }
    }
}
