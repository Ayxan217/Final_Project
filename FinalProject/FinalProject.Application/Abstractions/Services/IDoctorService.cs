using FinalProject.Application.DTOs.Doctor;
using FinalProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Application.Abstractions.Services
{
    public interface IDoctorService
    {
        Task<ICollection<DoctorItemDto>> GetAllAsync(int page,int take);
        Task<GetDoctorDto> GetByIdAsync(int id);
        Task CreateAsync(CreateDoctorDto doctorDto);
        Task UpdateAsync(int id, UpdateDoctorDto doctorDto);
        Task DeleteAsync(int id);
    }
}
