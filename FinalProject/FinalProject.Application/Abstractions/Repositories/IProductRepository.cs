using FinalProject.Application.Abstractions.Repositories.Generic;
using FinalProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Application.Abstractions.Repositories
{
    public interface  IProductRepository : IRepository<Product>
    {
        public Task<IEnumerable<Product>> GetProductsByPriceRange(decimal minPrice, decimal maxPrice);
        public Task<IEnumerable<Product>> GetProductsByPriceDescending(int page,int take);
        public Task<IEnumerable<Product>> GetProductsByPriceAscending(int page, int take);

        public Task<ICollection<Product>> GetProductsWithReviews(int page, int take);
        public Task<Product> GetProductWithReviewsByIdAsync(int id);
    }

}
