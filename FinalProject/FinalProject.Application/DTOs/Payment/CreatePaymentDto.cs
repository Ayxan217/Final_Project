using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Application.DTOs.Payment
{
    public record CreatePaymentDto(decimal amount,string currency,
        string CardNumber,int ExpMont,int ExpYear,string CVC);
  
}
