using FinanceControl.Domain.Entities;
using FinanceControl.Domain.Interfaces.Service;
using FinanceControl.Shared.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace FinanceControl.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransactionController : Controller
    {
        private readonly ILogger<TransactionController> _logger;
        private readonly ITransactionService _transactionService;

        public TransactionController(ILogger<TransactionController> logger, ITransactionService transactionService)
        {
            _logger = logger;
            _transactionService = transactionService;
        }
        [HttpPost]
        public async Task<IActionResult> CreateTransactionAsync([FromBody] CreateTransactionRequestDto requestDto)
        {
            await _transactionService.CreateTransactionAsync(requestDto);
            return Ok();
        }

        [HttpGet("by-id/{transactionId:int}")]
        public async Task<IActionResult> GetTransactionByIdAsync([FromRoute] int transactionId)
        {
            TransactionResponseDto? transaction = await _transactionService.GetTransactionByIdAsync(transactionId);
            if (transaction == null)
                return NotFound(new {message = "Transaction not found!"});

            return Ok(transaction);
        }

        [HttpGet("paged/{page:int}&{pageSize:int}")]
        public async Task<IActionResult> GetAllTransactionsPagedAsync([FromRoute] int page, [FromRoute] int pageSize)
        {
            var response = await _transactionService.GetAllTransactionsPagedAsync(page, pageSize);
            return Ok(response);
        }
    }
}
