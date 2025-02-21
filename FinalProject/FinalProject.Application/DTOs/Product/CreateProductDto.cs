using FinalProject.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Application.DTOs.Product
{
    public record CreateProductDto(
        IFormFile Photo,
        string Name,
        string SKU,
        decimal Price,
        string Description,
        int CategoryId);
   
}
