using IUstaFinalProject.Application.Repositories;
using IUstaFinalProject.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IUstaFinalProject.Application.Features.Commands.Payments.AddPayment
{
    public class AddPaymentCommandHandler : IRequestHandler<AddPaymentCommandRequest, AddPaymentCommandResponse>
    {
        private readonly IPaymentWriteRepository repository;

        public AddPaymentCommandHandler(IPaymentWriteRepository repository)
        {
            this.repository = repository;
        }
        public async Task<AddPaymentCommandResponse> Handle(AddPaymentCommandRequest request, CancellationToken cancellationToken)
        {
            var payment = new Payment
            {
                Id = Guid.NewGuid(),
                CreatedDate = DateTime.Now,
                CustomerId = request.CustomerId,
                Amount = request.Amount,
                Payed = request.Payed,
                WorkerId = request.WorkerId
            };

            await repository.AddAsync(payment);
            await repository.SaveAsync();
            return new();
        }
    }
}
