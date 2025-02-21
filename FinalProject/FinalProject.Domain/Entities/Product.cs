using FinalProject.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Domain.Entities
{
    public class Product : BaseNameableEntity
    {
        public string ImageUrl { get; set; }
        public string ImagePublicId { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string SKU { get; set; }
        

        // Relational

        public int CategoryId { get; set; }
        public Category Category { get; set; }
        
        public ICollection<Review>? Reviews { get; set; }
        
    }
}
