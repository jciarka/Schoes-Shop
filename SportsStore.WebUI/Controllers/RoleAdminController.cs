using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using ShoesStore.WebUI.Models;
using ShoesStore.IdentityDomain.Entities;
using ShoesStore.IdentityDomain.Infrastructure;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ShoesStore.WebUI.Controllers
{
    [Authorize(Roles = "RoleAdmin")]
    public class RoleAdminController : Controller
    {
        // GET: RoleAdmin
        public ActionResult Index()
        {
            return View(RoleManager.Roles);
        }

        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public async Task<ActionResult> Create([Required] string roleName)
        {
            if (ModelState.IsValid)
            {
                AppRole role = new AppRole(roleName);
                IdentityResult createResult = await RoleManager.CreateAsync(role);
                if (createResult.Succeeded)
                {
                    TempData["SuccesMessage"] = $"Poprawnie utworzono role: {roleName}";
                    return RedirectToAction("Index");
                }
                else
                {
                    AddErrorsFromResult(createResult);
                }
            }
            return View(roleName);
        }



        [HttpPost]
        public async Task<ActionResult> Delete(string Id)
        {
            if (ModelState.IsValid)
            {
                AppRole role = await RoleManager.FindByIdAsync(Id);
                if (role != null)
                {
                    IdentityResult deleteResult = await RoleManager.DeleteAsync(role);
                    if (deleteResult.Succeeded)
                    {
                        TempData["SuccesMessage"] = $"Poprawnie usunięto rolę: {role.Name}";
                    }
                    else
                    {
                        TempData["ErrorMessage"] = $"Błąd! Nie usinięto roli: {role.Name}";
                    }
                }
                else
                {
                    TempData["ErrorMessage"] = $"Nie znaleziono roli o Id: {Id}";
                }
            }
            return RedirectToAction("Index");
        }


        public async Task<ActionResult> Edit(string Id)
        {
            AppRole role = await RoleManager.FindByIdAsync(Id);
            if (role != null)
            {
                RoleEditModel editModel = createRoleEditModel(role);
                return View(editModel);
            }
            else
            {
                TempData["ErrorMessage"] = $"Nie znaleziono roli o Id: {Id}";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Edit(RoleModificateModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityResult result;
                if (model.IdsToAdd != null)
                { 
                    foreach (string userId in model.IdsToAdd)
                    {
                        result = await UserManager.AddToRoleAsync(userId, model.Role);
                        if (!result.Succeeded)
                        {
                            AddErrorsFromResult(result);
                            TempData["ErrorMessage"] = $"Błąd w edycji roli użytkownika " +
                                 $"{UserManager.FindByIdAsync(userId)}";
                            return View("Error", result.Errors);
                        }
                    }
                }

                if (model.IdsToRemove != null)
                {
                    foreach (string userId in model.IdsToRemove)
                    {
                        result = await UserManager.RemoveFromRoleAsync(userId, model.Role);
                        if (!result.Succeeded)
                        {
                            AddErrorsFromResult(result);
                            TempData["ErrorMessage"] = $"Błąd w edycji roli użytkownika " +
                                $"{UserManager.FindByIdAsync(userId)}";
                            return View("Error", result.Errors);
                        }
                    }
                }

                TempData["SuccesMessage"] = $"Poprawnie ustalono przynależność użytkowników do ról";
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }





        private RoleEditModel createRoleEditModel(AppRole role)
        {
            IEnumerable<string> memberIDs = role.Users.Select(member => member.UserId);
            IEnumerable<AppUser> members = UserManager.Users.Where(
                                        user => memberIDs.Any(memberId => memberId == user.Id));
            IEnumerable<AppUser> nonMembers = UserManager.Users.Except(members);
            RoleEditModel editModel = new RoleEditModel()
            {
                Role = role,
                Members = members,
                NonMembers = nonMembers
            };
            return editModel;
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

        private AppRoleManager RoleManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<AppRoleManager>();
            }
        }
    }
}