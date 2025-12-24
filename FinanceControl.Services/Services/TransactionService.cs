using FinanceControl.Data.Data;
using FinanceControl.Domain.Entities;
using FinanceControl.Domain.Enums;
using FinanceControl.Domain.Interfaces.Service;
using FinanceControl.Shared.Dtos;
using FinanceControl.Shared.Models;
using Microsoft.EntityFrameworkCore;

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
        Transaction transaction = new Transaction(
            requestDto.Value,
            Enum.Parse<EnumTransactionType>(requestDto.Type),
            requestDto.Category,
            requestDto.Date,
            Enum.Parse<EnumPaymentType>(requestDto.PaymentType),
            Enum.Parse<EnumPaymentRecurrence>(requestDto.Reccurence),
            requestDto.Description
            );

        await _context.Transactions.AddAsync(transaction);
        await _context.SaveChangesAsync();
    }

    public async Task<TransactionResponseDto?> GetTransactionByIdAsync(int transactionId)
    {
        var transaction = await _context.Transactions.FindAsync(transactionId);

        if (transaction == null)
            return null;

        TransactionResponseDto response = new TransactionResponseDto()
        {
            Value = transaction.Value,
            Category = transaction.Category,
            Type = transaction.Type.ToString(),
            TransactionDate = transaction.TransactionDate,
            PaymentType = transaction.PaymentType.ToString(),
            Reccurence = transaction.Reccurence.ToString()
        };

        return response;
    }

    public async Task<PagedResponse<TransactionResponseDto>> GetAllTransactionsPagedAsync(int page, int pageSize)
    {
        int offset = (page - 1) * pageSize;
        var transactionsTask = _context.Transactions.OrderByDescending(t => t.CreatedAt).Skip(offset).Take(pageSize).ToListAsync();

        var totalItemsTask = _context.Transactions.CountAsync();

        await Task.WhenAll(transactionsTask, totalItemsTask);

        var transactions = await transactionsTask;
        var totalItems = await totalItemsTask;
        int totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

        var transactionsDto = transactions.Select(t => new TransactionResponseDto
        {
            Value = t.Value,
            Category = t.Category,
            Type = t.Type.ToString(),
            TransactionDate = t.TransactionDate,
            PaymentType = t.PaymentType.ToString(),
            Reccurence = t.Reccurence.ToString()
        });

        PagedResponse<TransactionResponseDto> response = new PagedResponse<TransactionResponseDto>()
        {
            TotalItems = totalItems,
            CurrentPage = page,
            PageSize = pageSize,
            RowCount = transactions.Count(),
            TotalPages = totalPages,
            Items = transactionsDto
        };

        return response;
    }
}