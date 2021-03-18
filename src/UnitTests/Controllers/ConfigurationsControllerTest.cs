using AutoFixture;
using Microsoft.AspNetCore.Mvc;
using Moq;
using POC.BLL.Models;
using POC.BLL.Services;
using POC.Web.Controllers;
using Xunit;

namespace UnitTests.Controllers
{
    public class ConfigurationsControllerTest
    {
        private readonly ConfigurationController _cfgController;
        private readonly Mock<IConfigurationService<EmailServiceConfig>> _emailConfig;
        private readonly Mock<IConfigurationService<InstagramServiceConfig>> _instaConfig;
        private readonly Fixture _fixture;

        public ConfigurationsControllerTest()
        {
            _emailConfig = new Mock<IConfigurationService<EmailServiceConfig>>();
            _instaConfig = new Mock<IConfigurationService<InstagramServiceConfig>>();
            _cfgController = new ConfigurationController(_emailConfig.Object, _instaConfig.Object);
            _fixture = new Fixture();
        }

        [Fact]
        public async void GetEmailConfig_ConfigExist_ReturnsOkObjectResult()
        {
            //Arrange
            var settings = _fixture.Create<EmailServiceConfig>();
            _emailConfig.Setup(cfg => cfg.GetSettingsAsync()).ReturnsAsync(settings);

            //Act
            var response = await _cfgController.GetEmailConfig();

            //Assert
            Assert.NotNull(response);
            var okResult = Assert.IsType<OkObjectResult>(response.Result);
            var result = Assert.IsType<EmailServiceConfig>(okResult.Value);
            Assert.Equal(settings, result);
        }

        [Fact]
        public async void GetEmailConfig_ConfigDontExist_ReturnNoContentResult()
        {
            //Arrange
            _emailConfig.Setup(cfg => cfg.GetSettingsAsync());

            //Act
            var response = await _cfgController.GetEmailConfig();

            //Assert
            Assert.NotNull(response);
            Assert.IsType<NoContentResult>(response.Result);
        }

        [Fact]
        public async void SetEmailConfig_ModelValid_ReturnsOkObjectResult()
        {
            //Arrange
            var settings = _fixture.Create<EmailServiceConfig>();
            _emailConfig.Setup(cfg => cfg.UpdateSettingsAsync(settings));

            //Act
            var response = await _cfgController.SetEmailConfig(settings);

            //Assert
            Assert.NotNull(response);
            Assert.IsType<OkObjectResult>(response);
        }

        [Fact]
        public async void SetEmailConfig_ModelIsNotValid_BadRequestObjectResult()
        {
            //Arrange
            var settings = new EmailServiceConfig();
            _emailConfig.Setup(cfg => cfg.UpdateSettingsAsync(settings));
            _cfgController.ModelState.AddModelError("ModelState", "Error");
            //Act
            var response = await _cfgController.SetEmailConfig(settings);

            //Assert
            Assert.NotNull(response);
            Assert.IsType<BadRequestObjectResult>(response);
        }
    }
}
