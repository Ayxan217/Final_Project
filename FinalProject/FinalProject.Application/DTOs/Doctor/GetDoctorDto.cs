using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Application.DTOs.Doctor
{
    public record  GetDoctorDto(
        string Name,
    string Surname,
    string Email,
    string PhoneNumber,
    DateOnly JoinDate,
    string Description,
    int DepartmentId,
    string? Twitter,
    string? Skype,
    string? Facebook,
    string? Ven
        );
   
}
