using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Application.DTOs.Comment
{
    public record GetCommentDto(string UserId, int Id, string UserRole, string Content, string UserName, string DoctorId);
  
}
