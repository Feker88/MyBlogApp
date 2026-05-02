using CodePulse.Domain.Entities;
using CodePulse.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

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


        public Task<IEnumerable<BlogPost>> GetPostsByCategoryAsync(Guid categoryId);



        Task<IEnumerable<BlogPost>> GetPostsByAuthor(string author);

      
        Task<IEnumerable<BlogPost>> GetPostsByDateRange(DateTime startDate, DateTime endDate);
  
        Task<IEnumerable<BlogPost>> GetPostsByTitleOrContent(string searchTerm);



       
        Task<BlogPost> UpdatePostAsync(BlogPost post);

        Task<bool> DeletePostAsync(Guid id);

        Task<BlogPost> CreatePostAsync(BlogPost post);
        
    }
}
