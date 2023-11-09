using AspNetCoreHero.ToastNotification.Abstractions;
using Danweyne_Real_estate.Data;
using Danweyne_Real_estate.Models;
using DataAccess.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.Database_Models;
using Services.Implementation;
using Services.Interfaces;
using System.Diagnostics;
using System.Security.Claims;
using System.Text.Json;
using ApplicationDbContext = DataAccess.Context.ApplicationDbContext;

namespace Danweyne_Real_estate.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext ApplicationDbContext;
        private readonly UserManager<ApplicationUser> cc;
        public readonly INewsletterData _NewsletterData;
        private readonly IPropertyData propertyData;
        private readonly IBannerData bannerData;
        private readonly ICityData CityData;
        private readonly IStatusData _statusData;
        private readonly INotyfService _notyf;
        public HomeController(ICityData CityData, INotyfService notyf, IStatusData _statusData, ILogger<HomeController> logger, ApplicationDbContext ApplicationDbContext, UserManager<ApplicationUser> user, INewsletterData NewsletterData, IPropertyData propertyData, IBannerData bannerData)
        {
            this.CityData = CityData;
            this._statusData = _statusData;
            _logger = logger;
            this.ApplicationDbContext = ApplicationDbContext;
            cc = user;
            this.bannerData = bannerData;
            this.propertyData = propertyData;
            this._NewsletterData = NewsletterData;
            _notyf = notyf;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                HomeIndexViewModel indexViewModel = new HomeIndexViewModel();

                var property = await propertyData.GetActiveProperties();
                var result = property.OrderByDescending(x => x.DateTime).Take(7).ToList();
                var banner = await bannerData.GetAll();
                indexViewModel.Banners = banner.ToList();
                indexViewModel.Properties = result;

                //Properties Filter Data
                ViewBag.PropertyPriceMax = await propertyData.GetMax(c => c.PropertyPrice);
                ViewBag.AreaMax = await propertyData.GetMax(c => c.Area);
                ViewBag.BedRoomsMax = await propertyData.GetMax(c => c.BedRooms);
                ViewBag.BathRoomsMax = await propertyData.GetMax(c => c.BathRooms);
                ViewBag.Cities = await CityData.GetAll();
                ViewBag.Status = await _statusData.GetAll();
                //
                return View(indexViewModel);
            }
            catch
            {
                return View("Error");
            }
        }
        [HttpPost]
        public IActionResult Index([Bind] Newsletter obj)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    _NewsletterData.Insert(obj);
                    //TempData["msg"] 
                }
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message;
            }

            return RedirectToAction("Index");
        }
        public IActionResult banner()
        {
            if (User.Identity.IsAuthenticated)
            {
                if ((string)TempData.Peek("UserRole") == "Admin")
                {
                    return View();
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else { return RedirectToAction("Index", "Account"); }
        }
        [HttpPost]
        public IActionResult banner(bannerviewmodel banner)
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    if ((string)TempData.Peek("UserRole") == "Admin")
                    {
                        if (banner.BannerName != null)
                        {
                            Banner obj = new Banner();
                            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/assets/img");

                            //create folder if not exist
                            if (!Directory.Exists(path))
                                Directory.CreateDirectory(path);

                            //get file extension
                            FileInfo fileInfo = new FileInfo(banner.BannerUrl.FileName);
                            string fileName = banner.BannerUrl.FileName;

                            string fileNameWithPath = Path.Combine(path, fileName);

                            using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                            {
                                banner.BannerUrl.CopyTo(stream);
                            }
                            obj.BannerUrl = "Files/" + fileName;
                            // var filepath = new List<string>();

                            obj.BannerName = banner.BannerName;

                            obj.BannerUrl = "assets/img/" + fileName;
                            var result = bannerData.Insert(obj);
                            if (result != null)
                            {
                                _notyf.Success("Banner added successfully");
                                return RedirectToAction("Show", "Banner");
                            }
                        }
                        return RedirectToAction("Index", "Home");
                    }
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    _notyf.Error("Failed to added banner");
                    return RedirectToAction("Show", "Banner");
                }
            }
            catch
            {
                return View("Error");
            }
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public async Task<JsonResult> Filter(IFormCollection formCollection)
        {
            try
            {
                // Convert the IFormCollection to a dictionary
                string jsonstring = string.Empty;
                var val = formCollection["NewsLetter"];
                Newsletter obj = new Newsletter() { Email = val.FirstOrDefault().ToLower(), Id = 0 };
                var check = await _NewsletterData.GetByEmail(obj.Email);
                if (check != null)
                {
                    JsonResponseViewModel jsonResponseViewModel = new JsonResponseViewModel();
                    jsonResponseViewModel.ResponseCode = 200;
                    jsonResponseViewModel.ResponseMessage = "Already registered";
                    jsonstring = JsonSerializer.Serialize(jsonResponseViewModel);
                    return Json(jsonstring);
                }
                var res = await _NewsletterData.Insert(obj);

                if (res != null)
                {
                    JsonResponseViewModel jsonResponseViewModel = new JsonResponseViewModel();
                    jsonResponseViewModel.ResponseCode = 200;
                    jsonResponseViewModel.ResponseMessage = "Success";
                    jsonstring = JsonSerializer.Serialize(jsonResponseViewModel);
                }
                return Json(jsonstring);
            }
            catch
            {
                return null;
            }
        }
        public async Task<IActionResult> EmailData()
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {

                    if ((string)TempData.Peek("UserRole") == "Admin" || (string)TempData.Peek("UserRole") == "Agent")
                    {
                        var pro = await _NewsletterData.GetAll();

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

        public IActionResult Terms()
        {

            return View();
        }

        public async Task<IActionResult> DeleteNewsLetter(int id)
        {
            var email = await _NewsletterData.GetById(id);
            if (email == null)
            {
                return NotFound();
            }
            try
            {
                await _NewsletterData.Delete(id);
                _notyf.Success("Subscription deleted successfully", 3);
            }
            catch (Exception)
            {
                _notyf.Error("An error occurred while deleting the email.");
            }

            return RedirectToAction("EmailData");
        }
        [HttpGet]
        public async Task<IActionResult> UpdateNewsletter(int id)
        {
            try
            {
                var NewsLetter = await _NewsletterData.GetById(id);
                if (NewsLetter != null)
                {
                    return View(NewsLetter);
                }
                else
                {
                  
                    _notyf.Error("No News letter found");
                    return RedirectToAction("");
                }
               
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return View("Error");
            }
        }
        [HttpPost]
        public async Task<IActionResult> UpdateNewsletter(Newsletter newsletter)
        {
            try
            {
                var result = await _NewsletterData.Update(newsletter, newsletter.Id);
                if (result != null)
                {
                    _notyf.Success("added Successfully");
                    return RedirectToAction("emaildata", "Home");
                }
                else
                {
                    _notyf.Success("Unable To Edit"); 
                    //var Newsletter = await _NewsletterData.GetById(newsletter.Id);
                    return RedirectToAction("emaildata", "Home");
                }
               
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return View("Error");
            }
        }
    }


    public class abcViewComponent : ViewComponent
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public abcViewComponent(UserManager<ApplicationUser> userManager, IHttpContextAccessor _httpContextAccessor)
        {
            this.userManager = userManager;
            this._httpContextAccessor = _httpContextAccessor;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {

            try
            {
                ClaimsPrincipal principal = _httpContextAccessor.HttpContext.User as ClaimsPrincipal;
                var userId = principal.Claims.ToList()[1].Value;
                var user = await userManager.FindByNameAsync(userId);
                var role = await userManager.GetRolesAsync(user);
                ViewBag.RoleVB = role;
                ViewBag.user = user;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return View();
         }


    }
}