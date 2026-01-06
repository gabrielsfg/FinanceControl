using FinanceControl.Data.Data;
using FinanceControl.Domain.Entities;
using FinanceControl.Domain.Interfaces.Service;
using FinanceControl.Shared.Dtos;
using FinanceControl.Shared.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceControl.Services.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext _context;
        public CategoryService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result<CreateCategoryResponseDto>> CreateCategoryAsync(CreateCategoryRequestDto requestDto, int userId)
        {
            Category category = new Category
            {
                Name = requestDto.Name,
                UserId = userId,
            };

            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();

            return Result<CreateCategoryResponseDto>.Success(new CreateCategoryResponseDto
            {
                Id = category.Id,
                Name = category.Name
            });
        }
    }
}
