using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IUstaFinalProject.Application.Features.Commands.Agreements.UpdateCategory
{
    public class UpdateAgreementCommandRequest:IRequest<UpdateAgreementCommandResponse>
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public Guid WorkerId { get; set; }
        public string AgreementText { get; set; }
    }
}
