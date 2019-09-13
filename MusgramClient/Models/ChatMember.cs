using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusgramClient.Models
{
    public class ChatMember
    {
        public int Id { get; set; }
        public Chat GeneralChatId { get; set; }
        public User UserId { get; set; }
    }
}
