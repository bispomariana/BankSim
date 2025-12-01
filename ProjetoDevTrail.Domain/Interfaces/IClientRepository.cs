using ProjetoDevTrail.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoDevTrail.Domain.Interfaces
{
    public interface IClientRepository
    {
        Task<Client> GetByIdAsync(Guid ID);
        Task<Client> GetByCPFAsync(string CPF);
        Task AddAsync(Client client);
    }
}
