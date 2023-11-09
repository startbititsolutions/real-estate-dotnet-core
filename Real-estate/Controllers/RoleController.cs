using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using Danweyne_Real_estate.Models; // Replace with your actual project namespace and user model
using Models;

namespace Danweyne_Real_estate.Controllers
{
    public class RoleController : Controller
    {


        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> AddRoleToUser()
        {
            var roleName = "User";
            var roleExists = await _roleManager.RoleExistsAsync(roleName);
            if (!roleExists)
            {
                // Create the role if it doesn't exist
                await _roleManager.CreateAsync(new IdentityRole(roleName));
            }
            return RedirectToAction("Index", "Home"); // Replace with your actual action and controller
        }
    }
}

