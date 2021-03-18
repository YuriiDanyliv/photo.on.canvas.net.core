using AutoFixture;
using Microsoft.AspNetCore.Mvc;
using Moq;
using POC.BLL.Dto;
using POC.BLL.Models;
using POC.BLL.Services;
using POC.Web.Controllers;
using Xunit;

namespace UnitTests.Controllers
{
    public class AccountControllerTest
    {
        private readonly Mock<IAccountService> _accountService;
        private readonly Mock<IEmailConfirmService> _emailConfirmService;
        private readonly Mock<IConfigurationService<EmailServiceConfig>> _configService;
        private readonly AccountController _accountController;
        private readonly Fixture _fixture;

        public AccountControllerTest()
        {
            _accountService = new Mock<IAccountService>();
            _emailConfirmService = new Mock<IEmailConfirmService>();
            _configService = new Mock<IConfigurationService<EmailServiceConfig>>();
            _fixture = new Fixture();
            _accountController = new AccountController(
                _accountService.Object,
                _emailConfirmService.Object,
                _configService.Object);
        }

        [Fact]
        public async void GetUsers_UsersExist_ReturnsOkObjectResult()
        {
            //Arrange
            var param = new UserQueryParam();
            var users = _fixture.Create<PagesListModel<UserDto>>();
            _accountService.Setup(cfg => cfg.GetUsersAsync(param)).ReturnsAsync(users);

            //Act
            var response = await _accountController.GetUsers(param);

            //Assert
            Assert.NotNull(response);
            var okResult = Assert.IsType<OkObjectResult>(response.Result);
            var result = Assert.IsType<PagesListModel<UserDto>>(okResult.Value);
            Assert.Equal(users, result);
        }

        [Fact]
        public async void GetUsers_UsersDontExist_ReturnsNoContentResult()
        {
            //Arrange
            var param = new UserQueryParam();
            var users = new PagesListModel<UserDto>();
            _accountService.Setup(cfg => cfg.GetUsersAsync(param)).ReturnsAsync(users);

            //Act
            var response = await _accountController.GetUsers(param);

            //Assert
            Assert.NotNull(response);
            Assert.IsType<NoContentResult>(response.Result);
        }
    }
}
