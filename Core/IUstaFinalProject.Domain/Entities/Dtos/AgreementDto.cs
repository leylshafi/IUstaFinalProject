using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IUstaFinalProject.Domain.Entities.Dtos
{
    public class AgreementDto
    {
        public Guid CustomerId { get; set; }
        public Guid WorkerId { get; set; }
        public string AgreementText { get; set; }
    }
}
