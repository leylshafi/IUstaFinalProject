using IUstaFinalProject.Application.Features.Commands.Agreements.UpdateCategory;
using IUstaFinalProject.Application.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IUstaFinalProject.Application.Features.Commands.Categories.UpdateCategory
{
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommandRequest, UpdateCategoryCommandResponse>
    {
        private readonly ICategoryWriteRepository repository;
        private readonly ICategoryReadRepository ReadRepository;

        public UpdateCategoryCommandHandler(ICategoryWriteRepository repository, ICategoryReadRepository readRepository)
        {
            this.repository = repository;
            ReadRepository = readRepository;
        }

        public async Task<UpdateCategoryCommandResponse> Handle(UpdateCategoryCommandRequest request, CancellationToken cancellationToken)
        {
            var category = await ReadRepository.GetByIdAsync(request.Id.ToString());

            category.CategoryName = request.CategoryName;
            category.UpdatedTime= DateTime.UtcNow;

            repository.Update(category);
            await repository.SaveAsync();

            return new();
        }
    }
}
