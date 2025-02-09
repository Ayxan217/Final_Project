using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Application.DTOs.Comment
{
   public record CommentItemDto(int Id,
       string UserName,
       string UserRole,
       int DoctorId);
   
}

