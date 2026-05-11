using CodePulse.Domain.Entities;
using CodePulse.Domain.Repositories;
using CodePulse.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodePulse.Infrastructure.Repositories
{
    public class CategoryRepository : GenericRepository<BlogCategory>, ICategoryRepository
    {
        public CategoryRepository(AppDBContext context) : base(context)
        {
        }

        public async Task<BlogCategory?> GetByUrlHandleAsync(string urlHandle)
        {
            return await _dbSet
                .FirstOrDefaultAsync(c => c.UrlHandle == urlHandle);
        }

        public async Task<IEnumerable<BlogCategory>> GetAllSortedByNameAsync()
        {
            return await _dbSet
                .OrderBy(c => c.Name)
                .ToListAsync();
        }

        public async Task<bool> ExistsByUrlHandleAsync(string urlHandle)
        {
            return await _dbSet
                .AnyAsync(c => c.UrlHandle == urlHandle);
        }

        public async Task<bool> ExistsByNameAsync(string name)
        {
            return await _dbSet
                .AnyAsync(c => c.Name == name);
        }

        public async Task<IEnumerable<BlogCategory>> SearchByNameAsync(string searchTerm)
        {
            return await _dbSet
                .Where(c => c.Name.Contains(searchTerm))
                .OrderBy(c => c.Name)
                .ToListAsync();
        }
    }

}