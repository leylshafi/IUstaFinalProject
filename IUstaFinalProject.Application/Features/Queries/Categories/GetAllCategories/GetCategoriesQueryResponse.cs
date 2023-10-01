using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IUstaFinalProject.Application.Features.Queries.Categories.GetAllCategories
{
    public class GetCategoriesQueryResponse
    {
        public int TotalCount { get; set; }
        public object Categories { get; set; }
    }
}
