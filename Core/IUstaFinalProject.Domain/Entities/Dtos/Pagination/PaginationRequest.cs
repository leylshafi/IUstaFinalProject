﻿using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace IUstaFinalProject.Domain.Entities.Dtos.Pagination
{
    public class PaginationRequest
    {
        [Range(0, int.MaxValue)]
        [FromQuery(Name = "page")]
        [Required]
        public int Page { get; set; } = 1;

        [Range(0, int.MaxValue)]
        [FromQuery(Name = "pageSize")]
        [Required]
        public int PageSize { get; set; } = 10;
    }
}
