using System.ComponentModel.DataAnnotations;
using AspnetCoreIdentityApp.Web.Models;

namespace AspnetCoreIdentityApp.Web.ViewModels
{
    public class UserEditViewModel
    {
        public UserEditViewModel()
        {

        }

        public UserEditViewModel(string username, string email, string phone)
        {
            Username = username;
            Email = email;
            Phone = phone;
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

        [DataType(DataType.Date)]
        [Display(Name = "Doğum Tarihi : ")]
        public DateTime? BirthDate { get; set; }
        
        [Display(Name = "Şehir : ")]
        public string City { get; set; }
        
        [Display(Name = "Cinsiyet : ")]
        public Gender? Gender { get; set; }

        [Display(Name = "Resim : ")]
        public IFormFile Picture { get; set; }

    }
}
