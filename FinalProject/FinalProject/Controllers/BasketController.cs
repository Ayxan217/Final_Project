using AutoMapper;
using FinalProject.Application.Abstractions.Services;
using FinalProject.Application.DTOs.Basket;
using FinalProject.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FinalProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BasketController : ControllerBase
    {
        private readonly IBasketService _basketService;
       

        public BasketController(IBasketService basketService)
        {
           _basketService = basketService;
          
        }
        [HttpPost("add-basket")]
        public async Task<IActionResult> AddBasket([FromForm] CreateBasketDto basketDto)
        {
            
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
                return Unauthorized("Plase login");


            try
            {
               await _basketService.AddBasketAsync(userId, basketDto.ProductId, basketDto.Quantity);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get-basket")]

        public async Task<IActionResult> Get()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized("Plase login");
            var basket = await _basketService.GetBasketAsync(userId);
           
            return Ok(basket);

        }

        [HttpPost("remove-item")]

        public async Task<IActionResult> Delete([FromForm] RemoveItemDto itemDto)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized("Plase login");

            await _basketService.RemoveItemAsync(userId,itemDto.ProductId);
            return NoContent();
        }

        [HttpPost("decrase-item-count")]

        public async Task<IActionResult> Decrase([FromForm] RemoveItemDto itemDto)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized("Plase login");

            await _basketService.DecreaseItemQuantityAsync(userId, itemDto.ProductId);
            return NoContent();
        }


    }
}
