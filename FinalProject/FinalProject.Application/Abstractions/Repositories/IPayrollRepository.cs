using FinalProject.Application.Abstractions.Repositories.Generic;
using FinalProject.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Application.Abstractions.Repositories
{
    
    public interface IPayrollRepository : IRepository<Payroll>
    {
        Task<Payroll> SearchPayrollAsync(int doctorId);
      
    }
}
