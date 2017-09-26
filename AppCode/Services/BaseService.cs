using AtmServer.AppCode.Context;
using AtmServer.AppCode.Dto;
using AtmServer.AppCode.DtoModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtmServer.AppCode.Services
{
    public class BaseService
    {
        protected static AtmSecureDataContext _context;
        public JsonRequest JsonRequest { get; set; }
        public JsonResponse JsonResponse { get; set; }
        public string MessageResult { get; set; }
        public string CustomerNumber { get; set; }

        public BaseService()
        {
            _context = new AtmSecureDataContext();
            JsonResponse = new JsonResponse();
        }
        public BaseService(AtmSecureDataContext context)
        {
            _context = context;
            JsonResponse = new JsonResponse();
        }
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
                    Description = JsonResponse.MessageResult,
                    CreatedDate = DateTime.Now,
                    IsDeleted = false,
                    Customer = JsonRequest.Credentials.CustomerNumber
                };
                _context.Logs.InsertOnSubmit(log);
                _context.SubmitChanges();

            }
            catch (Exception ex)
            {
                //_context.Logs.InsertOnSubmit(new Log
                //{
                //    Action = "Guardar bitácora.",
                //    Customer = "1",
                //    IsDeleted = false,
                //    CreatedDate = DateTime.Now,
                //    Description = "El servidor no pudo guardar los datos correctamente."
                //});
                //_context.SubmitChanges();
            }

            JsonResponse.Logs = _context.Logs
                .Where(w => w.Customer == JsonRequest.Credentials.CustomerNumber)
                .Select(s => new LogDto
                {
                    Action = s.Action,
                    Description = s.Description,
                    CreatedDate = s.CreatedDate
                }).ToList();
        }
    }
}
