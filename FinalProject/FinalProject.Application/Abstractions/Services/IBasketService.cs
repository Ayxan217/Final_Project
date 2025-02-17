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
        Task<Basket> GetBasket(string userId);
        Task CreateBasketAsync(string userId, int productId, int quantity);
        Task AddBasketAsyncDto(CreateBasketDto basketDto);
        Task UpdateBasketAsync(UpdateBasketDto basketDto);
    }
}
