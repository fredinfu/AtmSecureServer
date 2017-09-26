using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtmServer.AppCode.DtoModels
{
    public class ProductDto
    {
        public string ProductNumber { get; set; }
        public string ProductType { get; set; }
        public string Alias { get; set; }
        public decimal? Balance { get; set; }
        public decimal Credit { get; set; }
        public string CustomerNumber { get; set; }
    }
}
