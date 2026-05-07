using System;
using System.Collections.Generic;
using System.Text;

namespace CodePulse.Application.DTOs.CategoryPost
{
    /// <summary>
    /// class representing a data transfer object (DTO) for creating a new blog category, used for the request DTO when creating a category.
    /// </summary>
    public class CreateCategoryPostDto :BaseDto
    {
        public required string Name { get; set; } = string.Empty;
        public required string UrlHandle { get; set; } = string.Empty;
    }
}
