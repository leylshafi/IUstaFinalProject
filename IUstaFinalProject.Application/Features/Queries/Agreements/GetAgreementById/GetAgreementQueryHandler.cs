using IUstaFinalProject.Application.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IUstaFinalProject.Application.Features.Queries.Agreements.GetAgreementById
{
    public class GetAgreementQueryHandler : IRequestHandler<GetAgreementQueryRequest, GetAgreementQueryResponse>
    {
        private readonly IAgreementReadRepository repository;

        public GetAgreementQueryHandler(IAgreementReadRepository repository)
        {
            this.repository = repository;
        }
        public async Task<GetAgreementQueryResponse> Handle(GetAgreementQueryRequest request, CancellationToken cancellationToken)
        {
            var agreement = await repository.GetByIdAsync(request.Id.ToString());

            return new GetAgreementQueryResponse() { Agreement = agreement };
        }
    }
}
