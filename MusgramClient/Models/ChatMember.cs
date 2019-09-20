using Newtonsoft.Json;
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
        [JsonIgnore]
        public Chat Chat { get; set; }
        public User User { get; set; }
    }
}
