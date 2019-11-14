using BloggingEngineApi.Models;
using BloggingEngineApi.Context;
using BloggingEngineApi.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BloggingEngineApi.DbRepository
{
    public class BloggingDbRepository : IBloggingRepository
    {
        public async Task<Blog> GetBlogAsync(int id)
        {
            using (var db = new BloggingContext())
            {
                var blog = await db.Blogs.FirstAsync(b => b.BlogId == id);

                if (blog == null)
                {
                    return null;
                }

                blog.Posts = await db.Posts.Where(p => p.BlogId == id).ToListAsync();

                return blog;
            }
        }

        public async Task<List<Blog>> GetAllBlogsAsync()
        {
            List<Blog> blogs = new List<Blog>();

            using (var db = new BloggingContext())
            {
                foreach (var blog in db.Blogs)
                {
                    var posts = db.Posts.Where(p => p.BlogId == blog.BlogId);
                    blog.Posts = await posts.ToListAsync();
                    blogs.Add(blog);
                }
            }

            return blogs;
        }

        public async Task<Blog> CreateBlogAsync(Blog blog)
        {
            using (var db = new BloggingContext())
            {
                await db.AddAsync(blog);
                var result = await db.SaveChangesAsync();
                var newBlog = await db.Blogs.FirstAsync(b => b.BlogId == blog.BlogId);
                var newPosts = await db.Posts.Where(p => p.BlogId == newBlog.BlogId).ToListAsync();
                newBlog.Posts = newPosts;

                return newBlog;
            }
        }
        
        public async Task<Blog> UpdateBlogAsync(Blog blog)
        {
            using (var db = new BloggingContext())
            {
                db.Update(blog);
                await db.SaveChangesAsync();
                var updatedBlog = await db.Blogs.FirstAsync(b => b.BlogId == blog.BlogId);
                var updatedPosts = await db.Posts.Where(p => p.BlogId == updatedBlog.BlogId).ToListAsync();
                updatedBlog.Posts = updatedPosts;

                return updatedBlog;
            }
        }

        public async Task<bool> DeleteBlogAsync(int id)
        {
            using (var db = new BloggingContext())
            {
                var count = await db.Blogs.CountAsync(b => b.BlogId == id);

                if (count == 0)
                {
                    return false;
                }

                var blog = await db.Blogs.FirstAsync(b => b.BlogId == id);
                db.Remove(blog);
                await db.SaveChangesAsync();

                return true;
            }
        }
    }
}