using FinalProject.Application.DTOs.Product;
using FinalProject.Application.DTOs.ProductReview;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Application.Abstractions.Services
{
    public interface IReviewService 
    {
        Task<IEnumerable<ReviewItemDto>> GetAllAsync(int page, int take);
        Task<GetReviewDto> GetByIdAsync(int id);
        Task CreateAsync(string userId, CreateReviewDto reviewDto);
        Task UpdateAsync(int id, UpdateReviewDto reviewDto);
        Task DeleteAsync(int id);
    }
}
