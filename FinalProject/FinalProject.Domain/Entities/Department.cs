using FinalProject.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Domain.Entities
{
    public  class Department : BaseNameableEntity
    {
        public int? ChiefDoctorId { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }
        public ICollection<Doctor>? Doctors { get; set; }
    }
}
