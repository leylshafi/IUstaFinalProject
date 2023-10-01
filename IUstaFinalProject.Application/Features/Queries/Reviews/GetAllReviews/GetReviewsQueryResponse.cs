using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IUstaFinalProject.Application.Features.Queries.Reviews.GetAllReviews
{
    public class GetReviewsQueryResponse
    {
        public int TotalCount { get; set; }
        public object Reviews { get; set; }
    }
}
