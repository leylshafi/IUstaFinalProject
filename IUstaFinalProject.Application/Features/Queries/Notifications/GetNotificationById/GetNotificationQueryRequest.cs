using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IUstaFinalProject.Application.Features.Queries.Notifications.GetNotificationById
{
    public class GetNotificationQueryRequest:IRequest<GetNotificationQueryResponse>
    {
        public Guid Id { get; set; }
    }
}
