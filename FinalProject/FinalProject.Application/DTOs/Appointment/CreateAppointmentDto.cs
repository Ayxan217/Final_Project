using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Application.DTOs.Appointment
{
    public record CreateAppointmentDto(
    DateTime AppointmentDate,
    
    int PatientId,
    int DoctorId
);
}
