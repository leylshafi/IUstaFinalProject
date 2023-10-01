using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IUstaFinalProject.Application.Features.Queries.Payments.GetPaymentById
{
    public class GetPaymentQueryRequest:IRequest<GetPaymentQueryResponse>
    {
        public Guid Id { get; set; }
    }
}
