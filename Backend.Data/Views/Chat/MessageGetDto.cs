﻿using Backend.Data.Views.MessageFile;

namespace Backend.Data.Views.Message
{
    public class MessageGetDto
    {
        public int Id { get; set; }
        public int ConversationId { get; set; }
        public string Text { get; set; }
        public bool FromAdmin { get; set; }
        public DateTime DateSent { get; set; }
        public ICollection<MessageFileGetDto> MessageFiles { get; set; }
    }
}
