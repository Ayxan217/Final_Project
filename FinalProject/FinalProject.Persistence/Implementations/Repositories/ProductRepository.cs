using FinalProject.Application.Abstractions.Repositories;
using FinalProject.Domain.Entities;
using FinalProject.Persistence.Contexts;
using FinalProject.Persistence.Implementations.Repositories.Generic;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Persistence.Implementations.Repositories
{
    internal class ProductRepository : Repository<Product>,IProductRepository
    {
        private readonly AppDbContext _context;
        public ProductRepository(AppDbContext context) : base(context)
        {
        
             _context = context;

        }

        public async Task<IEnumerable<Product>> GetProductsByPriceRange(decimal minPrice, decimal maxPrice)
        {
            return await _context.Products
                .Where(p=>p.Price>=minPrice && p.Price<=maxPrice)
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsByPriceDescending(int page,int take)
        {
            return await _context.Products
                .OrderByDescending(p => p.Price)
                .Skip((page - 1) * take)
                .Take(take)
                .ToListAsync();
       
               
        }

        public async Task<IEnumerable<Product>> GetProductsByPriceAscending(int page, int take)
        {
            return await _context.Products
            .OrderBy(p => p.Price)
            .Skip((page - 1) * take)
            .Take(take)
            .ToListAsync();
        }
    }
}
