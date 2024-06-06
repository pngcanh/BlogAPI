using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogAPI.Data;
using BlogAPI.Model;

namespace BlogAPI.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BlogDbContext DbContext;

        private IRepositoryGenernic<Author> _author;
        private IRepositoryGenernic<Contact> _contact;
        private IRepositoryGenernic<Category> _category;
        private IPostRepository _post;

        public UnitOfWork(BlogDbContext DbContext)
        {
            this.DbContext = DbContext;
        }

        public IRepositoryGenernic<Author> Author
        {
            get
            {
                return _author ??= new Repository<Author>(DbContext);
            }
        }

        public IRepositoryGenernic<Category> Category
        {
            get
            {
                return _category ??= new Repository<Category>(DbContext);
            }
        }

        public IRepositoryGenernic<Contact> Contact
        {
            get
            {
                return _contact ??= new Repository<Contact>(DbContext);
            }
        }
        public IPostRepository Post
        {
            get
            {
                return _post ??= new PostRepository(DbContext);
            }
        }

        public void SaveChange()
        {
            DbContext.SaveChanges();

        }

        public async Task SaveChangeAsync()
        {
            await DbContext.SaveChangesAsync();

        }

        public void Dispose()
        {
            DbContext.Dispose();
        }
    }
}