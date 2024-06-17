using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogAPI.Data;
using BlogAPI.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace BlogAPI.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BlogDbContext DbContext;

        private readonly UserManager<BlogUser> userManager;
        private readonly SignInManager<BlogUser> signInManager;
        private readonly IConfiguration configuration;
        private RoleManager<IdentityRole> roleManager;
        private IRepositoryGenernic<Author> _author;
        private IRepositoryGenernic<Contact> _contact;
        private IRepositoryGenernic<Category> _category;
        private IPostRepository _post;
        private IAccountRepository _account;
        private IRoleRepository _role;

        public UnitOfWork(BlogDbContext DbContext, UserManager<BlogUser> userManager, SignInManager<BlogUser> signInManager, IConfiguration configuration, RoleManager<IdentityRole> roleManager)
        {
            this.DbContext = DbContext;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.configuration = configuration;
            this.roleManager = roleManager;
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
        public IAccountRepository Account
        {
            get
            {
                return _account ??= new AccountRepository(userManager, signInManager, configuration, roleManager);
            }
        }

        public IRoleRepository Role
        {
            get
            {
                return _role ??= new RoleRepository(roleManager);
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