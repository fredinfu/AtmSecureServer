using AtmServer.AppCode.Context;
using AtmServer.AppCode.Dto;
using System;

namespace AtmServer.AppCode.Controllers
{
    class AtmControllerBase
    {
        protected static AtmSecureDataContext _context;
        public JsonRequest JsonRequest { get; set; }
        public string Result{ get; set; }
        public string Username { get; set; }

        public void LlenarBitacora()
        {
            if (JsonRequest.Credentials.CustomerNumber.Trim() == string.Empty)
            {
                _context.Logs.InsertOnSubmit(new Log
                {
                    Action = "Ocurrió un problema con las credenciales.",
                    Customer = JsonRequest.Credentials.CustomerNumber,
                    CreatedDate = DateTime.Now,
                    Description = "El servidor no reconoció el usuario que emitió la acción."
                });
                _context.SubmitChanges();
                return;
            }

            try
            {
                var log = new Log
                {
                    Action = JsonRequest.Action,
                    Description = Result,
                    CreatedDate = DateTime.Now,
                    Customer = JsonRequest.Credentials.CustomerNumber
                };
                _context.Logs.InsertOnSubmit(log);
                _context.SubmitChanges();

            }catch (Exception ex){
                _context.Logs.InsertOnSubmit(new Log
                {
                    Action = "Guardar bitácora.",
                    Customer = "1",
                    CreatedDate = DateTime.Now,
                    Description = "El servidor no pudo guardar los datos correctamente."
                });
                _context.SubmitChanges();
            }
        }

    }
}
