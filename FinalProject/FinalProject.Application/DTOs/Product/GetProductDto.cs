﻿using FinalProject.Application.DTOs.ProductReview;
using FinalProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Application.DTOs.Product
{
    public record GetProductDto(
        string ImageUrl,
        string Name,
        string Description,
        string SKU,
        decimal Price,
        int CategoryId,
        ICollection<GetReviewDto>? Reviews);
   
}
