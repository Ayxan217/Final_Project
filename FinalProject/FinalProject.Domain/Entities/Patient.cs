using FinalProject.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Domain.Entities
{
    public class Patient : BaseNameableEntity
    {
        public string Surname { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public string Adress { get; set; }
        public string Phone {  get; set; }
        public string Email { get; set; }

        public ICollection<Appointment> Appointments { get; set; }


    }
}
