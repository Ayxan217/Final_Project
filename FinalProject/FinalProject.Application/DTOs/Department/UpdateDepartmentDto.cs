﻿using FinalProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Application.DTOs.Department
{
    public record UpdateDepartmentDto(string Name,string Description,int ChiefDoctorId);
   
}
