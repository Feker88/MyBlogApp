using System;
using System.Collections.Generic;
using System.Text;

namespace CodePulse.Application.DTOs.BlogPost
{
    /// <summary>
    /// class representing a data transfer object (DTO) for a blog post summary, 
    /// used for as the response DTO when listing blog posts, containing only summary information without the full content of the post.
    /// </summary>
    public class BlogPostSummaryDTO : BaseDto
    {
        public required string Title { get; set; }
        
        // ← summary only, no Content
        public required string ShortDescription { get; set; } 
        public required string UrlHandle { get; set; }
        public string? FeatureImageUrl { get; set; }
        public required string Author { get; set; }
        public bool IsVisible { get; set; }
        public Guid CategoryId { get; set; }          
        public string CategoryName { get; set; } = string.Empty; 

    }
}
