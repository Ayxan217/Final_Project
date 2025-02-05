using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Application.DTOs.Doctor
{
    

    public record DoctorItemDto(int Id,
        string Name,
    string Surname,
    string Description,
    int DepartmentId,
    string Email,
    string PhoneNumber,
    string? Twitter,
    string? Skype,
    string? Facebook,
    string? Ven,
    int CommentId
        );
   
}
