using FinalProject.Domain.Entites;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Domain.Entities
{
    public class Doctor : BaseNameableEntity
    {
        public string Surname { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateOnly JoinDate { get; set; }
        public string Description { get; set; }
        public string? Twitter {  get; set; }
        public string? Skype { get; set; }
        public string? Facebook { get; set; }
        public string? Ven {  get; set; }

       //Relational Properties
        public Payroll Payroll { get; set; }
        public int DepartmentId { get; set; }
        public Department Department { get; set; }
        public ICollection<Comment>? Comments { get; set; }
       

    }
}
