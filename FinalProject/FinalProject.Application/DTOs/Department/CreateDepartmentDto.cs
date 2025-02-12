using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Application.DTOs.Department
{
    public record CreateDepartmentDto(string Name,string Description,bool status,int ChiefDoctorId);
   
}
