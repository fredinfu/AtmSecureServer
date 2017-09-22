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
        public AccountService()
        {
            _context = new AtmSecureDataContext();
        }

        public AccountService(AtmSecureDataContext context)
        {
            _context = context;
        }

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
            LlenarBitacora();
        }

        public void Withdrawal()
        {
            var res = _context.Accounts.FirstOrDefault(w => w.AccountNumber == JsonRequest.Credentials.CustomerNumber);
            res.Balance = res.Balance - JsonRequest.Account.Withdrawal;

            JsonResponse.Account = new AccountDto
            {
                AccountNumber = res.AccountNumber,
                Description = res.Description,
                Balance = res.Balance
            };
        }
    }
}
