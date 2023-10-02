using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IUstaFinalProject.Domain.Entities.Dtos
{
    public class WorkerDto:UserDto
    {
        public string Name { get; set; }
        public string Category { get; set; }
    }
}
