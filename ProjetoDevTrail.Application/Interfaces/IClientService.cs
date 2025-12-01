using ProjetoDevTrail.Api.Models;
using ProjetoDevTrail.Application.DTOs.InputModels;
using ProjetoDevTrail.Application.DTOs.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoDevTrail.Application.Interfaces
{
    public interface IClientService
    {
        Task<ClientViewModel> GetByIdAsync(Guid ID);
        Task<ClientViewModel> GetByCPFAsync(string CPF);
        Task<ClientViewModel> AddAsync(ClientInputModel input);
    }
}
