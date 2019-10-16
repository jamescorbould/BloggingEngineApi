using System.Collections.Generic;
using System.Threading.Tasks;
using BloggingEngineApi.Models;

namespace BloggingEngineApi.Interfaces
{
    public interface IBloggingRepository
    {
        Blog GetBlog(int id);
        List<Blog> GetAllBlogs();
        Task<Blog> CreateBlog(Blog blog);
        Task<Blog> UpdateBlog(Blog blog);
        Task<bool> DeleteBlog(int id);
    }
}