using FinalProject.Application.Abstractions.Repositories.Generic;
using FinalProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Application.Abstractions.Repositories
{
    public interface IDoctorRepository : IRepository<Doctor>
    {
        Task<Doctor> GetDoctorAsyncWithComments(int id);
        Task<ICollection<Doctor>> GetDoctorsWithCommentsAsync(int page, int take);
    }
}
