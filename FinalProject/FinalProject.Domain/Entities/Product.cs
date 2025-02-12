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
        public decimal Price { get; set; }
        public bool InStock { get; set; }
        public string Description { get; set; }
        public string SKU { get; set; }
        

        // Relational

        public int CategoryId { get; set; }
        public Category Category { get; set; }
        
    }
}
