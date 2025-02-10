using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Domain.Entities
{
    public class AppUser : IdentityUser
    {
        
        public string Name { get; set; }
        public string Surname { get; set; }
        public bool IsActive { get; set; }
        public string? ResetCode { get; set; }
        public DateTime? ResetCodeExpireTime { get; set; }

        public ICollection<Comment>? Comments { get; set; }
    
    }
}
