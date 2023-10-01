using IUstaFinalProject.Application.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IUstaFinalProject.Application.Features.Queries.Agreements.GetAllAgreements
{
    public class GetAgreementsQueryHandler:IRequestHandler<GetAgreeentsQueryRequest, GetAgreementsQueryResponse>
    {
        private readonly IAgreementReadRepository repository;

        public GetAgreementsQueryHandler(IAgreementReadRepository repository)
        {
            this.repository = repository;
        }
        public async Task<GetAgreementsQueryResponse> Handle(GetAgreeentsQueryRequest request, CancellationToken cancellationToken)
        {
            var agreements = repository.GetAll(tracking: false);
            var totalCount = agreements.Count();

            var selecetedAgreements = agreements
                        .OrderBy(p => p.CreatedDate)
                        .Skip(request.Size * request.Page)
                        .Take(request.Size)
                        .Select(p => new
                        {
                            p.WorkerId,
                            p.CustomerId,
                            p.AgreementText
                        });

            return new() { Agreements = selecetedAgreements, TotalCount = totalCount };
        }
    }
}
