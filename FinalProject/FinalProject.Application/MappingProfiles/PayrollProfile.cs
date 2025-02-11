using AutoMapper;
using FinalProject.Application.DTOs.Department;
using FinalProject.Application.DTOs.Payroll;
using FinalProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Application.MappingProfiles
{
    internal class PayrollProfile : Profile
    {
        public PayrollProfile()
        {
            CreateMap<Payroll, GetPayrollDto>().ReverseMap();
            CreateMap<CreatePayrollDto, Payroll>();
            CreateMap<UpdatePayrollDto, Payroll>().ForMember(c => c.Id, opt => opt.Ignore());
            CreateMap<Payroll, PayrollItemDto>();
        }
    }
}
