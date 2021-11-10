﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using OnlineStore.DataAccess.Tests.IdentityApiTests.FakeIdentityManagers;
using OnlineStore.IdentityApi;
using OnlineStore.IdentityApi.Controllers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace OnlineStore.DataAccess.Tests.IdentityApiTests
{

    public class UsersInfoControllerTests
    {
        private UsersInfoController _usersInfoController;

        public UsersInfoControllerTests()
        {
            var fakeIdentityManager = new FakeIdentityManager();
            var userManager = fakeIdentityManager.GetMockUserManager();
            
            userManager.Setup(userManager => userManager.FindByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(new ApplicationUser { Id = "id", Email = "email", UserName = "user" });
            userManager.Setup(userManager => userManager.Users)
               .Returns(GetFakeApplicationUsers().AsQueryable());

            var roleManager = fakeIdentityManager.GetMockRoleManager();

            roleManager.Setup(roleManager => roleManager.Roles)
                .Returns(GetFakeIdentityRoles().AsQueryable());
            _usersInfoController = new UsersInfoController(userManager.Object, roleManager.Object); 
        }

        [Fact]
        public void GetAllUsers_ReturnsResponseWithUsers()
        {
            //Arrange
            var users = _usersInfoController.GetAllUsers();
            var usersObjResult = (OkObjectResult)users.Result;
            var usersValue = usersObjResult.Value;

            //Act
            var actual = usersValue as List<UserModel>;

            //Assert
            Assert.Equal(GetFakeApplicationUsers().Count, actual.Count);
        }

        [Fact]
        public async Task GetUserById_ReturnsResponceWithUser()
        {
            //Arrange
            var user = await _usersInfoController.GetUserById(It.IsAny<string>());
            var userObjResult = (OkObjectResult)user.Result;
            var userValue = userObjResult.Value;

            //Act
            var actual = userValue as UserModel;

            //Assert
            Assert.Equal("user" , actual.Username);
        }

        [Fact]
        public void GetAllRoles_ReturnsResponceWithAllRoles()
        {
            //Arrange
            var roles = _usersInfoController.GetAllRoles();
            var rolesObjResult = (OkObjectResult)roles.Result;
            var rolesValue = rolesObjResult.Value;

            //Act
            var actual = rolesValue as IEnumerable<string>;

            //Assert
            Assert.Equal(GetFakeIdentityRoles().Count, actual.Count());
        }

        [Fact]
        public async Task UserUpdating_ReturnsResponceWithSucces()
        {
            //Arrange
            var user = await _usersInfoController.UserUpdating(new UserModel {Id = "id", Email="email", Roles = new List<string> { "user", "admin"}, Username= "user" });
            var userObjResult = (OkObjectResult)user;
            var userValue = userObjResult.Value;

            //Act
            var actual = userValue as Response;

            //Assert
            Assert.Equal("Success", actual.Status);
        }

        public List<ApplicationUser> GetFakeApplicationUsers()
        {
            var users = new List<ApplicationUser>
             {
                  new ApplicationUser() { Id = "", Email = "", UserName = ""  },
                  new ApplicationUser() {  Id = "", Email = "", UserName = ""  }
             };
            return users;
        }

        public List<IdentityRole> GetFakeIdentityRoles()
        {
            var roles = new List<IdentityRole>
            {
                new IdentityRole { Id = "id", Name = "user" },
                new IdentityRole { Id = "id2", Name = "admin" }
            };
            return roles;
        }
    }
}