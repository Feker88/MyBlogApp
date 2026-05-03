using CodePulse.Domain.Entities;

namespace CodePulse.Application.Interfaces;

/// <summary>
/// Application service contract that defines use-case oriented operations for categories and blog posts.
/// Provides methods for managing categories and querying posts within them.
/// </summary>
/// <remarks>
/// Implementations should orchestrate domain repositories, domain services and the unit of work
/// to execute application workflows. Keep domain business rules inside the domain layer and
/// use this service to coordinate use cases, validation, and transaction boundaries.
/// </remarks>
public interface ICategoryPostService
{
    /// <summary>
    /// Asynchronously creates a new blog category and persists it to the data store.
    /// </summary>
    /// <param name="category">The blog category to create. Must not be null.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the created blog category, including
    /// any updates made during persistence (such as generated identifiers and timestamps).</returns>
    Task<BlogCategory> CreateCategoryAsync(BlogCategory category);

    /// <summary>
    /// Asynchronously retrieves a blog category by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the blog category to retrieve.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the blog category if found;
    /// otherwise, null.</returns>
    Task<BlogCategory?> GetCategoryByIdAsync(Guid id);

    /// <summary>
    /// Asynchronously retrieves a blog category by its URL handle.
    /// </summary>
    /// <param name="urlHandle">The URL-friendly identifier for the category.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the blog category if found;
    /// otherwise, null.</returns>
    Task<BlogCategory?> GetCategoryByUrlHandleAsync(string urlHandle);

    /// <summary>
    /// Asynchronously retrieves all blog categories from the data store.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains a collection of all blog categories.
    /// The collection is empty if no categories are found.</returns>
    Task<IEnumerable<BlogCategory>> GetAllCategoriesAsync();

    /// <summary>
    /// Asynchronously updates an existing blog category and persists the changes to the data store.
    /// </summary>
    /// <param name="category">The blog category entity containing the updated values. Must not be null.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the updated blog category entity as
    /// stored in the data store.</returns>
    Task<BlogCategory> UpdateCategoryAsync(BlogCategory category);

    /// <summary>
    /// Asynchronously deletes a blog category identified by the specified unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the blog category to delete.</param>
    /// <returns>A task that represents the asynchronous operation. The task result is <see langword="true"/> if the blog
    /// category was successfully deleted; otherwise, <see langword="false"/>.</returns>
    Task<bool> DeleteCategoryAsync(Guid id);

    /// <summary>
    /// Asynchronously searches for blog categories by name using a partial match.
    /// </summary>
    /// <param name="searchTerm">The search term to look for in category names. The search is case-insensitive.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a collection of blog categories that
    /// match the search criteria. The collection is empty if no categories are found.</returns>
    Task<IEnumerable<BlogCategory>> SearchCategoriesAsync(string searchTerm);

    /// <summary>
    /// Asynchronously checks if a blog category with the specified URL handle already exists.
    /// </summary>
    /// <param name="urlHandle">The URL-friendly identifier to check.</param>
    /// <returns>A task that represents the asynchronous operation. The task result is <see langword="true"/> if a category with
    /// the specified URL handle exists; otherwise, <see langword="false"/>.</returns>
    Task<bool> CategoryUrlHandleExistsAsync(string urlHandle);

    /// <summary>
    /// Asynchronously checks if a blog category with the specified name already exists.
    /// </summary>
    /// <param name="name">The category name to check.</param>
    /// <returns>A task that represents the asynchronous operation. The task result is <see langword="true"/> if a category with
    /// the specified name exists; otherwise, <see langword="false"/>.</returns>
    Task<bool> CategoryNameExistsAsync(string name);
}