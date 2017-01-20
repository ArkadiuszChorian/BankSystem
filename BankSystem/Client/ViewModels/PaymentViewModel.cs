using System.ComponentModel.DataAnnotations;

namespace Client.ViewModels
{
    public class PaymentViewModel
    {
        [Required]
        [Range(0.01, 9999999999999999.99, ErrorMessage = "Invalid amount (min = 0.01, max = 16 digits)")]
        [RegularExpression(@"^\d+.?\d{0,2}$", ErrorMessage = "Invalid amount (max two decimal points)")]       
        public decimal Amount { get; set; }
    }
}
