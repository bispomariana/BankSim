using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using ProjetoDevTrail.Api.Models;
using ProjetoDevTrail.Domain.Interfaces;
using ProjetoDevTrail.Infra.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoDevTrail.Infra.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly BankContext _context;
        public AccountRepository(BankContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Account account)
        {
            await _context.Accounts.AddAsync(account);
            await _context.SaveChangesAsync();
        }
        
        public async Task<Account> GetByIdAsync(Guid accountId)
        {
            var account = await _context.Accounts.FirstOrDefaultAsync(a => a.Account_Id == accountId);
            return await Task.FromResult(account); //PODE SER NULO
        }
        public async Task<Account> GetByNumberAsync(int accountNumber)
        {
            var account = await _context.Accounts.FirstOrDefaultAsync(a => a.Number == accountNumber);
            return await Task.FromResult(account); //PODE SER NULO
        }
        public async Task<IEnumerable<Account>> GetByClientIdAsync(Guid clientId)
        {
            var accounts = await _context.Accounts
                .Where(a => a.Client_Id == clientId)
                .ToListAsync();
            return await Task.FromResult(accounts);
        }

        public async Task<IEnumerable<SavingsAccount>> GetAllSavingsAccountsAsync()
        {
            return await _context.Accounts.OfType<SavingsAccount>().ToListAsync();
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
