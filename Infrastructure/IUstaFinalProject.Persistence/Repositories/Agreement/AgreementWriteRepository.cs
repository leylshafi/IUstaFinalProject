using IUstaFinalProject.Application.Repositories;
using IUstaFinalProject.Domain.Entities;
using IUstaFinalProject.Persistence.Contexts;

namespace IUstaFinalProject.Persistence.Repositories;
public class AgreementWriteRepository : WriteRepository<Agreement>, IAgreementWriteRepository
{
    public AgreementWriteRepository(AppDbContext context) : base(context)
    {

    }
}

