using FtpServerUI.AppCode.Context;
using FtpServerUI.AppCode.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FtpServerUI.AppCode.Controllers
{
    class UserController : ControllerBase
    {
        
        public UserController()
        {
            _context = new FtpSecureNetworkDataContext();
        }

        public UserController(JsonRequest json)
        {
            JsonRequest = json;
            _context = new FtpSecureNetworkDataContext();
        }

        public void Login()
        {
            var res = _context.Users
                .FirstOrDefault(w => w.Username == JsonRequest.Credentials.Username && 
                w.Password == JsonRequest.Credentials.Password);

            Result = res == null ? "Denegado" : "Autorizado";
            LlenarBitacora();
        }
    }
}
