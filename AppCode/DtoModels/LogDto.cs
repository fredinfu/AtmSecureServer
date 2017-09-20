using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FtpServerUI.AppCode.DtoModels
{
    public class LogDto
    {
        public string Description { get; set; }
        public string Action { get; set; }
        public DateTime CreatedDate { get; set; }
        public decimal? Balance { get; set; }
    }
}
