using System.Collections.Generic;
using System.Threading.Tasks;
using BloggingEngineApi.Models;

namespace BloggingEngineApi.Repositories
{
    public interface IBloggingRepository
    {
        Task<Blog> GetBlogAsync(int id);
        Task<List<Blog>> GetAllBlogsAsync();
        Task<Blog> CreateBlogAsync(Blog blog);
        Task<Blog> UpdateBlogAsync(Blog blog);
        Task<bool> DeleteBlogAsync(int id);
    }
}