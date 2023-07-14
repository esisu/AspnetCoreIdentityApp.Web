using AspnetCoreIdentityApp.Web.Models;
using Microsoft.AspNetCore.Identity;

namespace AspnetCoreIdentityApp.Web.CustomValidators
{
    public class UserValidator : IUserValidator<AppUser>
    {
        public Task<IdentityResult> ValidateAsync(UserManager<AppUser> manager, AppUser user)
        {
            var isDigit = int.TryParse(user.UserName[0]!.ToString(), out _);

            var errors = new List<IdentityError>();

            if (isDigit)
            {
                errors.Add(new() { Code = "UsernameContainFirstLetterDigit", Description = "Kullanıcı ilk karakteri sayısal karakter içeremez" });
            }

            if (errors.Any())
            {
                return Task.FromResult(IdentityResult.Failed(errors.ToArray()));
            }

            return Task.FromResult(IdentityResult.Success);

        }
    }
}
