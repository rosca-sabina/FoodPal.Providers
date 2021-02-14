using FoodPal.Providers.DomainModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FoodPal.Providers.DataAccess.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseModel
    {
        protected readonly ProvidersContext _providersContext;
        private readonly DbSet<T> _dbSet;

        public Repository(ProvidersContext providersContext)
        {
            _providersContext = providersContext ?? throw new ArgumentNullException(nameof(providersContext));
            _dbSet = _providersContext.Set<T>();
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public void Remove(T entity)
        {
            _dbSet.Remove(entity);
        }

        public async Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> expression)
        {
            return await _dbSet.SingleOrDefaultAsync(expression);
        }
    }
}
