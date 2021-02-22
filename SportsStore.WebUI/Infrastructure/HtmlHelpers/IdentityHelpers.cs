
using System.Web;
using System.Web.Mvc;
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

namespace SportsStore.WebUI.Infrastructure.HtmlHelpers
{
    static public class IdentityHelpers
    {
        static public MvcHtmlString GetUserName(this HtmlHelper html, string id)
        {
            AppUserManager userManager = HttpContext.Current.GetOwinContext()
                                                    .GetUserManager<AppUserManager>();
            return new MvcHtmlString(userManager.FindById(id).UserName);
        }
    }
}