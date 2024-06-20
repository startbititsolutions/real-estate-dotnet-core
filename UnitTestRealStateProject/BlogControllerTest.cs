using AspNetCoreHero.ToastNotification.Abstractions;
using Danweyne_Real_estate.Controllers;
using Danweyne_Real_estate.Models;
using Microsoft.AspNetCore.Mvc;
using Models.Database_Models;
using Moq;
using Services.Implementation;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestRealStateProject
{
    public class BlogControllerTest
    {

        private readonly Mock<BlogData> _blogDataMock;
        private readonly BlogController _blogController;
        private readonly Mock<INotyfService> _notyf;


        #region  // BlogDetails 
        [Fact]
        public async Task BlogDetails_ReturnsView_WithCorrectBlog_WhenIdIsValid()
        {
            // Arrange
            int validId = 1;
            var expectedBlog = new Blog { BlogId = validId, Title = "Test Blog" };
            _blogDataMock.Setup(bd => bd.GetById(validId)).ReturnsAsync(expectedBlog);

            // Act
            var result = await _blogController.BlogDetails(validId);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<Blog>(viewResult.Model);
            Assert.Equal(expectedBlog.BlogId, model.BlogId);
            Assert.Equal(expectedBlog.Title, model.Title);
        }

       

        [Fact]
        public async Task BlogDetails_ReturnsErrorView_WhenExceptionIsThrown()
        {
            // Arrange
            int validId = 1;
            _blogDataMock.Setup(bd => bd.GetById(validId)).Throws(new Exception());

            // Act
            var result = await _blogController.BlogDetails(validId);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal("Error", viewResult.ViewName);
        }


        #endregion

        #region  // blog list 
        [Fact]
        public async Task BlogList_ReturnsView_WithEmptyBlogList()
        {
            // Arrange
            _blogDataMock.Setup(bd => bd.GetAll()).ReturnsAsync(new List<Blog>());

            // Act
            var result = await _blogController.bloglist();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Blog>>(viewResult.Model);
            Assert.Empty(model);
            Assert.Equal("blog", viewResult.ViewName);
        }

        [Fact]
        public async Task BlogList_ReturnsView_WithBlogList()
        {
            // Arrange
            var blogs = new List<Blog>
    {
        new Blog { BlogId = 1, Title = "Blog 1" },
        new Blog { BlogId = 2, Title = "Blog 2" }
    };
            _blogDataMock.Setup(bd => bd.GetAll()).ReturnsAsync(blogs);

            // Act
            var result = await _blogController.bloglist();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Blog>>(viewResult.Model);
            Assert.Equal(blogs, model);
            Assert.Equal("blog", viewResult.ViewName);
        }

        


        #endregion


        #region // test method for form
        [Fact]
        public async Task Test_ErrorView_When_Exception_Occurs_In_BlogDetails()
        {
            // Arrange
            var mockBlogData = new Mock<IBlogData>();
            var mockNotyf = new Mock<INotyfService>();
            var controller = new BlogController(mockBlogData.Object, mockNotyf.Object);

            // Act
            mockBlogData.Setup(bd => bd.GetById(It.IsAny<int>())).Throws(new Exception());
            var result = await controller.BlogDetails(1);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal("Error", viewResult.ViewName);
        }
        #endregion

        #region // form method

        [Fact]
        public void Test_ErrorView_When_Exception_Occurs_In_Form()
        {
            // Arrange
            var mockBlogData = new Mock<IBlogData>();
            var mockNotyf = new Mock<INotyfService>();
            var controller = new BlogController(mockBlogData.Object, mockNotyf.Object);

            // Act
            var result = controller.form();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal("Error", viewResult.ViewName);
        }

        #endregion

        #region  /// test method Show()

        [Fact]
        public async Task Test_ErrorView_When_Exception_Occurs_In_Show()
        {
            // Arrange
            var mockBlogData = new Mock<IBlogData>();
            var mockNotyf = new Mock<INotyfService>();
            var controller = new BlogController(mockBlogData.Object, mockNotyf.Object);

            // Act
            mockBlogData.Setup(bd => bd.GetAll()).Throws(new Exception());
            var result = await controller.Show();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal("Error", viewResult.ViewName);
        }

        #endregion

        #region  // delete  
        [Fact]
        public async Task Test_ErrorView_When_Exception_Occurs_In_Delete()
        {
            // Arrange
            var mockBlogData = new Mock<IBlogData>();
            var mockNotyf = new Mock<INotyfService>();
            var controller = new BlogController(mockBlogData.Object, mockNotyf.Object);

            // Act
            mockBlogData.Setup(bd => bd.Delete(It.IsAny<int>())).Throws(new Exception());
            var result = await controller.Delete(1);

            // Assert
            var redirectResult = Assert.IsType<ViewResult>(result);
            Assert.Equal("404 not Found", redirectResult.ViewName);
        }


        #endregion

      

        [Fact]
        public void Form_ReturnsError_WhenAuthorNameIsNull()
        {
            // Arrange
           
            var blogViewModel = new blogviewmodel { AuthorName = null };

            // Act
            

            
            var result = _blogController.form(blogViewModel);

            // Assert
            var viewResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("bloglist", viewResult.ActionName);
            
        }


        [Fact]
        public void Form_ReturnsError_WhenBlogNameIsNull()
        {
            var blogViewModel = new  blogviewmodel()
            {
                AuthorName = "Test Author",
                BlogName = null
            };

            _blogDataMock.Setup(a=> a.Equals(blogViewModel)).Returns(true);    
            
            // Act
            var result = _blogController.form(blogViewModel);

            // Assert
            // Your assertions go here
            Assert.NotNull(result);
            // Further assertions based on what the Form method is supposed to return or do
        }
     

    }
}
