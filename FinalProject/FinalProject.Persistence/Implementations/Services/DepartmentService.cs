using AutoMapper;
using FinalProject.Application.Abstractions.Repositories;
using FinalProject.Application.Abstractions.Services;
using FinalProject.Application.DTOs.Department;
using FinalProject.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Persistence.Implementations.Services
{
    internal class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;

        public DepartmentService(IDepartmentRepository departmentRepository,IMapper mapper) 
        {
            _departmentRepository = departmentRepository;
            _mapper = mapper;
        }
        public async Task CreateAsync(CreateDepartmentDto departmentDto)
        {
            if (await _departmentRepository.AnyAsync(c => c.Name == departmentDto.Name)) throw new Exception("Alredy Exists");

            Department department = _mapper.Map<Department>(departmentDto);
            department.CreatedAt = DateTime.Now;
            department.ModifiedAt = DateTime.Now;
            await _departmentRepository.AddAsync(department);
            await _departmentRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            Department department = await _departmentRepository.GetbyIdAsync(id);
            if (department is null)
                throw new Exception("Department Not Found");

            _departmentRepository.Delete(department);
            await _departmentRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<DepartmentItemDto>> GetAllAsync(int page, int take)
        {
            IEnumerable<Department> departments = await _departmentRepository
                   .GetAll(skip: (page - 1) * take, take: take)
                   .ToListAsync();

            return _mapper.Map<IEnumerable<DepartmentItemDto>>(departments);
        }

        public async Task<GetDepartmentDto> GetByIdAsync(int id)
        {
            Department department = await _departmentRepository.GetbyIdAsync(id);

            if (department is null)
                return null;
            

            GetDepartmentDto departmentDto = _mapper.Map<GetDepartmentDto>(department);



            return departmentDto;
        }

        public async Task UpdateAsync(int id, UpdateDepartmentDto departmentDto)
        {
            Department department = await _departmentRepository.GetbyIdAsync(id);
            if (department is null)
                throw new Exception($"Department with id {id} was not found");

            bool exists = await _departmentRepository
                .AnyAsync(c => c.Name == departmentDto.Name && c.Id != id);
            if (exists)
                throw new Exception($"Department with name '{departmentDto.Name}' already exists");

            
            if (departmentDto.ChiefDoctorId.HasValue)
            {
                bool chiefDoctorExists = await _departmentRepository
                    .AnyAsync(d => d.ChiefDoctorId == departmentDto.ChiefDoctorId && d.Id != id);
                if (chiefDoctorExists)
                    throw new Exception($"Doctor with id {departmentDto.ChiefDoctorId} is already a chief doctor in another department");
            }

            _mapper.Map(departmentDto, department);
            department.ModifiedAt = DateTime.Now;

            _departmentRepository.Update(department);
            await _departmentRepository.SaveChangesAsync();
        }
    }
}
