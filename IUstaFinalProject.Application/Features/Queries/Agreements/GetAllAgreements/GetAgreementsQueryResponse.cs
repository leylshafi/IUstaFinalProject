using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IUstaFinalProject.Application.Features.Queries.Agreements.GetAllAgreements
{
    public class GetAgreementsQueryResponse
    {
        public int TotalCount { get; set; }
        public object Agreements { get; set; }
    }
}
