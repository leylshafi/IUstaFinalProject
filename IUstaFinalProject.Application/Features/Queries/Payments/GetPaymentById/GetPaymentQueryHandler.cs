using IUstaFinalProject.Application.Features.Queries.Notifications.GetNotificationById;
using IUstaFinalProject.Application.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IUstaFinalProject.Application.Features.Queries.Payments.GetPaymentById
{
    public class GetPaymentQueryHandler : IRequestHandler<GetPaymentQueryRequest, GetPaymentQueryResponse>
    {
        private readonly IPaymentReadRepository repository;

        public GetPaymentQueryHandler(IPaymentReadRepository repository)
        {
            this.repository = repository;
        }
        public async Task<GetPaymentQueryResponse> Handle(GetPaymentQueryRequest request, CancellationToken cancellationToken)
        {
            var payment = await repository.GetByIdAsync(request.Id.ToString());

            return new GetPaymentQueryResponse() { Payment = payment };
        }
    }
}
