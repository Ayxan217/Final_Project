using FinalProject.Application.Abstractions.Repositories;
using FinalProject.Application.DTOs.Basket;
using FinalProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Application.Abstractions.Services
{
    public interface IBasketService
    {
        Task<BasketDto> GetBasketAsync(string userId);
        Task AddBasketAsync(string userId, int productId, int quantity);
        Task RemoveItemAsync(string userId, int productId);
        Task DecreaseItemQuantityAsync(string userId, int productId);

    }
}
