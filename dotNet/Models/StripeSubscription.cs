using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabio.Models.Domain
{
    public class StripeSubscription
    {
        public string SubscriptionId { get; set; }
        public int UserId { get; set; }
        public string CustomerId { get; set; }
        public DateTime DateCreated { get; set; }
        public string Status { get; set; }
        public string PriceId { get; set; }

    }
}
