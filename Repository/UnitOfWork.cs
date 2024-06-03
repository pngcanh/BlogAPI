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
        private IRepository<Author> _author;
        private IRepository<Contact> _contact;
        private IRepository<Category> _category;
        private IRepository<Post> _post;


        public IRepository<Author> Author
        {
            get
            {
                return _author ??= new Repository<Author>(DbContext);
            }
        }

        public IRepository<Category> Category
        {
            get
            {
                return _category ??= new Repository<Category>(DbContext);
            }
        }

        public IRepository<Contact> Contact
        {
            get
            {
                return _contact ??= new Repository<Contact>(DbContext);
            }
        }

        public IRepository<Post> Post
        {
            get
            {
                return _post ??= new Repository<Post>(DbContext);
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