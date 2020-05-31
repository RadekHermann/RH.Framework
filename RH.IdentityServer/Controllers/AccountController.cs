
using System;
using System.Threading.Tasks;

using IdentityServer4.Services;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using RH.IdentityServer.Inftrastructure.Data.Identity;
using RH.IdentityServer.Models;

namespace RH.IdentityServer.Controllers
{
    [ApiController]
    [Route("auth/[controller]")]
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;
        private readonly IIdentityServerInteractionService interactionService;

        public AccountController(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            IIdentityServerInteractionService interactionService)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.interactionService = interactionService;
        }

        [HttpPost]
        [Route("{action}")]
        public IActionResult Create()
        {
            return Ok();
        }

        [HttpPost]
        [Route("{action}")]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var context = await this.interactionService.GetAuthorizationContextAsync(model.ReturnUrl);

                var user = await this.userManager.FindByNameAsync(model.UserName);
                if (user != null && await this.userManager.CheckPasswordAsync(user, model.Password))
                {
                    AuthenticationProperties? properties = null;
                    if (AccountOptions.AllowRememberLogin && model.RememberMe)
                    {
                        properties = new AuthenticationProperties
                        {
                            IsPersistent = true,
                            ExpiresUtc = DateTimeOffset.Now.Add(AccountOptions.RememberMeLoginDuration)
                        };
                    }

                    await this.signInManager.SignInAsync(user, properties);

                    if (context != null)
                    {
                        return Ok(model.ReturnUrl);
                    }
                    else
                    {
                        throw new Exception("invalid return URL");
                    }
                }

                ModelState.AddModelError(string.Empty, AccountOptions.InvalidCredentialsErrorMessage);
            }

            return BadRequest(ModelState);
        }

        [HttpGet]
        [Authorize]
        [Route("{action}")]
        public async Task<IActionResult> Logout(string logoutId)
        {
            var context = await this.interactionService.GetLogoutContextAsync(logoutId);
            if (context != null)
            {
                await this.signInManager.SignOutAsync();

                return Ok(context.PostLogoutRedirectUri);
            }

            return BadRequest();
        }
    }
}
