using FinanceControl.Shared.Dtos;
using FinanceControl.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceControl.Domain.Interfaces.Service
{
    public interface ICategoryService
    {
        Task<Result<CreateCategoryResponseDto>> CreateCategoryAsync(CreateCategoryRequestDto requestDto, int userId);
    }
}
