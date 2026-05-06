using System;
using System.Collections.Generic;
using System.Text;

namespace CodePulse.Application.DTOs.BlogPost
{
    public  class BlogPostDTO : BaseDto
    {

    
        public required  string Title { get; set; }
        public required  string Content { get; set; }  
        public required string ShortDescription { get; set; }
        public required string UrlHandle { get; set; }
        public string? FeatureImageUrl { get; set; }
        public required string Author { get; set; }
        public  bool IsVisible { get; set; }

    }
}
