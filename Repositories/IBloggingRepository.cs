using System.Collections.Generic;
using System.Threading.Tasks;
using BloggingEngineApi.Models;

namespace BloggingEngineApi.Repositories
{
    public interface IBloggingRepository
    {
        Blog GetBlog(int id);
        List<Blog> GetAllBlogs();
        Task<Blog> CreateBlogAsync(Blog blog);
        Task<Blog> UpdateBlogAsync(Blog blog);
        Task<bool> DeleteBlogAsync(int id);
    }
}