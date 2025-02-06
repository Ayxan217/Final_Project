using FinalProject.Application.DTOs.Patient;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Application.Abstractions.Services
{
    public interface IPatientService
    {
        Task<ICollection<PatientItemDto>> GetAllAsync(int page,int take);
        Task<GetPatientDto> GetByIdAsync(int id);
        Task CreateAsync(CreatePatientDto createPatientDto);
        Task UpdateAsync(UpdatePatientDto updatePatientDto);
        Task DeleteAsync(int id);
        Task<IEnumerable<GetPatientDto>> SearchAsync(string searchTerm);
    }
}
