using Entity=FinalProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinalProject.Application.DTOs.Product;


namespace FinalProject.Application.DTOs.Category
{
    
    public record GetCategoryDto(string name,ICollection<GetProductDto> Products);
    
}
