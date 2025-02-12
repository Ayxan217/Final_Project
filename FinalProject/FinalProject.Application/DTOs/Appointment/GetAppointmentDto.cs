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
     string AppointmentNumber,
     bool IsCanceled,
     string PatientName,
     string PatientCode,
     string PatientSurname,
     string DoctorName,
     string DoctorSurname
 );
}
