using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabio.Models.Requests
{
    public class SubscriptionAddRequest
    {
        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string SubscriptionId { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string CustomerId { get; set; }
        [Required]
        public DateTime DateCreated { get; set; }
        [Required]
        [StringLength(10, MinimumLength = 1)]
        public string Status { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string PriceId { get; set; }
    }
}
