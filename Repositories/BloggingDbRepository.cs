using BloggingEngineApi.Models;
using BloggingEngineApi.Context;
using BloggingEngineApi.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace BloggingEngineApi.DbRepository
{
    public class BloggingDbRepository : IBloggingRepository
    {
        public Blog GetBlog(int id)
        {
            using (var db = new BloggingContext())
            {
                var blog = db.Blogs.First(b => b.BlogId == id);

                if (blog == null)
                {
                    return null;
                }

                blog.Posts = db.Posts.Where(p => p.BlogId == id).ToList();

                return blog;
            }
        }

        public List<Blog> GetAllBlogs()
        {
            List<Blog> blogs = new List<Blog>();

            using (var db = new BloggingContext())
            {
                foreach (var blog in db.Blogs)
                {
                    var posts = db.Posts.Where(p => p.BlogId == blog.BlogId);
                    blog.Posts = posts.ToList();
                    blogs.Add(blog);
                }
            }

            return blogs;
        }

        public async Task<Blog> CreateBlog(Blog blog)
        {
            using (var db = new BloggingContext())
            {
                await db.AddAsync(blog);
                var result = await db.SaveChangesAsync();
                var newBlog = db.Blogs.First(b => b.BlogId == blog.BlogId);
                var newPosts = db.Posts.Where(p => p.BlogId == newBlog.BlogId).ToList();
                newBlog.Posts = newPosts;

                return newBlog;
            }
        }
        
        public async Task<Blog> UpdateBlog(Blog blog)
        {
            using (var db = new BloggingContext())
            {
                db.Update(blog);
                await db.SaveChangesAsync();
                var updatedBlog = db.Blogs.First(b => b.BlogId == blog.BlogId);
                var updatedPosts = db.Posts.Where(p => p.BlogId == updatedBlog.BlogId).ToList();
                updatedBlog.Posts = updatedPosts;

                return updatedBlog;
            }
        }

        public async Task<bool> DeleteBlog(int id)
        {
            using (var db = new BloggingContext())
            {
                var count = db.Blogs.Count(b => b.BlogId == id);

                if (count == 0)
                {
                    return false;
                }

                var blog = db.Blogs.First(b => b.BlogId == id);
                db.Remove(blog);
                await db.SaveChangesAsync();

                return true;
            }
        }
    }
}