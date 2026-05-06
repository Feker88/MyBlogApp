using System;
using System.Collections.Generic;
using System.Text;

namespace CodePulse.Application.DTOs.CategoryPost
{
    public class UpdateCategoryPostDto : BaseDto
    {
        public required string Name { get; set; } = string.Empty;
        
    }
}
