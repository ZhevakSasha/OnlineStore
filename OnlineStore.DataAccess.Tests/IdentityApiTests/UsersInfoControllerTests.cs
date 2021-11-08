using Microsoft.AspNetCore.Identity;
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
    public class UsersInfoControllerTests
    {
        
        public static Mock<UserManager<TUser>> MockUserManager<TUser>(List<TUser> ls) where TUser : class
        {
            var store = new Mock<IUserStore<TUser>>();
            var mgr = new Mock<UserManager<TUser>>(store.Object, null, null, null, null, null, null, null, null);
            mgr.Object.UserValidators.Add(new UserValidator<TUser>());
            mgr.Object.PasswordValidators.Add(new PasswordValidator<TUser>());

            mgr.Setup(x => x.DeleteAsync(It.IsAny<TUser>())).ReturnsAsync(IdentityResult.Success);
            mgr.Setup(x => x.CreateAsync(It.IsAny<TUser>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success).Callback<TUser, string>((x, y) => ls.Add(x));
            mgr.Setup(x => x.UpdateAsync(It.IsAny<TUser>())).ReturnsAsync(IdentityResult.Success);

            return mgr;
        }

        private Mock<UserManager<ApplicationUser>> _mockUserManager;

        private Mock<RoleManager<IdentityRole>> _mockRoleManager;

        private UsersInfoController _usersInfoController;

        private List<ApplicationUser> _users = new List<ApplicationUser>
             {
                  new ApplicationUser() { Id = "", Email = "", UserName = ""  },
                  new ApplicationUser() {  Id = "", Email = "", UserName = ""  }
             };
        public UsersInfoControllerTests()
        {
            _mockUserManager = new Mock<UserManager<ApplicationUser>>(_users);
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
