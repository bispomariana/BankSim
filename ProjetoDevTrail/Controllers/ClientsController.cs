using Microsoft.AspNetCore.Mvc;
using ProjetoDevTrail.Application.Interfaces;
using ProjetoDevTrail.Application.DTOs.InputModels;

namespace ProjetoDevTrail.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientsController : ControllerBase
    {
        private readonly IClientService _clientService;

        public ClientsController(IClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ClientInputModel input)
        {
            var result = await _clientService.AddAsync(input);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _clientService.GetByIdAsync(id);
            if (result == null) return NotFound();

            return Ok(result);
        }

        [HttpGet("cpf/{cpf}")]
        public async Task<IActionResult> GetByCpf(string cpf)
        {
            var result = await _clientService.GetByCPFAsync(cpf);
            if (result == null) return NotFound();

            return Ok(result);
        }
    }
}