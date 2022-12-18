using Microsoft.Extensions.Options;
using Sabio.Data.Providers;
using Sabio.Models.Domain.Config;
using Sabio.Models.Requests;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stripe;
using Sabio.Services.Interfaces;
using Sabio.Models.Domain;
using Microsoft.Extensions.Configuration.UserSecrets;
using Stripe.BillingPortal;
using Stripe.Checkout;
using SessionService = Stripe.Checkout.SessionService;
using Session = Stripe.Checkout.Session;

namespace Sabio.Services
{
    public class StripeSubscriptionService : IStripeSubscriptionService
    {
        IDataProvider _data = null;
        private StripeConfig _stripe = null;
        public StripeSubscriptionService(IDataProvider data, IOptions<StripeConfig> stripeModel)
        {
            _data = data;
            _stripe = stripeModel.Value;
        }

        public int Add(SubscriptionAddRequest model, int userId)
        {
            int id = 0;
            string procName = "[dbo].[StripeSubscriptions_Insert]";
            _data.ExecuteNonQuery(procName,
                inputParamMapper: delegate (SqlParameterCollection col)
                {
                    AddSingleSubscription(model, userId, col);

                    SqlParameter idOut = new SqlParameter("@Id", SqlDbType.Int);
                    idOut.Direction = ParameterDirection.Output;
                    col.Add(idOut);
                },
                returnParameters: delegate (SqlParameterCollection returnCollection)
                {
                    object oId = returnCollection["@Id"].Value;
                    int.TryParse(oId.ToString(), out id);
                });

            return id;
        }

        public string CreateInvoicePdf(string subscriptionId)
        {
            var subscriptionService = new SubscriptionService();
            var subscriptionDetail = subscriptionService.Get(subscriptionId);
            var invoiceService = new InvoiceService();
            var invoiceId = subscriptionDetail.LatestInvoiceId;
            var invoice = invoiceService.Get(invoiceId);
            return invoice.InvoicePdf;
        }

        public StripePayload GetSubscriptionDetail(string sessionId, int userId)
        {
            Session sessionDetail;
            StripePayment sessionPayload;
            MapSingleSessionDetail(sessionId, out sessionDetail, out sessionPayload);
            StripeSubscription subscriptionPayload = MapSingleSubscriptionDetail(userId, sessionDetail);
            var responsePayload = new StripePayload
            {
                SessionPayload = sessionPayload,
                SubscriptionPayload = subscriptionPayload,
                InvoiceDetail = CreateInvoicePdf(sessionDetail.SubscriptionId),
            };

            return (responsePayload);
        }

        private static void MapSingleSessionDetail(string sessionId, out Session sessionDetail, out StripePayment sessionPayload)
        {
            var sessionService = new SessionService();
            sessionDetail = sessionService.Get(sessionId);
            sessionPayload = new StripePayment
            {
                Amount = (double)(sessionDetail.AmountSubtotal / 100),
                AmountReceived = (double)(sessionDetail.AmountTotal / 100),
                Created = sessionDetail.Created,
                ProductName = GetProductName(sessionDetail.SubscriptionId),
                CustomerName = sessionDetail.CustomerDetails.Name,
            };
        }

        public static string GetProductName(string subscriptionId)
        {
            var subscriptionService = new SubscriptionService();
            var subscriptionDetail = subscriptionService.Get(subscriptionId);
            var productId = subscriptionDetail.Items.Data[0].Price.ProductId;
            var productService = new ProductService();
            var productDetail = productService.Get(productId);
            return productDetail.Name;
        }
        private static StripeSubscription MapSingleSubscriptionDetail(int userId, Session sessionDetail)
        {
            var subscriptionService = new SubscriptionService();
            var subscriptionDetail = subscriptionService.Get(sessionDetail.SubscriptionId);
            var subscriptionPayload = new StripeSubscription
            {
                SubscriptionId = sessionDetail.SubscriptionId,
                UserId = userId,
                CustomerId = sessionDetail.CustomerId,
                DateCreated = subscriptionDetail.Created,
                Status = subscriptionDetail.Status,
                PriceId = subscriptionDetail.Items.Data[0].Price.Id
            };
            return subscriptionPayload;
        }

        private static void AddSingleSubscription(SubscriptionAddRequest model, int userId, SqlParameterCollection col)
        {
            col.AddWithValue("@SubscriptionId", model.SubscriptionId);
            col.AddWithValue("@UserId", userId);
            col.AddWithValue("@CustomerId", model.CustomerId);
            col.AddWithValue("@DateCreated", model.DateCreated);
            col.AddWithValue("@Status", model.Status);
            col.AddWithValue("@PriceId", model.PriceId);
        }
    }

}
