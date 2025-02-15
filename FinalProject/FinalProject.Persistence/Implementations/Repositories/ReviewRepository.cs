using FinalProject.Application.Abstractions.Repositories;
using FinalProject.Domain.Entities;
using FinalProject.Persistence.Contexts;
using FinalProject.Persistence.Implementations.Repositories.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Persistence.Implementations.Repositories
{
    internal class ReviewRepository : Repository<Review>,IReviewRepository
    {
        public ReviewRepository(AppDbContext context) : base(context) { }
       
    }
}
