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
            ///Ignore system generated fields like Id, CreatedAt, UpdatedAt during mapping from DTO to Entity
            CreateMap<CreateBlogPostDTO, BlogPost>()
                .ForMember(dest=>dest.Id , opt=> opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());

            ///UpdatelogPostDTO --> BlogPost Entities
            /// Ignore timestamps — only Id comes from the DTO
            CreateMap<UpdateBlogPostDTO, BlogPost>()
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());

            ///BlogPost ENtities --> BlogPostDto FUll response 
            CreateMap<BlogPost, BlogPostDTO>();

            ///BlogPost ENtities --> BlogPostDto light response 
            CreateMap<BlogPost, BlogPostSummaryDTO>();

        }
    }
}
