using System;
using System.Collections.Generic;
using System.Text;

namespace CodePulse.Application.DTOs.BlogPost
{
    public class CreateBlogPostDTO : BaseDto
    {
        public required string Title { get; set; } = string.Empty;
        public required string ShortDescription { get; set; } = string.Empty;
        public required string Content { get; set; } = string.Empty;
        public required string UrlHandle { get; set; } = string.Empty;   
        public string? FeatureImageUrl { get; set; } = string.Empty;
        public required string Author { get; set; } = string.Empty;      
        public bool IsVisible { get; set; }
    }
}
