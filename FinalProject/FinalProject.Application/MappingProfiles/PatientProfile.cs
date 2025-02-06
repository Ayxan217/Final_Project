using AutoMapper;
using FinalProject.Application.DTOs.Doctor;
using FinalProject.Application.DTOs.Patient;
using FinalProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Application.MappingProfiles
{
    internal class PatientProfile : Profile
    {
        public PatientProfile()
        {
            CreateMap<CreatePatientDto, Patient>();
            CreateMap<UpdatePatientDto, Patient>().ForMember(x => x.Id, opt => opt.Ignore())
                .ForSourceMember(x => x.Name, opt => opt.DoNotValidate());
            CreateMap<Patient, GetPatientDto>().ReverseMap();
            CreateMap<Patient, PatientItemDto>();
        }
    }
}
