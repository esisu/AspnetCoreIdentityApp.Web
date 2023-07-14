using Microsoft.AspNetCore.Identity;

namespace AspnetCoreIdentityApp.Web.Localizations
{
    public class LocalizationIdentityDescriber : IdentityErrorDescriber
    {
        public override IdentityError DuplicateUserName(string userName)
        {
            return new IdentityError()
            {
                Code = "DuplicateUserName",
                Description = $"{userName} daha önce başka bir kullanıcı tarafından alınmıştır"
            };
            //return base.DuplicateUserName(userName);
        }

        public override IdentityError DuplicateEmail(string email)
        {
            return new IdentityError()
            {
                Code = "DuplicateEmail",
                Description = $"{email}  adresi daha önce başka bir kullanıcı tarafından alınmıştır"
            };
            //return base.DuplicateUserName(userName);
        }

        public override IdentityError PasswordTooShort(int length)
        {
            return new IdentityError()
            {
                Code = "PasswordTooShort",
                Description = "Şifre en az 6 karakterli olmalıdır"
            };
            //return base.PasswordTooShort(length);
        }
    }
}
