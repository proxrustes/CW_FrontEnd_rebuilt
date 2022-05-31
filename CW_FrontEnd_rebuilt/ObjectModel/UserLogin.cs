using System.ComponentModel.DataAnnotations;

namespace CW_FrontEnd_rebuilt.ObjectModel
{
    public class UserLogin
    {
        [Required(ErrorMessage = "Username requaired")]
        [MaxLength(24, ErrorMessage = "Max length is 24 symbols")]
        [Display(Name = "Username")]
        public string username { get; set; }

        [Required]
        [Display(Name = "Your password")]
        public string? password { get; set; }
    }
}
