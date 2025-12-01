using Microsoft.AspNetCore.Mvc;
using ProjetoDevTrail.Application.Interfaces;
using ProjetoDevTrail.Application.DTOs.InputModels;

namespace ProjetoDevTrail.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountsController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AccountInputModel input)
        {
            var result = await _accountService.CreateAsync(input);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _accountService.GetByIdAsync(id);
            if (result == null) return NotFound();

            return Ok(result);
        }

        [HttpGet("client/{cpf}")]
        public async Task<IActionResult> GetByClientCpf(string cpf)
        {
            var result = await _accountService.GetByClientCPF(cpf);
            return Ok(result);
        }

        [HttpPost("apply-interest")]
        public async Task<IActionResult> ApplyInterest()
        {
            await _accountService.ApplyMonthlyIncome();
            return Ok("Income successfully applied to savings accounts.");
        }
    }
}