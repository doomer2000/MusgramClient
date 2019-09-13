using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusgramClient.Models
{
    public class Friend
    {
        public int Id { get; set; }
        public User User { get; set; }
        public int _Friend_Id { get; set; }
        public User _Friend { get; set; }
    }
}
