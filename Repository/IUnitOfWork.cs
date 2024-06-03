using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogAPI.Data;
namespace BlogAPI.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Author> Author { get; }
        IRepository<Category> Category { get; }
        IRepository<Contact> Contact { get; }
        IRepository<Post> Post { get; }
        void SaveChange();
        Task SaveChangeAsync();
    }
}