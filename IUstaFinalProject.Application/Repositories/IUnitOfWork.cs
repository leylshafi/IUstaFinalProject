namespace IUstaFinalProject.Application.Repositories;

public interface IUnitOfWork
{
    ICustomerReadRepository CustomerReadRepository { get; }
    ICustomerWriteRepository CustomerWriteRepository { get; }

    IAgreementReadRepository AgreementReadRepository { get; }
    IAgreementWriteRepository AgreementWriteRepository { get; }

    IWorkerReadRepository WorkerReadRepository { get; }
    IWorkerWriteRepository WorkerWriteRepository { get; }

    ICategoryReadRepository CategoryReadRepository { get; }
    ICategoryWriteRepository CategoryWriteRepository { get; }

    IMessageReadRepository MessageReadRepository { get; }
    IMessageWriteRepository MessageWriteRepository { get; }



    Task<bool> SaveChangesAsync();
}
