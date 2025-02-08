using AutoMapper;
using FinalProject.Application.DTOs.Appointment;
using FinalProject.Application.DTOs.Department;
using FinalProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Application.MappingProfiles
{
    internal class AppointmentProfile : Profile
    {
        public AppointmentProfile()
        {
            CreateMap<Appointment, AppointmentItemDto>()
                .ConstructUsing(src => new AppointmentItemDto(
                    src.Id,
                    src.AppointmentDate,
                    $"{src.Patient.Name} {src.Patient.Surname}",
                    $"{src.Doctor.Name} {src.Doctor.Surname}"
                ));

            CreateMap<Appointment, GetAppointmentDto>()
                .ConstructUsing(src => new GetAppointmentDto(
                    src.Id,
                    src.AppointmentDate,
                    src.,
                    $"{src.Patient.Name} {src.Patient.Surname}",
                    $"{src.Doctor.Name} {src.Doctor.Surname}",
                    src.PatientId,
                    src.DoctorId,
                    src.CreatedDate,
                    src.ModifiedAt
                ));

            CreateMap<CreateAppointmentDto, Appointment>()
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => true))
                .ForMember(dest => dest.AppointmentDate, opt => opt.MapFrom(src => src.AppointmentDate))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.PatientId, opt => opt.MapFrom(src => src.PatientId))
                .ForMember(dest => dest.DoctorId, opt => opt.MapFrom(src => src.DoctorId));

            CreateMap<UpdateAppointmentDto, Appointment>()
                .ForMember(dest => dest.ModifiedAt, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.AppointmentDate, opt => opt.MapFrom(src => src.AppointmentDate))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.PatientId, opt => opt.MapFrom(src => src.PatientId))
                .ForMember(dest => dest.DoctorId, opt => opt.MapFrom(src => src.DoctorId));
        }
    }
}
