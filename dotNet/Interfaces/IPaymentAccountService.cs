using Sabio.Models;
using Sabio.Models.Domain;
using Sabio.Models.Requests.PaymentAccount;
using System.Collections.Generic;

namespace Sabio.Services.Interfaces
{
    public interface IPaymentAccountService
    {
        PaymentAccount SelectById(int id);
        int Add(PaymentAccountAddRequest model, int userId);
        void Update(PaymentAccountUpdateRequest model, int userId);
        void Delete(int id);
        Paged<PaymentAccount> SelectAllPagination(int pageIndex, int pageSize);
        Paged<PaymentAccount> SelectByCreatedByPagination(int pageIndex, int pageSize, int id);
    }
}