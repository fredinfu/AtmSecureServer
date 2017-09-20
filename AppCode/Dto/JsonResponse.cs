using FtpServerUI.AppCode.DtoModels; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FtpServerUI.AppCode.Dto
{
    public class JsonResponse : JsonFormat
    {
        public string MessageResult { get; set; }
        public string CustomerNumber { get; set; }
        public string CustomerName { get; set; }
        public List<LogDto> Logs { get; set; }
        public AccountDto Account { get; set; }
    }
}
