using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IUstaFinalProject.Application.Features.Queries.Reviews.GetReviewById
{
    public class GetReviewQueryRequest:IRequest<GetReviewQueryResponse>
    {
        public Guid Id { get; set; }
    }
}
