using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using BloggingEngineApi.Context;
using BloggingEngineApi.Models;
using System.Linq;
using System.Threading.Tasks;
using BloggingEngineApi.Repositories;
using Newtonsoft.Json.Linq;

namespace BloggingEngineApi.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class BloggingController : ControllerBase
    {
        private readonly IBloggingRepository _bloggingRepository;

        public BloggingController(IBloggingRepository bloggingRepository)
        {
            _bloggingRepository = bloggingRepository;
        }

        // GET api/blogging
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Blog>>> GetAsync()
        {
            return Ok(await _bloggingRepository.GetAllBlogsAsync());
        }

        // GET api/blogging/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Blog>> GetAsync(int id)
        {
            Blog blog = await _bloggingRepository.GetBlogAsync(id);

            if (blog != null)
            {
                return Ok(await _bloggingRepository.GetBlogAsync(id));
            }

            return NotFound();
        }

        // POST api/blogging
        [HttpPost]
        public async Task<ActionResult<Blog>> PostAsync([FromBody] Blog blog)
        {
            Blog resultBlog = await _bloggingRepository.CreateBlogAsync(blog);
            return Created(($"/api/blogging/{0}", resultBlog.BlogId).ToString(), resultBlog);
        }

        // PUT api/blogging/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Blog>> PutAsync(int id, [FromBody] Blog blog)
        {
            Blog updatedBlog = await _bloggingRepository.UpdateBlogAsync(blog);
            return Ok(updatedBlog);
        }

        // DELETE api/blogging/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<string>> DeleteAsync(int id)
        {
            var success = await _bloggingRepository.DeleteBlogAsync(id);

            dynamic jsonResponse = new JObject();

            if (success)
            {
                jsonResponse.message = string.Format($@"Blog with id {id} successfully deleted.");
                return Ok(jsonResponse);
            }

            jsonResponse.message = string.Format($@"Failed to delete Blog with id {id}.");
            return BadRequest(jsonResponse);
        }
    }
}
