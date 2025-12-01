using ProjetoDevTrail.Application.DTOs.InputModels;
using ProjetoDevTrail.Application.DTOs.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoDevTrail.Application.Interfaces
{
    public interface IAccountService
    {
        Task<AccountViewModel> GetByIdAsync(Guid ID);
        Task<AccountViewModel> CreateAsync(AccountInputModel input);
        Task<IEnumerable<AccountViewModel>> GetByClientCPF(string clientCPF);
        Task ApplyMonthlyIncome();
    }
}
