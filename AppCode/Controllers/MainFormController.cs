using AtmServer.AppCode.Context;
using AtmServer.AppCode.Dto;
using AtmServer.AppCode.Services;

namespace AtmServer.AppCode.Controllers
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

        public void ChangePin()
        {
            using (var context = new AtmSecureDataContext())
            {
                var service = new CustomerService(context);
                service.JsonRequest = JsonRequest;
                service.ChangePin();
                JsonResponse = service.JsonResponse;
            }
        }

        public void GetCustomer()
        {
            using (var context = new AtmSecureDataContext())
            {
                var service = new CustomerService(context);
                service.JsonRequest = JsonRequest;
                service.GetCustomer();
                JsonResponse = service.JsonResponse;
            }
        }

        public void UpdateTelefono()
        {
            using (var context = new AtmSecureDataContext())
            {
                var service = new CustomerService(context);
                service.JsonRequest = JsonRequest;
                service.UpdateTelefono();
                JsonResponse = service.JsonResponse;
            }
        }

        public void GetAccount()
        {
            using (var context = new AtmSecureDataContext())
            {
                var service = new AccountService(context);
                service.JsonRequest = JsonRequest;
                service.GetAccount(JsonRequest.Credentials.CustomerNumber);
                JsonResponse = service.JsonResponse;
            }
        }

        public void Withdrawal()
        {
            using (var context = new AtmSecureDataContext())
            {
                var service = new AccountService(context);
                service.JsonRequest = JsonRequest;
                service.Withdrawal();
                JsonResponse = service.JsonResponse;
            }
        }

        public void Deposit()
        {
            using (var context = new AtmSecureDataContext())
            {
                var service = new AccountService(context);
                service.JsonRequest = JsonRequest;
                service.Deposit();
                JsonResponse = service.JsonResponse;
            }
        }

        public void Transfer()
        {
            using (var context = new AtmSecureDataContext())
            {
                var service = new AccountService(context);
                service.JsonRequest = JsonRequest;
                service.Transfer();
                JsonResponse = service.JsonResponse;
            }
        }

        public void GetAllProductsByCustomer()
        {
            using (var context = new AtmSecureDataContext())
            {
                var service = new ProductService(context);
                service.JsonRequest = JsonRequest;
                service.GetAllByCustomer();
                JsonResponse = service.JsonResponse;
            }
        }

        public void PayProduct()
        {
            using (var context = new AtmSecureDataContext())
            {
                var service = new ProductService(context);
                service.JsonRequest = JsonRequest;
                service.Pay();
                JsonResponse = service.JsonResponse;
            }
        }

        public void GetAllLogByCustomer()
        {
            using (var context = new AtmSecureDataContext())
            {
                var service = new LogService(context);
                service.JsonRequest = JsonRequest;
                service.GetLogs();
                JsonResponse = service.JsonResponse;
            }
        }
    }
}
