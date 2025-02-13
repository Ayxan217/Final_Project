using FinalProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Application.DTOs.Product
{
    public record ProductItemDto(int Id,string Name, string Description, string SKU, decimal Price, bool InStock,int CategoryId,ICollection<Review>? Reviews);


}
