using System;
using System.Collections.Generic;
using System.Text;

namespace CodePulse.Application.DTOs.CategoryPost
{
    public class CategoryPostDto : BaseDto
    {
        public required string Name { get; set; } = string.Empty;
        public required string UrlHandle { get; set; } = string.Empty;

    }
}
