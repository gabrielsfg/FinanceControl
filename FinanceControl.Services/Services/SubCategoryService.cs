using FinanceControl.Data.Data;
using FinanceControl.Domain.Entities;
using FinanceControl.Domain.Interfaces.Service;
using FinanceControl.Shared.Dtos.Request;
using FinanceControl.Shared.Dtos.Respose;
using FinanceControl.Shared.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceControl.Services.Services
{
    public class SubCategoryService : ISubCategoryService
    {
        private readonly ApplicationDbContext _context;
        public SubCategoryService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result<IEnumerable<GetSubCategoryResponseDto>>> CreateSubCategoryAsync(CreateSubCategoryRequestDto requestDto, int userId)
        {
            var subCategory = new SubCategory()
            {
                Name = requestDto.Name,
                CategoryId = requestDto.CategoryId,
                UserId = userId
            };

            await _context.AddAsync(subCategory);
            await _context.SaveChangesAsync();

            var result = await GetAllSubCategoryAsync(userId);
            return Result<IEnumerable<GetSubCategoryResponseDto>>.Success(result);
        }

        public async Task<IEnumerable<GetSubCategoryResponseDto>> GetAllSubCategoryAsync(int userId)
        {
            var subCategories = await _context.SubCategories.Where(s => s.UserId.Equals(userId)).Select(s => new GetSubCategoryResponseDto
            {
                Name = s.Name,
                CategoryId = s.CategoryId,
                Id = s.Id
            }).ToListAsync();

            return subCategories;
        }

        public async Task<GetSubCategoryResponseDto?> GetSubCategoryByIdAsync(int id, int userId)
        {
            var subCategory = await _context.SubCategories.FirstOrDefaultAsync(s => s.UserId.Equals(userId) && s.Id.Equals(id));

            if (subCategory == null)
                return null;

            var result = new GetSubCategoryResponseDto()
            {
                Name = subCategory.Name,
                CategoryId = subCategory.CategoryId,
                Id = subCategory.Id
            };

            return result;
        }

        public async Task<Result<IEnumerable<GetSubCategoryResponseDto>>> UpdateSubCategoryAsync(UpdateSubCategoryRequestDto requestDto, int userId)
        {
            var subCategory = await _context.SubCategories.FirstOrDefaultAsync(s => s.UserId.Equals(userId) && s.Id.Equals(requestDto.Id));

            if (subCategory == null)
                return null;

            subCategory.Name = requestDto.Name;
            subCategory.CategoryId = requestDto.CategoryId;

            await _context.SaveChangesAsync();
            var result = await GetAllSubCategoryAsync(userId);

            return Result<IEnumerable<GetSubCategoryResponseDto>>.Success(result);
        }

        public async Task<Result<IEnumerable<GetSubCategoryResponseDto>>> DeleteSubCategoryAsync(int id, int userId)
        {
            var subCategory = _context.SubCategories.FirstOrDefault(s => s.UserId.Equals(userId) && s.Id.Equals(id));

            if (subCategory == null)
                return null;

            _context.Remove(subCategory);
            await _context.SaveChangesAsync();

            var result = await GetAllSubCategoryAsync(userId);

            return Result<IEnumerable<GetSubCategoryResponseDto>>.Success(result);
        }
    }
}
