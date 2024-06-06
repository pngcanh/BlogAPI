using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BlogAPI.Data;
using BlogAPI.Model;
using Microsoft.EntityFrameworkCore;
using BlogAPI.Util;


namespace BlogAPI.Repository
{
    public class PostRepository : IPostRepository
    {
        ///////////////
        private readonly BlogDbContext DbContext;

        public PostRepository(BlogDbContext DbContext)
        {
            this.DbContext = DbContext;
        }

        public void Add(Post entity)
        {
            {
                // var post = new Post
                // {
                //     Content = entity.Content,
                //     Title = entity.Title,
                //     AuthorID = entity.AuthorID,
                //     CateID = entity.CateID,
                //     Photo = ResizeAndCompressImage.CompressImage(entity.FileUpload)
                // };
                DbContext.Posts.Add(entity);
            }
        }


        public void Delete(int id)
        {
            var post = DbContext.Posts.Find(id);
            if (post != null)
            {
                DbContext.Posts.Remove(post);
            }
        }

        public void Delete(Post entity)
        {
            if (DbContext.Entry(entity).State == EntityState.Detached)
            {
                DbContext.Attach(entity);
            }
            DbContext.Remove(entity);
        }

        public Post Get(Expression<Func<Post, bool>> predicate)
        {
            var post = DbContext.Posts.Include(a => a.Author).Include(c => c.Category).Where(predicate).FirstOrDefault();
            return post;
        }

        public IEnumerable<Post> GetAll()
        {
            var posts = DbContext.Posts.Include(a => a.Author).Include(c => c.Category).ToList();
            return posts;
        }

        public async Task<IEnumerable<Post>> GetAllAsync()
        {
            var posts = await DbContext.Posts.Include(a => a.Author).Include(c => c.Category).ToListAsync();
            return posts;
        }

        public async Task<Post> GetAsync(Expression<Func<Post, bool>> predicate)
        {
            var post = await DbContext.Posts.Include(a => a.Author).Include(c => c.Category).Where(predicate).FirstOrDefaultAsync();
            return post;
        }

        public Post GetByID(int id)
        {
            var post = DbContext.Posts.Include(a => a.Author).Include(c => c.Category).FirstOrDefault(p => p.ID == id);
            if (post != null)
                post.View++;
            return post;
        }

        public async Task<Post> GetByIDAsync(int id)
        {
            var post = await DbContext.Posts.Include(a => a.Author).Include(c => c.Category).FirstOrDefaultAsync(p => p.ID == id);
            if (post != null)
                post.View++;
            return post;
        }

        public IEnumerable<Post> GetMany(Expression<Func<Post, bool>> predicate)
        {
            var posts = DbContext.Posts.Include(a => a.Author).Include(c => c.Category).Where(predicate).ToList();
            return posts;
        }

        public async Task<IEnumerable<Post>> GetManyAsync(Expression<Func<Post, bool>> predicate)
        {
            var posts = await DbContext.Posts.Include(a => a.Author).Include(c => c.Category).Where(predicate).ToListAsync();
            return posts;
        }

        public void Update(Post entity)
        {
            DbContext.Update(entity);
        }
    }
}