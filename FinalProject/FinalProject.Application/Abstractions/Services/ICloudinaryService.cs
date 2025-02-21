﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Application.Abstractions.Services
{
    public interface ICloudinaryService
    {
        Task<(string imageUrl, string publicId)> UploadAsync(IFormFile file);
        Task DeleteAsync(string publicId);
    }
}
