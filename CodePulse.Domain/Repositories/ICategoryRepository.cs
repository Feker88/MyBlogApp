namespace CodePulse.Domain.Repositories;

using CodePulse.Domain.Entities;

/// <summary>
/// Repository interface for BlogCategory entity operations.
/// Extends the generic repository with category-specific queries.
/// </summary>
public interface ICategoryRepository : IGenericRepository<BlogCategory>
{
    /// <summary>
    /// Gets a category by its URL handle.
    /// </summary>
    /// <param name="urlHandle">The URL-friendly identifier for the category</param>
    /// <returns>The category if found; otherwise null</returns>
    Task<BlogCategory?> GetByUrlHandleAsync(string urlHandle);

    /// <summary>
    /// Gets all categories sorted by name.
    /// </summary>
    /// <returns>Enumerable collection of categories sorted alphabetically</returns>
    Task<IEnumerable<BlogCategory>> GetAllSortedByNameAsync();

    /// <summary>
    /// Checks if a category with the given URL handle already exists.
    /// </summary>
    /// <param name="urlHandle">The URL-friendly identifier to check</param>
    /// <returns>True if category exists; otherwise false</returns>
    Task<bool> ExistsByUrlHandleAsync(string urlHandle);

    /// <summary>
    /// Checks if a category with the given name already exists.
    /// </summary>
    /// <param name="name">The category name to check</param>
    /// <returns>True if category exists; otherwise false</returns>
    Task<bool> ExistsByNameAsync(string name);

   

    /// <summary>
    /// Searches for categories by name (partial match).
    /// </summary>
    /// <param name="searchTerm">The search term</param>
    /// <returns>Enumerable collection of matching categories</returns>
    Task<IEnumerable<BlogCategory>> SearchByNameAsync(string searchTerm);
}
