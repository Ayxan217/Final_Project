﻿
using FinalProject.Application.DTOs.Payroll;
using FinalProject.Application.DTOs.Product;
using FinalProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Application.Abstractions.Services
{
    public interface IProductService
    {
        Task<ICollection<ProductItemDto>> GetAllAsync(int page, int take);
        Task<GetProductDto> GetByIdAsync(int id);
        Task CreateAsync(CreateProductDto productDto);
        Task UpdateAsync(int id, UpdateProductDto productDto);
        Task DeleteAsync(int id);
        Task<IEnumerable<GetProductDto>> FilterByPriceAsync(decimal minPrice, decimal maxPrice);
        public Task<IEnumerable<GetProductDto>> GetProductsByPriceDescending(int page,int take);
        public Task<IEnumerable<GetProductDto>> GetProductsByPriceAscending(int page, int take);
    }
}
