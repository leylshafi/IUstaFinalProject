using IUstaFinalProject.Application.Repositories;
using IUstaFinalProject.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IUstaFinalProject.Application.Features.Commands.Notifications.AddNotification
{
    public class AddNotificationCommandHandler : IRequestHandler<AddNotificationCommandRequest, AddNotificationCommandResponse>
    {
        private readonly INotificationWriteRepository repository;

        public AddNotificationCommandHandler(INotificationWriteRepository repository)
        {
            this.repository = repository;
        }
        public async Task<AddNotificationCommandResponse> Handle(AddNotificationCommandRequest request, CancellationToken cancellationToken)
        {
            var notification = new Notification
            {
                Id = Guid.NewGuid(),
                CreatedDate = DateTime.Now,
                Content = request.Content,
                CustomerId = request.CustomerId,
                Read = request.Read
            };

            await repository.AddAsync(notification);
            await repository.SaveAsync();
            return new();
        }
    }
}
