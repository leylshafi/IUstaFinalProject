using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IUstaFinalProject.Application.Features.Queries.Messages.GetAllMessages
{
    public class GetMessagesQueryResponse
    {
        public int TotalCount { get; set; }
        public object Messages { get; set; }
    }
}
