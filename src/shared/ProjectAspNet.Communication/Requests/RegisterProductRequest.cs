﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAspNet.Communication.Requests
{
    public class RegisterProductRequest
    {
        public string ProductName { get; set; } = string.Empty;
        public string Description {  get; set; } = string.Empty;
        public double Price { get; set; }
        public int Quantity { get; set; }
        public string Brand { get; set; } = string.Empty;
        
    }
}