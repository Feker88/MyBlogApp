using AutoMapper;
using CodePulse.Application.DTOs.BlogPost;
using CodePulse.Application.DTOs.CategoryPost;
using CodePulse.Application.Interfaces;
using CodePulse.Domain.Entities;
using CodePulse.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CodePulse.Application.Services;

/// <summary>
/// Application service that implements use-cases related to categories and the posts within them.
/// Orchestrates repositories and the unit of work for category-post operations.
/// </summary>
public class CategoryPostService : ApplicationService, ICategoryPostService
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IBlogPostRepository _blogPostRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CategoryPostService(
        ICategoryRepository categoryRepository,
        IBlogPostRepository blogPostRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
        _blogPostRepository = blogPostRepository ?? throw new ArgumentNullException(nameof(blogPostRepository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _mapper= mapper?? throw new ArgumentNullException(nameof(mapper));
    }

    // Create a new category
    public async Task<CategoryPostDto> CreateCategoryAsync(CreateCategoryPostDto category)
    {        
        var entity = _mapper.Map<BlogCategory>(category);
        var created = await _categoryRepository.AddAsync(entity);
        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<CategoryPostDto>(created);
    }

    // Get category by id
    public async Task<CategoryPostDto?> GetCategoryByIdAsync(Guid id)
    {
        var entity = _categoryRepository.GetByIdAsync(id);
        return _mapper.Map<CategoryPostDto>(entity);
    }

    // Get category by url handle
    public  async Task<CategoryPostDto?> GetCategoryByUrlHandleAsync(string urlHandle)
    {
        var entity = _categoryRepository.GetByUrlHandleAsync(urlHandle);
        return _mapper.Map<CategoryPostDto>(entity);
    }
        

    // Get all categories
    public async  Task<IEnumerable<CategoryPostDto>> GetAllCategoriesAsync()
    {
        var entities = _categoryRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<CategoryPostDto>>(entities);
    }
    

   
    // Update category
    public async Task<CategoryPostDto> UpdateCategoryAsync(CreateCategoryPostDto category)
    {

        var entity = _mapper.Map<BlogCategory>(category);

        var updated= await _categoryRepository.UpdateAsync(entity);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<CategoryPostDto>(updated);
    }

    // Delete category
    public async Task<bool> DeleteCategoryAsync(Guid id)
    {
        var result = await _categoryRepository.DeleteAsync(id);
        if (result)
            await _unitOfWork.SaveChangesAsync();
        return result;
    }

    // Search categories by name
    public async  Task<IEnumerable<CategoryPostDto>> SearchCategoriesAsync(string searchTerm)
    {
            var entities = _categoryRepository.SearchByNameAsync(searchTerm);
        return _mapper.Map<IEnumerable<CategoryPostDto>>(entities);
    }

    // Check if url handle exists
    public  Task<bool> CategoryUrlHandleExistsAsync(string urlHandle) => _categoryRepository.ExistsByUrlHandleAsync(urlHandle);

    // Check if name exists
    public  Task<bool> CategoryNameExistsAsync(string name) => _categoryRepository.ExistsByNameAsync(name);
}
