using AspNetCoreHero.ToastNotification.Abstractions;
using Danweyne_Real_estate.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.Database_Models;
using NuGet.ContentModel;
using Services.Helpers;
using Services.Implementation;
using Services.Interfaces;
using System;
using System.Security.Claims;
using System.Text.Json;
using System.Text.RegularExpressions;
using AdditionalDetails = Models.Database_Models.AdditionalDetails;

namespace Danweyne_Real_estate.Controllers
{
    public class PropertiesController : Controller
    {
        private readonly IPropertyData _property;
        private readonly IFeatureData _featureData;
        private readonly IAdditionalDetails _additionaldetails;
        private readonly IStatusData _statusData;
        private readonly IPropertyImageData PropertyImageData;
        private readonly ICategoryData _categoryData;
        public readonly IPropertyFilter PropertyFilter;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ICityData CityData;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly INotyfService _notyf;

        public PropertiesController(IHttpContextAccessor _httpContextAccessor, INotyfService notyf, IPropertyData propertyData, IFeatureData featureData, IAdditionalDetails additionalDetails, IStatusData statusData, IPropertyImageData imageData,IPropertyFilter propertyFilter, UserManager<ApplicationUser> userManager, ICityData cityData, ICategoryData categoryData )
        {
            _property = propertyData;
            _featureData = featureData;
            _additionaldetails = additionalDetails;
            _statusData = statusData;
            PropertyImageData = imageData;
            PropertyFilter = propertyFilter;
            this.userManager = userManager;
            CityData = cityData;
            _categoryData = categoryData;
            this._httpContextAccessor = _httpContextAccessor;
            _notyf = notyf;
        }
       
