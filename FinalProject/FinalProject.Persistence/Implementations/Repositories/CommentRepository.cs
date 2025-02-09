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
    internal class CommentRepository : Repository<Comment>,ICommentRepository
    {
        private readonly AppDbContext _context;
        public CommentRepository(AppDbContext context) : base(context) {
        
        _context = context;
        
        }


        public async Task<Comment> GetByIdWithDetailsAsync(int id)
        {
            return await _context.Comments
                .Include(c => c.AppUser)
                .Include(c => c.Doctor)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Comment>> GetDoctorCommentsAsync(int doctorId)
        {
            return await _context.Comments
                .Include(c => c.AppUser)
                .Where(c => c.DoctorId == doctorId)
                .OrderByDescending(c => c.CreatedAt)
                .ToListAsync();
        }

    }
}
