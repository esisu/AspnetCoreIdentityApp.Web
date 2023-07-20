using System.ComponentModel.DataAnnotations;

namespace AspnetCoreIdentityApp.Web.ViewModels
{
    public class PasswordChangeViewModel
    {
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Şifre alanını Lütfen Boş Bırakmayınız")]
        [Display(Name = "Mevcut Şifre : ")]
        public string PasswordOld { get; set; } = null;

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Yeni Şifre alanını Lütfen Boş Bırakmayınız")]
        [Display(Name = "Yeni Şifre : ")]
        [MinLength(6,ErrorMessage = "Şifreniz En az 6 karakter olabilir")]
        public string PasswordNew { get; set; } = null;

        [DataType(DataType.Password)]
        [Compare(nameof(PasswordNew), ErrorMessage = "Şifreler aynı değil kontrol ediniz")]
        [Required(ErrorMessage = "Şifre Tekrar alanını Lütfen Boş Bırakmayınız")]
        [Display(Name = "Şifre Tekrar : ")]
        [MinLength(6, ErrorMessage = "Şifreniz En az 6 karakter olabilir")]
        public string PasswordConfirm { get; set; } = null;
    }
}
