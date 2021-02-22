using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using SportsStore.IdentityDomain.Entities;
using SportsStore.IdentityDomain.Infrastructure;

namespace SportsStore.IdentityDomain.Infrastructure
{
    public class AppUserManager : UserManager<AppUser>
    {
        public AppUserManager(IUserStore<AppUser> store) : base(store)
        { 
        }

        public static AppUserManager Create(IdentityFactoryOptions<AppUserManager> options,
                                            IOwinContext context)
        {
            AppIdentityDbContext db = context.Get<AppIdentityDbContext>();
            AppUserManager manager = new AppUserManager(new UserStore<AppUser>(db));

            manager.PasswordValidator = new PasswordValidator()
            {
                RequiredLength = 6,
                RequireDigit = false,
                RequireLowercase = true,
                RequireUppercase = true,
                RequireNonLetterOrDigit = false
            };

            manager.UserValidator = new UserValidator<AppUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = true,
                RequireUniqueEmail = true
            };

            return manager;
        }
    }
}
