using System.ComponentModel.DataAnnotations;

namespace AspnetCoreIdentityApp.Web.ViewModels
{
    public class SignUpViewModel
    {

        public SignUpViewModel()
        {

        }

        public SignUpViewModel(string username, string email, string phone, string password, string confirmPassword)
        {
            Username = username;
            Email = email;
            Phone = phone;
            Password = password;
            ConfirmPassword = confirmPassword;
        }

        [Required(ErrorMessage = "Ad alanını Lütfen Boş Bırakmayınız")]
        [Display(Name = "Kullanıcı Adı : ")]
        public string Username { get; set; }

        [EmailAddress(ErrorMessage = "Email Formatı yanlış kontrol ediniz")]
        [Required(ErrorMessage = "Email alanını Lütfen Boş Bırakmayınız")]
        [Display(Name = "E-Posta : ")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Telefon alanını Lütfen Boş Bırakmayınız")]
        [Display(Name = "Telefon : ")]
        public string Phone { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Şifre alanını Lütfen Boş Bırakmayınız")]
        [Display(Name = "Şifre : ")]
        [MinLength(6, ErrorMessage = "Şifreniz En az 6 karakter olabilir")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare(nameof(Password),ErrorMessage = "Şifreler aynı değil kontrol ediniz")]
        [Required(ErrorMessage = "Şifre Tekrar alanını Lütfen Boş Bırakmayınız")]
        [Display(Name = "Şifre Tekrar : ")]
        [MinLength(6, ErrorMessage = "Şifreniz En az 6 karakter olabilir")]
        public string ConfirmPassword { get; set; }

    }
}
