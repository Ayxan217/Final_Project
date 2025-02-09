using FinalProject.Application.DTOs.Comment;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Application.Validators.CommentValidators
{
    public class CreateCommentDtoValidator : AbstractValidator<CreateCommentDto>
    {
        public CreateCommentDtoValidator()
        {
            RuleFor(x => x.Content)
                .NotEmpty().WithMessage("Comment content is required")
                .MaximumLength(800).WithMessage("Comment must not exceed 800 characters");



            RuleFor(x => x.DoctorId)
                .NotEmpty().WithMessage("Doctor ID is required")
                .GreaterThan(0).WithMessage("Please provide a valid Doctor ID");
    }
}
}
