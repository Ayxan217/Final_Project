using FinalProject.Application.Abstractions.Repositories;
using FinalProject.Domain.Entities;
using FinalProject.Persistence.Contexts;
using FinalProject.Persistence.Implementations.Repositories.Generic;
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
        public DoctorRepository(AppDbContext context) : base(context)
        {
        }

    }
}
