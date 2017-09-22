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
    public class ProductService : BaseService
    {
        public ProductService() : base() {}

        public ProductService(AtmSecureDataContext context) : base(context) {}

        public ProductService(JsonRequest json)
        {
            JsonRequest = json;
            _context = new AtmSecureDataContext();
        }

        public void GetAllByCustomer()
        {

            var res = _context.Products
                .Where(w => w.CustomerNumber == JsonRequest.Credentials.CustomerNumber)
                .Select(s => new ProductDto
                {
                    ProductNumber = s.ProductNumber,
                    ProductType = s.ProductType,
                    Alias = s.Alias,
                    Balance = s.Balance,
                    Credit = s.Credit
                }).ToList();

            JsonResponse.Account.Products = res;
            JsonResponse.MessageResult = "Se ha consultado los productos del usuario.";
            LlenarBitacora();
        }


        public void GetProducto(string productNumber)
        {

            var res = _context.Products.FirstOrDefault(w => w.ProductNumber == productNumber);

            JsonResponse.Product = new ProductDto
            {
                ProductNumber = res.ProductNumber,
                ProductType = res.ProductType,
                Alias = res.Alias,
                Balance = res.Balance,
                Credit = res.Credit
            };
            JsonResponse.MessageResult = $"Se ha consultado el producto: {res.Alias} del usuario.";
            LlenarBitacora();
        }

        public void Pay()
        {
            var res = _context.Products.FirstOrDefault(w => w.ProductNumber == JsonRequest.DestinyNumber);
            res.Balance -= JsonRequest.Account.Deposit;

            if(res.ProductType == "CREDITCARD") { res.Credit += Convert.ToDecimal(JsonRequest.Account.Deposit); }
            JsonResponse.MessageResult = $"Se ha hecho un pago con el monto de: L.{JsonRequest.Account.Deposit} para el producto: {res.Alias}.";
            LlenarBitacora();
        }

    }
}
