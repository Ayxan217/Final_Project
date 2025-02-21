using FinalProject.Application.DTOs.Comment;

namespace FinalProject.Application.DTOs.Doctor
{


    public record DoctorItemDto(int Id,
        string? ImageUrl,
        string Name,
    string Surname,
    string Description,
    int DepartmentId,
    string Email,
    string PhoneNumber,
    string? Twitter,
    string? Skype,
    string? Facebook,
    string? Ven,
    ICollection<GetCommentDto> Comments


        );

}
