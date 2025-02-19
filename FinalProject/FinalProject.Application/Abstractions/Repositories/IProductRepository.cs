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
         Task<IEnumerable<Product>> GetProductsByPriceRange(decimal minPrice, decimal maxPrice);
         Task<IEnumerable<Product>> GetProductsByPriceDescending(int page,int take);
          Task<IEnumerable<Product>> GetProductsByPriceAscending(int page, int take);

         Task<ICollection<Product>> GetProductsWithReviews(int page, int take);
         Task<Product> GetProductWithReviewsByIdAsync(int id);
    }

}
