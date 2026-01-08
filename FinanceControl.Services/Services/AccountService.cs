using FinanceControl.Data.Data;
using FinanceControl.Domain.Entities;
using FinanceControl.Domain.Interfaces.Service;
using FinanceControl.Shared.Dtos.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceControl.Services.Services
{
    public class AccountService : IAccountService
    {
        private readonly ApplicationDbContext _context;

        public AccountService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateAccountAsync(CreateAccountRequestDto requestDto, int userId)
        {
            var account = new Account()
            {
                UserId = userId,
                Name = requestDto.Name,
                CurrentBalance = requestDto.CurrentBalance,
                GoalAmount = requestDto.GoalAmount,
                IsDefaultAccount = true
            };

            await _context.Accounts.AddAsync(account);
            await _context.SaveChangesAsync();
        }
    }
}
