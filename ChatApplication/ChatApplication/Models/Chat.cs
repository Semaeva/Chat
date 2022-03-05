﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApplication.Models
{
    public class Chat
    {
        public int id { get; set; }
        public string msg { get; set; }
       
        public int personId { get; set; }
        public Person Person { get; set; }

    }
}