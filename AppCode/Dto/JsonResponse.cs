using AtmServer.AppCode.DtoModels; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtmServer.AppCode.Dto
{
    public class JsonResponse : JsonFormat
    {
        public JsonResponse()
        {
            Account = new AccountDto();
            Product = new ProductDto();
        }
        public string MessageResult { get; set; }
        public string CustomerNumber { get; set; }
        public string CustomerName { get; set; }
        public string Telefono { get; set; }
        public AccountDto Account { get; set; }
        public ProductDto Product { get; set; }
        public List<LogDto> Logs { get; set; }
    }
}
