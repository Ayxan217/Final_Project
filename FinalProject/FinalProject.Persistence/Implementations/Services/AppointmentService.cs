using AutoMapper;
using FinalProject.Application.Abstractions.Repositories;
using FinalProject.Application.Abstractions.Services;
using FinalProject.Application.DTOs.Appointment;
using FinalProject.Domain.Entities;
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

            Appointment appointment = _mapper.Map<Appointment>(appointmentDto);
            appointment.CreatedAt = DateTime.Now;
            appointment.ModifiedAt = DateTime.Now;
            await _appointmentRepository.AddAsync(appointment);
            await _appointmentRepository.SaveChangesAsync();    
        }

        public async Task UpdateAsync(int id,UpdateAppointmentDto appointmentDto)
        {
            var appointment = await _appointmentRepository.GetbyIdAsync(id);
            if (appointment is null)
                throw new NotFoundException("Appointment not found");

            _mapper.Map(appointmentDto, appointment);
            appointment.ModifiedAt = DateTime.Now;
             _appointmentRepository.Update(appointment);
            await _appointmentRepository.SaveChangesAsync();



        }

        public async Task DeleteAsync(int id)
        {
            var appointment = await _appointmentRepository.GetbyIdAsync(id);
            if (appointment is null)
                throw new NotFoundException("Appointment not found");

            _appointmentRepository.Delete(appointment);
            await _appointmentRepository.SaveChangesAsync();    
        }
    }
}
