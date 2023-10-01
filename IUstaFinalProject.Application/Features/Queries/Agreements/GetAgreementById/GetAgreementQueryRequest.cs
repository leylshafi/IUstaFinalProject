using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IUstaFinalProject.Application.Features.Queries.Agreements.GetAgreementById
{
    public class GetAgreementQueryRequest:IRequest<GetAgreementQueryResponse>
    {
        public Guid Id { get; set; }
    }
}
