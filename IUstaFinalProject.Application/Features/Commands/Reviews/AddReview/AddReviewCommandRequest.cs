using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IUstaFinalProject.Application.Features.Commands.Reviews.AddReview
{
    public class AddReviewCommandRequest : IRequest<AddReviewCommandResponse>
    {
        public Guid WorkerId { get; set; }
        public Guid CustomerId { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
    }
}
