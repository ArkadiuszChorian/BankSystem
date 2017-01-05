using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Client.ViewModels
{
    public class PaymentViewModel
    {
        public int AmountMain { get; set; }

        [Range(0, 99)]
        public int AmountReminder { get; set; }

        public decimal DecimalAmount()
        {
            var main = (decimal) AmountMain;
            var reminder = (decimal) AmountReminder;

            return main + reminder / 100;
        }
    }
}
