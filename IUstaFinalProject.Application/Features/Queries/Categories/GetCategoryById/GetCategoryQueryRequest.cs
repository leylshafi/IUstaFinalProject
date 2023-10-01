using MediatR;

namespace IUstaFinalProject.Application.Features.Queries.Categories.GetCategoryById
{
    public class GetCategoryQueryRequest:IRequest<GetCategoryQueryResponse>
    {
        public Guid Id { get; set; }
    }
}
