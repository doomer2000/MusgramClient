﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusgramClient.Models
{
    public class Chat
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsPrivate { get; set; }
        public string ImagePath { get; set; }
        [NonSerialized]
        public ICollection<Message> Messages;
        public ICollection<ChatMember> ChatMembers { get; set; }
    }
}
