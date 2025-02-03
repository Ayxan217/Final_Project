using FinalProject.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Persistence.Configurations
{
    internal class PayrollConfiguration : IEntityTypeConfiguration<Payroll>
    {
        public void Configure(EntityTypeBuilder<Payroll> builder)
        {
            builder.Property(x=>x.Salary).HasColumnType("decimal(8,2)");
            builder.Property(x => x.NetSalary).HasColumnType("decimal(8,2)");
            builder.Property(x => x.Taxes).HasColumnType("decimal(8,2)");
        }
    }
}
