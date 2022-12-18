using Sabio.Models.Requests;
using Stripe;

namespace Sabio.Services.Interfaces
{
    public interface ICheckoutService
    {
        string CreateCheckoutSessionId(CheckoutSessionRequest model, int userId);
        string CreateNewAccount();
        string CreateAccountLink(string accountId);
    }
}