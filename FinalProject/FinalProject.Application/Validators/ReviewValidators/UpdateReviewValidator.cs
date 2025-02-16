using FinalProject.Application.DTOs.ProductReview;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Application.Validators.ReviewValidators
{
    public class UpdateReviewValidator : AbstractValidator<UpdateReviewDto>
    {
        public UpdateReviewValidator()
        {
            RuleFor(x => x.Content).MaximumLength(300);
            RuleFor(x => x.Rating).GreaterThan(0).LessThan(6);
        }
    }
}
