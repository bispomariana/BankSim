using Microsoft.AspNetCore.Mvc;
using ProjetoDevTrail.Application.Interfaces;
using ProjetoDevTrail.Application.DTOs.InputModels;

namespace ProjetoDevTrail.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public TransactionsController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpPost("deposit")]
        public async Task<IActionResult> Deposit([FromBody] DepositInputModel input)
        {
            var result = await _transactionService.Deposit(input);
            return Ok(result);
        }

        [HttpPost("withdraw")]
        public async Task<IActionResult> Withdraw([FromBody] WithdrawInputModel input)
        {
            var result = await _transactionService.Withdraw(input);
            return Ok(result);
        }

        [HttpPost("transfer")]
        public async Task<IActionResult> Transfer([FromBody] TransferInputModel input)
        {
            var result = await _transactionService.Transfer(input);
            return Ok(result);
        }

        [HttpGet("history/{accountNumber}")]
        public async Task<IActionResult> GetHistory(int accountNumber)
        {
            var result = await _transactionService.GetTransactionHistory(accountNumber);
            return Ok(result);
        }
    }
}