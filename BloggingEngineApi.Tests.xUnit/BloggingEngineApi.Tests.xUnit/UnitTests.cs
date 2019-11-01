using System.Collections.Generic;
using BloggingEngineApi.Controllers.V1;
using BloggingEngineApi.Models;
using Xunit;
using Moq;
using BloggingEngineApi.Repositories;

namespace BloggingEngineApi.Tests.xUnit
{
    public class UnitTests
    {
        /// <summary>
        /// Mock the backing repository for the blogging controller and call methods on the controller.
        /// </summary>
        [Fact]
        public void Get_Blog_Test_By_Id()
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

            repositoryMock.Setup(x => x.GetBlog(1)).Returns(blogMock);

            var bloggingController = new BloggingController(repositoryMock.Object);
            var actualResponse = bloggingController.Get(1);
            var expectedResponse = blogMock;
            Assert.Equal(actualResponse, expectedResponse);
        }
    }
}
