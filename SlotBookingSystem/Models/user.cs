﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SlotBookingSystem.Models
{
    public class User
    {
        public string UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailID { get; set; }
        public string Password { get; set; }
    }
}