using FinalProject.Application.Abstractions.Services;
using FinalProject.Application.DTOs.Comment;
using FinalProject.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SendGrid.Helpers.Errors.Model;
using System.Security.Claims;

namespace FinalProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentsController(ICommentService commentService)
        {
            _commentService = commentService;
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<GetCommentDto>> GetById(int id)
        {
            try
            {
                var comment = await _commentService.GetCommentByIdAsync(id);
                return Ok(comment);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CommentItemDto>>> GetDoctorComments(int doctorId)
        {
            var comments = await _commentService.GetDoctorCommentsAsync(doctorId);
            return Ok(comments);
        }

        [Authorize(Roles ="Patient")]
        [HttpPost("doctor/{doctorId}")]
        public async Task<IActionResult> Create(int doctorId, [FromBody] CreateCommentDto createCommentDto)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
                return Unauthorized();

            
           

            await _commentService.CreateCommentAsync(createCommentDto);
            return StatusCode(StatusCodes.Status201Created);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (userId == null)
                    return Unauthorized();

                GetCommentDto comment = await _commentService.GetCommentByIdAsync(id);

                
                bool isAdmin = User.IsInRole("Admin");
                if (!isAdmin && comment.UserId != userId)
                    return Forbid();

                await _commentService.DeleteCommentAsync(id);
                return Ok();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}

