using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models.Database_Models;
using Services.Implementation;
using Services.Interfaces;
using System.Text.Json;
namespace Danweyne_Real_estate.Controllers
{
   
    public class ContactController : Controller
    {
        public readonly IContactData contactData;
        private readonly INotyfService _notyf;
        public ContactController(IContactData contactData, INotyfService notyf)
        {

            this.contactData = contactData;
            _notyf = notyf;
        }
        public  IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index([Bind] Contact obj)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    contactData.Insert(obj);
                    //TempData["msg"] 
                }
            }
            catch (Exception ex)
            {
                return View("Error");
            }

            return RedirectToAction("Index");
        }
        public async Task<IActionResult> ShowData()
        {
            //TempData.Clear();
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    if ((string)TempData.Peek("UserRole") == "Admin" || (string)TempData.Peek("UserRole") == "Agent")
                    {
                        var pro = await contactData.GetAll();

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
            var contact = await contactData.GetById(id);
            if (contact == null)
            {
                return NotFound();
            }
            try
            {
                await contactData.Delete(id);
                _notyf.Success("Contact deleted  successfully", 3);
            }
            catch (Exception)
            {
                _notyf.Error("An error occurred while deleting the contact.");
            }

            return RedirectToAction("ShowData");

            
            //return Json(new { success = true, message = "Contact deleted successfully." });
        }


        public async Task<IActionResult> Update(int id)
        {
            var contact = await contactData.GetById(id);
            if (contact == null)
            {
                return NotFound();
            }

            return View(contact);
        }
        [HttpPost]
        public async Task<IActionResult> Update(Contact updatedContact)
        {
            if (ModelState.IsValid)
            {
                var existingContact = await contactData.GetById(updatedContact.ContactId);
                if (existingContact == null)
                {
                    return NotFound();
                }

                var result = await contactData.Update(updatedContact, updatedContact.ContactId);
                if (result != null)
                {
                     _notyf.Success("Contact update successfully", 3);

                    return RedirectToAction("showdata", "Contact");
                }
            }
            _notyf.Error("Failed to update contact", 3);
           
            return View(updatedContact);
        }
    }
}
    

