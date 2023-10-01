using IUstaFinalProject.Application.Features.Queries.Payments.GetPaymentById;
using IUstaFinalProject.Application.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IUstaFinalProject.Application.Features.Queries.Reviews.GetReviewById
{
    public class GetReviewQueryHandler : IRequestHandler<GetReviewQueryRequest, GetReviewQueryResponse>
    {
        private readonly IReviewReadRepository repository;

        public GetReviewQueryHandler(IReviewReadRepository repository)
        {
            this.repository = repository;
        }
        public async Task<GetReviewQueryResponse> Handle(GetReviewQueryRequest request, CancellationToken cancellationToken)
        {
            var review = await repository.GetByIdAsync(request.Id.ToString());

            return new GetReviewQueryResponse() { Review = review };
        }
    }
}
