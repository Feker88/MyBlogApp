using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CodePulse.Application.Interfaces;
using CodePulse.Domain.Entities;
using CodePulse.Domain.Repositories;

namespace CodePulse.Application.Services;

/// <summary>
/// Application service that implements use-cases related to categories and the posts within them.
/// Orchestrates repositories and the unit of work for category-post operations.
/// </summary>
public class CategoryPostService : ApplicationService
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IBlogPostRepository _blogPostRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CategoryPostService(
        ICategoryRepository categoryRepository,
        IBlogPostRepository blogPostRepository,
        IUnitOfWork unitOfWork)
    {
        _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
        _blogPostRepository = blogPostRepository ?? throw new ArgumentNullException(nameof(blogPostRepository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    // Create a new category
    public async Task<BlogCategory> CreateCategoryAsync(BlogCategory category)
    {
        var created = await _categoryRepository.AddAsync(category);
        await _unitOfWork.SaveChangesAsync();
        return created;
    }

    // Get category by id
    public Task<BlogCategory?> GetCategoryByIdAsync(Guid id) => _categoryRepository.GetByIdAsync(id);

    // Get category by url handle
    public Task<BlogCategory?> GetCategoryByUrlHandleAsync(string urlHandle) => _categoryRepository.GetByUrlHandleAsync(urlHandle);

    // Get all categories
    public Task<IEnumerable<BlogCategory>> GetAllCategoriesAsync() => _categoryRepository.GetAllAsync();

   

    // Update category
    public async Task<BlogCategory> UpdateCategoryAsync(BlogCategory category)
    {
        var updated = await _categoryRepository.UpdateAsync(category);
        await _unitOfWork.SaveChangesAsync();
        return updated;
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
    public Task<IEnumerable<BlogCategory>> SearchCategoriesAsync(string searchTerm) => _categoryRepository.SearchByNameAsync(searchTerm);

    // Check if url handle exists
    public Task<bool> CategoryUrlHandleExistsAsync(string urlHandle) => _categoryRepository.ExistsByUrlHandleAsync(urlHandle);

    // Check if name exists
    public Task<bool> CategoryNameExistsAsync(string name) => _categoryRepository.ExistsByNameAsync(name);
}
