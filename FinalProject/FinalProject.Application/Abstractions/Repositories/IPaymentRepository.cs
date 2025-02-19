using FinalProject.Application.Abstractions.Repositories.Generic;
using FinalProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Application.Abstractions.Repositories
{
    public interface IPaymentRepository : IRepository<Payment>
    {
    }
}
