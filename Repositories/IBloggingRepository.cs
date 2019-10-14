using System.Collections.Generic;
using BloggingEngineApi.Models;

namespace BloggingEngineApi.BloggingRepository
{
    public interface IBloggingRepository
    {
        Blog GetBlog(int id);
        List<Blog> GetAllBlogs();
        Blog CreateBlog();
        Blog UpdateBlog();
        void DeleteBlog();
    }
}