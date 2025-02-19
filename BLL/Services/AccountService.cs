using BLL.DTOs;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
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
        public Task<SystemAccount> GetAccountByIdAsync(short accountId)
        {
            if (accountId <= 0)
            {
                throw new ArgumentException("Account ID must be greater than zero.", nameof(accountId));
            }

            // Gọi phương thức trong repository để lấy tài khoản theo ID
            var account = _accountRepository.GetAccountByIdAsync(accountId);
            return account;
        }

        public Task<SystemAccount> GetAccountByEmailAsync(string email)
        {
            

            // Gọi phương thức trong repository để lấy tài khoản theo ID
            var account = _accountRepository.GetAccountByEmailAsync(email);
            return account;
        }


        public async Task<List<SystemAccount>> GetAllAccountsAsync()
        {
            return await _accountRepository.GetAllAsync();
        }


        public async Task CreateAccountAsync(SystemAccountVM newAccount)
        {
            if (newAccount == null)
                throw new ArgumentNullException(nameof(newAccount));

            // Đếm số lượng tài khoản hiện có trong DB
            int totalAccounts = await _accountRepository.CountAsync();

            var systemAccount = new SystemAccount
            {
                AccountId = (short)(totalAccounts + 1), // Gán AccountId tự động
                AccountName = newAccount.AccountName,
                AccountEmail = newAccount.AccountEmail,
                AccountPassword = "123", 
                AccountRole = newAccount.AccountRole
            };

            await _accountRepository.AddAsync(systemAccount);
        }
        public async Task DeleteAccountAsync(short id)
        {
            try
            {
                await _accountRepository.DeleteAsync(id);
            }
            catch (DbUpdateException ex) // Lỗi khóa ngoại
            {
                throw new Exception("Cannot delete account. It is linked to existing news.");
            }
        }

        // Phương thức kiểm tra liên kết và xóa tài khoản
        public async Task<bool> HasRelatedEntitiesAsync(short id)
        {
            return await _accountRepository.HasRelatedEntitiesAsync(id); // Gọi repository để kiểm tra
        }

        public async Task UpdateProfile(SystemAccountVM accountVM)
        {
            // Lấy tài khoản hiện tại từ DB để đảm bảo các trường không bị thay đổi nếu null
            var account = await _accountRepository.GetAccountByIdAsync(accountVM.AccountId);

            if (account == null)
                throw new ArgumentException("Account not found.");

            // Chỉ cập nhật các trường không phải null
            if (!string.IsNullOrEmpty(accountVM.AccountName))
            {
                account.AccountName = accountVM.AccountName;
            }

            if (!string.IsNullOrEmpty(accountVM.AccountEmail))
            {
                account.AccountEmail = accountVM.AccountEmail;
            }

            if (!string.IsNullOrEmpty(accountVM.AccountPassword))
            {
                account.AccountPassword = accountVM.AccountPassword; // Lưu ý: xử lý mật khẩu (hashing, encryption)
            }

            // Cập nhật thông tin tài khoản vào DB
            await _accountRepository.UpdateAccountAsync(account);
        }

        

        public async Task UpdateAccountAsync(SystemAccountVM accountVM)
        {
            // Lấy tài khoản hiện tại từ DB để đảm bảo các trường không bị thay đổi nếu null
            var account = await _accountRepository.GetAccountByIdAsync(accountVM.AccountId);

            if (account == null)
                throw new ArgumentException("Account not found.");

            // Chỉ cập nhật các trường không phải null
            if (!string.IsNullOrEmpty(accountVM.AccountName))
            {
                account.AccountName = accountVM.AccountName;
            }

            if (!string.IsNullOrEmpty(accountVM.AccountEmail))
            {
                account.AccountEmail = accountVM.AccountEmail;
            }

            if (!string.IsNullOrEmpty(accountVM.AccountPassword))
            {
                account.AccountPassword = accountVM.AccountPassword; // Lưu ý: xử lý mật khẩu (hashing, encryption)
            }

            if (!string.IsNullOrEmpty(accountVM.AccountRole+""))
            {
                account.AccountRole = accountVM.AccountRole; // Lưu ý: xử lý mật khẩu (hashing, encryption)
            }


            // Cập nhật thông tin tài khoản vào DB
            await _accountRepository.UpdateAccountAsync(account);
        }

        public async Task<SystemAccount> DisableAccount(short id)
        {
            
            Console.WriteLine("++++++++++++++++++++ Id" + id);
            var result = await _accountRepository.DisableAccount(id);
            if (result == false)
            {
                throw new Exception("Disable account failed!");
            }
            return await GetAccountByIdAsync(id);
        }

        public IEnumerable<SystemAccount> SearchAccounts(string name, int? role)
        {
            return _accountRepository.SearchAccounts(name, role);
        }

        public async Task<bool> IsEmailUniqueAsync(string email)
        {
            return await _accountRepository.IsEmailUniqueAsync(email);
        }
    }
}
