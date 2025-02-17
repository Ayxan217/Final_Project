using FinalProject.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Domain.Entities
{
    public class Basket : BaseEntity
    {
        public string UserId { get; set; }
        public ICollection<BasketItem>? Items { get; set; }
    }
}
