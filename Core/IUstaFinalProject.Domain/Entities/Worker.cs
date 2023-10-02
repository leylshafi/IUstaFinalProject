using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IUstaFinalProject.Domain.Entities
{
    public class Worker : User
    {
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
