using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Application.DTOs.Appointment
{
    public record AppointmentItemDto(
     int Id,
     DateTime AppointmentDate,
     string PatientCode,
     string AppointmentNumber,
     bool IsCanceled,
     string PatientName,
     string PatientSurname,
     string DoctorName,
     string DoctorSurname
 );
}
