using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BlogAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace BlogAPI.Repository
{
    public class Repository<T> : IRepositoryGenernic<T> where T : class
    {

        private readonly BlogDbContext DbContext;

        private readonly DbSet<T> DbSet;
        public Repository(BlogDbContext dbContext)
        {
            DbContext = dbContext;
            DbSet = dbContext.Set<T>();
        }

        public void Add(T entity)
        {
            DbSet.Add(entity);
        }

        public void Update(T entity)
        {
            DbSet.Attach(entity);
            DbContext.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(object id)
        {
            var data = DbSet.Find(id);
            if (data != null)
                DbSet.Remove(data);
        }
        public void Delete(T entity)
        {
            if (DbContext.Entry(entity).State == EntityState.Detached)
            {
                DbContext.Attach(entity); // Đính kèm đối tượng vào DbSet nếu nó chưa được đính kèm
            }
            DbContext.Remove(entity);
        }

        public T Get(Expression<Func<T, bool>> predicate)
        {
            var data = DbSet.FirstOrDefault(predicate);
            return data;
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> predicate)
        {
            return await DbSet.FirstOrDefaultAsync(predicate);
        }

        public IEnumerable<T> GetAll()
        {
            return DbSet.ToList();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await DbSet.ToListAsync();
        }

        public T GetByID(object id)
        {
            return DbSet.Find(id);
        }

        public async Task<T> GetByIDAsync(object id)
        {
            return await DbSet.FindAsync(id);
        }

        public IEnumerable<T> GetMany(Expression<Func<T, bool>> predicate)
        {
            var data = DbSet.Where(predicate);
            return data;
        }

        public async Task<IEnumerable<T>> GetManyAsync(Expression<Func<T, bool>> predicate)
        {
            var data = await DbSet.Where(predicate).ToListAsync();
            return data;
        }

    }
}