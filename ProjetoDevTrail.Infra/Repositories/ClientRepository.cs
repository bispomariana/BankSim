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
    public class ClientRepository : IClientRepository
    {
        private readonly BankContext _context;
        public ClientRepository(BankContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Api.Models.Client client)
        {
            await _context.Clients.AddAsync(client);
            await _context.SaveChangesAsync();
        }
        public async Task<Client> GetByIdAsync(Guid clientId)
        {
            var client = await _context.Clients.Include(c => c.Accounts).FirstOrDefaultAsync(c => c.Client_Id == clientId);
            return await Task.FromResult(client); //PODE SER NULO
        }
        public async Task<Client> GetByCPFAsync(string cpf)
        {
            var client = await _context.Clients.Include(c => c.Accounts).FirstOrDefaultAsync(c => c.CPF == cpf);
            return await Task.FromResult(client); //PODE SER NULO
        }
    }
}
