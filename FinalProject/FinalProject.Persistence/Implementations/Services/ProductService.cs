using AutoMapper;
using FinalProject.Application.Abstractions.Repositories;
using FinalProject.Application.Abstractions.Services;
using FinalProject.Application.DTOs.Patient;
using FinalProject.Application.DTOs.Product;
using FinalProject.Domain.Entities;
using FinalProject.Persistence.Implementations.Repositories;
using Microsoft.EntityFrameworkCore;
using SendGrid.Helpers.Errors.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Persistence.Implementations.Services
{
    internal class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _categoryRepository;

        public ProductService(IProductRepository productRepository,
            ICategoryRepository categoryService,
            IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _categoryRepository = categoryService;

        }
        public async Task CreateAsync(CreateProductDto productDto)
        {
            if (!await _categoryRepository.AnyAsync(c => c.Id == productDto.CategoryId))
                throw new Exception("Category does not exists");
            Product product = _mapper.Map<Product>(productDto);
            product.CreatedAt = DateTime.Now;
            product.ModifiedAt = DateTime.Now;
            await _productRepository.AddAsync(product);
            await _productRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            Product product = await _productRepository.GetbyIdAsync(id);
            if (product == null)
                throw new NotFoundException($"Product with ID {id} not found.");

            _productRepository.Delete(product);
            await _productRepository.SaveChangesAsync();
        }

        public async Task<ICollection<ProductItemDto>> GetAllAsync(int page, int take)
        {
            ICollection<Product> products = await _productRepository.GetAll(skip: (page - 1) * take, take: take)
                 .ToListAsync();
            
            return _mapper.Map<ICollection<ProductItemDto>>(products);
        }

        public async Task<GetProductDto> GetByIdAsync(int id)
        {
            Product product = await _productRepository.GetbyIdAsync(id);
            if (product is null)
                throw new NotFoundException($"Product with ID {id} not found.");

            return _mapper.Map<GetProductDto>(product);
        }

        public async Task UpdateAsync(int id, UpdateProductDto productDto)
        {
            if (!await _categoryRepository.AnyAsync(c => c.Id == productDto.CategoryId))
                throw new Exception("Category does not exists");
            Product product = await _productRepository.GetbyIdAsync(id);
            if (product is null)
                throw new NotFoundException($"Product with ID {id} not found.");

            _mapper.Map(productDto, product);
            product.ModifiedAt = DateTime.Now;
            _productRepository.Update(product);
            await _productRepository.SaveChangesAsync();
        }


        public async Task<IEnumerable<GetProductDto>> FilterByPriceAsync(decimal minPrice, decimal maxPrice)
        {
            var products = await _productRepository.GetProductsByPriceRange(minPrice, maxPrice);
            return _mapper.Map<IEnumerable<GetProductDto>>(products);
        }

      

        public async Task<IEnumerable<GetProductDto>> GetProductsByPriceDescending(int page,int take)
        {
            var products = await _productRepository.GetProductsByPriceDescending(page, take);
                
            return _mapper.Map<IEnumerable<GetProductDto>>(products);
        }

        public async Task<IEnumerable<GetProductDto>> GetProductsByPriceAscending(int page, int take)
        {
            var products = await _productRepository.GetProductsByPriceAscending(page, take);

            return _mapper.Map<IEnumerable<GetProductDto>>(products);
        }
    }
}
