using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabio.Models.Requests

{
    public class CheckoutSessionRequest
    {
        [Required]
        public string PriceId { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string CurrentUserEmail { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 1)]
        public string Name { get; set; }
    }
}
