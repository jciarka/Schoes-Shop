using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;

namespace SportsStore.WebUI.Infrastructure.Abstract
{
    public interface IAuthProvider
    {
        bool Authenticate(string username, string password);

        Task<bool> AuthenticateAsync(string username, string password);

        void SignOut();

        bool IsSignedIn();

        string GetName();

        IdentityUserRole[] GetRoles();

        Task<IdentityUserRole[]> GetRolesAsync();





    }
}
