using AutoMapper;
using CodePulse.Application.DTOs.BlogPost;
using CodePulse.Domain.Entities;


namespace CodePulse.Application.Mappers
{
    public  class BlogPostMappingProfile : Profile
    {
        public BlogPostMappingProfile() 
        {

            ///CreateBlogPostDTO --> BlogPost Entities
            CreateMap<CreateBlogPostDTO, BlogPost>();

            ///UpdatelogPostDTO --> BlogPost Entities
            CreateMap<UpdateBlogPostDTO, BlogPost>();

            ///BlogPost ENtities --> BlogPostDto FUll response 
            CreateMap<BlogPost, BlogPostDTO>();

            ///BlogPost ENtities --> BlogPostDto light response 
            CreateMap<BlogPost, BlogPostSummaryDTO>();

        }
    }
}
