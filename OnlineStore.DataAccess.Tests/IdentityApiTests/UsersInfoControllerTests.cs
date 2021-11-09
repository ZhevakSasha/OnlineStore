using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using OnlineStore.IdentityApi;
using OnlineStore.IdentityApi.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace OnlineStore.DataAccess.Tests.IdentityApiTests
{
    public class FakeUserManager : UserManager<ApplicationUser>
    {
        public FakeUserManager()
            : base(new Mock<IUserStore<ApplicationUser>>().Object,
               new Mock<IOptions<IdentityOptions>>().Object,
               new Mock<IPasswordHasher<ApplicationUser>>().Object,
               new IUserValidator<ApplicationUser>[0],
               new IPasswordValidator<ApplicationUser>[0],
               new Mock<ILookupNormalizer>().Object,
               new Mock<IdentityErrorDescriber>().Object,
               new Mock<IServiceProvider>().Object,
               new Mock<ILogger<UserManager<ApplicationUser>>>().Object)
        { }
    }

    public class FakeRoleManager : RoleManager<IdentityRole>
    {
        public FakeRoleManager()
            : base(new Mock<IRoleStore<IdentityRole>>().Object,
                  new Mock<IEnumerable<IRoleValidator<IdentityRole>>>().Object,
                  new Mock<ILookupNormalizer>().Object,
                  new Mock<IdentityErrorDescriber>().Object,
                  new Mock<ILogger<RoleManager<IdentityRole>>>().Object)
        { }
    }

    public class UsersInfoControllerTests
    {
        private Mock<FakeUserManager> _mockUserManager;

        private Mock<RoleManager<IdentityRole>> _mockRoleManager;

        private UsersInfoController _usersInfoController;

        public UsersInfoControllerTests()
        {
            var roleStore = new Mock<IRoleStore<IdentityRole>>();
            var roleValidator = new Mock<IEnumerable<IRoleValidator<IdentityRole>>>();
            var lookupNormalizer = new Mock<ILookupNormalizer>();
            var identityErrorDescriber = new Mock<IdentityErrorDescriber>()  ;
            var logger = new Mock<ILogger<RoleManager<IdentityRole>>>();

            var _users = new List<ApplicationUser>
             {
                  new ApplicationUser() { Id = "", Email = "", UserName = ""  },
                  new ApplicationUser() {  Id = "", Email = "", UserName = ""  }
             }.AsQueryable();

            _mockUserManager = new Mock<FakeUserManager>();
            _mockUserManager.Setup(userManager => userManager.FindByIdAsync(It.IsAny<string>()))
               .ReturnsAsync(new ApplicationUser { Id = "", Email = "", UserName = "" });
            _mockUserManager.Setup(userManager => userManager.Users)
               .Returns(_users);
            _mockRoleManager = new Mock<RoleManager<IdentityRole>>(roleStore.Object, roleValidator.Object, lookupNormalizer.Object, identityErrorDescriber.Object, logger.Object);
            _usersInfoController = new UsersInfoController(_mockUserManager.Object, _mockRoleManager.Object); 
        }

        [Fact]
        public void GetAllUsersReturnsResponseWithUsers()
        {
            var _users = new List<ApplicationUser>
             {
                  new ApplicationUser() { Id = "", Email = "", UserName = ""  },
                  new ApplicationUser() {  Id = "", Email = "", UserName = ""  }
             }.AsQueryable();
            var users = _usersInfoController.GetAllUsers();

            Assert.Equal(users.Value.Count(), _users.Count());
        }


    }
}
