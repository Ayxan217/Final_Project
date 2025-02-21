namespace FinalProject.Application.DTOs.Appointment
{
    public record CreateAppointmentDto(
    DateTime AppointmentDate,

    int PatientId,
    int DoctorId
);
}
