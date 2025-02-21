using FinalProject.Application.Abstractions.Repositories;
using FinalProject.Domain.Entities;
using FinalProject.Persistence.Contexts;
using FinalProject.Persistence.Implementations.Repositories.Generic;

namespace FinalProject.Persistence.Implementations.Repositories
{
    internal class DoctorRepository : Repository<Doctor>, IDoctorRepository
    {

        public DoctorRepository(AppDbContext context) : base(context)
        {
        }

    }
}
