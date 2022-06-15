using System.ComponentModel.DataAnnotations;

namespace CW_FrontEnd_rebuilt.ObjectModel
{
    public class UserLogin
    {
        [Required(ErrorMessage = "Username required")]

        public string username { get; set; }

        [Required(ErrorMessage = "Password required")]
        public string? password { get; set; }
    }
}
