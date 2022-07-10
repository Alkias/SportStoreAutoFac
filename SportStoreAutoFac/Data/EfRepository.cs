using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SportStoreAutoFac.Data
{
    public class EfRepository<T> : IAsyncRepository<T> where T : BaseEntity
    {
        //see:https://github.com/zHaytam/RepositoryPatternExample/issues/1

        #region Fields

        protected StoreDbContext Context;

        #endregion

        #region Ctor

        public EfRepository(StoreDbContext context) {
            Context = context;
        }

        #endregion

        #region Methods

        public ValueTask<T> GetById(int id) {
            return Context.Set<T>().FindAsync(id);
        }

        public Task<T> FirstOrDefault(Expression<Func<T, bool>> predicate) {
            return Context.Set<T>().FirstOrDefaultAsync(predicate);
        }

        public async Task Add(T entity) {
            // await Context.AddAsync(entity);
            await Context.Set<T>().AddAsync(entity);
            await Context.SaveChangesAsync();
        }

        public Task Update(T entity) {
            // In case AsNoTracking is used
            Context.Entry(entity).State = EntityState.Modified;
            return Context.SaveChangesAsync();
        }

        public Task Remove(T entity) {
            Context.Set<T>().Remove(entity);
            return Context.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAll() {
            return await Context.Set<T>().ToListAsync();
        }

        public async Task<IEnumerable<T>> GetWhere(Expression<Func<T, bool>> predicate) {
            return await Context.Set<T>().Where(predicate).ToListAsync();
        }

        public Task<int> CountAll() {
            return Context.Set<T>().CountAsync();
        }

        public Task<int> CountWhere(Expression<Func<T, bool>> predicate) {
            return Context.Set<T>().CountAsync(predicate);
        }

        public IQueryable<T> GetTable() {
            return Context.Set<T>();
        }

        #endregion
    }
}