using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IUstaFinalProject.Domain.Entities.Dtos
{
    public class NotificationDto
    {
        public Guid CustomerId { get; set; }
        public string Content { get; set; }
        public bool Read { get; set; }
    }
}
