using FinalProject.Application.DTOs.Basket;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Application.Validators.BasketValidators
{
    public class RemoveItemDtoValidator : AbstractValidator<RemoveItemDto>
    {
        public RemoveItemDtoValidator()
        {
            RuleFor(x=>x.ProductId).NotEmpty().GreaterThan(0);
        }
    }
}
