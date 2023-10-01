using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IUstaFinalProject.Application.Features.Queries.Payments.GetAllPayments
{
    public class GetPaymentsQueryResponse
    {
        public int TotalCount { get; set; }
        public object Payments { get; set; }
    }
}
