using AutoMapper;
using FinalProject.Application.Abstractions.Repositories;
using FinalProject.Application.Abstractions.Services;
using FinalProject.Application.DTOs.Appointment;
using FinalProject.Domain.Entities;
using FinalProject.Domain.Extensions;
using FinalProject.Persistence.Implementations.Repositories;
using Microsoft.EntityFrameworkCore;
using SendGrid.Helpers.Errors.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Persistence.Implementations.Services
{
    internal class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IMapper _mapper;
        private readonly IDoctorRepository _doctorRepository;
        private readonly IPatientRepository _patientRepository;

        public AppointmentService(IAppointmentRepository appointmentRepository
            , IMapper mapper
            ,IDoctorRepository doctorRepository
            ,IPatientRepository patientRepository)
        {
            _appointmentRepository = appointmentRepository;
            _mapper = mapper;
            _doctorRepository = doctorRepository;
            _patientRepository = patientRepository;
        }

        public async Task<GetAppointmentDto> GetByIdAsync(int id)
        {
            Appointment? appointment = await _appointmentRepository.GetAppointmentByIdWithDetailsAsync(id);

            if (appointment is null)
                throw new NotFoundException("Appointment not found");

            return _mapper.Map<GetAppointmentDto>(appointment);
        }

        public async Task<IEnumerable<AppointmentItemDto>> GetAllAsync(int page,int take)
        {
            IEnumerable<Appointment> appointments = await _appointmentRepository
                            .GetAllAppointmentsWithDetailsAsync((page - 1) * take, take: take);
                            
                            
            return _mapper.Map<IEnumerable<AppointmentItemDto>>(appointments);
        }

        public async Task CreateAsync(CreateAppointmentDto appointmentDto)
        {
            if (!await _doctorRepository.AnyAsync(d => d.Id == appointmentDto.DoctorId))
                throw new Exception("Doctor does not exists");

            if (!await _patientRepository.AnyAsync(p => p.Id == appointmentDto.PatientId))
                throw new Exception("Patient does not exists");

            DateTime roundedDate = appointmentDto.AppointmentDate.RoundToNearest10Minutes();
            DateTime oneWeekLater = DateTime.UtcNow.AddDays(90);
            if (roundedDate > oneWeekLater)
                throw new Exception("Appointments can only be made up to 3 month in advance.");

            Appointment? existingAppointment = await _appointmentRepository
           .GetAppointmentByDateAndDoctorAsync(roundedDate, appointmentDto.DoctorId);

            if (existingAppointment is not null)
                throw new Exception($"thre is already an appointment at this time");

            var hasExistingAppointment = await _appointmentRepository
          .HasPatientAppointmentForDateAsync(appointmentDto.PatientId, roundedDate.Date);

            if (hasExistingAppointment)
            {
                throw new Exception(
                    $"Patient already has an appointment on {roundedDate.Date:dd/MM/yyyy}. " +
                    "Only one appointment per day is allowed.");
            }

            Appointment appointment = _mapper.Map<Appointment>(appointmentDto);
            appointment.AppointmentDate = roundedDate;
            appointment.CreatedAt = DateTime.Now;
            appointment.ModifiedAt = DateTime.Now;
            appointment.AppointmentNumber = Guid.NewGuid().ToString().Substring(0,8).ToUpper();
            await _appointmentRepository.AddAsync(appointment);
            await _appointmentRepository.SaveChangesAsync();    
        }



        public async Task UpdateAsync(int id,UpdateAppointmentDto appointmentDto)
        {
            Appointment appointment = await _appointmentRepository.GetbyIdAsync(id);
            if (appointment is null)
                throw new NotFoundException("Appointment not found");

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
