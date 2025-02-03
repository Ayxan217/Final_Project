using FinalProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Application.DTOs.Department
{
    public record GetDepartmentDto(int Id,string Name,string Description);
  
}
