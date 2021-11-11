using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using OnlineStore.DataAccess.Tests.IdentityApiTests.FakeIdentityManagers;
using OnlineStore.IdentityApi;
using OnlineStore.IdentityApi.Controllers;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace OnlineStore.DataAccess.Tests.IdentityApiTests
{
    /// <summary>
    /// AuthenticateController tests.
    /// </summary>
    public class AuthenticateControllerTests
    {
        /// <summary>
        /// AuthenticateController object.
        /// </summary>
        private AuthenticateController authenticateController;

        /// <summary>
        /// Configuration mock object.
        /// </summary>
        private Mock<IConfiguration> _configuration;

        /// <summary>
        /// UserManager mock object.
        /// </summary>
        private Mock<UserManager<ApplicationUser>> userManager;

        /// <summary>
        /// Tests constructor.
        /// </summary>
        public AuthenticateControllerTests()
        {
            var fakeIdentityManager = new FakeIdentityManager();
            userManager = fakeIdentityManager.GetMockUserManager(); 
           
            var roleManager = fakeIdentityManager.GetMockRoleManager();
            _configuration = new Mock<IConfiguration>();
            
            authenticateController = new AuthenticateController(userManager.Object, roleManager.Object, _configuration.Object);
        }

        /// <summary>
        /// Testing success login method.
        /// </summary>
        [Fact]
        public async Task Login_ReturnsResponceWithSucces()
        {
            //Arrange
            userManager.Setup(userManager => userManager.FindByNameAsync(It.IsAny<string>()))
               .ReturnsAsync(new ApplicationUser { Id = "id", Email = "email", UserName = "User" });
            userManager.Setup(userManager => userManager.CheckPasswordAsync(It.IsAny<ApplicationUser>(), "User*123"))
                .ReturnsAsync(true);
            userManager.Setup(userManager => userManager.GetRolesAsync(It.IsAny<ApplicationUser>()))
                .ReturnsAsync(new List<string> { "User", "Admin" });
            _configuration.Setup(config => config["JWT:Secret"]).Returns("ByYM000OLlMQG6VVVp1OH7Xzyr7gHuw1qvUC5dcGt3SNM");

            var responce = await authenticateController.Login(new LoginModel { Username = "User", Password = "User*123"} );

            //Act
            var actualResponce = (OkObjectResult)responce;

            //Assert
            Assert.Equal(200, actualResponce.StatusCode);
        }

        /// <summary>
        /// Testing unauthorized login.
        /// </summary>
        [Fact]
        public async Task Login_ReturnsResponceWithUnauthorized()
        {
            //Arrange
            userManager.Setup(userManager => userManager.FindByNameAsync(It.IsAny<string>()))
               .ReturnsAsync(new ApplicationUser { Id = "id", Email = "email", UserName = "User" });
            userManager.Setup(userManager => userManager.CheckPasswordAsync(It.IsAny<ApplicationUser>(), "User*123"))
                .ReturnsAsync(false);

            var responce = await authenticateController.Login(new LoginModel { Username = "User", Password = "User*123" });

            //Act
            var actualResponce = (UnauthorizedResult)responce;

            //Assert
            Assert.Equal(401, actualResponce.StatusCode);
        }

        /// <summary>
        /// Testing success register.
        /// </summary>
        [Fact]
        public async Task Register_ReturnsResponceWithSucces()
        {
            //Arrange
            userManager.Setup(userManager => userManager.CreateAsync(It.IsAny<ApplicationUser>(), "User*123"))
                .ReturnsAsync(IdentityResult.Success);
            var responce = await authenticateController.Register(new RegisterModel { Username = "User", Password = "User*123", Email= "Email123@gmail.com" });

            //Act
            var actualResponce = (ObjectResult)responce;

            //Assert
            Assert.Equal(200, actualResponce.StatusCode);
        }

        /// <summary>
        /// Testing error when registering.
        /// </summary>
        [Fact]
        public async Task Register_ReturnsResponceWithError()
        {
            //Arrange
            userManager.Setup(userManager => userManager.FindByNameAsync(It.IsAny<string>()))
               .ReturnsAsync(new ApplicationUser { Id = "id", Email = "Email123@gmail.com", UserName = "User" });
            userManager.Setup(userManager => userManager.CreateAsync(It.IsAny<ApplicationUser>(), "User*123"))
                .ReturnsAsync(IdentityResult.Failed());

            var responce = await authenticateController.Register(new RegisterModel { Username = "User", Password = "User*123", Email = "Email123@gmail.com" });

            //Act
            var actualResponce = (ObjectResult)responce;

            //Assert
            Assert.Equal(500, actualResponce.StatusCode);
        }
    }
}
