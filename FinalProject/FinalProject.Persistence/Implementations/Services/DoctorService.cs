using AutoMapper;
using FinalProject.Application.Abstractions.Repositories;
using FinalProject.Application.Abstractions.Services;
using FinalProject.Application.DTOs.Department;
using FinalProject.Application.DTOs.Doctor;
using FinalProject.Domain.Entities;
using FinalProject.Persistence.Contexts;
using FinalProject.Persistence.Implementations.Repositories;
using Microsoft.EntityFrameworkCore;
using SendGrid.Helpers.Errors.Model;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Persistence.Implementations.Services
{
    internal class DoctorService : IDoctorService
    {
        private readonly IDoctorRepository _doctorRepository;
        private readonly IMapper _mapper;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly ICloudinaryService _cloudinaryService;

        public DoctorService(IDoctorRepository doctorRepository,
            IMapper mapper,
            IDepartmentRepository departmentRepository,
            ICloudinaryService cloudinaryService
            )
        {
            _doctorRepository = doctorRepository;
            _mapper = mapper;
            _departmentRepository = departmentRepository;
            _cloudinaryService = cloudinaryService;
        }

        public async Task CreateAsync(CreateDoctorDto doctorDto)
        {
            
            if (!await _departmentRepository.AnyAsync(d => d.Id == doctorDto.DepartmentId))
                throw new Exception("Department does not exists");
            if (doctorDto.Photo == null)
                throw new Exception("Please upload Image");
            string imageUrl = await _cloudinaryService.UploadAsync(doctorDto.Photo);
            Doctor doctor = _mapper.Map<Doctor>(doctorDto);
            doctor.ImageUrl = imageUrl; 
            doctor.CreatedAt = DateTime.Now;
            doctor.ModifiedAt = DateTime.Now;
            doctor.JoinDate = DateOnly.FromDateTime(DateTime.Now);
            await _doctorRepository.AddAsync(doctor);
            await _doctorRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var doctor = await _doctorRepository.GetbyIdAsync(id);
            if (doctor is null)
                throw new NotFoundException("Doctor not found");

            _doctorRepository.Delete(doctor);
            await _doctorRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<DoctorItemDto>> GetAllAsync(int page,int take)
        {
            IEnumerable<Doctor> doctors = await _doctorRepository
                   .GetAll(skip: (page - 1) * take, take: take)
                   .ToListAsync();


            return _mapper.Map<IEnumerable<DoctorItemDto>>(doctors);


        }

        public async Task<GetDoctorDto> GetByIdAsync(int id)
        {
            Doctor doctor = await _doctorRepository.GetbyIdAsync(id);

            if (doctor is null)
                return null;


            GetDoctorDto doctorDto = _mapper.Map<GetDoctorDto>(doctor);



            return doctorDto;
        }

        public async Task UpdateAsync(int id, UpdateDoctorDto doctorDto)
        {
            if (!await _departmentRepository.AnyAsync(d => d.Id == doctorDto.DepartmentId))
                throw new Exception("Department does not exists");
            var doctor = await _doctorRepository.GetbyIdAsync(id);
            if (doctor is null)
                throw new NotFoundException("Doctor not found");

            _mapper.Map(doctorDto, doctor);
            doctor.ModifiedAt = DateTime.Now;
            await _doctorRepository.SaveChangesAsync();
        }
    }
}
