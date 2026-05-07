using AutoMapper;
using CodePulse.Application.DTOs.BlogPost;
using CodePulse.Application.Interfaces;
using CodePulse.Domain.Entities;
using CodePulse.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodePulse.Application.Services
{
    /// <summary>
    /// Application service that implements use-cases related to BlogPost services.
    /// Orchestrates repositories and the unit of work for post operations.
    /// </summary>

    public class BlogPostServices : ApplicationService , IBlogPostServices
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IBlogPostRepository _blogPostRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;


        public BlogPostServices(ICategoryRepository categoryRepository, 
                                IBlogPostRepository blogPostRepository, 
                                IUnitOfWork unitOfWork,
                                IMapper mapper)
        {
            _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
            _blogPostRepository = blogPostRepository ?? throw new ArgumentNullException(nameof(blogPostRepository));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Asynchronously retrieves all blog posts that belong to the specified category.
        /// </summary>
        /// <param name="categoryId">The unique identifier of the category for which to retrieve blog posts.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a collection of blog posts
        /// associated with the specified category. The collection is empty if no posts are found.</returns>
        public async Task<IEnumerable<BlogPostSummaryDTO>> GetPostsByCategoryAsync(Guid categoryId)
        {
          var entities =   _blogPostRepository.GetPostsByCategoryAsync(categoryId);
            return _mapper.Map<IEnumerable<BlogPostSummaryDTO>>(entities);
        }
             
        /// <summary>
        /// Asynchronously retrieves all blog posts authored by the specified author.
        /// </summary>
        /// <param name="author">The name of the author whose blog posts are to be retrieved. Cannot be null.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a collection of blog posts
        /// written by the specified author. The collection is empty if the author has no posts.</returns>
        public async Task<IEnumerable<BlogPostSummaryDTO>> GetPostsByAuthor(string author)
        {
            var entities = _blogPostRepository.GetPostByAuthorAsync(author);
            return _mapper.Map<IEnumerable<BlogPostSummaryDTO>>(entities);
          
        }
       /// <summary>
       /// Asynchronously retrieves all blog posts published within the specified date range.
       /// </summary>
       /// <param name="startDate">The start date of the range. Only posts published on or after this date are included.</param>
       /// <param name="endDate">The end date of the range. Only posts published on or before this date are included.</param>
       /// <returns>A task that represents the asynchronous operation. The task result contains a collection of blog posts
       /// published within the specified date range. The collection is empty if no posts are found.</returns>
        public async  Task<IEnumerable<BlogPostSummaryDTO>> GetPostsByDateRange(DateTime startDate, DateTime endDate)
        {
            var entities = _blogPostRepository.GetPostByDateRangeAsync(startDate, endDate);
            return _mapper.Map<IEnumerable<BlogPostSummaryDTO>>(entities);
        }

        
        /// <summary>
        /// Asynchronously retrieves all blog posts whose title or content contains the specified search term.
        /// </summary>
        /// <param name="searchTerm">The text to search for within the title or content of blog posts. The search is case-insensitive. Cannot be
        /// null.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a collection of blog posts that
        /// match the search criteria. The collection is empty if no posts are found.</returns>
        public async Task<IEnumerable<BlogPostSummaryDTO>> GetPostsByTitleOrContent(string searchTerm) 
        {
            var entities = _blogPostRepository.SearchPostByTitleOrContent(searchTerm);
            return _mapper.Map<IEnumerable<BlogPostSummaryDTO>>(entities);
        }


        /// <summary>
        /// Updates an existing blog post asynchronously and persists the changes to the data store.
        /// </summary>
        /// <param name="post">The blog post entity containing the updated values. Must not be null.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the updated blog post entity as stored
        /// in the data store.</returns>
        public async Task<BlogPostDTO> UpdatePostAsync(UpdateBlogPostDTO post)
        {
            var entity = _mapper.Map<BlogPost>(post);

            var updated = await _blogPostRepository.UpdateAsync(entity);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<BlogPostDTO>(updated);
        }
        /// <summary>
        /// Asynchronously deletes a blog post identified by the specified unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the blog post to delete.</param>
        /// <returns>A task that represents the asynchronous operation. The task result is <see langword="true"/> if the blog
        /// post was successfully deleted; otherwise, <see langword="false"/>.</returns>
        public async Task<bool> DeletePostAsync(Guid id)
        {

            var result = await _blogPostRepository.DeleteAsync(id);
            if (result)
                await _unitOfWork.SaveChangesAsync();
            return result;
        }
        /// <summary>
        /// Asynchronously creates a new blog post and persists it to the data store.
        /// </summary>
        /// <param name="post">The blog post to create. Must not be null.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the created blog post, including
        /// any updates made during persistence (such as generated identifiers).</returns>
        public async Task<BlogPostDTO> CreatePostAsync(CreateBlogPostDTO post)
        {
            var entity = _mapper.Map<BlogPost>(post);

            var created = await _blogPostRepository.AddAsync(entity);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<BlogPostDTO>(created);
        }

    }
}
