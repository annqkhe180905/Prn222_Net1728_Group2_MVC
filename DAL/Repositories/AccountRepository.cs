using DAL.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly FunewsManagementContext _context;

        public AccountRepository(FunewsManagementContext context)
        {
            _context = context;
        }



        public async Task<int> CountAsync()
        {
            return await _context.SystemAccounts.CountAsync();
        }

        public async Task<bool> IsEmailUniqueAsync(string email)
        {
            return !await _context.SystemAccounts.AnyAsync(x => x.AccountEmail == email);
        }



        public async Task AddAsync(SystemAccount account)
        {
            if (account == null)
                throw new ArgumentNullException(nameof(account));

            await _context.SystemAccounts.AddAsync(account);
            await _context.SaveChangesAsync();
        }

        public async Task<SystemAccount> GetAccountByEmailAsync(string email)
        {
            return await _context.SystemAccounts.FirstOrDefaultAsync(x => x.AccountEmail == email);
        }



        public async Task<SystemAccount> GetAccountByIdAsync(short accountId)
        {
            if (accountId <= 0)
            {
                throw new ArgumentException("Account ID must be greater than zero.", nameof(accountId));
            }

            // Truy vấn dữ liệu từ cơ sở dữ liệu
            return await _context.SystemAccounts.FirstOrDefaultAsync(x => x.AccountId == accountId);
        }
        public async Task<List<SystemAccount>> GetAllAsync()
        {
            var list = await _context.SystemAccounts.ToListAsync();
            list.Reverse(); // Đảo ngược danh sách
            return list;
        }

        // Cập nhật tài khoản
        public async Task UpdateAccountAsync(SystemAccount account)
        {
            var existingAccount = await _context.SystemAccounts
                                                 .FirstOrDefaultAsync(a => a.AccountId == account.AccountId);

            if (existingAccount != null)
            {
                existingAccount.AccountName = account.AccountName;
                existingAccount.AccountEmail = account.AccountEmail;
                existingAccount.AccountPassword = account.AccountPassword;

                _context.SystemAccounts.Update(existingAccount);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> DisableAccount(short id)
        {
            var account = await _context.SystemAccounts.FindAsync(id);
            if (account != null)
            {
                account.AccountRole = 3;
                _context.SystemAccounts.Update(account);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> HasRelatedEntitiesAsync(short id)
        {
            var account = await _context.SystemAccounts
                .Include(a => a.NewsArticles)  // Giả sử Account có liên kết với News
                .FirstOrDefaultAsync(a => a.AccountId == id);

            // Kiểm tra nếu có bất kỳ News nào liên kết với tài khoản này
            if (account != null && account.NewsArticles.Any())
            {
                return true; // Có quan hệ, không thể xóa
            }

            return false; // Không có quan hệ, có thể xóa
        }

        public async Task DeleteAsync(short id)
        {
            var account = await _context.SystemAccounts.FindAsync(id); // id là kiểu short
            if (account != null)
            {
                _context.SystemAccounts.Remove(account);
                await _context.SaveChangesAsync();
            }
        }


        public IEnumerable<SystemAccount> SearchAccounts(string name, int? role)
        {
            var query = _context.SystemAccounts.AsQueryable();

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(a => a.AccountName.Contains(name));
            }

            if (role.HasValue)
            {
                query = query.Where(a => a.AccountRole == role.Value);
            }

            return query.ToList();
        }
    }
}
