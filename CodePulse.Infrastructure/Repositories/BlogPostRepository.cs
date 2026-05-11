using CodePulse.Domain.Entities;
using CodePulse.Domain.Repositories;
using CodePulse.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodePulse.Infrastructure.Repositories
{
    public class BlogPostRepository : GenericRepository<BlogPost>, IBlogPostRepository
    {
        public BlogPostRepository(AppDBContext context) : base(context)
        {
        }

        public async Task<IEnumerable<BlogPost>> GetPostsByCategoryAsync(Guid categoryId)
        {
            return await _dbSet
                .Where(p => p.CategoryId == categoryId)
                .ToListAsync();
        }

        public async Task<IEnumerable<BlogPost>> GetPostByAuthorAsync(string author)
        {
            return await _dbSet
                .Where(p => p.Author == author)
                .ToListAsync();
        }

        public async Task<IEnumerable<BlogPost>> GetPostByDateRangeAsync(
            DateTime minDateRange, DateTime maxDateRange)
        {
            return await _dbSet
                .Where(p => p.CreatedAt >= minDateRange && p.CreatedAt <= maxDateRange)
                .ToListAsync();
        }

        public async Task<IEnumerable<BlogPost>> SearchPostByTitleOrContent(string searchTerm)
        {
            return await _dbSet
                .Where(p => p.Title.Contains(searchTerm) || p.Content.Contains(searchTerm))
                .ToListAsync();
        }
    }
}
