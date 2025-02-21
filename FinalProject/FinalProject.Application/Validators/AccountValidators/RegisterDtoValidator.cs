﻿using FinalProject.Application.DTOs.Account;
using FluentValidation;

namespace FinalProject.Application.Validators.AccountValidators
{
    public class RegisterDtoValidator : AbstractValidator<RegisterDto>
    {
        public RegisterDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MinimumLength(2).MaximumLength(100)
                .Matches("^[a-zA-ZçğıöşüəƏÇĞİÖŞÜ]+$");
            RuleFor(x => x.Surname).NotEmpty().MinimumLength(2).MaximumLength(100)
                .Matches("^[a-zA-ZçğıöşüəƏÇĞİÖŞÜ]+$");
            RuleFor(x => x.UserName).NotEmpty().MinimumLength(4).MaximumLength(100);
            RuleFor(x => x.Email).NotEmpty().MinimumLength(6).MaximumLength(256)
                .Matches("(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*|\"(?:[\\x01-\\x08\\x0b\\x0c\\x0e-\\x1f\\x21\\x23-\\x5b\\x5d-\\x7f]|\\\\[\\x01-\\x09\\x0b\\x0c\\x0e-\\x7f])*\")@(?:(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?|\\[(?:(?:(2(5[0-5]|[0-4][0-9])|1[0-9][0-9]|[1-9]?[0-9]))\\.){3}(?:(2(5[0-5]|[0-4][0-9])|1[0-9][0-9]|[1-9]?[0-9])|[a-z0-9-]*[a-z0-9]:(?:[\\x01-\\x08\\x0b\\x0c\\x0e-\\x1f\\x21-\\x5a\\x53-\\x7f]|\\\\[\\x01-\\x09\\x0b\\x0c\\x0e-\\x7f])+)\\])");
            RuleFor(x => x.Password).NotEmpty().MinimumLength(8).MaximumLength(64);


        }
    }
}
