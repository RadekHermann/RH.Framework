using System.ComponentModel.DataAnnotations;

namespace RH.IdentityServer.Models
{
    public class LoginModel
    {
        [Required]
        public string UserName { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;

        public bool RememberMe { get; set; }

        public string ReturnUrl { get; set; } = null!;
    }
}
