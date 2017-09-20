using FtpServerUI.AppCode.DtoModels; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FtpServerUI.AppCode.Dto
{
    public class JsonRequest : JsonFormat
    {
        public string Service { get; set; }
        public string Action { get; set; }

    }
}
