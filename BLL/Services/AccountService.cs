using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly AdminAccount _adminAccount;

        public AccountService(IAccountRepository accountRepository, IOptions<AdminAccount> adminAccount)
        {
            _accountRepository = accountRepository;
            _adminAccount = adminAccount.Value;
        }
        public async Task<SystemAccount> Login(string email, string password)
        {
            if (email.IsNullOrEmpty() || password.IsNullOrEmpty()) return null;
            if (email == _adminAccount.Email && password == _adminAccount.Password)
            {
                return new SystemAccount
                {
                    AccountName = "Admin",
                    AccountEmail = email,
                    AccountPassword = password,
                    AccountRole = 0,
                };
            }
            var user = await _accountRepository.GetAccountByEmailAsync(email);
            if (user == null) return null;
            if (user.AccountPassword != password) return null;
            return user;
        }
    }
}
