using Microsoft.AspNetCore.Identity;
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

    public class UsersInfoControllerTests
    {
        private Mock<FakeUserManager> _mockUserManager;

        private Mock<RoleManager<IdentityRole>> _mockRoleManager;

        private UsersInfoController _usersInfoController;

        private List<ApplicationUser> _users = new List<ApplicationUser>
             {
                  new ApplicationUser() { Id = "", Email = "", UserName = ""  },
                  new ApplicationUser() {  Id = "", Email = "", UserName = ""  }
             };

        public UsersInfoControllerTests()
        {
            _mockUserManager = new Mock<FakeUserManager>();
            _mockRoleManager = new Mock<RoleManager<IdentityRole>>();
            _usersInfoController = new UsersInfoController(_mockUserManager.Object, _mockRoleManager.Object); 
        }

        [Fact]
        public void GetAllUsersReturnsResponseWithUsers()
        {
            var users = _usersInfoController.GetAllUsers();

            Assert.Equal(users.Value.Count(), _users.Count());
        }
    }
}
