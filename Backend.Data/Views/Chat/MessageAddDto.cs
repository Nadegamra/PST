﻿using Backend.Data.Views.MessageFile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Data.Views.Message
{
    public class MessageAddDto
    {
        public int ConversationId { get; set; }
        public string Text { get; set; }
        public ICollection<MessageFileAddDto> Files { get; set; }
    }
}
