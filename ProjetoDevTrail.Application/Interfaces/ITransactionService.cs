using ProjetoDevTrail.Application.DTOs.InputModels;
using ProjetoDevTrail.Application.DTOs.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoDevTrail.Application.Interfaces
{
    public interface ITransactionService
    {
        Task<TransactionViewModel> Deposit(DepositInputModel input);
        Task<TransactionViewModel> Withdraw(WithdrawInputModel input);
        Task<TransactionViewModel> Transfer(TransferInputModel input);
        Task<IEnumerable<TransactionViewModel>> GetTransactionHistory(int accountNumber);
    }
}
