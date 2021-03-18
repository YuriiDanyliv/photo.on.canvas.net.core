using AutoFixture;
using Microsoft.AspNetCore.Mvc;
using Moq;
using POC.BLL.Dto;
using POC.BLL.Services;
using POC.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace UnitTests.Controllers
{
    public class CanvasControllerTest
    {
        private readonly Mock<ICanvasService> _canvasService;
        private readonly CanvasController _canvasController;
        private readonly Fixture _fixture;

        public CanvasControllerTest()
        {
            _fixture = new Fixture();
            _canvasService = new Mock<ICanvasService>();
            _canvasController = new CanvasController(_canvasService.Object);
        }

        [Fact]
        public async void GetCanvases_CanvasesExist_ReturnsOkObjectResult()
        {
            //Arrange
            var canvases = _fixture.CreateMany<CanvasDto>().ToList();
            _canvasService.Setup(cfg => cfg.GetCanvasesAsync()).ReturnsAsync(canvases);

            //Act
            var response = await _canvasController.GetCanvases();

            //Assert
            Assert.NotNull(response);
            var okResult = Assert.IsType<OkObjectResult>(response.Result);
            var result = Assert.IsType<List<CanvasDto>>(okResult.Value);
            Assert.Equal(canvases, result);
        }

        [Fact]
        public async void GetCanvases_CanvasesDontExist_ReturnNoContentResult()
        {
            //Arrange
            _canvasService.Setup(cfg => cfg.GetCanvasesAsync()).ReturnsAsync(new List<CanvasDto>());

            //Act
            var response = await _canvasController.GetCanvases();

            //Assert
            Assert.NotNull(response);
            Assert.IsType<NoContentResult>(response.Result);
        }

        [Fact]
        public async void CreateCanvas_ModelIsValid_ReturnsOkObjectResult()
        {
            //Arrange
            var canvas = _fixture.Create<CreateCanvasDto>();
            _canvasService.Setup(cfg => cfg.CreateCanvasAsync(canvas));

            //Act
            var response = await _canvasController.CreateCanvas(canvas);

            //Assert
            Assert.NotNull(response);
            Assert.IsType<OkObjectResult>(response);
        }

        [Fact]
        public async void CreateCanvas_ModelIsNotValid_ReturnBadRequestObjectResult()
        {
            //Arrange
            _canvasService.Setup(cfg => cfg.CreateCanvasAsync(new CreateCanvasDto()));
            _canvasController.ModelState.AddModelError("Fields", "Required");
            //Act
            var response = await _canvasController.CreateCanvas(new CreateCanvasDto());

            //Assert
            Assert.NotNull(response);
            Assert.IsType<BadRequestObjectResult>(response);
        }

        [Fact]
        public async void DeleteCanvas_ValidId_ReturnsOkObjectResult()
        {
            //Arrange
            var id = Guid.NewGuid().ToString();
            _canvasService.Setup(cfg => cfg.DeleteCanvasByIdAsync(id));
            //Act
            var response = await _canvasController.DeleteCanvasById(id);

            //Assert
            Assert.NotNull(response);
            Assert.IsType<OkObjectResult>(response);
        }

        [Fact]
        public async void DeleteCanvas_InvalidId_ReturnBadRequestObjectResult()
        {
            //Arrange
            _canvasService.Setup(cfg => cfg.DeleteCanvasByIdAsync(null));

            //Act
            var response = await _canvasController.DeleteCanvasById(null);

            //Assert
            Assert.NotNull(response);
            Assert.IsType<BadRequestObjectResult>(response);
        }
    }
}
