using FinalProject.Application.DTOs.Basket;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Application.Validators.BasketValidators
{
    public class CreateBasketDtoValidator : AbstractValidator<CreateBasketDto>
    {
        public CreateBasketDtoValidator()
        {
            RuleFor(x => x.Quantity).LessThanOrEqualTo(10).GreaterThan(0).NotEmpty();
        }
    }
}
