using AspnetCoreIdentityApp.Web.CustomValidators;
using AspnetCoreIdentityApp.Web.Localizations;
using AspnetCoreIdentityApp.Web.Models;

namespace AspnetCoreIdentityApp.Web.Extensions
{
    public static class StartupExtension
    {
        public static void AddIdentityWithExtension(this IServiceCollection service)
        {
            service.AddIdentity<AppUser, AppRole>(
                option =>
                {
                    option.User.RequireUniqueEmail = true;
                    option.User.AllowedUserNameCharacters = "abcdefghijklmnoprstuvyzqwx1234567890_";

                    option.Password.RequiredLength = 6;
                    option.Password.RequireNonAlphanumeric = false;
                    option.Password.RequireLowercase = true;
                    option.Password.RequireUppercase = false;
                    option.Password.RequireDigit = false;
                }
            ).AddPasswordValidator<PasswordValidator>().AddUserValidator<UserValidator>().AddErrorDescriber<LocalizationIdentityDescriber>().AddEntityFrameworkStores<AppDbContext>();
        }
    }
}
