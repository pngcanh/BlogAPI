using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BlogAPI.Repository
{
    public interface IRepository<T> where T : class
    {
        // DbSet<T> DbSet { get; }
        // DbContext DbContext { set; get; }

        #region Sync Method
        //  lay tat ca entities
        IEnumerable<T> GetAll();

        // Lay theo ID
        T GetByID(object id);

        //lay entities theo dieu kien
        IEnumerable<T> GetMany(Expression<Func<T, bool>> predicate);

        //lay entity theo dieu kien
        T Get(Expression<Func<T, bool>> predicate);

        //them moi 1 entity
        void Add(T entity);

        //update entity
        void Update(T entity);

        //xoa 1 entity
        void Delete(object id);

        //xoa 1 entity
        void Delete(T entity);

        #endregion

        #region Async Method
        //Lay tat ca entities
        Task<IEnumerable<T>> GetAllAsync();

        //Lay theo ID
        Task<T> GetByIDAsync(object id);

        //Lay theo dieu kien 
        Task<IEnumerable<T>> GetManyAsync(Expression<Func<T, bool>> predicate);

        //Update 1 entities 
        Task<T> GetAsync(Expression<Func<T, bool>> predicate);

        #endregion
    }
}