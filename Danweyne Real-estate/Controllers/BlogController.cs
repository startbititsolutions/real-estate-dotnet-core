using AspNetCoreHero.ToastNotification.Abstractions;
using AutoMapper.Features;
using Danweyne_Real_estate.Models;
using Microsoft.AspNetCore.Mvc;
using Models.Database_Models;
using Services.Implementation;
using Services.Interfaces;
using System.Text.Json;

namespace Danweyne_Real_estate.Controllers
{
    public class BlogController : Controller
    {
        public readonly IBlogData blogData;
        private readonly INotyfService _notyf;
        public BlogController(IBlogData blogData, INotyfService notyf)
        {

            this.blogData = blogData;
            _notyf = notyf;
        }
        public async Task<IActionResult> BlogDetails(int id)
        {
            try
            {
                if (id == 0)
                {
                    return NotFound();
                }
                var Blog = await blogData.GetById(id);
                return View(Blog);
            }
            catch
            {
                return View("Error");
            }
        }
        public async Task<IActionResult> bloglist()
        {
            try
            {
                var blog = await blogData.GetAll();
                return View(blog);
            }
            catch
            {
                return View("Error");
            }
        }
        public IActionResult form()
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    if ((string)TempData.Peek("UserRole") == "Admin")
                    {
                        return View();
                    }

                }
                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return View("Error");
            }
        }
        [HttpPost]
        public IActionResult form(blogviewmodel blog)
        {
            try
            {
                if (blog.AuthorName != null)
                {
                    Blog blogobj = new Blog();
                    string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/assets/img");

                    //create folder if not exist
                    if (!Directory.Exists(path))
                        Directory.CreateDirectory(path);

                    //get file extension
                    FileInfo fileInfo = new FileInfo(blog.BlogImg.FileName);
                    string fileName = blog.BlogImg.FileName;

                    string fileNameWithPath = Path.Combine(path, fileName);

                    using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                    {
                        blog.BlogImg.CopyTo(stream);
                    }
                    blogobj.BlogImg = "Files/" + fileName;
                    // var filepath = new List<string>();

                    blogobj.AuthorName = blog.AuthorName;
                    blogobj.BlogName = blog.BlogName;
                    blogobj.Summary = blog.Summary;
                    blogobj.BlogDesc = blog.BlogDesc;
                    blogobj.DateTime = DateTime.Now;
                    blogobj.Title = blog.Title;
                    blogobj.BlogImg = "assets/img/" + fileName;
                    var result = blogData.Insert(blogobj);
                    if (result != null)
                    {
                        _notyf.Success("Blog added successfully");
                        return RedirectToAction("show", "blog");
                    }
                }
                _notyf.Error("Failed to add blog");
                return RedirectToAction("bloglist", "Blog");
            }
            catch
            {
                return View("Error");
            }
        }
        public async Task<IActionResult> Show()
        {
            //TempData.Clear();
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    if ((string)TempData.Peek("UserRole") == "Admin" || (string)TempData.Peek("UserRole") == "Agent")
                    {
                        var pro = await blogData.GetAll();

                        return View(pro);
                    }
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            catch
            {
                return View("Error");
            }
        }



        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var blog = await blogData.GetById(id);
            if (blog == null)
            {
                return NotFound();
            }

            try
            {
                await blogData.Delete(id);
                _notyf.Success("Blog deleted successfully");
            }
            catch (Exception)
            {
                _notyf.Error("An error occurred while deleting the blog.");
            }

            return RedirectToAction("Show");
        }



        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var blog = await blogData.GetById(id);
            if (blog == null)
            {
                return NotFound();
            }

            var viewModel = new UpdateBlogViewModel
            {
                BlogId = blog.BlogId,
                Title = blog.Title,
                AuthorName = blog.AuthorName,
                BlogName = blog.BlogName,
                Summary = blog.Summary,
                BlogDesc = blog.BlogDesc,
                OldImage = blog.BlogImg
            };

            return View("Update", viewModel);
        }


        [HttpPost]
        public async Task<IActionResult> Update(UpdateBlogViewModel blog)
        {
            if (ModelState.IsValid)
            {
                var existingBlog = await blogData.GetById(blog.BlogId);
                if (existingBlog == null)
                {
                    return NotFound();
                }

                existingBlog.Title = blog.Title;
                existingBlog.AuthorName = blog.AuthorName;
                existingBlog.BlogName = blog.BlogName;
                existingBlog.Summary = blog.Summary;
                existingBlog.BlogDesc = blog.BlogDesc;



                if (blog.NewImage != null)
                {
                    string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/assets/img");

                    // Create the folder if it doesn't exist
                    if (!Directory.Exists(path))
                        Directory.CreateDirectory(path);

                    // Generate a unique file name
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(blog.NewImage.FileName);
                    string fileNameWithPath = Path.Combine(path, fileName);

                    // Save the new image to the specified path
                    using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                    {
                        await blog.NewImage.CopyToAsync(stream);
                    }

                    existingBlog.BlogImg = "assets/img/" + fileName;
                }
                else
                {
                    // If no new image is uploaded, retain the old image path
                    existingBlog.BlogImg = blog.OldImage;
                }

                var result = await blogData.Update(existingBlog, existingBlog.BlogId);
                if (result != null)
                {
                    _notyf.Success("Blog updated successfully");
                    return RedirectToAction("Show", "Blog");
                }
            }
            else
            {
                // Exclude image validation error if the image is not required
                if (blog.NewImage == null && ModelState["newImage"] != null)
                    ModelState["newImage"].Errors.Clear();
            }

            _notyf.Error("Failed to update blog");
            return View(blog);
        }
    }
}






