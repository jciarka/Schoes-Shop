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
using System.ComponentModel.DataAnnotations;

namespace SportsStore.WebUI.Models
{
    public class RoleEditModel
    {
        public AppRole Role { get; set; }
        public IEnumerable<AppUser> Members { get; set; }
        public IEnumerable<AppUser> NonMembers { get; set; }

    }

    public class RoleModificateModel
    {
        public string Role { get; set; }
        public string[] IdsToAdd { get; set; }
        public string[] IdsToRemove { get; set; }
    }
}