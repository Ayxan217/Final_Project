﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Application.DTOs.Payroll
{
    public record PayrollItemDto(int Id,
        int DoctorId,
        decimal Salary,
        decimal TaxRate,
        decimal InsuranceRate,
        decimal NetSalary,
        DateOnly PaymentTime);
  
}
