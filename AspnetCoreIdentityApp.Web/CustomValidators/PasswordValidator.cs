using AspnetCoreIdentityApp.Web.Models;
using Microsoft.AspNetCore.Identity;

namespace AspnetCoreIdentityApp.Web.CustomValidators
{
    public class PasswordValidator : IPasswordValidator<AppUser>
    {
        public Task<IdentityResult> ValidateAsync(UserManager<AppUser> manager, AppUser user, string? password)
        {
            var errors = new List<IdentityError>();

            if (password!.ToLower().Contains(user.UserName!.ToLower()))
            {
                errors.Add(new()
                {
                    Code = "PasswordContainsUsername",
                    Description = "Şifre alanı kullanıcı adı içeremez"
                });
            }

            if (password!.ToLower().StartsWith("1234"))
            {
                errors.Add(new()
                {
                    Code = "PasswordContain1234",
                    Description = "Şifre 1234 ile başlayamaz"
                });
            }

            if (errors.Any())
            {
                return Task.FromResult(IdentityResult.Failed(errors.ToArray()));
            }

            return Task.FromResult(IdentityResult.Success);
        }
    }
}
