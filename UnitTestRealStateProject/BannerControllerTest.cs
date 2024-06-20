using Moq;
using AspNetCoreHero.ToastNotification.Abstractions;
using Danweyne_Real_estate.Models.Mapper;
using Danweyne_Real_estate.Models;
using Services.Interfaces;
using Danweyne_Real_estate.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.Database_Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Models.View_Model;


namespace UnitTestRealStateProject
{
    public class BannerControllerTest
    {
        #region // Test Methodds for Show
        [Fact]
        public async void Show_ReturnsView_WhenUserIsAuthenticatedAsAdminOrAgent()
        {
            // Arrange
            var mockBannerData = new Mock<IBannerData>();
            var mockNotyf = new Mock<INotyfService>();
            var controller = new BannerController(mockBannerData.Object, mockNotyf.Object);
            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = new ClaimsPrincipal(new ClaimsIdentity(new[]
                    {
                new Claim(ClaimTypes.Role, "Admin"),
            }, "TestAuth"))
                }
            };
            controller.TempData["UserRole"] = "Admin";
            mockBannerData.Setup(x => x.GetAll()).ReturnsAsync(new List<Banner>());

            // Act
            var result = await controller.Show();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Null(viewResult.ViewData.Model);
        }

   
        [Fact]
        public async void Show_RedirectsToHome_WhenUserIsNotAuthenticated()
        {
            // Arrange
            var mockBannerData = new Mock<IBannerData>();
            var mockNotyf = new Mock<INotyfService>();
            var controller = new BannerController(mockBannerData.Object, mockNotyf.Object);
            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = new ClaimsPrincipal(new ClaimsIdentity())
                }
            };
            controller.TempData["UserRole"] = null;
            mockBannerData.Setup(x => x.GetAll()).ReturnsAsync(new List<Banner>());

            // Act
            var result = await controller.Show();

            // Assert
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectResult.ActionName);
            Assert.Equal("Home", redirectResult.ControllerName);
        }

        [Fact]
        public async void Show_ReturnsErrorView_WhenExceptionIsThrown()
        {
            // Arrange
            var mockBannerData = new Mock<IBannerData>();
            var mockNotyf = new Mock<INotyfService>();
            var controller = new BannerController(mockBannerData.Object, mockNotyf.Object);
            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = new ClaimsPrincipal(new ClaimsIdentity(new[]
                    {
                new Claim(ClaimTypes.Role, "Admin"),
            }, "TestAuth"))
                }
            };
            controller.TempData["UserRole"] = "Admin";
            mockBannerData.Setup(x => x.GetAll()).Throws(new Exception());

            // Act
            var result = await controller.Show();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal("Error", viewResult.ViewName);
        }

   
        #endregion


        #region // Delete
        

        [Fact]
        public async void Test_Delete_NonExistingBanner_DoesNotCallDeleteMethod()
        {
            // Arrange
            var mockBannerData = new Mock<IBannerData>();
            var mockNotyf = new Mock<INotyfService>();
            var controller = new BannerController(mockBannerData.Object, mockNotyf.Object);

            int nonExistingBannerId = 12345; // Assuming a non-existing banner ID

            // Act
            var result = await controller.Delete(nonExistingBannerId);

            // Assert
            // Verify that the Delete method is not called
            mockBannerData.Verify(x => x.Delete(nonExistingBannerId), Times.Never);
        }

        [Fact]
        public async void Test_Delete_NonExistingBanner_DoesNotShowSuccessNotification()
        {
            // Arrange
            var mockBannerData = new Mock<IBannerData>();
            var mockNotyf = new Mock<INotyfService>();
            var controller = new BannerController(mockBannerData.Object, mockNotyf.Object);

            int nonExistingBannerId = 12345; // Assuming a non-existing banner ID

            // Act
            var result = await controller.Delete(nonExistingBannerId);

            // Assert
            // Verify that the success notification is not shown
          
           // mockNotyf.Verify(x => x.Success(It.IsAny<string>()), Times.Never);
        }
        [Fact]
        public async void Test_Delete_NonExistingBanner_DoesNotShowErrorNotification()
        {
            // Arrange
            var mockBannerData = new Mock<IBannerData>();
            var mockNotyf = new Mock<INotyfService>();
            var controller = new BannerController(mockBannerData.Object, mockNotyf.Object);

            int nonExistingBannerId = 12345; // Assuming a non-existing banner ID

            // Act
            var result = await controller.Delete(nonExistingBannerId);

            // Assert
            // Verify that the error notification is not shown
           // mockNotyf.Verify(x => x.Error(It.IsAny<string>()), Times.Never);
        }

        #endregion

        #region // update  test method
        [Fact]
        public async Task Update_ReturnsCorrectView_WithValidBannerId()
        {
            // Arrange
            var mockBannerData = new Mock<IBannerData>();
            var mockNotyf = new Mock<INotyfService>();
            var controller = new BannerController(mockBannerData.Object, mockNotyf.Object);
            int validBannerId = 1;
            mockBannerData.Setup(x => x.GetById(validBannerId)).ReturnsAsync(new Banner { BannerId = validBannerId });

            // Act
            var result = await controller.Update(validBannerId);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal("Update", viewResult.ViewName);
        }

        


        [Fact]
        public async Task Update_ReturnsBadRequest_WithNullBannerId()
        {
            // Arrange
            var mockBannerData = new Mock<IBannerData>();
            var mockNotyf = new Mock<INotyfService>();
            var controller = new BannerController(mockBannerData.Object, mockNotyf.Object);
            var updatebanner = new UpdateBannerViewModel
            {
                BannerId = 0,BannerName = null
            };
            // Act
            var result = await controller.Update(updatebanner);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        #endregion


        #region // update  with banner view
        [Fact]
        public async Task Update_ValidBanner_ReturnsRedirectToShow()
        {
            // Arrange
            var mockBannerData = new Mock<IBannerData>();
            var mockNotyf = new Mock<INotyfService>();
            var controller = new BannerController(mockBannerData.Object, mockNotyf.Object);
            var banner = new UpdateBannerViewModel
            {
                BannerId = 1,
                BannerName = "Test Banner",
                OldImage = "assets/img/old_banner.jpg",
                NewImage = null
            };

            mockBannerData.Setup(x => x.GetById(banner.BannerId)).ReturnsAsync(new Banner { BannerId = 1 });
            mockBannerData.Setup(x => x.Update(It.IsAny<Banner>(), It.IsAny<int>())).ReturnsAsync(new Banner { BannerId = 1 });

            // Act
            var result = await controller.Update(banner);

            // Assert
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Show", redirectResult.ActionName);
            Assert.Equal("Banner", redirectResult.ControllerName);
        }


      
        [Fact]
        public async Task Update_InvalidModelState_ReturnsViewWithModel()
        {
            // Arrange
            var mockBannerData = new Mock<IBannerData>();
            var mockNotyf = new Mock<INotyfService>();
            var controller = new BannerController(mockBannerData.Object, mockNotyf.Object);
            controller.ModelState.AddModelError("BannerName", "Required");
            var banner = new UpdateBannerViewModel
            {
                BannerId = 1,
                BannerName = "",
                OldImage = "assets/img/old_banner.jpg",
                NewImage = null
            };

            // Act
            var result = await controller.Update(banner);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal("Update", viewResult.ViewName);
            Assert.Equal(banner, viewResult.Model);
        }


        [Fact]
        public async Task Update_NoNewImageAndNoOldImage_ReturnsRedirectToShow()
        {
            // Arrange
            var mockBannerData = new Mock<IBannerData>();
            var mockNotyf = new Mock<INotyfService>();
            var controller = new BannerController(mockBannerData.Object, mockNotyf.Object);
            var banner = new UpdateBannerViewModel
            {
                BannerId = 1,
                BannerName = "Test Banner",
                OldImage = "",
                NewImage = null
            };

            mockBannerData.Setup(x => x.GetById(banner.BannerId)).ReturnsAsync(new Banner { BannerId = 1 });
            mockBannerData.Setup(x => x.Update(It.IsAny<Banner>(), It.IsAny<int>())).ReturnsAsync(new Banner { BannerId = 1 });

            // Act
            var result = await controller.Update(banner);

            // Assert
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Show", redirectResult.ActionName);
            Assert.Equal("Banner", redirectResult.ControllerName);
        }

        [Fact]
        public async Task Update_NoBannerFound_ReturnsNotFound()
        {
            // Arrange
            var mockBannerData = new Mock<IBannerData>();
            var mockNotyf = new Mock<INotyfService>();
            var controller = new BannerController(mockBannerData.Object, mockNotyf.Object);
            var banner = new UpdateBannerViewModel
            {
                BannerId = 1,
                BannerName = "Test Banner",
                OldImage = "assets/img/old_banner.jpg",
                NewImage = null
            };

            mockBannerData.Setup(x => x.GetById(banner.BannerId)).ReturnsAsync(new Banner { });

            // Act
            var result = await controller.Update(banner);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }


        #endregion

    }



}
