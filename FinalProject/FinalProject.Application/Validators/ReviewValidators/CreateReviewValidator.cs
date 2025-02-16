using FinalProject.Application.DTOs.ProductReview;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Application.Validators.ReviewValidators
{
    public class CreateReviewValidator : AbstractValidator<CreateReviewDto>
    {
        public CreateReviewValidator()
        
        {
            RuleFor(x => x.Content).MaximumLength(300);
            RuleFor(x => x.ProductId).GreaterThan(0);
            RuleFor(x=>x.Rating).GreaterThan(0).LessThan(6);


        }
    }
}
