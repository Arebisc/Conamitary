﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conamitary.Database.Models
{
    public class File
    {
        public Guid Id { get; set; }
        public string Md5Checksum { get; set; }

        public Guid ReceipeId { get; set; }
        public Receipe Receipe { get; set; }
    }
}