using IUstaFinalProject.Domain.Entities.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IUstaFinalProject.Domain.Entities
{
    public class Review:BaseEntity
    {
        public Guid WorkerId { get; set; }
        public Guid CustomerId { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
    }
}
