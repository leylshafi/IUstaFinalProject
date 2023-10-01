using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IUstaFinalProject.Application.Features.Commands.Categories.AddCategory
{
    public class AddCategoryCommandRequest : IRequest<AddCategoryCommandResponse>
    {
        public string CategoryName { get; set; }
    }
}
