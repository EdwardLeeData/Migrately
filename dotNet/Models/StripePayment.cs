using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabio.Models.Domain
{
    public class StripePayment
    {
        public double Amount { get; set; }
        public double AmountReceived { get; set; }
        public DateTime Created { get; set; }
        public string ProductName { get; set; }
        public string CustomerName { get; set; }
    }
}
