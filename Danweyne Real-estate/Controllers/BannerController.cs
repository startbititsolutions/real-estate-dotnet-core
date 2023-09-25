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
    public class BannerController : Controller
    {
        public readonly IBannerData bannerData;
        private readonly INotyfService _notyf;
        public BannerController(IBannerData bannerData, INotyfService notyf)
        {

            this.bannerData = bannerData;
            _notyf = notyf;
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
                        var pro = await bannerData.GetAll();

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
            var banner = await bannerData.GetById(id);
            if (banner == null)
            {
                return NotFound();
            }
            try
            {
                await bannerData.Delete(id);
                _notyf.Success("Banner deleted successfully");
            }
            catch (Exception)
            {
                _notyf.Error("An error occurred while deleting the banner.");
            }

            return RedirectToAction("Show");
        }



        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var banner = await bannerData.GetById(id);
            if (banner == null)
            {
                return NotFound();
            }

            var viewModel = new UpdateBannerViewModel
            {
                BannerId = banner.BannerId,
                BannerName = banner.BannerName,

                OldImage = banner.BannerUrl
            };

            return View("Update", viewModel);
        }


        [HttpPost]
        public async Task<IActionResult> Update(UpdateBannerViewModel banner)
        {
            if (ModelState.IsValid)
            {
                var existingBannner = await bannerData.GetById(banner.BannerId);
                if (existingBannner == null)
                {
                    return NotFound();
                }


                existingBannner.BannerName = banner.BannerName;




                if (banner.NewImage != null)
                {
                    string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/assets/img");

                    // Create the folder if it doesn't exist
                    if (!Directory.Exists(path))
                        Directory.CreateDirectory(path);

                    // Generate a unique file name
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(banner.NewImage.FileName);
                    string fileNameWithPath = Path.Combine(path, fileName);

                    // Save the new image to the specified path
                    using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                    {
                        await banner.NewImage.CopyToAsync(stream);
                    }

                    existingBannner.BannerUrl = "assets/img/" + fileName;
                }
                else
                {
                    // If no new image is uploaded, retain the old image path
                    existingBannner.BannerUrl = banner.OldImage;
                }

                var result = await bannerData.Update(existingBannner, existingBannner.BannerId);
                if (result != null)
                {
                    _notyf.Success("Banner updated successfully");
                    return RedirectToAction("Show", "Banner");
                }
            }
            else
            {
                // Exclude image validation error if the image is not required
                if (banner.NewImage == null && ModelState["newImage"] != null)
                    ModelState["newImage"].Errors.Clear();
            }

            _notyf.Error("Failed to update banner");
            return View(banner);
        }
    }
}



