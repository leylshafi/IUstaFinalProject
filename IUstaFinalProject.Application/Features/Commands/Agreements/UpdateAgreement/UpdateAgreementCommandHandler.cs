using IUstaFinalProject.Application.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IUstaFinalProject.Application.Features.Commands.Agreements.UpdateCategory
{
    public class UpdateAgreementCommandHandler : IRequestHandler<UpdateAgreementCommandRequest, UpdateAgreementCommandResponse>
    {
        private readonly IAgreementWriteRepository repository;
        private readonly IAgreementReadRepository ReadRepository;

        public UpdateAgreementCommandHandler(IAgreementWriteRepository repository, IAgreementReadRepository readRepository)
        {
            this.repository = repository;
            ReadRepository = readRepository;
        }

        async Task<UpdateAgreementCommandResponse> IRequestHandler<UpdateAgreementCommandRequest, UpdateAgreementCommandResponse>.Handle(UpdateAgreementCommandRequest request, CancellationToken cancellationToken)
        {
            var agreement = await ReadRepository.GetByIdAsync(request.Id.ToString());

            agreement.AgreementText = request.AgreementText;
            agreement.CustomerId = request.CustomerId;
            agreement.WorkerId = request.WorkerId;
            agreement.UpdatedTime= DateTime.UtcNow;

            repository.Update(agreement);
            await repository.SaveAsync();

            return new();
        }
    }
}
