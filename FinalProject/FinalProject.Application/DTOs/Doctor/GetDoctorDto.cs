using FinalProject.Application.DTOs.Comment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Entity = FinalProject.Domain.Entities;

namespace FinalProject.Application.DTOs.Doctor
{
    public record  GetDoctorDto(
        string? ImageUrl,
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
    string? Ven,
    ICollection<GetCommentDto> Comments
        );
   
}
