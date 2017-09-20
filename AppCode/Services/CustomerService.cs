using FtpServerUI.AppCode.Context;
using FtpServerUI.AppCode.Dto;
using FtpServerUI.AppCode.DtoModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FtpServerUI.AppCode.Services
{
    public class CustomerService : BaseService
    {
        public CustomerService() : base() { }
        public CustomerService(AtmSecureDataContext context) : base(context) { }
        public void Login()
        {
            var res = _context.Customers
                .FirstOrDefault(w => w.CustomerNumber == JsonRequest.Credentials.CustomerNumber &&
                w.Pin == JsonRequest.Credentials.Pin);

            JsonResponse.MessageResult = res == null ? "Denegado" : "Autorizado";
            LlenarBitacora();
        }
    }
}
