using FinalProject.Application.Abstractions.Repositories.Generic;
using FinalProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Application.Abstractions.Repositories
{
    public interface IAppointmentRepository : IRepository<Appointment>
    {
        Task<IEnumerable<Appointment>> GetAllAppointmentsWithDetailsAsync(int page,int take);
        Task<Appointment?> GetAppointmentByIdWithDetailsAsync(int id);
        Task<Appointment?> GetAppointmentByDateAndDoctorAsync(DateTime date, int doctorId);
        Task<Appointment?> GetAppointmentByNumber(string appointmentNumber);
        Task<bool> HasPatientAppointmentForDateAsync(int patientId, DateTime date);
    }
}
