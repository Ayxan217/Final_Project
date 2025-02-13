using AutoMapper;
using FinalProject.Application.DTOs.Appointment;
using FinalProject.Application.DTOs.Product;
using FinalProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Application.MappingProfiles
{
    internal class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, GetProductDto>()
                 .ForMember(nameof(GetProductDto.Reviews), opt => opt.Ignore())
                .ForMember(nameof(GetProductDto.InStock), opt => opt.Ignore())
                .ReverseMap();
            CreateMap<CreateProductDto, Product>();
            CreateMap<UpdateProductDto, Product>().ForMember(p => p.Id, opt => opt.Ignore());

            CreateMap<Product, ProductItemDto>()
                .ForMember(nameof(ProductItemDto.Reviews), opt => opt.Ignore())
                .ForMember(nameof(ProductItemDto.InStock), opt => opt.Ignore());
        }
    }
}
