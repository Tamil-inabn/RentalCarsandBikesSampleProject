﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.ViewModel
{
    public class LoginCheckViewBAL
    {
        public int UserId { get; set; }
        public string? UserName { get; set; }
        public string? EmailId { get; set; }
        public string? Password { get; set; }
        public int? RollId { get; set; }
    }
}