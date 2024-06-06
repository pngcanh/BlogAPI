using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogAPI.Data;
namespace BlogAPI.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        IRepositoryGenernic<Author> Author { get; }
        IRepositoryGenernic<Category> Category { get; }
        IRepositoryGenernic<Contact> Contact { get; }
        IPostRepository Post { get; }
        void SaveChange();
        Task SaveChangeAsync();
    }
}