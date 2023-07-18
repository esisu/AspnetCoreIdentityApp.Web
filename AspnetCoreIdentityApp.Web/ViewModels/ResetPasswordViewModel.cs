using System.ComponentModel.DataAnnotations;

namespace AspnetCoreIdentityApp.Web.ViewModels
{
    public class ResetPasswordViewModel
    {
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Yeni Şifre alanını Lütfen Boş Bırakmayınız")]
        [Display(Name = "Yeni Şifre : ")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Yeni Şifreler aynı değil kontrol ediniz")]
        [Required(ErrorMessage = "Yeni Şifre Tekrar alanını Lütfen Boş Bırakmayınız")]
        [Display(Name = "Yeni Şifre Tekrar : ")]
        public string ConfirmPassword { get; set; }
    }
}
