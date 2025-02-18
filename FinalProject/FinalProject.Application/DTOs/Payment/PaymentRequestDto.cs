using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Application.DTOs.Payment
{
    public class PaymentRequestDto
    {
        public string Token { get; set; }
        public decimal Amount { get; set; }
        public string Currency
    }
}
