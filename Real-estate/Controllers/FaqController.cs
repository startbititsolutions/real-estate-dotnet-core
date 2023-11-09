using AspNetCoreHero.ToastNotification.Abstractions;
using Danweyne_Real_estate.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Differencing;
using Models.Database_Models;

using Services.Interfaces;
using System.Text.Json;

namespace Danweyne_Real_estate.Controllers
{
    public class FaqController : Controller
    {
        private readonly IFaqQue _faqQue;
        private readonly IFaqAns _faqAns;
        private readonly INotyfService _notyf;
        public FaqController(IFaqQue faqque, IFaqAns faqAns, INotyfService notyf)
        {
            _faqQue = faqque;
            _faqAns = faqAns;
            _notyf = notyf;

        }
        public async Task<IActionResult> Index()
        {
            try
            {
                var que = await _faqQue.GetAll();
                var ans = await _faqAns.GetAll();
                FaqQueViewModel vm = new FaqQueViewModel();
                vm.que = que.ToList();
                vm.ans = ans.ToList();
                return View(vm);
            }
            catch
            {
                return View("Error");
            }

        }
        public async Task<IActionResult> Add()
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    if ((string)TempData.Peek("UserRole") == "Admin")
                    {
                        return View();
                    }
                    else
                    {
                        return RedirectToAction("Index","Home");
                    }
                }
                else
                {
                    return RedirectToAction("Index", "Account");

                }
            }
            catch { return View("Error"); }
        }
        [HttpPost]
        public async Task<IActionResult> Add(IFormCollection formCollection)
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    if ((string)TempData.Peek("UserRole") == "Admin")
                    {
                        var formData = formCollection.ToDictionary(x => x.Key, x => x.Value.ToString());
                        string[] AnswerArray = formData["Answer"].Split(',');
                        var result = await _faqQue.Insert(new FaqQue() { que = formData["Question"] });
                       
                        foreach (var item in AnswerArray)
                        {
                            if (item != null && item != "")
                            {
                               
                                await _faqAns.Insert(new FaqAns() { ans = item, que_id = result.que_id });

                              
                            }
                        }
                       
                        return Json("Success");
                      
                    }
                    else
                    {
                        return Json("Permission Declined");
                    }
                }
                else
                {
                    return RedirectToAction("AdminShow");
                }
            }
            catch
            {
                return Json("Failed");
            }
        }
        [HttpGet]
        public async Task<IActionResult> EditFaqQuestion(int id)
        {
            if (id == 0)
            {
                return null;
            }
            try
            {
                var faqques= await _faqQue.GetById(id);

                
                return View(faqques);
            }catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return View("Error");
            }
        }
        [HttpPost]
        public async Task<IActionResult> EditFaqQuestion(FaqQue faqQue)
        {
            try
            {
                var result = await _faqQue.Update(faqQue, faqQue.que_id);

                if (result != null)
                {

                   
                    return RedirectToAction("Adminshow","Faq");
                    _notyf.Success("Edit Successfully", 3);
                }
                else
                {
                    _notyf.Warning("Unable To Edit", 3);
                    return RedirectToAction("Adminshow", "Faq");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return View("Error");
            }
        }
        [HttpGet]
        [Route("faq/EditFaqAnswere/{id?}")]
        public async Task<IActionResult> EditFaqAnswere(int id)
        {
            if (id == 0 )
            {
                return null;
            }
            try
            {
              var faqans= await _faqAns.GetById(id);
                return View(faqans);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return View("Error");
            }
        }
        [HttpPost]
        public async Task<IActionResult> EditFaqAnswer(FaqAns faqAns)
        {
            try
            {
               var result=await _faqAns.Update(faqAns, faqAns.ans_id);
                if (result != null)
                {
                    _notyf.Success("Edit Successfully", 3);
                    return RedirectToAction("Adminshow", "Faq");
                }
                else
                {
                    _notyf.Warning("Unable To Edit", 3);
                    return RedirectToAction("Adminshow", "Faq");
                }

               
            }catch (Exception ex)
            {
                Console.Write(ex.ToString());
                return View("Error");
            }
        }
        public async Task<IActionResult> DeleteFaqQuestion(int id)
        {
            try
            {
                var result = await _faqQue.Delete(id);
               
                if (result != null)
                {
                    _notyf.Success("Delete Successfully", 3);
                    return RedirectToAction("Adminshow", "Faq");
                }
                else
                {
                    _notyf.Warning("Faild to delete", 3);
                    return RedirectToAction("Adminshow", "Faq");
                }
                return Json(new { success = true, message = "Contact deleted successfully." });

            }
            catch(Exception ex) 
            {
                Console.Write(ex.ToString());
                return View("Error");
            }
        }
        public async Task<IActionResult> DeleteFaqAnswere(int id)
        {
            try
            {
                var result = await _faqAns.Delete(id)
;                   if (result != null)
                {
                    _notyf.Success("Delete successfully", 3);
                    return RedirectToAction("Adminshow", "Faq");
                }
                else
                {
                    _notyf.Warning("Faild to delete", 3);
                    return RedirectToAction("Adminshow", "Faq");
                }
               
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                return View("Error");
            }
        }
        public async Task<IActionResult> AdminShow(FaqQueViewModel faqQueViewModel)
        {
            try
            {
                var que = await _faqQue.GetAll();
                var ans = await _faqAns.GetAll();
                FaqQueViewModel vm = new FaqQueViewModel();
                vm.que = que.ToList();
                vm.ans = ans.ToList();
                return View(vm);

             
            }
            catch(Exception ex)
            {

            return View("Error");
            }
        }
        public async Task<IActionResult> AddMoreFaqAnswer(int id)
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    if ((string)TempData.Peek("UserRole") == "Admin")
                    {
                        var question = await _faqQue.GetById(id);
                        ViewBag.Question = question;
                        return View();
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    return RedirectToAction("Index", "Account");

                }
               
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                return View("Error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddMoreFaqAnswer(IFormCollection formCollection, int id)
        {
            try
            {
                var formData = formCollection.ToDictionary(x => x.Key, x => x.Value.ToString());
                var data = formData["object"];
                var mod = JsonSerializer.Deserialize<Test>(formData["object"]);
                var questionId = mod.question.Keys.FirstOrDefault();
                var question = mod.question.Values.FirstOrDefault();
                var newAnswers = mod.newans.ToList();
                var oldAnswers = mod.oldans.ToList();

                var deletedAnswerIds = formCollection["deletedAnswerIds"]; // Get the deletedAnswerIds from the form

                // Update the question
                FaqQue faqQue = new FaqQue();
                faqQue.que = question;
                faqQue.que_id = Convert.ToInt32(questionId);
                await _faqQue.Update(faqQue, faqQue.que_id);

                // Update the existing answers
                for (int i = 0; i < oldAnswers.Count; i++)
                {
                    var answerId = oldAnswers[i].Keys.FirstOrDefault();
                    var answerText = oldAnswers[i].Values.FirstOrDefault();

                    FaqAns faqAns = new FaqAns();
                    faqAns.ans_id = Convert.ToInt32(answerId);
                    faqAns.ans = answerText;
                    faqAns.que_id = Convert.ToInt32(questionId);
                    await _faqAns.Update(faqAns, faqAns.ans_id);
                    //_notyf.Success("Update successfully", 3);
                }

                // Delete the answers
                foreach (var deletedAnswerId in deletedAnswerIds)
                {
                    await _faqAns.Delete(Convert.ToInt32(deletedAnswerId));
                }

                // Add the new answers
                foreach (var answerText in newAnswers)
                {
                    FaqAns faqAns = new FaqAns();
                    faqAns.que_id = Convert.ToInt32(questionId);
                    faqAns.ans = answerText;
                    await _faqAns.Insert(faqAns);
                }

                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return View("Error");
            }
        }
    }


    public class Test
    {
        public Dictionary<string, string> question { get; set; }
        public List<string> newans { get; set; }
        public List<Dictionary<string, string>> oldans { get; set; }
    }
    public class Testder
    {
        public int val { get; set; }
        public string val2 { get; set; }
    }
}
public class Testder2
{
    public int val { get; set; }
    // public Testder testder { get; set; }
}



    

