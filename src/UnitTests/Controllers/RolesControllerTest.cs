using AutoFixture;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using POC.BLL.Models;
using POC.BLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using WEB.Controllers;
using Xunit;

namespace UnitTests.Controllers
{
    public class RolesControllerTest
    {
        private readonly Mock<IRolesService> _rolesService;
        private readonly RolesController _rolesController;
        private readonly Fixture _fixture;

        public RolesControllerTest()
        {
            _rolesService = new Mock<IRolesService>();
            _rolesController = new RolesController(_rolesService.Object);
            _fixture = new Fixture();
        }

        [Fact]
        public void GetAvailableRoles_RolesExist_ReturnsOkObjectResult()
        {
            //Arrange
            var roles = _fixture.CreateMany<IdentityRole>().ToList();
            _rolesService.Setup(cfg => cfg.GetRoles()).Returns(roles);

            //Act
            var response = _rolesController.GetAvailableRoles();

            //Assert
            Assert.NotNull(response);
            var okResult = Assert.IsType<OkObjectResult>(response.Result);
            var result = Assert.IsType<List<IdentityRole>>(okResult.Value);
            Assert.Equal(roles, result);
        }

        [Fact]
        public void GetAvailableRoles_RolesDontExist_ReturnsNoContentResult()
        {
            //Arrange
            var roles = new List<IdentityRole>();
            _rolesService.Setup(cfg => cfg.GetRoles()).Returns(roles);

            //Act
            var response = _rolesController.GetAvailableRoles();

            //Assert
            Assert.NotNull(response);
            Assert.IsType<NoContentResult>(response.Result);
        }

        [Fact]
        public async void GetUserRoles_RolesExist_ReturnsOkObjectResult()
        {
            //Arrange
            var roles = _fixture.CreateMany<string>().ToList();
            var id = Guid.NewGuid().ToString();
            _rolesService.Setup(cfg => cfg.GetUserRolesAsync(id)).ReturnsAsync(roles);

            //Act
            var response = await _rolesController.GetUserRoles(id);

            //Assert
            Assert.NotNull(response);
            var okResult = Assert.IsType<OkObjectResult>(response.Result);
            var result = Assert.IsType<List<string>>(okResult.Value);
            Assert.Equal(roles, result);
        }

        [Fact]
        public async void GetUserRoles_RolesDoNotExist_ReturnsNoContentResult()
        {
            //Arrange
            var roles = new List<string>();
            var id = Guid.NewGuid().ToString();
            _rolesService.Setup(cfg => cfg.GetUserRolesAsync(id)).ReturnsAsync(roles);

            //Act
            var response = await _rolesController.GetUserRoles(id);

            //Assert
            Assert.NotNull(response);
            Assert.IsType<NoContentResult>(response.Result);
        }

        [Fact]
        public async void GetUserRoles_InvalidId_ReturnsBadRequestObjectResult()
        {
            //Arrange
            var roles = _fixture.CreateMany<string>().ToList();
            string id = null;
            _rolesService.Setup(cfg => cfg.GetUserRolesAsync(id)).ReturnsAsync(roles);

            //Act
            var response = await _rolesController.GetUserRoles(id);

            //Assert
            Assert.NotNull(response);
            Assert.IsType<BadRequestObjectResult>(response.Result);
        }

        [Fact]
        public async void EditUserRoles_ResultSucceeded_ReturnsOkObjectResult()
        {
            //Arrange
            var model = _fixture.Create<EditRolesModel>();
            var identityResult = new EditUserRolesResultModel() { Succeeded = true, Errors = new List<IdentityError>() };
            _rolesService.Setup(cfg => cfg.EditUserRolesAsync(model)).ReturnsAsync(identityResult);

            //Act
            var response = await _rolesController.EditUserRoles(model);

            //Assert
            Assert.NotNull(response);
            var okResult = Assert.IsType<OkObjectResult>(response.Result);
            var result = Assert.IsType<EditUserRolesResultModel>(okResult.Value);
            Assert.Equal(identityResult, result);
        }

        [Fact]
        public async void EditUserRoles_ResultFail_ReturnsBadRequestObjectResult()
        {
            //Arrange
            var model = _fixture.Create<EditRolesModel>();
            var identityResult = new EditUserRolesResultModel() { Succeeded = false, Errors = new List<IdentityError>() };
            _rolesService.Setup(cfg => cfg.EditUserRolesAsync(model)).ReturnsAsync(identityResult);

            //Act
            var response = await _rolesController.EditUserRoles(model);

            //Assert
            Assert.NotNull(response);
            Assert.IsType<BadRequestObjectResult>(response.Result);
        }
    }
}
