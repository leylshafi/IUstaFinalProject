using IUstaFinalProject.Application.Repositories;
using IUstaFinalProject.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IUstaFinalProject.Application.Features.Commands.Categories.AddCategory
{
    public class AddCategoryCommandHandler : IRequestHandler<AddCategoryCommandRequest, AddCategoryCommandResponse>
    {
        private readonly ICategoryWriteRepository repository;

        public AddCategoryCommandHandler(ICategoryWriteRepository repository)
        {
            this.repository = repository;
        }
        public async Task<AddCategoryCommandResponse> Handle(AddCategoryCommandRequest request, CancellationToken cancellationToken)
        {
            var category = new Category
            {
                Id = Guid.NewGuid(),
                CreatedDate = DateTime.Now,
                CategoryName = request.CategoryName
            };

            await repository.AddAsync(category);
            await repository.SaveAsync();
            return new();
        }
    }
}
