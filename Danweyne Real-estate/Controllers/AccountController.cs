using AspNetCoreHero.ToastNotification.Abstractions;
using AutoMapper;
using Danweyne_Real_estate.Models;
using Danweyne_Real_estate.Models.Mapper;
using DataAccess.Context;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Identity.UI.V5.Pages.Account.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Models;
using Models.Database_Models;
using Models.View_Model;
using NuGet.Protocol.Plugins;
using Services.SmtpService;
using System.Security.Claims;
using ResetPasswordModel = Danweyne_Real_estate.Models.ResetPasswordModel;

namespace Danweyne_Real_estate.Controllers
{
    public class AccountController : Controller
    {
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailService _emailService;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly INotyfService _notyf;
        public AccountController(IMapper mapper, UserManager<ApplicationUser> userManager, INotyfService notyf, IEmailService emailService, SignInManager<ApplicationUser> signInManager)
        {
            _mapper = mapper;
            _userManager = userManager;
            _emailService = emailService;
            _signInManager = signInManager;
             _notyf = notyf;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    if ((string)TempData.Peek("UserRole") != null)
                    {
                        return View();
                    }
                    else
                    {
                        var userId = User.Claims.ToList()[1].Value;
                        var user = await _userManager.FindByNameAsync(userId);
                        var role = await _userManager.GetRolesAsync(user);
                        TempData["UserRole"] = (string)role[0];
                    }
                }
                else
                {
                    TempData["UserRole"] = "User";
                }
            }
            catch
            {
                return View("Error");
            }

