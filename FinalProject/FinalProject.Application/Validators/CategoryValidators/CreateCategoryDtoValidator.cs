using FinalProject.Application.DTOs.Category;
using FinalProject.Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Application.Validators.CategoryValidators
{
    public class CreateCategoryDtoValidator : AbstractValidator<CreateCategoryDto>
    {
        public CreateCategoryDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MinimumLength(3).MaximumLength(100)
                .Matches(@"^[a-zA-ZəƏıİöÖüÜğĞçÇşŞ]+$");

        }
    }
}
