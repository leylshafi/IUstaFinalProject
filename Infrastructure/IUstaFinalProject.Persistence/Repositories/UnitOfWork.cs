using IUstaFinalProject.Application.Repositories;
using IUstaFinalProject.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IUstaFinalProject.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly AppDbContext _context;

        public UnitOfWork(AppDbContext context)
        {
            this._context = context;
        }
        public ICustomerReadRepository CustomerReadRepository => new CustomerReadRepository(_context);

        public ICustomerWriteRepository CustomerWriteRepository => new CustomerWriteRepository(_context);



        public IWorkerReadRepository WorkerReadRepository => new WorkerReadRepository(_context);

        public IWorkerWriteRepository WorkerWriteRepository => new WorkerWriteRepository(_context);


        public IAgreementReadRepository AgreementReadRepository => new AgreementReadRepository(_context);
        public IAgreementWriteRepository AgreementWriteRepository => new AgreementWriteRepository(_context);


        public ICategoryReadRepository CategoryReadRepository => new CategoryReadRepository(_context);

        public ICategoryWriteRepository CategoryWriteRepository => new CategoryWriteRepository(_context);


        public IMessageReadRepository MessageReadRepository => new MessageReadRepository(_context);

        public IMessageWriteRepository MessageWriteRepository => new MessageWriteRepository(_context);



        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
