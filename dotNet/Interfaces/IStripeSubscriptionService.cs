using Sabio.Models.Domain;
using Sabio.Models.Requests;

namespace Sabio.Services.Interfaces
{
    public interface IStripeSubscriptionService
    {
        int Add(SubscriptionAddRequest model, int userId);
        string CreateInvoicePdf(string subscriptionId);
        public StripePayload GetSubscriptionDetail(string sessionId, int userId);
        StripeInvoicePeriod GetInvoicePeriod(int userId);
        StripeSubscribedCustomer GetInvoice(int userId);
    }
}