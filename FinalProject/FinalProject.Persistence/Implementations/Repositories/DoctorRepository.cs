using FinalProject.Application.Abstractions.Repositories;
using FinalProject.Domain.Entities;
using FinalProject.Persistence.Contexts;
using FinalProject.Persistence.Implementations.Repositories.Generic;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Persistence.Implementations.Repositories
{
    internal class DoctorRepository :Repository<Doctor> , IDoctorRepository
    {
        private readonly AppDbContext _context;
        public DoctorRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<ICollection<Doctor>> GetDoctorsWithCommentsAsync(int page, int take)
        {
            return await _context.Doctors
         .Include(c=>c.Comments)
         .Skip((page - 1) * take)
         .Take(take)
         .ToListAsync();

        }


        public async Task<Doctor> GetDoctorAsyncWithComments(int id)
        {
           return  await _context.Doctors
                .Include(c=>c.Comments)
                .FirstOrDefaultAsync(p => p.Id == id);

        }

    }
}
