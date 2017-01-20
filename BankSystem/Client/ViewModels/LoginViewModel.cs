using System.ComponentModel.DataAnnotations;

namespace Client.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [MaxLength(256)]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
