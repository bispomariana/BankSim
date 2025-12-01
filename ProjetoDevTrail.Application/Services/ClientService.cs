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

namespace ProjetoDevTrail.Application.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;

        public ClientService(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public async Task<ClientViewModel> AddAsync(ClientInputModel input)
        {
            var cleanCPF = input.CPF.Replace(".", "").Replace("-", "").Trim();
            var client = await _clientRepository.GetByCPFAsync(cleanCPF);

            if (client != null)
                throw new ArgumentException("Client already registered.");

            var newClient = input.ToEntity();

            await _clientRepository.AddAsync(newClient);

            return newClient.ToViewModel();
        }

        public async Task<ClientViewModel> GetByIdAsync(Guid clientId)
        {
            var client = await _clientRepository.GetByIdAsync(clientId);
            if (client == null)
                throw new ArgumentException("Client not found.");
            return client.ToViewModel();
        }

        public async Task<ClientViewModel> GetByCPFAsync(string cpf)
        {
            var cleanCPF = cpf.Replace(".", "").Replace("-", "").Trim();
            var client = await _clientRepository.GetByCPFAsync(cleanCPF);
            if (client == null)
                throw new ArgumentException("Client not found.");
            return client.ToViewModel();
        }
    }
}
