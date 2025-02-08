using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Application.DTOs.Appointment
{
    public record UpdateAppointmentDto(
      int Id,
      DateTime AppointmentDate,
      string Description,
      int PatientId,
      int DoctorId
  );
}
