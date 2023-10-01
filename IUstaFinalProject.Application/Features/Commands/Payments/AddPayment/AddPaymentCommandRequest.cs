using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IUstaFinalProject.Application.Features.Commands.Payments.AddPayment
{
    public class AddPaymentCommandRequest : IRequest<AddPaymentCommandResponse>
    {
        public Guid CustomerId { get; set; }
        public Guid WorkerId { get; set; }
        public decimal Amount { get; set; }
        public bool Payed { get; set; }
    }
}
