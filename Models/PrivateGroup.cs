﻿using ChatApp.Models.Interface;

namespace ChatApp.Models
{
    public class PrivateGroup : Group
    {
        public User GroupAdmin { get; set; }
    }
}
