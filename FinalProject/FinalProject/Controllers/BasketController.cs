using FinalProject.Application.Abstractions.Services;
using FinalProject.Application.DTOs.Basket;
using FinalProject.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FinalProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketService _basketService;

        public BasketController(IBasketService basketService)
        {
           _basketService = basketService;
        }
        [HttpPost("create-and-add")]
        public async Task<IActionResult> CreateAndAddToBasket([FromForm] CreateBasketDto model)
        {
            
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("Plase login");
            }

            try
            {
                
                
               await _basketService.CreateBasketAsync(userId, model.ProductId, model.Quantity);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
