using FtpServerUI.AppCode.DtoModels; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FtpServerUI.AppCode.Dto
{
    class JsonRequest : JsonFormat
    {
        public string Controller { get; set; }
        public string Action { get; set; }
        public UserDto UserAux { get; set; }
        public FileDto FileAux { get; set; }

    }
}
