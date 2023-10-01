using IUstaFinalProject.Application.Features.Queries.Agreements.GetAgreementById;
using IUstaFinalProject.Application.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IUstaFinalProject.Application.Features.Queries.Categories.GetCategoryById
{
    public class GetCategoryQueryHandler : IRequestHandler<GetCategoryQueryRequest, GetCategoryQueryResponse>
    {
        private readonly ICategoryReadRepository repository;

        public GetCategoryQueryHandler(ICategoryReadRepository repository)
        {
            this.repository = repository;
        }
        public async Task<GetCategoryQueryResponse> Handle(GetCategoryQueryRequest request, CancellationToken cancellationToken)
        {
            var category = await repository.GetByIdAsync(request.Id.ToString());

            return new GetCategoryQueryResponse() { Category = category };
        }
    }
}
