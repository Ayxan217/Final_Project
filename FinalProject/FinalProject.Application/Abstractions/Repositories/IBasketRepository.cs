using FinalProject.Application.Abstractions.Repositories.Generic;
using FinalProject.Application.DTOs.Basket;
using FinalProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Application.Abstractions.Repositories
{
    public interface IBasketRepository : IRepository<Basket>
    {
        Task<Basket> GetBasketByUserIdAsync(string userId);
        
    }
}
