using CodePulse.Application.DTOs;
using CodePulse.Application.DTOs.BlogPost;
using CodePulse.Domain.Entities;

namespace CodePulse.Application.Interfaces
{

    /// <summary>
    /// Application service contract that defines use-case oriented operations for blog posts.
    /// Provides methods for creating, reading, updating, deleting, and searching blog posts.
    /// </summary>
    /// <remarks>
    /// Implementations should orchestrate domain repositories, domain services and the unit of work
    /// to execute application workflows. Keep domain business rules inside the domain layer and
    /// use this service to coordinate use cases, validation, and transaction boundaries.
    /// </remarks>
    public interface IBlogPostServices
    {
        /// <summary>
        /// Asynchronously retrieves all blog posts associated with a specific category.
        /// </summary>
        /// <param name="categoryId">The unique identifier of the category for which to retrieve blog posts.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a collection of blog posts
        /// associated with the specified category. The collection is empty if no posts are found for the category.</returns>
        public Task<IEnumerable<BlogPostSummaryDTO>> GetPostsByCategoryAsync(Guid categoryId);

        /// <summary>
        /// Asynchronously retrieves all blog posts authored by the specified author.
        /// </summary>
        /// <param name="author">The name of the author whose blog posts are to be retrieved. Cannot be null or empty.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a collection of blog posts
        /// written by the specified author. The collection is empty if the author has no posts.</returns>
        Task<IEnumerable<BlogPostSummaryDTO>> GetPostsByAuthor(string author);

        /// <summary>
        /// Asynchronously retrieves all blog posts published within the specified date range.
        /// </summary>
        /// <param name="startDate">The start date of the range. Only posts created on or after this date are included.</param>
        /// <param name="endDate">The end date of the range. Only posts created on or before this date are included.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a collection of blog posts
        /// created within the specified date range. The collection is empty if no posts are found.</returns>
        Task<IEnumerable<BlogPostSummaryDTO>> GetPostsByDateRange(DateTime startDate, DateTime endDate);

        /// <summary>
        /// Asynchronously retrieves all blog posts whose title or content contains the specified search term.
        /// </summary>
        /// <param name="searchTerm">The text to search for within the title or content of blog posts. The search is case-insensitive. Cannot be null or empty.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a collection of blog posts that
        /// match the search criteria. The collection is empty if no posts are found.</returns>
        Task<IEnumerable<BlogPostSummaryDTO>> GetPostsByTitleOrContent(string searchTerm);

        /// <summary>
        /// Asynchronously updates an existing blog post and persists the changes to the data store.
        /// </summary>
        /// <param name="post">The blog post entity containing the updated values. Must not be null.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the updated blog post entity as stored
        /// in the data store.</returns>
        Task<BlogPostDTO> UpdatePostAsync(UpdateBlogPostDTO post);

        /// <summary>
        /// Asynchronously deletes a blog post identified by the specified unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the blog post to delete.</param>
        /// <returns>A task that represents the asynchronous operation. The task result is <see langword="true"/> if the blog
        /// post was successfully deleted; otherwise, <see langword="false"/>.</returns>
        Task<bool> DeletePostAsync(Guid id);

        /// <summary>
        /// Asynchronously creates a new blog post and persists it to the data store.
        /// </summary>
        /// <param name="post">The blog post to create. Must not be null.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the created blog post, including
        /// any updates made during persistence (such as generated identifiers and timestamps).</returns>
        Task<BlogPostDTO> CreatePostAsync(CreateBlogPostDTO post);
    }
}