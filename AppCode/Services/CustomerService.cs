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
    public class CustomerService : BaseService
    {
        public CustomerService() : base() { }
        public CustomerService(AtmSecureDataContext context) : base(context) { }

        public void GetCustomer()
        {
            var res = _context.Customers
                .FirstOrDefault(w => w.CustomerNumber == JsonRequest.Credentials.CustomerNumber);
            JsonResponse.Telefono = res.Telefono;
            JsonResponse.MessageResult = "Se ha consultado el telefono del usuario.";
            LlenarBitacora();
        }

        public void UpdateTelefono()
        {
            var res = _context.Customers
                .FirstOrDefault(w => w.CustomerNumber == JsonRequest.Credentials.CustomerNumber);
            res.Telefono = JsonRequest.Telefono;
            JsonResponse.MessageResult = "Se ha actualizado el telefono.";
            LlenarBitacora();
        }

        public void Login()
        {
            var res = _context.Customers
                .FirstOrDefault(w => w.CustomerNumber == JsonRequest.Credentials.CustomerNumber &&
                w.Pin == JsonRequest.Credentials.Pin);

            JsonResponse.MessageResult = res == null ? "Denegado" : "Autorizado";
            LlenarBitacora();
        }

        public void ChangePin()
        {
            var res = _context.Customers.FirstOrDefault(w => w.CustomerNumber == JsonRequest.Credentials.CustomerNumber);
            res.Pin = JsonRequest.Credentials.NewPin;
            JsonResponse.MessageResult = $"Se ha cambiado el Pin de la cuenta: {JsonRequest.Credentials.CustomerNumber}";
            LlenarBitacora();

        }
    }
}
