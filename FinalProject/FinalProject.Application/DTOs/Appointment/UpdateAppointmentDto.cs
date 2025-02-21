namespace FinalProject.Application.DTOs.Appointment
{
    public record UpdateAppointmentDto(

      DateTime AppointmentDate,

      int PatientId,
      int DoctorId
  );
}
