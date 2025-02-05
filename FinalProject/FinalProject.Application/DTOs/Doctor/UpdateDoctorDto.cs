using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Application.DTOs.Doctor
{
    public record  UpdateDoctorDto(string Name,
    string Surname,
    string Email,
    string PhoneNumber,
    string Description,
    int DepartmentId,
    string? Twitter,
    string? Skype,
    string? Facebook,
    string? Ven);
    
}
