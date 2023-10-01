using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IUstaFinalProject.Application.Features.Queries.Messages.GetMessageById
{
    public class GetMessageQueryRequest:IRequest<GetMessageQueryResponse>
    {
        public Guid Id { get; set; }
    }
}
