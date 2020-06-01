using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Blog.Data
{
    public class BlogContext : DbContext
    {
        public BlogContext(DbContextOptions<BlogContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            //TODO: Move to separate configs
            modelBuilder.Entity<Post>()
                .HasOne(p => p.Author)
                .WithMany(p => p.Posts)
                .HasForeignKey(fk => fk.AuthorId);

            modelBuilder.Entity<Post>().HasOne(p => p.Blog)
                .WithMany(p => p.Posts)
                .HasForeignKey(fk => fk.BlogId);

            modelBuilder.Entity<Blog>().HasOne(p => p.Author)
                .WithMany(p => p.Blogs)
                .HasForeignKey(fk => fk.AuthorId);

            

            base.OnModelCreating(modelBuilder);
        }
    }
}
