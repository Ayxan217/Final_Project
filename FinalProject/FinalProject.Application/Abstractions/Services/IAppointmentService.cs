using FinalProject.Application.DTOs.Appointment;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Application.Abstractions.Services
{
    public interface IAppointmentService
    {
        Task<GetAppointmentDto> GetByIdAsync(int id);
        Task<IEnumerable<AppointmentItemDto>> GetAllAsync(int page,int take);
        Task CreateAsync(CreateAppointmentDto appointmentDto);
        Task UpdateAsync(int id,UpdateAppointmentDto appointmentDto);
        Task DeleteAsync(int id);
    }
}
