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
    public class LogService : BaseService
    {
        public LogService() : base() { }
        public LogService(AtmSecureDataContext context) : base(context) { }

        public void GetLogs()
        {
            var res = _context.Logs
                .Where(w => w.Customer == JsonRequest.Credentials.CustomerNumber)
                .Select(s => new LogDto
                {
                    Action = s.Action,
                    Description = s.Description,
                    CreatedDate = s.CreatedDate
                }).ToList(); ;

            JsonResponse.Logs = res;

            JsonResponse.MessageResult = "Se ha consultado la bitacora.";
            //LlenarBitacora();
        }

    }
}
