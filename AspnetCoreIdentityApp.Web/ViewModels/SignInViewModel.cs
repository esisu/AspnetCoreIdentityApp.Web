using System.ComponentModel.DataAnnotations;

namespace AspnetCoreIdentityApp.Web.ViewModels
{
    public class SignInViewModel
    {
        public SignInViewModel()
        {
            
        }

        public SignInViewModel(string email, string password)
        {
            Email = email;
            Password = password;
        }

        [Display(Name = "Email : ")]
        [EmailAddress(ErrorMessage = "Email Formatı yanlış kontrol ediniz")]
        [Required(ErrorMessage = "Email alanını Lütfen Boş Bırakmayınız")]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Şifre alanını Lütfen Boş Bırakmayınız")]
        [Display(Name = "Şifre : ")]
        public string Password { get; set; }

        
        [Display(Name = "Beni Hatırla ")]
        public bool RememberMe { get; set; }
    }
}
