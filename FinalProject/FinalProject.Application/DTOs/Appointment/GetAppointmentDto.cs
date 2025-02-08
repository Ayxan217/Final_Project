using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Application.DTOs.Appointment
{
    public record GetAppointmentDto(
     int Id,
     DateTime AppointmentDate,
     string Description,
     string PatientFullName,
     string DoctorFullName,
     int PatientId,
     int DoctorId,
     DateTime CreatedAt,
     DateTime? ModifiedAt
 );
}
