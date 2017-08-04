using FtpServerUI.AppCode.Context;
using FtpServerUI.AppCode.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FtpServerUI.AppCode.Controllers
{
    class ControllerBase
    {
        protected static FtpSecureNetworkDataContext _context;
        public JsonRequest JsonRequest { get; set; }
        public string Result{ get; set; }
        public string Username { get; set; }

        public void LlenarBitacora()
        {
            if (JsonRequest.Credentials.Username.Trim() == string.Empty)
            {
                _context.UserLogs.InsertOnSubmit(new UserLog
                {
                    Action = "Ocurrió un problema con las credenciales.",
                    CreatedBy = 1,
                    CreatedDate = DateTime.Now,
                    Description = "El servidor no reconoció el usuario que emitió la acción."
                });
                _context.SubmitChanges();
                return;
            }

            try
            {
                var log = new UserLog
                {
                    Action = JsonRequest.Action,
                    Description = Result,
                    CreatedDate = DateTime.Now,
                    CreatedByUsername = JsonRequest.Credentials.Username
                };
                _context.UserLogs.InsertOnSubmit(log);
                _context.SubmitChanges();

            }catch (Exception ex){
                _context.UserLogs.InsertOnSubmit(new UserLog
                {
                    Action = "Guardar bitácora.",
                    CreatedBy = 1,
                    CreatedDate = DateTime.Now,
                    Description = "El servidor no pudo guardar los datos correctamente."
                });
                _context.SubmitChanges();
            }
        }

    }
}
