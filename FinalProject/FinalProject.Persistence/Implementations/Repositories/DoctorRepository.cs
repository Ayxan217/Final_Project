using FinalProject.Application.Abstractions.Repositories;
using FinalProject.Domain.Entities;
using FinalProject.Persistence.Contexts;
using FinalProject.Persistence.Implementations.Repositories.Generic;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.Persistence.Implementations.Repositories
{
    internal class DoctorRepository : Repository<Doctor>, IDoctorRepository
    {
        private readonly AppDbContext _context;

        public DoctorRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public Task<Doctor> SearchByIdentityNumberAsync(string identityNumber)
        {
            return _context.Doctors.Include(d => d.Appointments)
                .ThenInclude(a=>a.Patient)
                .FirstOrDefaultAsync(d => d.IdentityCode == identityNumber);

        }
    }
}
