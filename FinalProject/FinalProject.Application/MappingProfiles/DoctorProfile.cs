using AutoMapper;
using FinalProject.Application.DTOs.Doctor;
using FinalProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Application.MappingProfiles
{
    internal class DoctorProfile : Profile
    {
        public DoctorProfile()
        {
            CreateMap<CreateDoctorDto, Doctor>();
            CreateMap<UpdateDoctorDto, Doctor>().ForMember(x=>x.Id,opt=>opt.Ignore())
                .ForSourceMember(x=>x.Name,opt=>opt.DoNotValidate());
            CreateMap<Doctor,GetDoctorDto>().ReverseMap();
            CreateMap<Doctor, DoctorItemDto>();

        }
    }
}
