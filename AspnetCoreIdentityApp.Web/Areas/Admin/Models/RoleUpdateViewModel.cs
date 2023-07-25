using System.ComponentModel.DataAnnotations;

namespace AspnetCoreIdentityApp.Web.Areas.Admin.Models
{
    public class RoleUpdateViewModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Rol Adı alanını Lütfen Boş Bırakmayınız")]
        [Display(Name = "Rol Adı : ")]
        public string Name { get; set; }

    }
}
