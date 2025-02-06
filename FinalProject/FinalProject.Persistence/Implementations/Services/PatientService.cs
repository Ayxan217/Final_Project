using AutoMapper;
using FinalProject.Application.Abstractions.Repositories;
using FinalProject.Application.Abstractions.Services;
using FinalProject.Application.DTOs.Patient;
using FinalProject.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using SendGrid.Helpers.Errors.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Persistence.Implementations.Services
{
    internal class PatientService : IPatientService
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IMapper _mapper;

        public PatientService(IPatientRepository patientRepository, IMapper mapper)
        {
            _patientRepository = patientRepository;
            _mapper = mapper;
        }

        public async Task<ICollection<PatientItemDto>> GetAllAsync(int page = 1,int take = 3)
        {
            var patients = await _patientRepository.GetAll(skip: (page - 1) * take, take: take)
                   .ToListAsync();
            ;
            return _mapper.Map<List<PatientItemDto>>(patients);
        }

        public async Task<GetPatientDto> GetByIdAsync(int id)
        {
            var patient = await _patientRepository.GetbyIdAsync(id);
            if (patient is null)
                throw new NotFoundException($"Patient with ID {id} not found.");

            return _mapper.Map<GetPatientDto>(patient);
        }

     

        public async Task UpdateAsync(UpdatePatientDto updatePatientDto)
        {
            var patient = await _patientRepository.GetbyIdAsync(updatePatientDto.Id);
            if (patient is null)
                throw new NotFoundException($"Patient with ID {updatePatientDto.Id} not found.");

            _mapper.Map(updatePatientDto, patient);
             _patientRepository.Update(patient);
            await _patientRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var patient = await _patientRepository.GetbyIdAsync(id);
            if (patient == null)
                throw new NotFoundException($"Patient with ID {id} not found.");

            _patientRepository.Delete(patient);
            await _patientRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<GetPatientDto>> SearchAsync(string searchTerm)
        {
            var patients = await _patientRepository.SearchPatientsAsync(searchTerm);
            return _mapper.Map<IEnumerable<GetPatientDto>>(patients);
        }

       

        public async Task CreateAsync(CreatePatientDto patientDto)
        {
            Patient patient = _mapper.Map<Patient>(patientDto);
            patient.CreatedAt = DateTime.Now;
            patient.ModifiedAt = DateTime.Now;
            await _patientRepository.AddAsync(patient);
            await _patientRepository.SaveChangesAsync();

            
        }

       
    }
}
