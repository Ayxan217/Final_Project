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

        public AppointmentService(IAppointmentRepository appointmentRepository, IMapper mapper)
        {
            _appointmentRepository = appointmentRepository;
            _mapper = mapper;
        }

        public async Task<GetAppointmentDto> GetByIdAsync(int id)
        {
            var appointment = await _appointmentRepository.GetbyIdAsync(id);
            if (appointment == null)
                throw new NotFoundException("Appointment not found");

            return _mapper.Map<GetAppointmentDto>(appointment);
        }

        public async Task<IEnumerable<AppointmentItemDto>> GetAllAsync(int page,int take)
        {
            IEnumerable<Appointment> appointments = await _appointmentRepository
                            .GetAll(skip:(page-1)*take,take:take)
                            .ToListAsync();
                            
            return _mapper.Map<List<AppointmentItemDto>>(appointments);
        }

        public async Task CreateAsync(CreateAppointmentDto appointmentDto)
        {
            var appointment = _mapper.Map<Appointment>(appointmentDto);
            await _appointmentRepository.AddAsync(appointment);
            await _appointmentRepository.SaveChangesAsync();    
        }

        public async Task UpdateAsync(int id,UpdateAppointmentDto appointmentDto)
        {
            var appointment = await _appointmentRepository.GetbyIdAsync(id);
            if (appointment is null)
                throw new NotFoundException("Appointment not found");

            _mapper.Map(appointment, appointment);
             _appointmentRepository.Update(appointment);
            await _appointmentRepository.SaveChangesAsync();



        }

        public async Task DeleteAsync(int id)
        {
            var appointment = await _appointmentRepository.GetbyIdAsync(id);
            if (appointment == null)
                throw new NotFoundException("Appointment not found");

            _appointmentRepository.Update(appointment);
            await _appointmentRepository.SaveChangesAsync();    
        }
    }
}
