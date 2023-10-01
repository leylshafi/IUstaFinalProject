using IUstaFinalProject.Application.Features.Queries.Categories.GetCategoryById;
using IUstaFinalProject.Application.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IUstaFinalProject.Application.Features.Queries.Messages.GetMessageById
{
    public class GetMessageQueryHandler : IRequestHandler<GetMessageQueryRequest, GetMessageQueryResponse>
    {
        private readonly IMessageReadRepository repository;

        public GetMessageQueryHandler(IMessageReadRepository repository)
        {
            this.repository = repository;
        }
        public async Task<GetMessageQueryResponse> Handle(GetMessageQueryRequest request, CancellationToken cancellationToken)
        {
            var message = await repository.GetByIdAsync(request.Id.ToString());

            return new GetMessageQueryResponse() { Message = message };
        }
    }
}
