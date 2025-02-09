using AutoMapper;
using FinalProject.Application.Abstractions.Repositories;
using FinalProject.Application.Abstractions.Services;
using FinalProject.Application.DTOs.Comment;
using FinalProject.Domain.Entities;
using FinalProject.Persistence.Implementations.Repositories;
using SendGrid.Helpers.Errors.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Persistence.Implementations.Services
{
    internal class CommentSerive : ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IMapper _mapper;

        public CommentSerive(ICommentRepository commentRepository
            ,IMapper mapper)
        {
            _commentRepository = commentRepository;
            _mapper = mapper;
        }
        public async Task CreateCommentAsync(CreateCommentDto commentDto)
        {
            Comment comment = _mapper.Map<Comment>(commentDto);
            comment.CreatedAt = DateTime.Now;
            comment.ModifiedAt = DateTime.Now;
            await _commentRepository.AddAsync(comment);
            await _commentRepository.SaveChangesAsync();

        }

        public async Task DeleteCommentAsync(int id)
        {
            Comment comment = await _commentRepository.GetbyIdAsync(id);
            if (comment is null)
                throw new Exception("Department Not Found");

            _commentRepository.Delete(comment);
            await _commentRepository.SaveChangesAsync();
        }

        public async Task<GetCommentDto> GetCommentByIdAsync(int id)
        {
            Comment comment = await _commentRepository.GetByIdWithDetailsAsync(id);

            if (comment is null)
                throw new NotFoundException("Comment not found");

            return _mapper.Map<GetCommentDto>(comment);
        }

        public async Task<IEnumerable<CommentItemDto>> GetDoctorCommentsAsync(int doctorId)
        {
            IEnumerable<Comment> comments = await _commentRepository.GetDoctorCommentsAsync(doctorId);
            return _mapper.Map<IEnumerable<CommentItemDto>>(comments);
        }
    }
}
