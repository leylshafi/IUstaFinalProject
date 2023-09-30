using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IUstaFinalProject.Domain.Entities.Dtos
{
    public class PaymentDto
    {
        public Guid CustomerId { get; set; }
        public Guid WorkerId { get; set; }
        public decimal Amount { get; set; }
        public bool Payed { get; set; }
    }
}
