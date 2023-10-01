using IUstaFinalProject.Application.Repositories;
using IUstaFinalProject.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IUstaFinalProject.Application.Features.Commands.Agreements.AddAgreement
{
    public class AddAgreementCommandHandler : IRequestHandler<AddAgreementCommandRequest, AddAgreementCommandResponse>
    {
        private readonly IAgreementWriteRepository repository;

        public AddAgreementCommandHandler(IAgreementWriteRepository repository)
        {
            this.repository = repository;
        }
        public async Task<AddAgreementCommandResponse> Handle(AddAgreementCommandRequest request, CancellationToken cancellationToken)
        {
            var agreement = new Agreement
            {
                Id = Guid.NewGuid(),
                CreatedDate = DateTime.Now,
                AgreementText = request.AgreementText,
                 CustomerId= request.CustomerId,
                 WorkerId= request.WorkerId,
            };

            await repository.AddAsync(agreement);
            await repository.SaveAsync();
            return new();
        }
    }
}
