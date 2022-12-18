using Microsoft.Extensions.Options;
using Sabio.Data;
using Sabio.Data.Providers;
using Sabio.Models.Domain;
using Sabio.Models.Domain.Config;
using Sabio.Models.Requests;
using Sabio.Services.Interfaces;
using Stripe;
using Stripe.Checkout;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabio.Services
{
    public class CheckoutService : ICheckoutService
    {
        IDataProvider _data = null;
        private StripeConfig _stripe = null;

        public CheckoutService(IDataProvider data, IOptions<StripeConfig> stripeModel)
        {
            _data = data;
            _stripe = stripeModel.Value;
        }

        public string CreateCheckoutSessionId(CheckoutSessionRequest model, int userId)
        {
            var domain = _stripe.StripeUrl;
            string customerId = null;
            LookUp customer = GetCustomerId(userId);
            customerId = customer.Name;
            if (customerId == null)
            {
                var cOptions = new CustomerCreateOptions
                {
                    Email = model.CurrentUserEmail,
                    Name = model.Name,
                };
                var customerService = new CustomerService();
                customerId = customerService.Create(cOptions).Id;

                string procName = "[dbo].[StripeCustomers_Insert]";
                _data.ExecuteNonQuery(procName,
                    inputParamMapper: delegate (SqlParameterCollection col)
                    {
                        AddSingleCustomer(userId, col, customerId);
                    });
            }
            else
            {
                customerId = customer.Name;
            }

            var options = new SessionCreateOptions
            {
                SuccessUrl = domain + "/products/processing?session_id={CHECKOUT_SESSION_ID}",
                CancelUrl = domain + "/products",
                PaymentMethodTypes = new List<string>
                {
                    "card",
                },
                Mode = "subscription",
                Customer = customerId,
                LineItems = new List<SessionLineItemOptions>
                {
                    new SessionLineItemOptions
                    {
                        Price = model.PriceId,
                        Quantity = 1,
                    },
                },
            };
            var service = new SessionService();
            Session session = service.Create(options);
            return session.Id;
        }

        public LookUp GetCustomerId(int userId)
        {
            int startingIndex = 0;
            string ProcName = "[dbo].[StripeCustomers_GetById]";
            LookUp customer = new LookUp();
            _data.ExecuteCmd(ProcName,
                inputParamMapper: delegate (SqlParameterCollection col)
                {
                    col.AddWithValue("@UserId", userId);
                },
                singleRecordMapper: delegate (IDataReader reader, short set)
                {
                    customer.Id = reader.GetSafeInt32(startingIndex++);
                    customer.Name = reader.GetSafeString(startingIndex++);
                });
            return customer;
        }

        private static void AddSingleCustomer(int userId, SqlParameterCollection col, string customerId)
        {
            col.AddWithValue("@UserId", userId);
            col.AddWithValue("CustomerId", customerId);
        }

        public string CreateNewAccount()
        {
            var options = new AccountCreateOptions { Type = "standard" };
            var service = new AccountService();
            return service.Create(options).Id;
        }

        public string CreateAccountLink(string accountId)
        {
            var options = new AccountLinkCreateOptions
            {
                Account = accountId,
                RefreshUrl = "https://example.com/reauth",
                ReturnUrl = "https://example.com/return",
                Type = "account_onboarding",
            };
            var service = new AccountLinkService();
            return service.Create(options).Url;
        }
    }
}

