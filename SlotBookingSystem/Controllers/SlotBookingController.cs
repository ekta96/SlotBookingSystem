using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SlotBookingSystem.Models;

namespace SlotBookingSystem.Controllers
{
    public class SlotBookingController : ApiController
    {
        public User[] Get()
        {
            return new User[]
            {
                new User
                {
                    UserID = "A0001",
                    FirstName = "Glenn Block"
                },
                new User
                {
                    UserID = "A0002",
                    FirstName = "Dan Roth"
                }
            };
        }
    }
}
