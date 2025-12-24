using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinanceControl.Shared.Dtos;

namespace FinanceControl.Domain.Interfaces.Service
{
    public interface ITransactionService
    {
        Task CreateTransactionAsync(CreateTransactionRequestDto requestDto);
    }
}
