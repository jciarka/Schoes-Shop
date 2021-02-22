using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using SportsStore.WebUI.Models;
using SportsStore.IdentityDomain.Entities;
using SportsStore.IdentityDomain.Infrastructure;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;

namespace SportsStore.WebUI.Controllers
{
    [Authorize(Roles = "AccountAdmin")]
    public class AccountAdminController : Controller
    {
        public ActionResult Index()
        {
            return View(UserManager.Users);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(CreateUserModel model)
        {
            if (ModelState.IsValid)
            {
                AppUser user = new AppUser { UserName = model.Name, Email = model.Email };
                IdentityResult result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    TempData["Message"] = $"Utworzono użytkownika {user.UserName}";
                    return RedirectToAction("Index");
                }
                else 
                {
                    AddErrorsFromResult(result);
                }
            }
            return View(model);
        }
        

        [HttpPost]
        public async Task<ActionResult> Delete(string id)
        {
            AppUser user = await UserManager.FindByIdAsync(id);
            if (user != null)
            {
                IdentityResult deleteResult= await UserManager.DeleteAsync(user);
                if (deleteResult.Succeeded)
                {
                    TempData["Message"] = $"Usunięto użytkownika {user.UserName}";
                    ModelState.AddModelError("", "Error na próbe");
                    return RedirectToAction("Index");
                }
                else
                {
                    AddErrorsFromResult(deleteResult);
                }
            }
            else
            {
                ModelState.AddModelError("", "Nie znaleziono użytkownika");
            }
            return RedirectToAction("Index");
        }


        public async Task<ActionResult> Edit(string id)
        {
            AppUser user = await UserManager.FindByIdAsync(id);
            if (user != null)
            {
                return View(user);
            }
            else
            {
                return Redirect("Index");
            }
        }


        [HttpPost]
        public async Task<ActionResult> Edit(string id, string email, string password)
        {
            AppUser user = await UserManager.FindByIdAsync(id);
            if (user != null)
            {
                user.Email = email;
                IdentityResult emailResult = await UserManager.UserValidator.ValidateAsync(user);
                if (!emailResult.Succeeded)
                {
                    AddErrorsFromResult(emailResult);
                }

                IdentityResult passwordResult = null;
                if (password != String.Empty)
                {
                    passwordResult = await UserManager.PasswordValidator.ValidateAsync(password);
                    if (passwordResult.Succeeded)
                    {
                        user.PasswordHash = UserManager.PasswordHasher.HashPassword(password);
                    }
                    else
                    {
                        AddErrorsFromResult(passwordResult);
                    }
                }

                if (emailResult.Succeeded && password != string.Empty && passwordResult.Succeeded)
                {
                    IdentityResult updateResult = await UserManager.UpdateAsync(user);
                    if (updateResult.Succeeded)
                    {
                        TempData["Message"] = $"Poprawnie edytowano użytkownika {user.UserName}";
                        return RedirectToAction("Index");
                    }
                    AddErrorsFromResult(updateResult);
                }
            }
            else 
            {
                ModelState.AddModelError("", "Nie znaleziono użytkownika");
            }
            return View(user);
        }

        private void AddErrorsFromResult(IdentityResult result)
        {
            foreach (string error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private AppUserManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<AppUserManager>();
            }
        }
    }
}