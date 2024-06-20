using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using Models;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Identity;
using AutoMapper;
using Services.SmtpService;
using Danweyne_Real_estate.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Security.Claims;
using Models.View_Model;
using Microsoft.AspNetCore.Authentication;
using Danweyne_Real_estate.Models.Mapper;
using Danweyne_Real_estate.Models;
using Models.Database_Models;
using Microsoft.Extensions.Options;
using NuGet.Protocol;
using System.Security.Principal;



namespace UnitTestRealStateProject
{
    public class AccountControllerTest
    {

        private readonly Mock<IMapper> _mapper;
        private readonly Mock<UserManager<ApplicationUser>> _userManager;
        private readonly Mock<IEmailService> _emailService;
        private readonly Mock<SignInManager<ApplicationUser>> _signInManager;
        private readonly Mock<INotyfService> _notyf;
        private readonly AccountController accountController;
        private readonly Mock<HttpContext> _mockHttpContext;
        private readonly Mock<ClaimsPrincipal> _mockUser;
        private readonly Mock<ITempDataDictionary> _mockTempData;
        private readonly AccountController accountController1;
        private readonly Mock<IIdentity> _mockIdentityUser;

        public AccountControllerTest()
        {
            _mapper = new Mock<IMapper>();

            _userManager = new Mock<UserManager<ApplicationUser>>(
                Mock.Of<IUserStore<ApplicationUser>>(),
                null, null, null, null, null, null, null, null);

            _emailService = new Mock<IEmailService>();

            _signInManager = new Mock<SignInManager<ApplicationUser>>(
                _userManager.Object,
                Mock.Of<IHttpContextAccessor>(),
                Mock.Of<IUserClaimsPrincipalFactory<ApplicationUser>>(),
                null, null, null, null);

            _notyf = new Mock<INotyfService>();
            _mockIdentityUser  = new Mock<IIdentity>();

            _mockHttpContext = new Mock<HttpContext>();
            _mockUser = new Mock<ClaimsPrincipal>();
            _mockTempData = new Mock<ITempDataDictionary>();
            accountController = new AccountController(_mapper.Object, _userManager.Object, _notyf.Object, _emailService.Object, _signInManager.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = _mockHttpContext.Object
                },
                TempData = _mockTempData.Object
            };

            
            _mockHttpContext.Setup(c => c.User).Returns(_mockUser.Object);
            
            _mockTempData.Setup(t => t.Peek("UserRole")).Returns("Admin");

            _mockIdentityUser.Setup(a => a.IsAuthenticated).Returns(true);


        }

        #region  //  Test Method for Index() 

        [Fact]
        public void Index_ReturnsView_WhenUserIsAuthenticated()
        {

            // Arrange
            _userManager.Setup(x => x.FindByNameAsync(It.IsAny<string>())).ReturnsAsync(new ApplicationUser());
           


            // Act
            var result = accountController.Index();

            // Assert
            Xunit.Assert.NotNull(result);
            Xunit.Assert.Equal("Index", result.ToString());
        }

       

        [Fact]
        public void Index_ReturnsErrorView_WhenExceptionIsThrown()
        {
            // Arrange
            _userManager.Setup(x => x.FindByNameAsync(It.IsAny<string>())).Throws(new Exception());

            // Act
            var result = accountController.Index();

            // Assert
            Xunit.Assert.NotNull(result);
            Xunit.Assert.Equal("Error", result.ToString());
        }

        [Fact]
        public void Index_SetsUserRole_ToUser_WhenUserIsNotAuthenticated()
        {
            // Arrange
            _userManager.Setup(x => x.FindByNameAsync(It.IsAny<string>())).ReturnsAsync((ApplicationUser)null);

            // Act
            accountController.Index();

            // Assert
            Xunit.Assert.Equal("User", accountController.TempData["UserRole"]);
        }

        [Fact]
        public void Index_SetsUserRole_ToUser_WhenUserHasNoRole()
        {
            // Arrange
            _userManager.Setup(x => x.FindByNameAsync(It.IsAny<string>())).ReturnsAsync(new ApplicationUser());
            _userManager.Setup(x => x.GetRolesAsync(It.IsAny<ApplicationUser>())).ReturnsAsync(Array.Empty<string>());

            // Act
            accountController.Index();

            // Assert
            Xunit.Assert.Equal("User", accountController.TempData["UserRole"]);
        }

