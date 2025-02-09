using FinalProject.Application.DTOs.Comment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Application.Abstractions.Services
{
    public interface ICommentService
    {
        Task<IEnumerable<CommentItemDto>> GetDoctorCommentsAsync(int doctorId);
        Task<GetCommentDto> GetCommentByIdAsync(int id);
        Task CreateCommentAsync(CreateCommentDto commentDto);
        Task DeleteCommentAsync(int id);
    }
}
