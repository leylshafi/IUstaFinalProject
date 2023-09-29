using IUstaFinalProject.Application.Repositories;
using IUstaFinalProject.Domain.Entities;
using IUstaFinalProject.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IUstaFinalProject.Persistence.Repositories
{
    public class MessageReadRepository : ReadRepository<Message>, IMessageReadRepository
    {
        public MessageReadRepository(AppDbContext context) : base(context)
        {

        }
    }
}
