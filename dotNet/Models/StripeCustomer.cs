using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabio.Models.Domain
{
    public class StripeSubscribedCustomer
    {
        public string SubscriptionId { get; set; }
        public string CustomerId { get; set; }
    }
}
