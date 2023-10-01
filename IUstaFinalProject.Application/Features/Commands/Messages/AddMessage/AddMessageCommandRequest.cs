using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IUstaFinalProject.Application.Features.Commands.Messages.AddMessage
{
    public class AddMessageCommandRequest : IRequest<AddMessageCommandResponse>
    {
        public Guid SenderId { get; set; }
        public Guid ReceiverId { get; set; }
        public string Content { get; set; }
        public bool Read { get; set; }
    }
}
