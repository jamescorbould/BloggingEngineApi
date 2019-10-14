using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using BloggingEngineApi.Context;
using BloggingEngineApi.Models;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace BloggingEngineApi.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class BloggingController : ControllerBase
    {
        // GET api/blogging
        [HttpGet]
        public ActionResult<IEnumerable<Blog>> Get()
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

            return Ok(blogs);
        }

        // GET api/blogging/5
        [HttpGet("{id}")]
        public ActionResult<Blog> Get(int id)
        {
            using (var db = new BloggingContext())
            {
                var blog = db.Blogs.First(b => b.BlogId == id);

                if (blog == null)
                {
                    return NotFound();
                }

                blog.Posts = db.Posts.Where(p => p.BlogId == id).ToList();

                return Ok(blog);
            }
        }

        // POST api/blogging
        [HttpPost]
        public async Task<ActionResult<Blog>> Post([FromBody] Blog blog)
        {
            using (var db = new BloggingContext())
            {
                await db.AddAsync(blog);
                var result = await db.SaveChangesAsync();
                var newBlog = db.Blogs.First(b => b.BlogId == blog.BlogId);
                var newPosts = db.Posts.Where(p => p.BlogId == newBlog.BlogId).ToList();
                newBlog.Posts = newPosts;

                return Created(($"/api/blogging/{0}", blog.BlogId).ToString(), blog);
            }
        }

        // PUT api/blogging/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Blog>> Put(int id, [FromBody] Blog blog)
        {
            using (var db = new BloggingContext())
            {
                db.Update(blog);
                await db.SaveChangesAsync();
                var updatedBlog = db.Blogs.First(b => b.BlogId == blog.BlogId);
                var updatedPosts = db.Posts.Where(p => p.BlogId == updatedBlog.BlogId).ToList();
                updatedBlog.Posts = updatedPosts;

                return Ok(updatedBlog);
            }
        }

        // DELETE api/blogging/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            using (var db = new BloggingContext())
            {
                var count = db.Blogs.Count(b => b.BlogId == id);

                if (count == 0)
                {
                    return NotFound();
                }

                var blog = db.Blogs.First(b => b.BlogId == id);
                db.Remove(blog);
                await db.SaveChangesAsync();

                dynamic jsonResponse = new JObject();
                jsonResponse.message = string.Format($@"Blog with id {id} successfully deleted.");

                return Ok(jsonResponse);
            }
        }
    }
}