        public async Task<IActionResult> Index()
        {

            try
            {
              
                if (TempData["FilterString"] != null)
                {
                    ViewBag.PropertyPriceMax = await _property.GetMax(c => c.PropertyPrice);
                    ViewBag.AreaMax = await _property.GetMax(c => c.Area);
                    ViewBag.BedRoomsMax = await _property.GetMax(c => c.BedRooms);
                    ViewBag.BathRoomsMax = await _property.GetMax(c => c.BathRooms);
                    ViewBag.Cities = await CityData.GetAll();
                    ViewBag.Status = await _statusData.GetAll();
                    IEnumerable<Property> jrv = await PropertyFilter.Filter((string)TempData["FilterString"]);
                    var count = await PropertyFilter.FilterCount((string)TempData["FilterString"]);
                    var totalpagec = Math.Ceiling((double)count / 9);
                    TempData["TotalPage"] = (int)totalpagec;
                    TempData["Offset"] = 0;
                    var page = (0 + 9) / 9;
                    TempData["currentPage"] = page;
                    TempData["prev"] = page * 9 - 18;
                    TempData["next"] = page * 9;
                    return View(jrv);
                }
                //var propertylist = await _property.GetAll();
                //var propertycount = await _property.Count();
                var activeProperties = await _property.GetActiveProperties();
                var propertycount = activeProperties.Count();
                var totalpage = Math.Ceiling((double)propertycount / 9);
                TempData["TotalPage"] = (int)totalpage;
                var top9 = await _property.GetTop(5);
                ViewBag.PropertyPriceMax = await _property.GetMax(c => c.PropertyPrice);
                ViewBag.AreaMax = await _property.GetMax(c => c.Area);
                ViewBag.BedRoomsMax = await _property.GetMax(c => c.BedRooms);
                ViewBag.BathRoomsMax = await _property.GetMax(c => c.BathRooms);
                ViewBag.Cities = await CityData.GetAll();
                ViewBag.Status = await _statusData.GetAll();
                var data = activeProperties.Skip(0).Take(9);
                TempData["Offset"] = 0;
                TempData["currentPage"] = 1;
                TempData["prev"] = 1 * 9 - 18;
                TempData["next"] = 1 * 9;
                return View(data);
            }
            catch
            {
                return View("Error");
            }

            
           
        }
        public async Task<IActionResult> GetAllHome(IFormCollection formCollection)
        {
            try
            {
                var formData = formCollection.ToDictionary(x => x.Key, x => x.Value.ToString());
                TempData["FormData"] = formData;
                var jsonString = JsonSerializer.Serialize(formData);
                TempData["FilterString"] = jsonString;
                return Json("Success"); ;
            }
            catch
            {
                return Json(new { Status = "Failed" }); ;
            }

        }
        public async Task<IActionResult> PropertyList(IFormCollection formCollection)
        {
            try
            {
                var formData = formCollection.ToDictionary(x => x.Key, x => x.Value.ToString());
                var jsonString = JsonSerializer.Serialize(formData);
                IEnumerable<Property> jrv = await PropertyFilter.Filter(jsonString);
                var count = await PropertyFilter.FilterCount(jsonString);
                var totalpage = Math.Ceiling((double)count / 9);
                TempData["TotalPage"] = (int)totalpage;
                TempData["Offset"] = formData["Offset"];
                var page = (Convert.ToInt32(formData["Offset"]) + 9) / 9;
                TempData["currentPage"] = page;
                TempData["prev"] = page * 9 - 18;
                TempData["next"] = page * 9;
                if (formCollection == null)
                {
                    return Json("Error");
                }
                // List<Property> properties = JsonSerializer.Deserialize<List<Property>>(jrv.ResponseMessage);
                return PartialView("PropertyList", jrv);
            }
            catch
            {
                return View("Error");
            }
        }
        [Route("/property/{id}")]
        public async Task<IActionResult> property(string id)
        {
            try
            {
                var str = GetUntilOrEmpty(id);
                var pro = await _property.GetById(Convert.ToInt32(str));
                if (pro == null)
                {
                    return NotFound();
                }
                var feat = await _featureData.GetByPropertyId(pro.PropertyId);
                var details = await _additionaldetails.GetById(pro.PropertyId);
                propertymodelview val = new propertymodelview();
                val.Property = pro;
                val.Features = feat;
                val.AdditionalDetails = details;
                return View(val);
            }
            catch
            {
                return View("Error");
            }
        }
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {

                    if ((string)TempData.Peek("UserRole") == "Admin" || (string)TempData.Peek("UserRole") == "Agent")
                    {

                        ViewBag.City = await CityData.GetAll();
                        ViewBag.Status = await _statusData.GetAll();
                        ViewBag.Catagory = await _categoryData.GetAll();
                    }
                    return View();

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
        public async Task<IActionResult> Add(AddpropertyViewModel property)
        {
            try
            {
                var userId = User.Claims.ToList()[1].Value;
                var user = await userManager.FindByNameAsync(userId);
                //Check for true
                if (property.TermsAndCondition == true)
                {
                    Property propertyobj = new Property();
                    string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/assets/" + user.UserName + "/" + property.Name);
                    //create folder if not exist
                    if (!Directory.Exists(path))
                        Directory.CreateDirectory(path);
                    //get file extension
                    FileInfo fileInfo = new FileInfo(property.File.FileName);
                    string fileName = property.File.FileName;
                    string fileNameWithPath = Path.Combine(path, fileName);
                    using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                    {
                        property.File.CopyTo(stream);
                    }
                    propertyobj.CoverImageUrl = "assets/" + user.UserName + "/" + property.Name + "/" + fileName;
                    var filepath = new List<string>();
                    foreach (var file in property.MultiFiles)
                    {

                        path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/assets/" + user.UserName + "/" + property.Name);

                        //create folder if not exist
                        if (!Directory.Exists(path))
                            Directory.CreateDirectory(path);


                        fileNameWithPath = Path.Combine(path, file.FileName);

                        using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                        {
                            file.CopyTo(stream);
                        }
                        filepath.Add("assets/" + user.UserName + "/" + property.Name + "/" + file.FileName);
                        /* propertyobj.Images.Append(fileNameWithPath)*/
                    }
                    var listbath = property.Baths.Split(',').ToList();
                    var maxbath = listbath.Select(int.Parse).Max();
                    var listbed = property.Beds.Split(',').ToList();
                    var listarea = property.Area.Split(",").ToList();
                    var maxbed = listbed.Select(int.Parse).Max();
                    var city =await CityData.GetByName(property.Name);
                    var maxarea = listarea.Select(int.Parse).Max();
                    propertyobj.PropertyName = property.Name;
                    propertyobj.PropertyPrice = Convert.ToInt32(property.Price);
                    propertyobj.PropertyDescription = property.Description;
                    propertyobj.CityId = Convert.ToInt32(property.City);
                    propertyobj.Area = maxarea;
                    propertyobj.CatagoryId = Convert.ToInt32(property.Catagory);
                    propertyobj.StatusId = Convert.ToInt32(property.Status);
                    // propertyobj.City.State.StateId = Convert.ToInt32(property.State);
                    propertyobj.Garage = property.Garage;
                    propertyobj.PropertyVideo = property.VidUrl;
                    propertyobj.DateTime = DateTime.Now;
                    propertyobj.BathRooms = maxbath;
                    propertyobj.BedRooms = maxbed;
                    propertyobj.UserId = user.Id;
                    var result = await _property.Insert(propertyobj);
                    var newpropertyid = result.PropertyId;
                    foreach (var item in filepath)
                    {
                        PropertyImage propertyImage = new PropertyImage();
                        propertyImage.ImageUrl = item;
                        propertyImage.PropertyId = newpropertyid;
                        await PropertyImageData.Insert(propertyImage);
                    }
                    var feature = new Features();
                    feature.Stories = property.Features.Stories;
                    feature.HurricaneShutters = property.Features.HurricaneShutters;
                    feature.EmergencyExit = property.Features.EmergencyExit;
                    feature.Ceilings = property.Features.Ceilings;
                    feature.DoubleSinks = property.Features.DoubleSinks;
                    feature.FirePlace = property.Features.FirePlace;
                    feature.JogPath = property.Features.JogPath;
                    feature.LaundryRoom = property.Features.LaundryRoom;
                    feature.SwimmingPool = property.Features.SwimmingPool;
                    feature.PropertyId = newpropertyid;
                    await _featureData.Insert(feature);
                    var additional = new AdditionalDetails();
                    additional.PropertyId = newpropertyid;
                    additional.BuiltIn = property.AdditionalDetails.BuiltIn;
                    additional.View = property.AdditionalDetails.View;
                    additional.WaterFrontDescription = property.AdditionalDetails.WaterFrontDescription;
                    additional.Parking = property.AdditionalDetails.Parking;
                    additional.WaterFront = property.AdditionalDetails.WaterFront;
                    await _additionaldetails.Insert(additional);
                    return RedirectToAction("Index");
                }
                return RedirectToAction("Index", "Home");

            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return View("Error");
            }
                    
        }

        public async Task<IActionResult> Show()
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    if ((string)TempData.Peek("UserRole") == "Admin" || (string)TempData.Peek("UserRole") == "Agent")
                    {
                        var pro = await _property.GetAll();
                        var users = userManager.Users.Select(u => new UserViewModel
                        {
                            UserName = u.UserName,
                            // Map other properties as needed
                        }).ToList();

                        var viewModel = pro.Select(p => new propertymodelview
                        {
                            Property = p,
                            Users = users
                        }).ToList();

                        return View(viewModel);
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
        public async Task<IActionResult> FilterProperties(string userId)
        {
            Console.WriteLine($"userId: {userId}");
            try
            {
                if (string.IsNullOrEmpty(userId))
                {
                    // Show all properties
                    var properties = await _property.GetAll();
                    var propertyViewModels = properties.Select(p => new propertymodelview { Property = p }).ToList();
                    return PartialView("_PropertiesTable", propertyViewModels);
                }
                else
                {
                    // Filter properties based on the selected agent
                    var properties = await _property.GetByAgentName(userId);
                    var propertyViewModels = properties.Select(p => new propertymodelview { Property = p }).ToList();
                    return PartialView("_PropertiesTable", propertyViewModels);
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                return PartialView("_PropertiesTable", new List<propertymodelview>());
            }
        }

        public async Task<IActionResult> EnableProperty(int propertyId)
        {
            try
            {
                var property = await _property.GetById(propertyId);
                if (property != null)
                {
                    property.IsActive = true;
                    // Update the property in the database
                    await _property.Update(property, propertyId);
                    _notyf.Success("Property is enabled");
                }
                else
                {
                    _notyf.Error("Property not found");
                }

                return RedirectToAction("Show", "Properties");
            }
            catch
            {
                return View("Error");
            }
        }


        public async Task<IActionResult> DisableProperty(int propertyId)
        {
            try
            {
                var property = await _property.GetById(propertyId);
                if (property != null)
                {
                    property.IsActive = false;
                    // Update the property in the database
                    await _property.Update(property, propertyId);
                    _notyf.Success("Property has been disabled successfully.");
                }
                else
                {
                    _notyf.Error("Property not found.");
                }


                return RedirectToAction("Show", "Properties");
            }
            catch
            {
                return View("Error");
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {

                var property = await _property.GetById(id);
                var result = await _property.Delete(id);
                return RedirectToAction("Index", "Property");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return View("Error");
            }
        }
        public string GetUntilOrEmpty(string text, string stopAt = "-")
        {
            if (!String.IsNullOrWhiteSpace(text))
            {
                int charLocation = text.IndexOf(stopAt, StringComparison.Ordinal);

                if (charLocation > 0)
                {
                    return text.Substring(0, charLocation);
                }
            }

            return String.Empty;
        }


    }
}
    

