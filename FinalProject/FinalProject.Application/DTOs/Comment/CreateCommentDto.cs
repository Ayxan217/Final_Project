﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Application.DTOs.Comment
{
    public record CreateCommentDto(
    string Content,
    string UserName,
    string? UserEmail,
    int DoctorId
);
}
