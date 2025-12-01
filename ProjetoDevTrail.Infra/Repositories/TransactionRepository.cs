using Microsoft.EntityFrameworkCore;
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
    public class TransactionRepository : ITransactionRepository
    {
        private readonly BankContext _context;
        public TransactionRepository(BankContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Transaction transaction)
        {
            await _context.Transactions.AddAsync(transaction);
        }

        public async Task<IEnumerable<Transaction>> GetByAccountNumberAsync(int accountNumber)
        {
            var accounts = _context.Accounts.Where(a => a.Number == accountNumber).Select(a => a.Account_Id).ToList();
            var transactions = _context.Transactions.Include(t => t.OriginAccount).Include(t => t.TargetAccount)
                .Where(t => accounts.Contains(t.Origin_Account_Id) || accounts.Contains((Guid)t.Target_Account_Id!));
            return await Task.FromResult(transactions);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
