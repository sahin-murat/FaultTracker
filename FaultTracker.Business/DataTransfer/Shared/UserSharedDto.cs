﻿using System;
using System.Collections.Generic;
using System.Text;

namespace FaultTracker.Business.DataTransfer.Shared
{
    public class UserSharedDto
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string profilepicture { get; set; }
    }
}
