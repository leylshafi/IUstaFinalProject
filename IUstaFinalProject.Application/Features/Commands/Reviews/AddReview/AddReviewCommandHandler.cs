using IUstaFinalProject.Application.Repositories;
using IUstaFinalProject.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IUstaFinalProject.Application.Features.Commands.Reviews.AddReview
{
    public class AddReviewCommandHandler : IRequestHandler<AddReviewCommandRequest, AddReviewCommandResponse>
    {
        private readonly IReviewWriteRepository repository;

        public AddReviewCommandHandler(IReviewWriteRepository repository)
        {
            this.repository = repository;
        }
        public async Task<AddReviewCommandResponse> Handle(AddReviewCommandRequest request, CancellationToken cancellationToken)
        {
            var review = new Review
            {
                Id = Guid.NewGuid(),
                CreatedDate = DateTime.Now,
                Comment = request.Comment,
                CustomerId = Guid.NewGuid(),
                Rating = request.Rating,
                WorkerId = request.WorkerId,
            };

            await repository.AddAsync(review);
            await repository.SaveAsync();
            return new();
        }
    }
}
