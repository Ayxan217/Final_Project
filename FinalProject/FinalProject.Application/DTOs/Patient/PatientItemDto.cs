using FinalProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Application.DTOs.Patient
{
    public record PatientItemDto(
        string IdentityCode,
        int Id,
      string Name,
     string Surname,
     DateOnly DateOfBirth,
     string Adress,
     string Phone,
     string Email
     );
    
}
