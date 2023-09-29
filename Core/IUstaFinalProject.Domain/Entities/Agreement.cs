using IUstaFinalProject.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IUstaFinalProject.Domain.Entities
{
    public class Agreement:BaseEntity
    {
        public Guid CustomerId { get; set; }
        public Guid WorkerId { get; set; }
        public string AgreementText { get; set; }
    }
}
