using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;

namespace Client.ViewModels
{
    public class TransferViewModel
    {
        [Required]
        [StringLength(26, ErrorMessage = "Invalid id length (must be 26 digits)", MinimumLength = 26)]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Invalid id format (must contain digits only)")]
        public string DestinationId { get; set; }  

        [Required]
        [MaxLength(256, ErrorMessage = "Title too long (max 256 characters)")]   
        public string Title { get; set; }

        [Required]
        [Range(0.01, 9999999999999999.99, ErrorMessage = "Invalid amount (min = 0.01, max = 16 digits)")]
        [RegularExpression(@"^\d+.?\d{0,2}$", ErrorMessage = "Invalid amount (max two decimal points)")]
        public decimal Amount { get; set; }
    }
}
