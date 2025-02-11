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
            ICollection<Patient> patients = await _patientRepository.GetAll(skip: (page - 1) * take, take: take)
                   .ToListAsync();
            ;
            return _mapper.Map<ICollection<PatientItemDto>>(patients);
        }

        public async Task<GetPatientDto> GetByIdAsync(int id)
        {
            Patient patient = await _patientRepository.GetbyIdAsync(id);
            if (patient is null)
                throw new NotFoundException($"Patient with ID {id} not found.");

            return _mapper.Map<GetPatientDto>(patient);
        }

     

        public async Task UpdateAsync(int id,UpdatePatientDto updatePatientDto)
        {
            Patient patient = await _patientRepository.GetbyIdAsync(id);
            if (patient is null)
                throw new NotFoundException($"Patient with ID {id} not found.");

            _mapper.Map(updatePatientDto, patient);
            patient.ModifiedAt = DateTime.Now;
             _patientRepository.Update(patient);
            await _patientRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            Patient patient = await _patientRepository.GetbyIdAsync(id);
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
            patient.IdentityCode = Guid.NewGuid().ToString().Substring(0,7).ToUpperInvariant();
            await _patientRepository.AddAsync(patient);
            await _patientRepository.SaveChangesAsync();

            
        }

        public async Task<GetPatientDto> SearchIdentityAsync(string IdentityCode)
        {
            if (IdentityCode.Length != 7)
                throw new Exception("Identity code must be 7 length");
            Patient patient = await _patientRepository.SearchPatientIdentityAsync(IdentityCode);
            if(patient is null)
                throw new Exception("Patient does not exists");
            return _mapper.Map<GetPatientDto>(patient);
        }
    }
}
