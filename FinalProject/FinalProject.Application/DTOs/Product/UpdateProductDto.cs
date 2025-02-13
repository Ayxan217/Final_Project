using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Application.DTOs.Product
{
    public record UpdateProductDto(string Name,string SKU,string Description, decimal Price,int CategoryId);
   
}
