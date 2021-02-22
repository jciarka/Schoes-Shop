using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SportsStore.WebUI.Infrastructure.Abstract;
using SportsStore.WebUI.Models;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace SportsStore.WebUI.Controllers
{
    public class AccountController : Controller
    {
        IAuthProvider authProvider;

        public AccountController(IAuthProvider auth)
        {
            authProvider = auth;
        }

        public ViewResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                bool result = await authProvider.AuthenticateAsync(model.UserName, model.Password);
                if (result)
                {
                    return Redirect(returnUrl ?? Url.Action("List", "Product"));//Url.Action("Index","Admin"));
                }
                else
                {
                    ModelState.AddModelError("", "Nieprawidłowa nazwa użytkownika lub nieprawidłowe hasło");
                    return View();
                }
            }
            return View();
        }

        public RedirectResult Logout(string returnUrl)
        {
            authProvider.SignOut();
            return Redirect(returnUrl);
        }
    }
}