using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IUstaFinalProject.Application.Features.Queries.Notifications.GetAllNotifications
{
    public class GetNotificationsQueryResponse
    {
        public int TotalCount { get; set; }
        public object Notifications { get; set; }
    }
}
