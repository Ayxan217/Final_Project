﻿using FinalProject.Domain.Entites;
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
        //public Doctor Doctor { get; set; }
        public int UserId { get; set; }
        //public AppUser AppUser { get; set; }
    }
}
