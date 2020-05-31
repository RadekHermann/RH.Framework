using System;

using Microsoft.AspNetCore.Identity;

namespace RH.IdentityServer.Inftrastructure.Data.Identity
{
    public class AppUser : IdentityUser<Guid>
    {
        public AppUser(string userName) : base(userName)
        {
        }
    }
}
