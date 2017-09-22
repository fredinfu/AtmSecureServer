using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FtpServerUI.AppCode.DtoModels
{
    public class AccountDto
    {
        public string AccountNumber { get; set; }
        public string Description { get; set; }
        public decimal? Balance{ get; set; }
        public decimal? Withdrawal { get; set; }
        public decimal? Deposit { get; set; }
        public decimal? Transfer { get; set; }
    }
}
