using AtmServer.AppCode.DtoModels; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtmServer.AppCode.Dto
{
    public class JsonRequest : JsonFormat
    {
        public string Service { get; set; }
        public string Action { get; set; }
        public AccountDto Account { get; set; }
        public string DestinyNumber { get; set; }
        public string Telefono { get; set; }

    }
}
