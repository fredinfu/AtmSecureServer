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
    public class AccountService : BaseService
    {
        public AccountService() : base() {}

        public AccountService(AtmSecureDataContext context) : base(context) {}

        public AccountService(JsonRequest json)
        {
            JsonRequest = json;
            _context = new AtmSecureDataContext();
        }

        public void GetAccount(string accountNumber)
        {

            var res = _context.Accounts.FirstOrDefault(w => w.AccountNumber == accountNumber);

            JsonResponse.Account = new AccountDto
            {
                AccountNumber = res.AccountNumber,
                Description = res.Description,
                Balance = res.Balance
            };
            JsonResponse.MessageResult = "Se ha consultado el saldo de la cuenta.";
            LlenarBitacora();
        }

        public void Withdrawal()
        {
            var res = _context.Accounts.FirstOrDefault(w => w.AccountNumber == JsonRequest.Credentials.CustomerNumber);
            if(res.Balance - JsonRequest.Account.Withdrawal < 0)
            {
                JsonResponse.Account = new AccountDto
                {
                    AccountNumber = res.AccountNumber,
                    Description = res.Description,
                    Balance = res.Balance
                };
                JsonResponse.MessageResult = "No se puede retirar esa cantidad de dinero, supera a tus fondos actuales.";
                LlenarBitacora();
                return;

            }
            res.Balance -= JsonRequest.Account.Withdrawal;
            JsonResponse.Account = new AccountDto
            {
                AccountNumber = res.AccountNumber,
                Description = res.Description,
                Balance = res.Balance
            };
            JsonResponse.MessageResult = $"Se ha retirado de tu cuenta L.{JsonRequest.Account.Withdrawal}.";
            LlenarBitacora();
        }

        public void Deposit()
        {
            var res = _context.Accounts.FirstOrDefault(w => w.AccountNumber == JsonRequest.Credentials.CustomerNumber);
            res.Balance += JsonRequest.Account.Deposit;
            JsonResponse.Account = new AccountDto
            {
                AccountNumber = res.AccountNumber,
                Description = res.Description,
                Balance = res.Balance
            };
            JsonResponse.MessageResult = $"Se ha depositado a su cuenta L.{JsonRequest.Account.Withdrawal}.";
            LlenarBitacora();
        }

        public void Transfer()
        {
            var account = _context.Accounts.FirstOrDefault(w => w.AccountNumber == JsonRequest.Credentials.CustomerNumber);

            if(account.Balance-JsonRequest.Account.Transfer < 0) { JsonResponse.MessageResult = "No se puede transferir esa cantidad de dinero, supera a tus fondos actuales."; LlenarBitacora(); return; }

            account.Balance -= JsonRequest.Account.Transfer;

            var destinyAccount = _context.Accounts.FirstOrDefault(w => w.AccountNumber == JsonRequest.DestinyNumber);
            destinyAccount.Balance += JsonRequest.Account.Transfer;

            JsonResponse.MessageResult = $"Se ha transferido L.{JsonRequest.Account.Transfer} a la cuenta {JsonRequest.DestinyNumber}.";
            LlenarBitacora();
        }
    }
}
