using FinalProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Application.DTOs.Basket
{
    
    public record UpdateBasketDto(ICollection<BasketItem> Items);
    
}
