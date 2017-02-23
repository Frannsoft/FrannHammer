using System.ComponentModel.DataAnnotations;

namespace FrannHammer.Models
{
    public class RegisterExternalBindingModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}