using AutoFixture;
using Microsoft.AspNetCore.Mvc;
using Moq;
using POC.BLL.Dto;
using POC.BLL.Services;
using POC.Web.Controllers;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace UnitTests.Controllers
{
    public class InstagramControllerTest
    {
        private readonly Mock<IInstagramService> _instaService;
        private readonly InstagramController _instaController;
        private readonly Fixture _fixture;

        public InstagramControllerTest()
        {
            _instaService = new Mock<IInstagramService>();
            _instaController = new InstagramController(_instaService.Object);
            _fixture = new Fixture();
        }

        [Fact]
        public async void GetStoriesByName_StoriesExist_ReturnsOkObjectResult()
        {
            //Arrange
            var name = "story name";
            var stories = _fixture.CreateMany<InstagramMediaDto>().ToList();
            _instaService.Setup(cfg => cfg.GetStoriesAsync(name)).ReturnsAsync(stories);

            //Act
            var response = await _instaController.GetStories(name);

            //Assert
            Assert.NotNull(response);
            var okResult = Assert.IsType<OkObjectResult>(response.Result);
            var result = Assert.IsType<List<InstagramMediaDto>>(okResult.Value);
            Assert.Equal(stories, result);
        }

        [Fact]
        public async void GetStoriesByName_StoriesDontExist_ReturnsNoContentResult()
        {
            //Arrange
            var name = "story name";
            var stories = new List<InstagramMediaDto>();
            _instaService.Setup(cfg => cfg.GetStoriesAsync(name)).ReturnsAsync(stories);

            //Act
            var response = await _instaController.GetStories(name);

            //Assert
            Assert.NotNull(response);
            Assert.IsType<NoContentResult>(response.Result);
        }
    }
}
