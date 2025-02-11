using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Application.DTOs.Payroll
{
    public record UpdatePayrollDto
        (decimal Salary,
        decimal TaxRate,
        decimal InsuranceRate);
}
