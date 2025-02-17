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

        public async Task<SystemAccount> GetAccountByEmailAsync(string email)
        {
            return await _context.SystemAccounts.FirstOrDefaultAsync(x => x.AccountEmail == email);
        }

    }
}