            return View();
        }
       
        [HttpPost]
        public async Task<IActionResult> Registering(UserRegistrationModel userModel)
        {
            if (!ModelState.IsValid)
            {
                TempData["RegisterSucessMessage"] = "please enter details correctly";
                return RedirectToAction("Index");
            }
            try
            {
                var user = _mapper.Map<ApplicationUser>(userModel);
                user.EmailConfirmed = true;
                var result = await _userManager.CreateAsync(user, userModel.Password);

                if (!result.Succeeded)
                {

                   
                    _notyf.Error("Something Went Wrong Please Contact Admin");
                    return RedirectToAction("Index");
                }
                else
                {
                  
                    _notyf.Success("Registered Sucessfully");

                    if ((string)TempData.Peek("UserRole") == "Admin")
                    {
                        await _userManager.AddToRoleAsync(user, userModel.UserRole);
                       
                    }
                    else
                    {
                        await _userManager.AddToRoleAsync(user, "User");
                       
                    }
                    return RedirectToAction("Thankyou");
                }
               
            }

            catch
            {
                return View("Error");
            }
           
        }
        [HttpGet]
        public IActionResult Login()
        {

            return View();

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UserLoginModel userModel)
        {

            if (!ModelState.IsValid)
            {
                TempData["LoginError"] = "Wrong Login Cridential";

                return RedirectToAction("Index");
            }
            try
            {
                var user = await _userManager.FindByEmailAsync(userModel.Email);
                if (user != null &&
                    await _userManager.CheckPasswordAsync(user, userModel.Password))
                {
                    if (user.LockoutEnabled == true)
                    {
                        var identity = new ClaimsIdentity(IdentityConstants.ApplicationScheme);
                        identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id));
                        identity.AddClaim(new Claim(ClaimTypes.Name, user.UserName));
                        await HttpContext.SignInAsync(IdentityConstants.ApplicationScheme,
                            new ClaimsPrincipal(identity));
                        var role = await _userManager.GetRolesAsync(user);
                        TempData["UserRole"] = (string)role[0];
                        return RedirectToAction("Index", "Home");
                    }
                    return RedirectToAction("Check", "Account");
                }
                else
                {

                    TempData["Loginerror"] = "Invalid Email or Passowrd";
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                return View("Error");
            }

        }
        /*        [HttpPost]
                public async Task<IActionResult> Logout()
                {
                    await _signInManager.SignOutAsync();
                    return RedirectToAction("Index", "Home");
                }*/
        /* private IActionResult RedirectToLocal(string returnUrl)
         {
             if (Url.IsLocalUrl(returnUrl))
                 return Redirect(returnUrl);
             else
                 return RedirectToAction(nameof(HomeController.Index), "Home");

         }*/
        public async Task<IActionResult> YourProfile()
        {
            try
            {
                var userid = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (userid != null)
                {
                    var user = await _userManager.FindByIdAsync(userid);
                    ProfileViewModel yourprofile = new ProfileViewModel();
                    yourprofile.twitterurl = user.twitterurl;
                    yourprofile.imgurl = user.ImageUrl;
                    yourprofile.PhoneNumber = user.PhoneNumber;
                    yourprofile.Address = user.Address;
                    yourprofile.linkedinurl = user.linkedinurl;
                    yourprofile.googleurl = user.googleurl;
                    yourprofile.InstagramUrl = user.InstagramUrl;
                    yourprofile.Facebookurl = user.Facebookurl;
                    yourprofile.Description = user.Discription;
                   
                    return View(yourprofile);
                }

                return RedirectToAction("Index", "Account");
            }
            catch
            {
                return View("Error");
            }

        }
        [HttpPost]
        public async Task<IActionResult> YourProfile(ProfileViewModel profile)
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    var userid = User.FindFirstValue(ClaimTypes.NameIdentifier);
                    var user = await _userManager.FindByIdAsync(userid);
                    if (profile.File != null)
                    {

                        string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/assets/" + user.UserName + "/ProfilePicture");

                        //create folder if not exist
                        if (!Directory.Exists(path))
                            Directory.CreateDirectory(path);

                        //get file extension
                        FileInfo fileInfo = new FileInfo(profile.File.FileName);
                        string fileName = profile.File.FileName;

                        string fileNameWithPath = Path.Combine(path, fileName);

                        using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                        {
                            profile.File.CopyTo(stream);
                        }
                        user.ImageUrl = "assets/" + user.UserName + "/ProfilePicture" + "/" + fileName;


                    }
                    else
                    {
                        user.ImageUrl = profile.imgurl;
                    }

                    user.PhoneNumber = profile.PhoneNumber;
                    user.Address = profile.Address;
                    user.Facebookurl = profile.Facebookurl;
                    user.Discription = profile.Description;
                    user.googleurl = profile.googleurl;
                    user.twitterurl = profile.twitterurl;
                    user.InstagramUrl = profile.InstagramUrl;
                    user.linkedinurl = profile.linkedinurl;

                    var result = await _userManager.UpdateAsync(user);


                    ViewBag.message = "data saved";
                    _notyf.Success("Profile is updated");
                    return RedirectToAction("YourProfile", "Account");
                }
                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return View("Error");
            }

        }


        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel forgotPasswordModel)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(forgotPasswordModel);
                var user = await _userManager.FindByEmailAsync(forgotPasswordModel.Email);
                if (user == null)
                {

                    TempData["forgoterror"] = "Email does not exist please enter correct email";
                    return RedirectToAction(nameof(ForgotPassword));
                }
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var callback = Url.Action(nameof(ResetPassword), "Account", new { token, email = user.Email }, Request.Scheme);
                //var message = new Message(new string[] { user.Email }, "Reset password token", callback, null);
                var email = new EmailData();
                email.EmailId = user.Email;
                email.EmailSubject = "Forgot Password";
                email.EmailBody = $"<table cellspacing=\"0\" border=\"0\" cellpadding=\"0\" width=\"100%\" bgcolor=\"#f2f3f8\"\r\n        style=\"@import url(https://fonts.googleapis.com/css?family=Rubik:300,400,500,700|Open+Sans:300,400,600,700); font-family: 'Open Sans', sans-serif;\">\r\n        <tr>\r\n            <td>\r\n                <table style=\"background-color: #f2f3f8; max-width:670px;  margin:0 auto;\" width=\"100%\" border=\"0\"\r\n                    align=\"center\" cellpadding=\"0\" cellspacing=\"0\">\r\n                    <tr>\r\n                        <td style=\"height:80px;\">&nbsp;</td>\r\n                    </tr>\r\n                    <tr>\r\n                        \r\n                    </tr>\r\n                    <tr>\r\n                        <td style=\"height:20px;\">&nbsp;</td>\r\n                    </tr>\r\n                    <tr>\r\n                        <td>\r\n                            <table width=\"95%\" border=\"0\" align=\"center\" cellpadding=\"0\" cellspacing=\"0\"\r\n                                style=\"max-width:670px;background:#fff; border-radius:3px; text-align:center;-webkit-box-shadow:0 6px 18px 0 rgba(0,0,0,.06);-moz-box-shadow:0 6px 18px 0 rgba(0,0,0,.06);box-shadow:0 6px 18px 0 rgba(0,0,0,.06);\">\r\n                                <tr>\r\n                                    <td style=\"height:40px;\">&nbsp;</td>\r\n                                </tr>\r\n                                <tr>\r\n                                    <td style=\"padding:0 35px;\">\r\n                                        <h1 style=\"color:#1e1e2d; font-weight:500; margin:0;font-size:32px;font-family:'Rubik',sans-serif;\">You have\r\n                                            requested to reset your password</h1>\r\n                                        <span\r\n                                            style=\"display:inline-block; vertical-align:middle; margin:29px 0 26px; border-bottom:1px solid #cecece; width:100px;\"></span>\r\n                                        <p style=\"color:#455056; font-size:15px;line-height:24px; margin:0;\">\r\n                                            We cannot simply send you your old password. A unique link to reset your\r\n                                            password has been generated for you. To reset your password, click the\r\n                                            following link and follow the instructions.\r\n                                        </p>\r\n                                        <a href=\"{callback}\"\r\n                                            style=\"background:#20e277;text-decoration:none !important; font-weight:500; margin-top:35px; color:#fff;text-transform:uppercase; font-size:14px;padding:10px 24px;display:inline-block;border-radius:50px;\">Reset\r\n                                            Password</a>\r\n                                    </td>\r\n                                </tr>\r\n                                <tr>\r\n                                    <td style=\"height:40px;\">&nbsp;</td>\r\n                                </tr>\r\n                            </table>\r\n                        </td>\r\n                    <tr>\r\n                        <td style=\"height:20px;\">&nbsp;</td>\r\n                    </tr>\r\n                   \r\n                    <tr>\r\n                        <td style=\"height:80px;\">&nbsp;</td>\r\n                    </tr>\r\n                </table>";
                email.IsHtml = true;
                await _emailService.SendEmailAsync(email);
                return RedirectToAction(nameof(ForgotPasswordConfirmation));
            }
            catch
            {
                return View("Error");
            }

        }
        public IActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ResetPassword(string token, string email)
        {
            var model = new ResetPasswordModel { Token = token, Email = email };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordModel resetPasswordModel)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(resetPasswordModel);
                var user = await _userManager.FindByEmailAsync(resetPasswordModel.Email);
                if (user == null)
                    RedirectToAction("Index", "Account");
                var resetPassResult = await _userManager.ResetPasswordAsync(user, resetPasswordModel.Token, resetPasswordModel.Password);
                if (!resetPassResult.Succeeded)
                {
                    foreach (var error in resetPassResult.Errors)
                    {
                        ModelState.TryAddModelError(error.Code, error.Description);
                        _notyf.Error("Something Went Wrong Please Contact Admin");
                    }
                    return View();
                }
                _notyf.Success("Reset password successfully");
                return RedirectToAction("Index", "Account");
            }
            catch
            {
                return View("Error");
            }
        }

        [HttpGet]
        public IActionResult ChangePassword()
        {
            if (User.Identity.IsAuthenticated)
            {


                return View();
            }


            return RedirectToAction("Index", "Home");


        }


        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> ChangePassword(ChangePasswordModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound();
            }


            var changePasswordResult = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
            if (!changePasswordResult.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "Current password is incorrect.");

                ViewData["ErrorMessage"] = "Current password is incorrect.";
                return View(model);
            }

            await _signInManager.RefreshSignInAsync(user);
            TempData["SuccessMessage"] = "Password changed successfully.";
            return RedirectToAction("ChangePassword");
        }

        private bool IsCurrentPasswordValid(string currentPassword)
        {
            // Add your validation logic here
            // Return true if the current password is valid, false otherwise
            return (currentPassword == "correctpassword");
        }
        [HttpGet]
        public IActionResult Users()
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {

                    if ((string)TempData.Peek("UserRole") == "Admin" || (string)TempData.Peek("UserRole") == "Agent")
                    {
                        var users = _userManager.Users.Select(u => new UserViewModel
                        {
                            Id = u.Id,
                            UserName = u.UserName,
                            Email = u.Email,
                            lockoutenabled = u.LockoutEnabled

                            // Map other properties as needed
                        }).ToList();

                        return View(users);
                    }
                    else
                    {

                        return RedirectToAction("Index", "Home");
                    }
                }
                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return View("Error");
            }
        }
        public async Task<IActionResult> EnableUser(string userId)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user != null)
                {
                    user.LockoutEnabled = true;
                    user.LockoutEnd = null;
                    await _userManager.UpdateAsync(user);
                    _notyf.Success("The user is enabled");
                }
                else
                {
                    _notyf.Error("The user is  disabled");
                }
                return RedirectToAction("Users");
            }
            catch
            {
                return View("Error");
            }
        }

        public async Task<IActionResult> DisableUser(string userId)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user != null)
                {
                    user.LockoutEnabled = false;
                    user.LockoutEnd = DateTimeOffset.MaxValue;
                    await _userManager.UpdateAsync(user);
                    _notyf.Success("The user is  disabled");
                    // await _signInManager.SignOutAsync();
                    if (User.Identity.IsAuthenticated && User.Identity.Name == user.UserName)
                    {
                        await HttpContext.SignOutAsync();
                    }


                }

                return RedirectToAction("Users");
            }
            catch
            {
                return View("Error");
            }
        }
        public ActionResult Check()
        {
            return View();
        }
        [HttpGet]
        public IActionResult RegisterAsAgent()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                return View("Error");
            }
        }
        [HttpPost]
        public async Task<IActionResult> RegisterAsAgent(UserRegistrationModel userModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    TempData["RegisterSucessMessage"] = "please enter details correctly";
                    return RedirectToAction("Index");
                }
                var user = _mapper.Map<ApplicationUser>(userModel);
                user.EmailConfirmed = true;
                var result = await _userManager.CreateAsync(user, userModel.Password);

                if (!result.Succeeded)
                {

                    TempData["RegisterSucessMessage"] = "Something Went Wrong Please Contact Admin";

                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["RegisterSucessMessage"] = "Registered Sucessfully";

                    await _userManager.AddToRoleAsync(user, userModel.UserRole);


                    await _userManager.AddToRoleAsync(user, "Agent");

                }
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return View("Error");
            }
        }
        public ActionResult Thankyou()
        {
           
                return View();  
          
         }
           
        
        [HttpPost]
        public async Task<IActionResult> CheckOldPassword(string oldPassword)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Json("User not found");
            }

            var passwordVerificationResult = await _userManager.CheckPasswordAsync(user, oldPassword);
            if (!passwordVerificationResult)
            {
                return Json("Current password is incorrect");
            }
            return Json(true);



        }
    }
}


