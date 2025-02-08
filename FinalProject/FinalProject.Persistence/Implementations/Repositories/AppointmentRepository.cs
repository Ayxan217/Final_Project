using FinalProject.Application.Abstractions.Repositories;
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
    internal class AppointmentRepository : Repository<Appointment>,IAppointmentRepository
    {
        private readonly AppDbContext _context;
        public AppointmentRepository(AppDbContext context) : base(context) {

              _context = context;
        
        }

        public async Task<IEnumerable<Appointment>> GetAllAppointmentsWithDetailsAsync(int page,int take)
        {
            return await _context.Appointments
                .Include(a => a.Doctor)
                .Include(a => a.Patient)
                .ToListAsync();
        }

        
        public async Task<Appointment?> GetAppointmentByIdWithDetailsAsync(int id)
        {
            return await _context.Appointments
                .Include(a => a.Doctor)
                .Include(a => a.Patient)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

    }
}
