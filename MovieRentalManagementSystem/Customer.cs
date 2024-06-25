﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRMS_Model
{
    public class Customer
    {
        public string Username;
        public string Password;
        public DateTime DateVerified;
        public DateTime DateCreated = DateTime.Now;
        public DateTime DateUpdated;
        public CustomerProfile Profile;
        public int Status;

    }
}
