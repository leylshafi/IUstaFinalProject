using IUstaFinalProject.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IUstaFinalProject.Domain.Entities
{
    public class Category:BaseEntity
    {
        public string CategoryName { get; set; }
        public ICollection<Worker> Workers { get; set; }
    }
}
