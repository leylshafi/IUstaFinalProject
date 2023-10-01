using IUstaFinalProject.Application.Features.Queries.Agreements.GetAllAgreements;
using IUstaFinalProject.Application.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IUstaFinalProject.Application.Features.Queries.Messages.GetAllMessages
{
    public class GetMessagesQueryHandler : IRequestHandler<GetMessagesQueryRequest, GetMessagesQueryResponse>
    {
        private readonly IMessageReadRepository repository;

        public GetMessagesQueryHandler(IMessageReadRepository repository)
        {
            this.repository = repository;
        }
        public async Task<GetMessagesQueryResponse> Handle(GetMessagesQueryRequest request, CancellationToken cancellationToken)
        {
            var messages = repository.GetAll(tracking: false);
            var totalCount = messages.Count();

            var seleceted = messages
                        .OrderBy(p => p.CreatedDate)
                        .Skip(request.Size * request.Page)
                        .Take(request.Size)
                        .Select(p => new
                        {
                            p.SenderId,
                            p.ReceiverId,
                            p.Content,
                            p.Read
                        });

            return new() { Messages = seleceted, TotalCount = totalCount };
        }
    }
}
