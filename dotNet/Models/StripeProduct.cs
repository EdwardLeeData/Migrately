using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabio.Models.Domain
{
    public class StripeProduct
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ProductId { get; set; }
        public string PriceId { get; set; }
        public decimal Amount { get; set; }
        public string Term { get; set; }

    }
}
