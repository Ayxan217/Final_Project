using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Application.DTOs.Payment
{
  public record ConfirmPaymentDto(string PaymentIntentId,string PaymentMethodId);
}
