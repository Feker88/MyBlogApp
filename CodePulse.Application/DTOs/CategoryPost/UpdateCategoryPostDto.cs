using System;
using System.Collections.Generic;
using System.Text;

namespace CodePulse.Application.DTOs.CategoryPost
{
    /// <summary>
    /// class representing a data transfer object (DTO) for updating an existing blog category, used for the request DTO when updating a category.
    /// </summary>
    public class UpdateCategoryPostDto : BaseDto
    {
        public required string Name { get; set; } = string.Empty;
        
    }
}
