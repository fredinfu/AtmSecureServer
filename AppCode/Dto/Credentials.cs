﻿using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtmServer.AppCode.Dto
{
    public class Credentials
    {
        public string CustomerNumber { get; set; }
        public string Pin { get; set; }
        public string NewPin { get; set; }
        public string Name { get; set; }
        public string Hash { get; set; }
    }
}
