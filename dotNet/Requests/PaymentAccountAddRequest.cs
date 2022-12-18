using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabio.Models.Requests.PaymentAccount
{
    public class PaymentAccountAddRequest
    {
        [Required]
        [StringLength(int.MaxValue, MinimumLength = 1)]
        public string AccountId { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        public int PaymentTypeId { get; set; }

    }
}