        #endregion

        #region // Test Method for Resistration 
        [Fact]
        public async Task Registering_Success_RedirectsToThankyou_And_AssignsRole()
        {
            // Arrange


            var userRegistrationModel = new UserRegistrationModel
            {
                Email = "test@example.com",
                Password = "Test123456",
                UserRole = "Admin",
                ConfirmPassword = "1234",
                FirstName="demo",
                LastName="last"
            };


            

            var applicationUser = new ApplicationUser { Email = userRegistrationModel.Email, EmailConfirmed = true  };
            _userManager.Setup(um => um.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
               .ReturnsAsync(IdentityResult.Success);
            _userManager.Setup(um => um.AddToRoleAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
               .ReturnsAsync(IdentityResult.Success);


            // Act
            var result = await accountController.Registering(userRegistrationModel);

            // Assert
            var redirectResult = Xunit.Assert.IsType<RedirectToActionResult>(result);
            Xunit.Assert.Equal("Thankyou", redirectResult.ActionName);
            _userManager.Verify(um => um.AddToRoleAsync(applicationUser, userRegistrationModel.UserRole), Times.Once);
            _notyf.Setup(a => a.Success("Registered Sucessfully", null));
        }
        [Fact]
        public async Task Registering_Failure_DoesNotRedirectAndShowsErrorMessage()
        {
            // Arrange
            var mockMapper = new Mock<IMapper>();
            var mockUserManager = new Mock<UserManager<ApplicationUser>>();
            var mockEmailService = new Mock<IEmailService>();
            var mockSignInManager = new Mock<SignInManager<ApplicationUser>>();
            var mockNotyfService = new Mock<INotyfService>();

            var userRegistrationModel = new UserRegistrationModel
            {
                Email = "test@example.com",
                Password = "Test123456",
                UserRole = "Admin"
            };

            var applicationUser = new ApplicationUser { Email = userRegistrationModel.Email };
            _userManager.Setup(um => um.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
               .ReturnsAsync(IdentityResult.Failed(new IdentityError { Description = "Registration failed" }));

           
            // Act
            var result = await accountController.Registering(userRegistrationModel);

            // Assert
            var viewResult = Xunit.Assert.IsType<ViewResult>(result);
            Xunit.Assert.Equal("Error", viewResult.ViewName);
            _notyf.Setup(a => a.Success("Something Went Wrong Please Contact Admin", null));
        }



        [Fact]
        public async Task Registering_RoleAssignmentFailure_DoesNotRedirectAndShowsErrorMessage()
        {
            // Arrange
            var mockMapper = new Mock<IMapper>();
            var mockUserManager = new Mock<UserManager<ApplicationUser>>();
            var mockEmailService = new Mock<IEmailService>();
            var mockSignInManager = new Mock<SignInManager<ApplicationUser>>();
            var mockNotyfService = new Mock<INotyfService>();

            var userRegistrationModel = new UserRegistrationModel
            {
                Email = "test@example.com",
                Password = "Test123456",
                UserRole = "Admin"
            };

            var applicationUser = new ApplicationUser { Email = userRegistrationModel.Email };
            mockUserManager.Setup(um => um.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
               .ReturnsAsync(IdentityResult.Success);
            mockUserManager.Setup(um => um.AddToRoleAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
               .ReturnsAsync(IdentityResult.Failed(new IdentityError { Description = "Role assignment failed" }));

         

            // Act
            var result = await accountController.Registering(userRegistrationModel);

            // Assert
            var viewResult = Xunit.Assert.IsType<ViewResult>(result);
            Xunit.Assert.Equal("Error", viewResult.ViewName);
            _notyf.Setup(a => a.Success("Something Went Wrong Please Contact Admin", null));

        }

        [Fact]
        public async Task Registering_EmailAlreadyExists_DoesNotRedirectAndShowsErrorMessage()
        {
            // Arrange
            var mockMapper = new Mock<IMapper>();
            var mockUserManager = new Mock<UserManager<ApplicationUser>>();
            var mockEmailService = new Mock<IEmailService>();
            var mockSignInManager = new Mock<SignInManager<ApplicationUser>>();
            var mockNotyfService = new Mock<INotyfService>();

            var userRegistrationModel = new UserRegistrationModel
            {
                Email = "test@example.com",
                Password = "Test123456",
                UserRole = "Admin"
            };

            var applicationUser = new ApplicationUser { Email = userRegistrationModel.Email };
            mockUserManager.Setup(um => um.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
               .ReturnsAsync(IdentityResult.Failed(new IdentityError { Description = "Email already exists" }));

           

            // Act
            var result = await accountController.Registering(userRegistrationModel);

            // Assert
            var redirectResult = Xunit.Assert.IsType<RedirectToActionResult>(result);
            Xunit.Assert.Equal("Index", redirectResult.ActionName);
            //TempDataAssert.Contains(result, "RegisterSucessMessage", "Email does not exist please enter correct email");
        }



       

        #endregion
        #region    // Test Method for Login
        [Fact]
        public async void Login_Success()
        {
            // Arrange
            var userLoginModel = new UserLoginModel { Email = "test@example.com", Password = "password123" };
            var user = new ApplicationUser { Email = "test@example.com", LockoutEnabled = true  , UserName="test" };
            _userManager.Setup(um => um.FindByEmailAsync(userLoginModel.Email)).ReturnsAsync(user);
            _userManager.Setup(um => um.CheckPasswordAsync(user, userLoginModel.Password)).ReturnsAsync(true);
            _userManager.Setup(um => um.GetRolesAsync(user)).ReturnsAsync(new[] { "User" });
             

            // Act
            var result = await accountController.Login(userLoginModel);

            // Assert
            var redirectResult =  Xunit.Assert.IsType<RedirectToActionResult>(result);
            Xunit.Assert.Equal("Index", redirectResult.ActionName);
            Xunit.Assert.Equal("Home", redirectResult.ControllerName);
            Xunit.Assert.Equal("User", accountController.TempData["UserRole"]);
        }



        [Fact]
        public async void Login_InvalidEmail()
        {
            // Arrange
            var userLoginModel = new UserLoginModel { Email = "invalid@example.com", Password = "password123" };
            _userManager.Setup(um => um.FindByEmailAsync(userLoginModel.Email)).ReturnsAsync((ApplicationUser)null);

            // Act
            var result = await accountController.Login(userLoginModel);

            // Assert
            var redirectResult = Xunit.Assert.IsType<RedirectToActionResult>(result);
            Xunit.Assert.Equal("Index", redirectResult.ActionName);
            _notyf.Setup(a => a.Success("Invalid Email", null));
        }



        [Fact]
        public async void Login_InvalidPassword()
        {
            // Arrange
            var userLoginModel = new UserLoginModel { Email = "test@example.com", Password = "invalid" };
            var user = new ApplicationUser { Email = "test@example.com" };
            _userManager.Setup(um => um.FindByEmailAsync(userLoginModel.Email)).ReturnsAsync(user);
            _userManager.Setup(um => um.CheckPasswordAsync(user, userLoginModel.Password)).ReturnsAsync(false);

            // Act
            var result = await accountController.Login(userLoginModel);

            // Assert
            var redirectResult = Xunit.Assert.IsType<RedirectToActionResult>(result);
            Xunit.Assert.Equal("Index", redirectResult.ActionName);
            _notyf.Setup(a => a.Success("Invalid Passowrd", null));

        }

        
       

        [Fact]
        public async void Login_Exception()
        {
            // Arrange
            var userLoginModel = new UserLoginModel { Email = "test@example.com", Password = "password123" };
            _userManager.Setup(um => um.FindByEmailAsync(userLoginModel.Email)).ThrowsAsync(new Exception());

            // Act
            var result = await accountController.Login(userLoginModel);

            // Assert
            var viewResult = Xunit.Assert.IsType<ViewResult>(result);
            Xunit.Assert.Equal("Error", viewResult.ViewName);
        }
        #endregion


        #region // Test Method for  YourProfile

        [Fact]
        public async Task YourProfile_PopulatesAddressAndPhoneNumber()
        {

            // Act
            var result = await accountController.YourProfile();

            // Assert
            var viewResult = Xunit.Assert.IsType<ViewResult>(result);
            var model = Xunit.Assert.IsType<ProfileViewModel>(viewResult.Model);
            Xunit.Assert.Equal(model.PhoneNumber, "1234567890");
            Xunit.Assert.Equal(model.Address, "123 Main St");
        }


        [Fact]
        public async Task YourProfile_RedirectsToIndex_WhenUserNotFound()
        {
            // Arrange
            _userManager.Setup(um => um.FindByIdAsync(It.IsAny<string>()))
                .ReturnsAsync((ApplicationUser)null);

            // Act
            var result = await accountController.YourProfile();

            // Assert
            var redirectResult = Xunit.Assert.IsType<RedirectToActionResult>(result);
            Xunit.Assert.Equal("Index", redirectResult.ActionName);
            Xunit.Assert.Equal("Account", redirectResult.ControllerName);
        }



        [Fact]
        public async Task YourProfile_HandlesException()
        {
            // Arrange
            _userManager.Setup(um => um.FindByIdAsync(It.IsAny<string>()))
                .ThrowsAsync(new Exception());

            // Act
            var result = await accountController.YourProfile();

            // Assert
            var viewResult = Xunit.Assert.IsType<ViewResult>(result);
            Xunit.Assert.Equal("Error", viewResult.ViewName);
        }

        #endregion

        #region // forgot password
       
       

       
        [Fact]
        public void Test_ForgotPassword_InvalidEmail_ReturnsView()
        {
            // Arrange
            
            ForgotPasswordViewModel invalidEmailModel = new ForgotPasswordViewModel { Email = "invalid_email@example.com" };

            // Act
            var result = accountController.ForgotPassword(invalidEmailModel);

            // Assert
            _notyf.Setup(a => a.Success("invalid mail", null));
        }


        [Fact]
        public async Task Test_ForgotPassword_ValidEmail_RedirectsToConfirmation()
        {
            // Arrange
            
            ForgotPasswordViewModel validEmailModel = new ForgotPasswordViewModel { Email = "valid_email@example.com" };

            // Act
            var result = await accountController.ForgotPassword(validEmailModel);

            // Assert
            var redirectResult = Xunit.Assert.IsType<RedirectToActionResult>(result);
            Xunit.Assert.Equal("ForgotPasswordConfirmation", redirectResult.ActionName);
        }



        [Fact]
        public async Task Test_ForgotPassword_UserNotFound_ReturnsView()
        {
            // Arrange
            var mockUserManager = new Mock<UserManager<ApplicationUser>>(new Mock<IUserStore<ApplicationUser>>().Object, null, null, null, null);
            mockUserManager.Setup(um => um.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync((ApplicationUser)null);
            
            ForgotPasswordViewModel nonExistentEmailModel = new ForgotPasswordViewModel { Email = "non_existent_email@example.com" };

            // Act
            var result = await accountController.ForgotPassword(nonExistentEmailModel);

            // Assert
            var viewResult = Xunit.Assert.IsType<ViewResult>(result);
            Xunit.Assert.IsType<ForgotPasswordViewModel>(viewResult.Model);
        }

        #endregion


        #region   // change password
        [Fact]
        public async Task ChangePassword_Success_RedirectsToProfile()
        {
            // Arrange
            var mockUserManager = new Mock<UserManager<ApplicationUser>>();
            var mockSignInManager = new Mock<SignInManager<ApplicationUser>>();
            var mockNotyfService = new Mock<INotyfService>();
            

            var user = new ApplicationUser { Id = "1", UserName = "testUser", Email = "test@example.com" };

            _userManager.Setup(um => um.GetRolesAsync(user)).ReturnsAsync(new[] { "User" });



            var model = new ChangePasswordModel { OldPassword = "oldPassword", NewPassword = "newPassword", ConfirmPassword = "newPassword" };

            // Act
            var result = await accountController.ChangePassword(model);

            // Assert
            var redirectResult = Xunit.Assert.IsType<RedirectToActionResult>(result);
            Xunit.Assert.Equal("ChangePassword", redirectResult.ActionName);
            _notyf.Verify(ns => ns.Success("Password changed successfully",null));
        }

        #endregion




        #region // enable user   Date  17/06/2024
        [Fact]
        public async Task EnableUser_EnablesUser_WhenUserExists()
        {
            // Arrange
            var userId = "12345";
            var user = new ApplicationUser { Id = userId, LockoutEnabled = false, LockoutEnd = DateTimeOffset.MaxValue };
            _userManager.Setup(x => x.FindByIdAsync(userId)).ReturnsAsync(user);

            // Act
            var result = await accountController.EnableUser(userId);

            // Assert
            _userManager.Verify(x => x.UpdateAsync(It.Is<ApplicationUser>(u => u.Id == userId && u.LockoutEnabled && u.LockoutEnd == null)), Times.Once);
            var redirectResult = Xunit.Assert.IsType<RedirectToActionResult>(result);
            Xunit.Assert.Equal("Users", redirectResult.ActionName);
            _notyf.Verify(ns => ns.Success("The user is enabled", null));

        }


        [Fact]
        public async Task EnableUser_DoesNotEnableUser_WhenUserDoesNotExist()
        {
            // Arrange
            var userId = "12345";
            _userManager.Setup(x => x.FindByIdAsync(userId)).ReturnsAsync((ApplicationUser)null);

            // Act
            var result = await accountController.EnableUser(userId);

            // Assert
            _userManager.Verify(x => x.UpdateAsync(It.IsAny<ApplicationUser>()), Times.Never);
            var redirectResult = Xunit.Assert.IsType<RedirectToActionResult>(result);
            Xunit.Assert.Equal("Users", redirectResult.ActionName);
            _notyf.Verify(x => x.Error("The user is  disabled", null));
        }


        [Fact]
        public async Task EnableUser_HandlesException_WhenUserExists()
        {
            // Arrange
            var userId = "12345";
            var user = new ApplicationUser { Id = userId, LockoutEnabled = false, LockoutEnd = DateTimeOffset.MaxValue };
            _userManager.Setup(x => x.FindByIdAsync(userId)).ReturnsAsync(user);
            _userManager.Setup(x => x.UpdateAsync(It.IsAny<ApplicationUser>())).Throws(new Exception());

            // Act
            var result = await accountController.EnableUser(userId);

            // Assert
            _userManager.Verify(x => x.UpdateAsync(It.Is<ApplicationUser>(u => u.Id == userId && u.LockoutEnabled && u.LockoutEnd == null)), Times.Once);
            var viewResult = Xunit.Assert.IsType<ViewResult>(result);
            Xunit.Assert.Equal("Error", viewResult.ViewName);
        }

      

        [Fact]
        public async Task EnableUser_HandlesEmptyUserId()
        {
            // Arrange
            string userId = "";

            // Act
            var result = await accountController.EnableUser(userId);

            // Assert
            var redirectResult = Xunit.Assert.IsType<RedirectToActionResult>(result);
            Xunit.Assert.Equal("Users", redirectResult.ActionName);
            _notyf.Verify(x => x.Error("The user is  disabled", null));
        }
        #endregion  


        #region // Disabled  User
        [Fact]
        public async Task DisableUser_Exception_ReturnsError()
        {
            // Arrange
           

            _userManager.Setup(um => um.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(new ApplicationUser());
            _userManager.Setup(um => um.UpdateAsync(It.IsAny<ApplicationUser>())).ThrowsAsync(new Exception("Database error"));

            // Act
            var result = await accountController.DisableUser("testUserId");

            // Assert
            var viewResult = Xunit.Assert.IsType<ViewResult>(result);
            Xunit.Assert.Equal("Error", viewResult.ViewName);
        }

        [Fact]
        public async Task DisableUser_ValidUserId_UserDisabled()
        {
            // Arrange
        
            _userManager.Setup(um => um.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(new ApplicationUser());
            _userManager.Setup(um => um.UpdateAsync(It.IsAny<ApplicationUser>())).ReturnsAsync(IdentityResult.Success);

            // Act
            var result = await accountController.DisableUser("testUserId");

            // Assert
            var redirectResult = Xunit.Assert.IsType<RedirectToActionResult>(result);
            Xunit.Assert.Equal("Users", redirectResult.ActionName);
            _notyf.Verify(n => n.Success("The user is disabled", null));
        }


    

        [Fact]
        public async Task DisableUser_UserIdNotFound_ReturnsError()
        {
            // Arrange
            
            _userManager.Setup(um => um.FindByIdAsync(It.IsAny<string>())).ReturnsAsync((ApplicationUser)null);

            // Act
            var result = await accountController.DisableUser("testUserId");

            // Assert
            var viewResult = Xunit.Assert.IsType<RedirectToActionResult>(result);
            Xunit.Assert.Equal("Error", viewResult.ActionName);
        }
        #endregion


        #region  // check test Method
        [Fact]
        public void Test_Check_Action_Returns_View()
        {
            // Arrange
            var controller = new AccountController(_mapper.Object, _userManager.Object, _notyf.Object, _emailService.Object, _signInManager.Object);

            // Act
            var result = controller.Check();

            // Assert
            Xunit.Assert.IsType<ViewResult>(result);
        }

        
        [Fact]
        public void Test_Check_Action_Does_Not_Return_Null()
        {
            // Arrange
            var controller = new AccountController(_mapper.Object, _userManager.Object, _notyf.Object, _emailService.Object, _signInManager.Object);

            // Act
            var result = controller.Check();

            // Assert
            Xunit.Assert.NotNull(result);
        }

        

        [Fact]
        public void Test_Check_Action_Is_Accessible_To_Authenticated_Users()
        {
            // Arrange
            var controller = new AccountController(_mapper.Object, _userManager.Object, _notyf.Object, _emailService.Object, _signInManager.Object);
            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = new ClaimsPrincipal(new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, "TestUser") }))
                }
            };

            // Act
            var result = controller.Check();

            // Assert
            Xunit.Assert.IsType<ViewResult>(result);
        }
        #endregion

        #region // Register Agent
        [Fact]
        public async Task RegisterAsAgent_Success()
        {
            // Arrange
            var userModel = new UserRegistrationModel { Email = "test@example.com", Password = "Test123456", UserRole = "Agent" };
            var userManagerMock = new Mock<UserManager<ApplicationUser>>(MockBehavior.Strict);
            var mapperMock = new Mock<IMapper>();
            var notyfMock = new Mock<INotyfService>();

            var identityUser = new ApplicationUser() { EmailConfirmed = true };
            userManagerMock.Setup(x => x.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
                             .ReturnsAsync(IdentityResult.Success);
            userManagerMock.Setup(x => x.AddToRoleAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
                             .ReturnsAsync(IdentityResult.Success);

            // Act
            var result = await accountController.RegisterAsAgent(userModel);

            // Assert
            var redirectResult = Xunit.Assert.IsType<RedirectToActionResult>(result);
            Xunit.Assert.Equal("Index", redirectResult.ActionName);
            Xunit.Assert.Equal("Home", redirectResult.ControllerName);
        }

        [Fact]
        public async void RegisterAsAgent_SetsTempData_OnSuccess()
        {
            // Arrange
            var userModel = new UserRegistrationModel { Email = "test@example.com", Password = "Test123456", UserRole = "Agent" };
            _userManager.Setup(um => um.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);
            _userManager.Setup(um => um.AddToRoleAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);
            var identityUser = new ApplicationUser() { EmailConfirmed = true };
            // Act
            var result = await accountController.RegisterAsAgent(userModel);

            // Assert
            var redirectResult = Xunit.Assert.IsType<RedirectToActionResult>(result);
            Xunit.Assert.Equal("Index", redirectResult.ActionName);
            Xunit.Assert.Equal("Registered Sucessfully", null);
        }

        

        [Fact]
        public async void RegisterAsAgent_DoesNotSetTempData_OnException()
        {
            // Arrange
            var userModel = new UserRegistrationModel { Email = "test@example.com", Password = "Test123456", UserRole = "Agent" };
            _userManager.Setup(um => um.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
                .ThrowsAsync(new Exception("Something went wrong"));
            var identityUser = new ApplicationUser() { EmailConfirmed = true };
            // Act
            var result = await accountController.RegisterAsAgent(userModel);

            // Assert
            var viewResult = Xunit.Assert.IsType<ViewResult>(result);
            Xunit.Assert.Equal("Error", viewResult.ViewName);
            Xunit.Assert.Null(accountController.TempData["RegisterSucessMessage"]);
        }

        

        
        #endregion


        #region // check old password
        [Fact]
        public async Task CheckOldPassword_ReturnsUserNotFound_WhenOldPasswordIsNull()
        {
            // Arrange
           
            _userManager.Setup(um => um.GetUserAsync(It.IsAny<ClaimsPrincipal>())).ReturnsAsync((ApplicationUser)null);

            // Act
            var result = await accountController.CheckOldPassword(null);

            // Assert
            var jsonResult = Xunit.Assert.IsType<JsonResult>(result);
            Xunit.Assert.Equal("User not found", jsonResult.Value);
        }


        [Fact]
        public async Task CheckOldPassword_ReturnsIncorrectPassword_WhenOldPasswordIsWrong()
        {
            // Arrange
           
            var user = new ApplicationUser { Id = "1", UserName = "testUser" };
            _userManager.Setup(um => um.GetUserAsync(It.IsAny<ClaimsPrincipal>())).ReturnsAsync(user);
            _userManager.Setup(um => um.CheckPasswordAsync(user, It.IsAny<string>())).ReturnsAsync(false);

            // Act
            var result = await accountController.CheckOldPassword("wrongPassword");

            // Assert
            var jsonResult = Xunit.Assert.IsType<JsonResult>(result);
            Xunit.Assert.Equal("Current password is incorrect", jsonResult.Value);
        }

        [Fact]
        public async Task CheckOldPassword_ReturnsTrue_WhenOldPasswordIsCorrect()
        {
            // Arrange

            var user = new ApplicationUser { Id = "1", UserName = "testUser" };
            _userManager.Setup(um => um.GetUserAsync(It.IsAny<ClaimsPrincipal>())).ReturnsAsync(user);
            _userManager.Setup(um => um.CheckPasswordAsync(user, It.IsAny<string>())).ReturnsAsync(true);

            // Act
            var result = await accountController.CheckOldPassword("correctPassword");

            // Assert
            var jsonResult = Xunit.Assert.IsType<JsonResult>(result);
            Xunit.Assert.True((bool)jsonResult.Value);

        }

        [Fact]
        public async Task CheckOldPassword_ReturnsUserNotFound_WhenUserIsNotAuthenticated()
        {
            // Arrange

            _userManager.Setup(um => um.GetUserAsync(It.IsAny<ClaimsPrincipal>())).ReturnsAsync((ApplicationUser)null);

            // Act
            var result = await accountController.CheckOldPassword("anyPassword");

            // Assert
            var jsonResult = Xunit.Assert.IsType<JsonResult>(result);
            Xunit.Assert.Equal("User not found", jsonResult.Value);
        }

        [Fact]
        public async Task CheckOldPassword_ReturnsIncorrectPassword_WhenUserIsAuthenticatedButPasswordVerificationFails()
        {
            // Arrange
            
            var user = new ApplicationUser { Id = "1", UserName = "testUser" };
            _userManager.Setup(um => um.GetUserAsync(It.IsAny<ClaimsPrincipal>())).ReturnsAsync(user);
            _userManager.Setup(um => um.CheckPasswordAsync(user, It.IsAny<string>())).ReturnsAsync(false);

            // Act
            var result = await accountController.CheckOldPassword("wrongPassword");

            // Assert
            var jsonResult = Xunit.Assert.IsType<JsonResult>(result);
            Xunit.Assert.Equal("Current password is incorrect", jsonResult.Value);
        }
        #endregion
    }


}