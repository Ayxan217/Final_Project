using FinalProject.Application.DTOs.Appointment;
using FluentValidation;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Application.Validators.AppointmentValidators
{
    public class UpdateAppointmentDtoValidator : AbstractValidator<UpdateAppointmentDto>
    {
        public UpdateAppointmentDtoValidator()
        {
         

            RuleFor(x => x.PatientId)
            .NotEmpty()
                .GreaterThan(0);


            RuleFor(x => x.DoctorId)
            .NotEmpty()
            .GreaterThan(0);


            RuleFor(x => x.AppointmentDate)
            .NotEmpty()
            .Must(BeAValidFutureDate)
            .WithMessage("Appointment Cant be in the past");
            
                

        }
        private bool BeAValidFutureDate(DateTime date)
        {
            return date > DateTime.Now;
        }

    }
}
