using AutoMapper;
using CodePulse.Application.DTOs.CategoryPost;
using CodePulse.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodePulse.Application.Mappers
{
    public class CategoryPostMappingProfile : Profile
    {
        public CategoryPostMappingProfile()
        {
            // CreateCategoryPostDto → BlogCategory entity
            CreateMap<CreateCategoryPostDto, BlogCategory>();

            // UpdateCategoryPostDto → BlogCategory entity
            CreateMap<UpdateCategoryPostDto, BlogCategory>();

            // BlogCategory entity → BlogCategoryDto (response)
            CreateMap<BlogCategory, CategoryPostDto>();
        }
    }
}