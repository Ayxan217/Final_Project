using FinalProject.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Persistence.Contexts
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {

        //public AppDbContext() { }
       
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { } 
       

        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Payroll> Payrolls { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Patient> Patients { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=LAPTOP-04DP7Q7I\\SQLEXPRESS;database=FinalProjectDB;trusted_connection=true;TrustServerCertificate=true"
  );
            base.OnConfiguring(optionsBuilder);
        }
    }
}
