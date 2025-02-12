using AutoMapper;
using FinalProject.Application.DTOs.Category;
using FinalProject.Application.DTOs.Department;
using FinalProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Application.MappingProfiles
{
    internal class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, GetCategoryDto>().ReverseMap();
            CreateMap<CreateCategoryDto, Category>();
            CreateMap<UpdateCategoryDto, Category>().ForMember(c => c.Id, opt => opt.Ignore())
                .ForSourceMember(c => c.Name, opt => opt.DoNotValidate());
            CreateMap<Category, CategoryItemDto>();
        }

    }
}
