using IUstaFinalProject.Application.Features.Queries.Agreements.GetAllAgreements;
using IUstaFinalProject.Application.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IUstaFinalProject.Application.Features.Queries.Categories.GetAllCategories
{
    public class GetCategoriesQueryHandler : IRequestHandler<GetCategoriesQueryRequest, GetCategoriesQueryResponse>
    {
        private readonly ICategoryReadRepository repository;

        public GetCategoriesQueryHandler(ICategoryReadRepository repository)
        {
            this.repository = repository;
        }
        public async Task<GetCategoriesQueryResponse> Handle(GetCategoriesQueryRequest request, CancellationToken cancellationToken)

        {
            var categories = repository.GetAll(tracking: false);
            var totalCount = categories.Count();

            var seleceted = categories
                        .OrderBy(p => p.CreatedDate)
                        .Skip(request.Size * request.Page)
                        .Take(request.Size)
                        .Select(p => new
                        {
                            p.CategoryName
                        });

            return new() { Categories = seleceted, TotalCount = totalCount };
        }
    }
}
