using FtpServerUI.AppCode.Context;
using FtpServerUI.AppCode.Dto;
using FtpServerUI.AppCode.DtoModels;
using FtpServerUI.AppCode.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FtpServerUI.AppCode.Controllers
{
    public class MainFormController : BaseController
    {
        
        public void Login()
        {
            using (var context = new AtmSecureDataContext())
            {
                var service = new CustomerService(context);
                service.JsonRequest = JsonRequest;
                service.Login();
                JsonResponse = service.JsonResponse;
            }
        }

        public void GetAccount()
        {
            using (var context = new AtmSecureDataContext())
            {
                var service = new AccountService(context);
                service.JsonResponse.MessageResult = "Consulta de saldo";
                service.GetAccount(JsonRequest.Credentials.CustomerNumber);
                JsonResponse = service.JsonResponse;
            }
        }
    }
}
