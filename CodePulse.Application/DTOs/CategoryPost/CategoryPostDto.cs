using System;
using System.Collections.Generic;
using System.Text;

namespace CodePulse.Application.DTOs.CategoryPost
{
    /// <summary>
    /// class representing a data transfer object (DTO) for a blog category, used for as the response DTO.
    /// </summary>
    public class CategoryPostDto : BaseDto
    {
        public required string Name { get; set; } = string.Empty;
        public required string UrlHandle { get; set; } = string.Empty;
        public int PostCount { get; set; }            

    }
}
