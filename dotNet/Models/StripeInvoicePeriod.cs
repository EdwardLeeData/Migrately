using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabio.Models.Domain
{
    public class StripeInvoicePeriod
    {
        public DateTime InvoiceStart { get; set; }
        public DateTime InvoiceEnd { get; set; }
    }
}
