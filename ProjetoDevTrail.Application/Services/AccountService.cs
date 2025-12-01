using Microsoft.Identity.Client;
using ProjetoDevTrail.Api.Models;
using ProjetoDevTrail.Application.DTOs.InputModels;
using ProjetoDevTrail.Application.DTOs.ViewModels;
using ProjetoDevTrail.Application.Interfaces;
using ProjetoDevTrail.Application.DTOs.Mappers;
using ProjetoDevTrail.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjetoDevTrail.Infra.Data;

namespace ProjetoDevTrail.Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IClientRepository _clientRepository;
        private readonly ITransactionRepository _transactionRepository;
        private readonly BankContext _context;
        public AccountService(BankContext context, IAccountRepository accountRepository, IClientRepository clientRepository, ITransactionRepository transactionRepository)
        {
            _accountRepository = accountRepository;
            _clientRepository = clientRepository;
            _transactionRepository = transactionRepository;
            _context = context;
        }

        public async Task<AccountViewModel> CreateAsync(AccountInputModel input)
        {
            var cleanCPF = input.CPF_Client.Replace(".", "").Replace("-", "").Trim();
            var client = await _clientRepository.GetByCPFAsync(cleanCPF);

            if (client == null)
                throw new ArgumentException("Client not found.");
            
            var newAccount = input.ToEntity(client.Client_Id);

            await _accountRepository.AddAsync(newAccount);

            return newAccount.ToViewModel();
        }
        public async Task<AccountViewModel> GetByIdAsync(Guid accountId)
        {
            var account = await _accountRepository.GetByIdAsync(accountId);
            if (account == null)
                throw new ArgumentException("Account not found.");
            return account.ToViewModel();
        }

        public async Task<IEnumerable<AccountViewModel>> GetByClientCPF(string clientCPF)
        {
            var cleanCPF = clientCPF.Replace(".", "").Replace("-", "").Trim();
            var client = await _clientRepository.GetByCPFAsync(cleanCPF);

            if (client == null)
                throw new ArgumentException("Client not found.");

            var accounts = await _accountRepository.GetByClientIdAsync(client.Client_Id);
            return accounts.Select(c => c.ToViewModel());
        }

        public async Task ApplyMonthlyIncome()
        {
            var allSavingsAccounts = await _accountRepository.GetAllSavingsAccountsAsync();
            foreach (var account in allSavingsAccounts)
            {
                if (account.Balance > 0)
                {
                    decimal interest = account.Balance * 0.005m;
                    account.Balance += interest;
                    await _transactionRepository.AddAsync(new Transaction {Transaction_Id = Guid.NewGuid(),
                        Type = TransactionType.Yield_Rate,
                        Amount = interest,
                        DateTime = DateTime.UtcNow,
                        Origin_Account_Id = account.Account_Id,
                        OriginAccount = account,
                        Target_Account_Id = null,
                        TargetAccount = null});
                }
            }
            await _context.SaveChangesAsync();
        }

    }
}
