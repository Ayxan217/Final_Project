using FinalProject.Application.Abstractions.Repositories;
using FinalProject.Application.Abstractions.Services;
using FinalProject.Application.DTOs.Basket;
using FinalProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Persistence.Implementations.Services
{
    internal class BasketService : IBasketService
    {
        private readonly IBasketRepository _basketRepository;

        public BasketService(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }

        public Task AddBasketAsyncDto(CreateBasketDto basketDto)
        {
            throw new NotImplementedException();
        }

        public async Task CreateBasketAsync(string userId, int productId, int quantity)
        {
           
            var existingBasket = await _basketRepository.GetBasketByUserIdAsync(userId);
            if (existingBasket == null)
            {
               
                existingBasket = new Basket
                {
                    UserId = userId,
                    Items = new List<BasketItem>()
                };


                await _basketRepository.SaveChangesAsync();
            }

            
            var existingItem = existingBasket.Items.FirstOrDefault(item => item.ProductId == productId);
            if (existingItem != null)
            {
                
                existingItem.Quantity += quantity;
            }
            else
            {

                existingBasket.Items.Add(new BasketItem
                {
                    ProductId = productId,
                    Quantity = quantity
                });
            }


             _basketRepository.Update(existingBasket);
           await _basketRepository.SaveChangesAsync();

        }


        public Task<Basket> GetBasket(string userId)
        {
            return _basketRepository.GetBasketByUserIdAsync(userId);
        }

        public Task UpdateBasketAsync(UpdateBasketDto basketDto)
        {
            throw new NotImplementedException();
        }
    }
}
