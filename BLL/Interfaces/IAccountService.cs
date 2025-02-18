using BLL.DTOs;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IAccountService
    {
        public Task<SystemAccount> Login(string email, string password);

        public Task<SystemAccount> GetAccountByIdAsync(short accountId);

        Task UpdateAccountAsync(SystemAccountVM accountVM);
        Task UpdateProfile(SystemAccountVM accountVM);

        Task<List<SystemAccount>> GetAllAccountsAsync();

        Task CreateAccountAsync(SystemAccountVM account);
        Task DeleteAccountAsync(short id);
        Task<bool> HasRelatedEntitiesAsync(short id); // Kiểm tra có liên kết với bảng khác không

        Task<SystemAccount> GetAccountByEmailAsync(string email);
        IEnumerable<SystemAccount> SearchAccounts(string name, int? role);

        Task<bool> IsEmailUniqueAsync(string email);
        Task<SystemAccount> DisableAccount(short id);
    }
}
