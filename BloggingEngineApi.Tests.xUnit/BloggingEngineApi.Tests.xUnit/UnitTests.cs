using System.Collections.Generic;
using BloggingEngineApi.Controllers.V1;
using BloggingEngineApi.Models;
using Xunit;
using Moq;
using BloggingEngineApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace BloggingEngineApi.Tests.xUnit
{
    public class UnitTests
    {
        /// <summary>
        /// Mock the backing repository for the blogging controller and call methods on the controller.
        /// </summary>
        [Fact]
        public async void Get_Blog_Mock_By_Id_Async()
        {
            var repositoryMock = new Mock<IBloggingRepository>();

            var blogMock = new Blog()
            {
                BlogId = 1,
                Name = "Corbs Blog",
                Posts = new List<Post>()
                {
                    new Post()
                    {
                        BlogId = 1,
                        Content = "ipso facto",
                        PostId = 1,
                        Title = "Post for this blog"
                    }
                },
                Url = "test.com"
            };

            repositoryMock.Setup(x => x.GetBlogAsync(1)).ReturnsAsync(blogMock);

            var bloggingController = new BloggingController(repositoryMock.Object);
            var actualResponse = await bloggingController.GetAsync(1);
            var expectedResponse = new ActionResult<Blog>(new OkObjectResult(blogMock));
            Assert.Equal(expectedResponse.Value, actualResponse.Value);
        }

        [Fact]
        public async void Get_All_Blogs_Mock_Async()
        {
            var repositoryMock = new Mock<IBloggingRepository>();

            var blogMocks = new List<Blog>()
            {
                new Blog
                {
                    BlogId = 1,
                    Name = "Corbs Blog",
                    Posts = new List<Post>()
                    {
                        new Post()
                        {
                            BlogId = 1,
                            Content = "ipso facto",
                            PostId = 1,
                            Title = "Post for this blog"
                        }
                    },
                    Url = "test.com"
                },
                new Blog
                {
                    BlogId = 2,
                    Name = "Bron's Blog",
                    Posts = new List<Post>()
                    {
                        new Post()
                        {
                            BlogId = 1,
                            Content = "qed",
                            PostId = 1,
                            Title = "Post for this blog"
                        }
                    },
                    Url = "testing.com"
                }
            };

            repositoryMock.Setup(x => x.GetAllBlogsAsync()).ReturnsAsync(blogMocks);

            var bloggingController = new BloggingController(repositoryMock.Object);
            var actualResponse = await bloggingController.GetAsync();
            var expectedResponse = new ActionResult<List<Blog>>(new OkObjectResult(blogMocks));
            Assert.Equal(expectedResponse.Value, actualResponse.Value);
        }

        [Fact]
        public async void Delete_Blog_Mock_By_Id_Async()
        {
            var repositoryMock = new Mock<IBloggingRepository>();

            dynamic jsonResponseMock = new JObject();
            jsonResponseMock.message = "Blog with id 2 successfully deleted.";

            repositoryMock.Setup(x => x.DeleteBlogAsync(2)).ReturnsAsync(true);

            var bloggingController = new BloggingController(repositoryMock.Object);
            var response = await bloggingController.DeleteAsync(2);

            var expectedResponse = new ActionResult<string>(new OkObjectResult(jsonResponseMock));

            Assert.Equal(expectedResponse.Value, response.Value);
        }
    }
}
