﻿using FinalProject.Application.Abstractions.Repositories;
using FinalProject.Domain.Entities;
using FinalProject.Persistence.Contexts;
using FinalProject.Persistence.Implementations.Repositories.Generic;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Persistence.Implementations.Repositories
{
    internal class PayrollRepository : Repository<Payroll> , IPayrollRepository
    {
        private readonly AppDbContext _context;
        public PayrollRepository(AppDbContext context) : base(context) {
        
        _context = context;
        }


        public async Task<Payroll> SearchPayrollAsync(int doctorId)
        {
            return await _context.Payrolls
                .FirstOrDefaultAsync(p => p.DoctorId == doctorId);
        } 
      
    }
}
