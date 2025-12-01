using ProjetoDevTrail.Api.Models;
using ProjetoDevTrail.Application.DTOs.InputModels;
using ProjetoDevTrail.Application.DTOs.ViewModels;
using ProjetoDevTrail.Application.Interfaces;
using ProjetoDevTrail.Application.DTOs.Mappers;
using ProjetoDevTrail.Domain.Interfaces;
using ProjetoDevTrail.Infra.Data; 

namespace ProjetoDevTrail.Application.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly BankContext _context;

        public TransactionService(BankContext context,
                                  ITransactionRepository transactionRepository,
                                  IAccountRepository accountRepository)
        {
            _transactionRepository = transactionRepository;
            _accountRepository = accountRepository;
            _context = context;
        }

        private void ValidateFunds(Account account, decimal amount)
        {
            decimal availableBalance = account.Balance;

            if (account is CurrentAccount currentAccount)
            {
                availableBalance += currentAccount.OverdraftLimit;
            }

            if (availableBalance < amount)
            {
                throw new ApplicationException("Insufficient funds (considering the overdraft limit).");
            }
        }

        public async Task<TransactionViewModel> Deposit(DepositInputModel input)
        {
            var account = await _accountRepository.GetByNumberAsync(input.AccountNumber);
            if (account == null) throw new KeyNotFoundException("Account was not found.");

            if (input.Amount <= 0) throw new ArgumentException("Invalid amount.");

            account.Balance += input.Amount;

            var transaction = new Transaction
            {
                Transaction_Id = Guid.NewGuid(),
                Amount = input.Amount,
                DateTime = DateTime.Now,
                Type = TransactionType.Deposit,
                Origin_Account_Id = account.Account_Id,
                OriginAccount = account,
                Target_Account_Id = null,
                TargetAccount = null
            };

            await _transactionRepository.AddAsync(transaction);

            await _context.SaveChangesAsync();

            return transaction.ToViewModel();
        }

        public async Task<TransactionViewModel> Withdraw(WithdrawInputModel input)
        {
            var account = await _accountRepository.GetByNumberAsync(input.AccountNumber);
            if (account == null) throw new KeyNotFoundException("Account was not found.");

            ValidateFunds(account, input.Amount);

            account.Balance -= input.Amount;

            var transaction = new Transaction
            {
                Transaction_Id = Guid.NewGuid(),
                Amount = input.Amount,
                DateTime = DateTime.Now,
                Type = TransactionType.Withdraw,
                Origin_Account_Id = account.Account_Id,
                OriginAccount = account,
                Target_Account_Id = null,
                TargetAccount = null
            };

            await _transactionRepository.AddAsync(transaction);

            await _context.SaveChangesAsync();

            return transaction.ToViewModel();
        }

        public async Task<TransactionViewModel> Transfer(TransferInputModel input)
        {
            var origin = await _accountRepository.GetByNumberAsync(input.OriginAccountNumber);
            var target = await _accountRepository.GetByNumberAsync(input.TargetAccountNumber);

            if (origin == null) throw new KeyNotFoundException("Invalid origin account.");
            if (target == null) throw new KeyNotFoundException("Invalid target account.");

            decimal fee = input.Amount * 0.005m;
            decimal totalDebit = input.Amount + fee;

            ValidateFunds(origin, totalDebit);

            origin.Balance -= totalDebit;

            target.Balance += input.Amount;

            var transferTransaction = new Transaction
            {
                Transaction_Id = Guid.NewGuid(),
                Amount = input.Amount,
                DateTime = DateTime.Now,
                Type = TransactionType.Transfer,
                Origin_Account_Id = origin.Account_Id,
                OriginAccount = origin,
                Target_Account_Id = target.Account_Id,
                TargetAccount = target
            };

            await _transactionRepository.AddAsync(transferTransaction);

            var feeTransaction = new Transaction
            {
                Transaction_Id = Guid.NewGuid(),
                Amount = fee,
                DateTime = DateTime.Now,
                Type = TransactionType.Operating_Fee, 
                Origin_Account_Id = origin.Account_Id,
                OriginAccount = origin,
                Target_Account_Id = null,
                TargetAccount = null
            };
            await _transactionRepository.AddAsync(feeTransaction);

            await _context.SaveChangesAsync();

            return transferTransaction.ToViewModel();
        }

        public async Task<IEnumerable<TransactionViewModel>> GetTransactionHistory(int accountNumber)
        {
            if (await _accountRepository.GetByNumberAsync(accountNumber) == null)
                throw new KeyNotFoundException("Account was not found.");

            var transactions = await _transactionRepository.GetByAccountNumberAsync(accountNumber);
            return transactions.Select(t => t.ToViewModel());
        }
    }
}