using FinalProject.Application.DTOs.Patient;
using FinalProject.Application.DTOs.Payroll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Application.Abstractions.Services
{
    public interface IPayrollService
    {
        Task<IEnumerable<PayrollItemDto>> GetAllAsync(int page, int take);
        Task<GetPayrollDto> GetByIdAsync(int id);
        Task CreateAsync(CreatePayrollDto payrollDto);
        Task UpdateAsync(int id, UpdatePayrollDto payrollDto);
        Task DeleteAsync(int id);
    }
}
