using System.ComponentModel.DataAnnotations;

namespace Client.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [MaxLength(256)]
        public string UserName { get; set; }

        [Required, DataType(DataType.Password)]
        [MinLength(3), MaxLength(100)]
        public string Password { get; set; }

        [Required, DataType(DataType.Password), Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
    }
}
