﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Data.Views.Console
{
    public class ImageDtoUpdate
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; } = "";
    }
}
