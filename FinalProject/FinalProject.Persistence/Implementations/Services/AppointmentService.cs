using AutoMapper;
using FinalProject.Application.Abstractions.Repositories;
using FinalProject.Application.Abstractions.Services;
using FinalProject.Application.DTOs.Appointment;
using FinalProject.Domain.Entities;
using FinalProject.Domain.Extensions;
using Microsoft.EntityFrameworkCore;
using SendGrid.Helpers.Errors.Model;

namespace FinalProject.Persistence.Implementations.Services
{
    internal class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IMapper _mapper;
        private readonly IDoctorRepository _doctorRepository;
        private readonly IPatientRepository _patientRepository;
        private readonly IEmailService _emailService;
        private readonly string EmailSubject = "Your Appointment Has Been Confirmed ✅";


        public AppointmentService(IAppointmentRepository appointmentRepository
            , IMapper mapper
            , IDoctorRepository doctorRepository
            , IPatientRepository patientRepository,
            IEmailService emailService)
        {
            _appointmentRepository = appointmentRepository;
            _mapper = mapper;
            _doctorRepository = doctorRepository;
            _patientRepository = patientRepository;
            _emailService = emailService;
        }

        public async Task<GetAppointmentDto> GetByIdAsync(int id)
        {
            Appointment? appointment = await _appointmentRepository.GetbyIdAsync(id, "Doctor", "Patient");

            if (appointment is null)
                throw new NotFoundException("Appointment not found");

            return _mapper.Map<GetAppointmentDto>(appointment);
        }

        public async Task<IEnumerable<AppointmentItemDto>> GetAllAsync(int page, int take)
        {
            IEnumerable<Appointment> appointments = await _appointmentRepository
                             .GetAll(null, null, false, false, skip: (page - 1) * take, take: take, "Doctor", "Patient")
                   .ToListAsync(); ;


            return _mapper.Map<IEnumerable<AppointmentItemDto>>(appointments);
        }

        public async Task CreateAsync(CreateAppointmentDto appointmentDto)
        {
            if (!await _doctorRepository.AnyAsync(d => d.Id == appointmentDto.DoctorId))
                throw new Exception("Doctor does not exists");


            if (!await _patientRepository.AnyAsync(p =>p.IdentityCode.Equals(appointmentDto.PatientCode)))
                throw new Exception("Patient does not exists");


            if (appointmentDto.AppointmentDate.TimeOfDay < TimeSpan.FromHours(9) ||
            appointmentDto.AppointmentDate.TimeOfDay > TimeSpan.FromHours(18))
            {
                throw new Exception("Appointments can only scheduled between 9:00 and 18:00");
            }


            DateTime roundedDate = appointmentDto.AppointmentDate.RoundToNearest10Minutes();
            DateTime maxFarDate = DateTime.UtcNow.AddDays(180);
            if (roundedDate > maxFarDate)
                throw new Exception("Appointments can only be made up to 6 month in advance.");


            Appointment? existingAppointment = await _appointmentRepository
           .GetAppointmentByDateAndDoctorAsync(roundedDate, appointmentDto.DoctorId);


            if (existingAppointment is not null)
                throw new Exception($"thre is already an appointment at this time");

            var hasExistingAppointment = await _appointmentRepository
          .HasPatientAppointmentForDateAsync(appointmentDto.PatientCode, roundedDate.Date);


            if (hasExistingAppointment)
            {
                throw new Exception(
                    $"Patient already has an appointment on {roundedDate.Date:dd/MM/yyyy}. " +
                    "Only one appointment per day is allowed.");
            }

            Patient patient = await _patientRepository
                .SearchPatientIdentityAsync(appointmentDto.PatientCode);


            Appointment appointment = _mapper.Map<Appointment>(appointmentDto);
            
            appointment.PatientId = patient.Id;
            appointment.AppointmentDate = roundedDate;
            appointment.CreatedAt = DateTime.Now;
            appointment.ModifiedAt = DateTime.Now;
            appointment.AppointmentNumber = Guid.NewGuid().ToString().Substring(0, 8).ToUpper();
            Doctor doctor = await _doctorRepository.GetbyIdAsync(appointment.DoctorId);

            string EmailMessage = $"  <p>Dear {patient.Name} {patient.Surname},</p>\r\n " +
       "       <p>We are pleased to inform you that your appointment has been successfully scheduled." +
       " Below are the details:</p>\r\n        <ul>\r\n         " +
       $"   <li><strong>Date:</strong>{appointment.AppointmentDate:g} </li>\r\n  " +
       $"     <li><strong>Doctor:</strong> Dr.{doctor.Name} {doctor.Surname} </li>\r\n               " +
       $"    </ul>\r\n  <p>Your appointment number is: {appointment.AppointmentNumber} .</p>\r\n " +
       "       <p>We look forward to seeing you!</p>\r\n     " +
       "   <p><strong>MedTex Clinic</strong></p>\"";

            await _emailService.SendEmailAsync(patient.Email, EmailSubject, EmailMessage);     
            await _appointmentRepository.AddAsync(appointment);
            await _appointmentRepository.SaveChangesAsync();
        }



        public async Task UpdateAsync(int id, UpdateAppointmentDto appointmentDto)
        {
            Appointment appointment = await _appointmentRepository.GetbyIdAsync(id);
            if (appointment is null)
                throw new NotFoundException("Appointment not found");

            if (appointmentDto.AppointmentDate.TimeOfDay < TimeSpan.FromHours(9) ||
       appointmentDto.AppointmentDate.TimeOfDay > TimeSpan.FromHours(18))
            {
                throw new Exception("Appointments can only scheduled between 9:00 and 18:00");
            }


            _mapper.Map(appointmentDto, appointment);
            appointment.ModifiedAt = DateTime.Now;
            _appointmentRepository.Update(appointment);
            await _appointmentRepository.SaveChangesAsync();



        }

        public async Task DeleteAsync(int id)
        {
            Appointment appointment = await _appointmentRepository.GetbyIdAsync(id);
            if (appointment is null)
                throw new NotFoundException("Appointment not found");

            _appointmentRepository.Delete(appointment);
            await _appointmentRepository.SaveChangesAsync();
        }

        public async Task CancelAppointmentAsync(string appointmentNumber)
        {
            Appointment? appointment = await _appointmentRepository.GetAppointmentByNumber(appointmentNumber);
            if (appointment is null)
                throw new NotFoundException("Appointment does not exist");
            appointment.IsCanceled = true;
            await _appointmentRepository.SaveChangesAsync();
        }
    }
}
