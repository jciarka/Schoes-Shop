using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShoesStore.WebUI.Infrastructure.Abstract;
using ShoesStore.WebUI.Models;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

using ShoesStore.IdentityDomain.Entities;
using ShoesStore.IdentityDomain.Infrastructure;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Security.Claims;

namespace ShoesStore.WebUI.Infrastructure.Concrete
{
    public class AppUserAuthProvider: IAuthProvider
    {
        public bool Authenticate(string username, string password)
        {
            throw new NotImplementedException();
        }
        public async Task<bool> AuthenticateAsync(string username, string password)
        {
            AppUser user = await UserManager.FindAsync(username, password);
            if (user == null)
            {
                return false;
            }
            else
            {
                ClaimsIdentity ident = await UserManager.CreateIdentityAsync(user, 
                                                DefaultAuthenticationTypes.ApplicationCookie);
                AuthManager.SignOut();
                AuthManager.SignIn(new AuthenticationProperties { IsPersistent = false }, ident);
                return true;
            }
        }

        /*Nowe funkcje od tąd*/
        public void SignOut()
        {
            AuthManager.SignOut();
        }

        public bool IsSignedIn()
        {
            return AuthManager.User.Identity.IsAuthenticated;
            //return HttpContext.Current.User.Identity.IsAuthenticated;
        }

        public string GetName()
        {
            if (!IsSignedIn())
            {
                return null;
            }
            return AuthManager.User.Identity.Name;
        }

        public IdentityUserRole[] GetRoles()
        {
            throw new NotImplementedException();
            /*
            if (!IsSignedIn())
            {
                return null;
            }
            string id = AuthManager.User.Identity.GetUserId();
            AppUser user = UserManager.FindById(id);
            if (id == null)
            {
                return null;
            }
            return user.Roles.ToArray();
            */
        }

        public async Task<IdentityUserRole[]> GetRolesAsync()
        {/*
            if (!IsSignedIn())
            {
                return null;
            }
            string id = AuthManager.User.Identity.GetUserId();
            AppUser user = await UserManager.FindByIdAsync(id);
            if (id == null)
            {
                return null;
            }
            throw new NotImplementedException();*/
            throw new NotImplementedException();
        }


        /*Nowe funkcje do tąd*/

        private IAuthenticationManager AuthManager
        {
            get
            {   //Tak można pobrać wartość bieżącego konteksty HTTP
                IAuthenticationManager authManager = HttpContext.Current.GetOwinContext().Authentication;
                if(authManager == null)
                {
                    throw new NotImplementedException("Can't get current Http context");
                }
                return authManager;
            }
        }

        private AppUserManager UserManager
        {
            get
            {   //Tak można pobrać wartość bieżącego konteksty HTTP
                AppUserManager appManager = HttpContext.Current.GetOwinContext().GetUserManager<AppUserManager>(); 
                if (appManager == null)
                {
                    throw new NotImplementedException("Can't get current Http context");
                }
                return appManager;                
            }
        }

        private AppRoleManager RoleManager
        {
            get
            {   //Tak można pobrać wartość bieżącego konteksty HTTP
                AppRoleManager appManager = HttpContext.Current.GetOwinContext().GetUserManager<AppRoleManager>();
                if (appManager == null)
                {
                    throw new NotImplementedException("Can't get current Http context");
                }
                return appManager;
            }
        }
    }


}
