using FinalProject.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Domain.Entities
{
    public class Payroll : BaseEntity
    {
        
        public decimal Salary { get; set; }
        public decimal Taxes { get; set; }
        public decimal NetSalary { get; set; }

        // Relational 
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }
    }
}
