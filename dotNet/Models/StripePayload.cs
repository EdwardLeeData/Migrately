using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Sabio.Models.Domain
{
    public class StripePayload
    {
        public StripePayment SessionPayload { get; set; }
        public StripeSubscription SubscriptionPayload { get; set; }
        public string InvoiceDetail { get; set; }
    }
}
