﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Domain.Entities
{
    public class StripeSettings
    {
        public string PublicKey { get; set; }
        public string SecretKey { get; set; }
    }
}
