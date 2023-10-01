using IUstaFinalProject.Application.Features.Queries.Messages.GetAllMessages;
using IUstaFinalProject.Application.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IUstaFinalProject.Application.Features.Queries.Notifications.GetAllNotifications
{
    public class GetNotificationsQueryHandler : IRequestHandler<GetNotificationsQueryRequest, GetNotificationsQueryResponse>
    {
        private readonly INotificationReadRepository repository;

        public GetNotificationsQueryHandler(INotificationReadRepository repository)
        {
            this.repository = repository;
        }
        public async Task<GetNotificationsQueryResponse> Handle(GetNotificationsQueryRequest request, CancellationToken cancellationToken)
        {
            var notifications = repository.GetAll(tracking: false);
            var totalCount = notifications.Count();

            var seleceted = notifications
                        .OrderBy(p => p.CreatedDate)
                        .Skip(request.Size * request.Page)
                        .Take(request.Size)
                        .Select(p => new
                        {
                            p.CustomerId,
                            p.Content,
                            p.Read,
                        });

            return new() { Notifications = seleceted, TotalCount = totalCount };
        }
    }
}
