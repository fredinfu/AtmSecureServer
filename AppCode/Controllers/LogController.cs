using AtmServer.AppCode.Context;
using AtmServer.AppCode.Dto;
using System.Collections.Generic;
using System.Linq;


namespace AtmServer.AppCode.Controllers
{
    class LogController : AtmControllerBase
    {
        
        public LogController()
        {
            _context = new AtmSecureDataContext();
        }

        public LogController(JsonRequest json)
        {
            JsonRequest = json;
            _context = new AtmSecureDataContext();
        }

        public IEnumerable<object> GetAll()
        {
            var res = _context.Logs
                .Select(s => new
                {
                    s.Description,
                    s.Action,
                    s.CreatedDate,
                    s.Customer
                }).ToList();
            return res;
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
