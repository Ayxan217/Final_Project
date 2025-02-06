using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Application.DTOs.Patient
{
    public record UpdatePatientDto(
        int Id,
      string Name,
     string Surname,
     string Adress,
     string Phone,
     string Email);
    
}
