using IUstaFinalProject.Application.Features.Queries.Payments.GetAllPayments;
using IUstaFinalProject.Application.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IUstaFinalProject.Application.Features.Queries.Reviews.GetAllReviews
{
    public class GetReviewsQueryHandler : IRequestHandler<GetReviewsQueryRequest, GetReviewsQueryResponse>
    {
        private readonly IReviewReadRepository repository;

        public GetReviewsQueryHandler(IReviewReadRepository repository)
        {
            this.repository = repository;
        }
        public async Task<GetReviewsQueryResponse> Handle(GetReviewsQueryRequest request, CancellationToken cancellationToken)
        {
            var reviews = repository.GetAll(tracking: false);
            var totalCount = reviews.Count();

            var seleceted = reviews
                        .OrderBy(p => p.CreatedDate)
                        .Skip(request.Size * request.Page)
                        .Take(request.Size)
                        .Select(p => new
                        {
                            p.CustomerId,
                            p.WorkerId,
                            p.Comment,
                            p.Rating
                        });

            return new() { Reviews = seleceted, TotalCount = totalCount };
        }

    }
}
