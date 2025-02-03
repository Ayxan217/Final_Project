﻿using FinalProject.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Persistence.Configurations
{
    internal class DoctorConfigurations : IEntityTypeConfiguration<Doctor>
    {
        public void Configure(EntityTypeBuilder<Doctor> builder)
        {
            builder.Property(x=> x.Name).IsRequired().HasColumnType("nvarchar(100)");
            builder.Property(x => x.Surname).IsRequired().HasColumnType("nvarchar(100)");
            builder.Property(x => x.Description).IsRequired().HasColumnType("nvarchar(3000)");

        }
    }
}
