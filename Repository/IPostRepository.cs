using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BlogAPI.Data;
using BlogAPI.Model;

namespace BlogAPI.Repository
{
    public interface IPostRepository
    {
        #region Sync Method
        //  lay tat ca entities
        IEnumerable<Post> GetAll();

        // Lay theo ID
        Post GetByID(int id);

        //lay entities theo dieu kien
        IEnumerable<Post> GetMany(Expression<Func<Post, bool>> predicate);

        //lay entity theo dieu kien
        Post Get(Expression<Func<Post, bool>> predicate);

        //them moi 1 entity
        void Add(Post entity);

        //update entity
        void Update(Post entity);

        //xoa 1 entity
        void Delete(int id);

        //xoa 1 entity
        void Delete(Post entity);

        #endregion

        #region Async Method
        //Lay tat ca entities
        Task<IEnumerable<Post>> GetAllAsync();

        //Lay theo ID
        Task<Post> GetByIDAsync(int id);

        //Lay theo dieu kien 
        Task<IEnumerable<Post>> GetManyAsync(Expression<Func<Post, bool>> predicate);

        //Update 1 entities 
        Task<Post> GetAsync(Expression<Func<Post, bool>> predicate);

        #endregion
    }
}