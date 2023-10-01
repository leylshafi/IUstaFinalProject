using IUstaFinalProject.Application.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IUstaFinalProject.Application.Features.Commands.Categories.RemoveCategory
{
    public class RemoveCategoryCommandHandler:IRequestHandler<RemoveCategoryCommandRequest, RemoveCategoryCommandResponse>
    {
        private readonly ICategoryWriteRepository repository;

        public RemoveCategoryCommandHandler(ICategoryWriteRepository repository)
        {
            this.repository = repository;
        }

        public async Task<RemoveCategoryCommandResponse> Handle(RemoveCategoryCommandRequest request, CancellationToken cancellationToken)
        {
            await repository.RemoveAsync(request.CategoryId.ToString());
            return new();
        }
    }
}
