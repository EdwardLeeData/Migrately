using Sabio.Models.Domain;
using Sabio.Models.Requests;

namespace Sabio.Services.Interfaces
{
    public interface IStripeProductService
    {
        StripeProduct GetById(int id);
    }
}