using FinalProject.Application.DTOs.Category;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Application.Validators.CategoryValidators
{
    internal class UpdateCategoryDtoValidator : AbstractValidator<UpdateCategoryDto>
    {
        public UpdateCategoryDtoValidator()
        {
            RuleFor(x=> x.Name).NotEmpty().MaximumLength(100).MinimumLength(3)
                .Matches(@"^[a-zA-ZəƏıİöÖüÜğĞçÇşŞ]+$"); 
        }
    }
}
