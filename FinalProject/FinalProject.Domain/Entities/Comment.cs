using FinalProject.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Domain.Entities
{
    public class Comment : BaseEntity
    {
        public string Content { get; set; }

        // Relational
        public int DoctorId { get; set; }
        public string UserId { get; set; }
    }
}
