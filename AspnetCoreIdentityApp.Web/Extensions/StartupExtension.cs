using AspnetCoreIdentityApp.Web.CustomValidators;
using AspnetCoreIdentityApp.Web.Localizations;
using AspnetCoreIdentityApp.Web.Models;
using Microsoft.AspNetCore.Identity;

namespace AspnetCoreIdentityApp.Web.Extensions
{
    public static class StartupExtension
    {
        public static void AddIdentityWithExtension(this IServiceCollection service)
        {
            service.Configure<DataProtectionTokenProviderOptions>(options =>
            {
                options.TokenLifespan = TimeSpan.FromHours(2);
            });

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

                    option.Lockout.DefaultLockoutTimeSpan=TimeSpan.FromMinutes(3);
                    option.Lockout.MaxFailedAccessAttempts = 3;
                }
            ).AddPasswordValidator<PasswordValidator>()
                .AddUserValidator<UserValidator>()
                .AddErrorDescriber<LocalizationIdentityDescriber>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();
        }
    }
}
