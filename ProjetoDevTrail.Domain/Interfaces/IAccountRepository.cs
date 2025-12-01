using ProjetoDevTrail.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoDevTrail.Domain.Interfaces
{
    public interface IAccountRepository
    {
        Task<Account> GetByIdAsync(Guid accountId);
        Task<Account> GetByNumberAsync(int accountNumber);
        Task<IEnumerable<Account>> GetByClientIdAsync(Guid clientId);
        Task<IEnumerable<SavingsAccount>> GetAllSavingsAccountsAsync();
        Task AddAsync(Account account);
        Task SaveChangesAsync();
    }
}
