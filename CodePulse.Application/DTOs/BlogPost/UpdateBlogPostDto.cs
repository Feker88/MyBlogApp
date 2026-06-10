using System;
using System.Collections.Generic;
using System.Text;

namespace CodePulse.Application.DTOs.BlogPost
{

    /// <summary>
    /// class representing a data transfer object (DTO) for updating an existing blog post, used for the request DTO when updating a blog post.
    /// It contains properties that can be updated for a blog post, such as Title, ShortDescription, Content, FeatureImageUrl, and IsVisible. 
    /// The UrlHandle and Author properties are not included in this DTO since they are typically not updated after the blog post is created.
    /// </summary>
    public class UpdateBlogPostDTO : BaseDto 
    {
        public required string Title { get; set; } = string.Empty;
        public required string ShortDescription { get; set; } = string.Empty;
        public required string Content { get; set; } = string.Empty;        
        public string? FeatureImageUrl { get; set; } = string.Empty;        
        public bool IsVisible { get; set; }

        public Guid CategoryId { get; set; }
    }
}
