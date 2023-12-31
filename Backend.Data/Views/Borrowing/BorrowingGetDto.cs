﻿using Backend.Data.Models;
using Backend.Data.Views.User;
using Backend.Data.Views.UserConsole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Data.Views.BorrowedConsole
{
    public class BorrowingGetDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public UserGet User { get; set; } 
        public int ConversationId { get; set; }
        public BorrowingStatus Status { get; set; }
        public ICollection<UserConsoleGetDto> UserConsoles { get; set; }
    }
}
