using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BlogAPI.Data
{
    public class BlogDbContext : DbContext
    {
        public DbSet<Category> Categories { set; get; }
        public DbSet<Post> Posts { set; get; }
        public DbSet<Author> Authors { set; get; }
        public DbSet<Contact> Contacts { set; get; }

        public BlogDbContext(DbContextOptions<BlogDbContext> options) : base(options)
        {
            //
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}