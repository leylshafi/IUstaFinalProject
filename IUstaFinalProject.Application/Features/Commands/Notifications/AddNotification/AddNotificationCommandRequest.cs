using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IUstaFinalProject.Application.Features.Commands.Notifications.AddNotification
{
    public class AddNotificationCommandRequest : IRequest<AddNotificationCommandResponse>
    {
        public Guid CustomerId { get; set; }
        public string Content { get; set; }
        public bool Read { get; set; }
    }
}
