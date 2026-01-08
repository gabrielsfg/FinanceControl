using FinanceControl.Shared.Dtos.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceControl.Domain.Interfaces.Service
{
    public interface IAccountService
    {
        Task CreateAccountAsync(CreateAccountRequestDto requestDto, int userId);
    }
}
