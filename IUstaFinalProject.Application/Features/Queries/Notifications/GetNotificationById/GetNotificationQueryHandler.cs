using IUstaFinalProject.Application.Features.Queries.Messages.GetMessageById;
using IUstaFinalProject.Application.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IUstaFinalProject.Application.Features.Queries.Notifications.GetNotificationById
{
    public class GetNotificationQueryHandler : IRequestHandler<GetNotificationQueryRequest, GetNotificationQueryResponse>
    {
        private readonly INotificationReadRepository repository;

        public GetNotificationQueryHandler(INotificationReadRepository repository)
        {
            this.repository = repository;
        }
        public async Task<GetNotificationQueryResponse> Handle(GetNotificationQueryRequest request, CancellationToken cancellationToken)
        {
            var notification = await repository.GetByIdAsync(request.Id.ToString());

            return new GetNotificationQueryResponse() { Notification = notification };
        }
    }
}
