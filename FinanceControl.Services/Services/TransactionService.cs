using FinanceControl.Data.Data;
using FinanceControl.Domain.Entities;
using FinanceControl.Domain.Interfaces.Service;
using FinanceControl.Shared.Dtos;

namespace FinanceControl.Services.Services;

public class TransactionService : ITransactionService
{
    private readonly ApplicationDbContext _context;
    public TransactionService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task CreateTransactionAsync(CreateTransactionRequestDto requestDto)
    {
        Transaction transaction = new Transaction(requestDto);
        await _context.AddAsync(requestDto);
    }
}