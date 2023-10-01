using IUstaFinalProject.Application.Features.Queries.Messages.GetAllMessages;
using IUstaFinalProject.Application.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IUstaFinalProject.Application.Features.Queries.Payments.GetAllPayments
{
    public class GetPaymentsQueryHandler : IRequestHandler<GetPaymentsQueryRequest, GetPaymentsQueryResponse>
    {
        private readonly IPaymentReadRepository repository;

        public GetPaymentsQueryHandler(IPaymentReadRepository repository)
        {
            this.repository = repository;
        }
        public async Task<GetPaymentsQueryResponse> Handle(GetPaymentsQueryRequest request, CancellationToken cancellationToken)
        {
            var payments = repository.GetAll(tracking: false);
            var totalCount = payments.Count();

            var seleceted = payments
                        .OrderBy(p => p.CreatedDate)
                        .Skip(request.Size * request.Page)
                        .Take(request.Size)
                        .Select(p => new
                        {
                            p.WorkerId,
                            p.CustomerId,
                            p.Amount,
                            p.Payed,
                        });

            return new() { Payments = seleceted, TotalCount = totalCount };
        }
    }
}
