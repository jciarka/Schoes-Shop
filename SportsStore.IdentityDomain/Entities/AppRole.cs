using Microsoft.AspNet.Identity.EntityFramework;

namespace SportsStore.IdentityDomain.Entities
{
    public class AppRole : IdentityRole
    {
        public AppRole() : base() {}

        public AppRole(string name) : base(name) { }
    }
}
