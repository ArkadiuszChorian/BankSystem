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
        [Required, StringLength(26, ErrorMessage = "Incorrect length", MinimumLength = 26)]
        public string DestinationId { get; set; }  
        [Required, MaxLength(256)]     
        public string Title { get; set; }
        [Required, Range(0, int.MaxValue)]
        public decimal Amount { get; set; }
    }
}
