using System.ComponentModel.DataAnnotations;

namespace AspnetCoreIdentityApp.Web.ViewModels
{
    public class ForgetPasswordViewModel
    {

        [Display(Name = "Email : ")]
        [EmailAddress(ErrorMessage = "Email Formatı yanlış kontrol ediniz")]
        [Required(ErrorMessage = "Email alanını Lütfen Boş Bırakmayınız")]
        public string Email { get; set; }

    }
}
