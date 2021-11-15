using Microsoft.AspNetCore.Identity;
using Moq;
using OnlineStore.IdentityApi;

namespace OnlineStore.DataAccess.Tests.IdentityApiTests.FakeIdentityManagers
{
    /// <summary>
    /// FakeIdentityManager class for creating mock objects of user and role managers.
    /// </summary>
    public class FakeIdentityManager
    {
        /// <summary>
        /// GetMockRoleManager method.
        /// </summary>
        /// <returns>Mock RoleManager object</returns>
        public Mock<RoleManager<IdentityRole>> GetMockRoleManager()
        {
            var roleStore = new Mock<IRoleStore<IdentityRole>>();
            return new Mock<RoleManager<IdentityRole>>(
                         roleStore.Object, null, null, null, null);
        }

        /// <summary>
        /// GetMockUserManager method.
        /// </summary>
        /// <returns>Mock UserManager object</returns>
        public Mock<UserManager<ApplicationUser>> GetMockUserManager()
        {
            var userStoreMock = new Mock<IUserStore<ApplicationUser>>();
            return new Mock<UserManager<ApplicationUser>>(
                userStoreMock.Object, null, null, null, null, null, null, null, null);
        }
    }
}
