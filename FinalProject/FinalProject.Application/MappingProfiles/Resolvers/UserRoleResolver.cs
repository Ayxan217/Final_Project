using AutoMapper;
using FinalProject.Application.DTOs.Comment;
using FinalProject.Domain.Entities;
using FinalProject.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Application.MappingProfiles
{
    public class UserRoleResolver : IValueResolver<Comment, GetCommentDto, string>
    {
        private readonly UserManager<AppUser> _userManager;

        public UserRoleResolver(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public string Resolve(Comment source, GetCommentDto destination, string destMember, ResolutionContext context)
        {
            var roles = _userManager.GetRolesAsync(source.AppUser).Result;
            return roles.FirstOrDefault() ?? Roles.Patient.ToString(); 
        }
    }
}
