using IUstaFinalProject.Application.Repositories;
using IUstaFinalProject.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IUstaFinalProject.Application.Features.Commands.Messages.AddMessage
{
    public class AddMessageCommandHandler : IRequestHandler<AddMessageCommandRequest, AddMessageCommandResponse>
    {
        private readonly IMessageWriteRepository repository;

        public AddMessageCommandHandler(IMessageWriteRepository repository)
        {
            this.repository = repository;
        }
        public async Task<AddMessageCommandResponse> Handle(AddMessageCommandRequest request, CancellationToken cancellationToken)
        {
            var message = new Message
            {
                Id = Guid.NewGuid(),
                CreatedDate = DateTime.Now,
                Content = request.Content,
                Read = request.Read,
                ReceiverId = request.ReceiverId,
                SenderId = request.SenderId
            };

            await repository.AddAsync(message);
            await repository.SaveAsync();
            return new();
        }
    }
}
