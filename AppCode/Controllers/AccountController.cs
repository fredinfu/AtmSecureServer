using FtpServerUI.AppCode.Context;
using FtpServerUI.AppCode.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FtpServerUI.AppCode.Controllers
{
    class AccountController : AtmControllerBase
    {
        
        public AccountController()
        {
            _context = new AtmSecureDataContext();
        }

        public AccountController(JsonRequest json)
        {
            JsonRequest = json;
            _context = new AtmSecureDataContext();
        }

        public void Login()
        {
            var res = _context.Customers
                .FirstOrDefault(w => w.CustomerNumber == JsonRequest.Credentials.CustomerNumber && 
                w.Pin == JsonRequest.Credentials.Pin);

            Result = res == null ? "Denegado" : "Autorizado";
            LlenarBitacora();
        }
    }
}
