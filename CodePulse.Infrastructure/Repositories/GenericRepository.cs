using CodePulse.Domain.Repositories;
using CodePulse.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodePulse.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {

        protected readonly AppDBContext _context;
        protected readonly DbSet<T> _dbSet;

       public  GenericRepository(AppDBContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _dbSet = _context.Set<T>();
        }

        public async Task<T> AddAsync(T entity)
        {
             await  _dbSet.AddAsync(entity);
             return entity;

        }

        public async Task<bool> DeleteAsync(Guid id)
        {

           var entity = await GetByIdAsync(id);
            if (entity is null)
                return false;

            _dbSet.Remove(entity);
            return true;
        }

        public async  Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async  Task<T?> GetByIdAsync(Guid id)
        {
          return    await _dbSet.FindAsync  (id);
           

        }

        public async Task<T> UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            return entity;

        }
    }
}
