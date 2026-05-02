

using CodePulse.Domain.Entities;

namespace CodePulse.Domain.Repositories
{
    /// <summary>
    /// Defines a contract for repository operations specific to blog posts, supporting standard data access methods
    /// such as create, read, update, and delete.
    /// </summary>
    /// <remarks>This interface extends <see cref="IGenericRepository{BlogPost}"/>, providing a base for
    /// implementing data access logic for <see cref="BlogPost"/> entities. Implementations may include additional
    /// methods tailored to blog post management.</remarks>
    public  interface IBlogPostRepository : IGenericRepository<BlogPost>
    {
        /// <summary>
        /// Gets all blog posts associated with a specific category.
        /// </summary>
        /// <param name="categoryId">The unique identifier of the category.</param>
        /// <returns>A collection of blog posts in the specified category.</returns>
        Task<IEnumerable<BlogPost>> GetPostsByCategoryAsync(Guid categoryId);

        /// <summary>
        /// Gets all blog posts written by a specific author.
        /// </summary>
        /// <param name="author">The name or identifier of the author.</param>
        /// <returns>A collection of blog posts written by the specified author.</returns>
        Task<IEnumerable<BlogPost>> GetPostByAuthorAsync(string author);

        /// <summary>
        /// Gets all blog posts within a specified date range.
        /// </summary>
        /// <param name="minDateRange">The start date of the range to filter posts by.</param>
        /// <param name="maxDateRange">The end date of the range to filter posts by.</param>
        /// <returns>A collection of blog posts created within the specified date range.</returns>
        Task<IEnumerable<BlogPost>> GetPostByDateRangeAsync(DateTime minDateRange, DateTime maxDateRange);

        /// <summary>
        /// Searches for blog posts by title or content.
        /// </summary>
        /// <param name="searchTerm">The search term to look for in post titles or content.</param>
        /// <returns>A collection of blog posts matching the search term.</returns>
        Task<IEnumerable<BlogPost>> SearchPostByTitleOrContent(string searchTerm);

    }
}


/*  CASES might be added  
•	Get posts by tag(s)
•	Get published posts only
•	Get draft/unpublished posts
•	Get recent posts (with limit)
•	Get featured/pinned posts
•	Get posts with pagination
•	Get popular posts (by view count or engagement)
•	Check if URL slug already exists
•	Get posts by status (published, draft, archived, etc.)
•	Increment post view count
•	Get related posts (by category or tags)
•	Get archived posts
•	Get posts count (total, by category, by author, etc.)
•	Get posts by month/year (for archive view)
•	Get all post slugs (for sitemap generation)
 */