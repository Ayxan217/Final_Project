using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Application.DTOs.ProductReview
{
    public record GetReviewDto(string Content,int ProductId,string UserId);

}
