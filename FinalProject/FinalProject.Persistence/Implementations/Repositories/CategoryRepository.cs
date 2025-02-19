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
    internal class CategoryRepository : Repository<Category>,ICategoryRepository
    {
        private readonly AppDbContext _context;
        public CategoryRepository(AppDbContext context) : base(context) {
        
            _context = context;
        
        
        }

        public async Task<IEnumerable<Category>> GetCategoryWithProducts(int page, int take)
        {
            return await _context.Categories.Include(c=> c.Products)
                .Skip((page - 1) * take)
                .Take(take)
                .ToListAsync();
        }
    }
}
