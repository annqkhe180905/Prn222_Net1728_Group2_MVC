using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IAccountRepository
    {
        public Task<SystemAccount> GetAccountByEmailAsync (string email);
        public Task<SystemAccount> GetAccountByIdAsync(short accountId);

        public Task UpdateAccountAsync(SystemAccount account);

        
            Task<int> CountAsync();
        

        Task<List<SystemAccount>> GetAllAsync();
        Task AddAsync(SystemAccount account);
        Task DeleteAsync(short id);

        Task<bool> DisableAccount(short id);
        Task<bool> IsEmailUniqueAsync(string email);
        IEnumerable<SystemAccount> SearchAccounts(string name, int? role);

        Task<bool> HasRelatedEntitiesAsync(short id); // Kiểm tra liên kết với bảng khác

        //    void DisableAccount(int id);
    }
}
