using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sabio.Models.Domain.Config;
using Sabio.Services.Interfaces;
using Sabio.Services;
using Sabio.Web.Controllers;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Sabio.Models.Requests;
using Sabio.Web.Models.Responses;
using Stripe;
using System;
using Sabio.Models.Domain;
using Stripe.Checkout;

namespace Sabio.Web.Api.Controllers
{
    [Route("api/subscription")]
    [ApiController]
    public class StripeSubscriptionApiController : BaseApiController
    {
        private IStripeSubscriptionService _service = null;
        private StripeConfig _stripe = null;
        private IAuthenticationService<int> _authService = null;


        public StripeSubscriptionApiController(IStripeSubscriptionService service
            , ILogger<StripeSubscriptionApiController> logger
            , IOptions<StripeConfig> stripeModel,
            IAuthenticationService<int> authService) : base(logger)
        {
            _service = service;
            _stripe = stripeModel.Value;
            _authService = authService;
        }

        [HttpPost()]
        public ActionResult<ItemResponse<int>> Create(SubscriptionAddRequest model)
        {
            int code = 201;
            BaseResponse response = null;
            StripeConfiguration.ApiKey = _stripe.StripeKey;
            try
            {
                int userId = _authService.GetCurrentUserId();
                int id = _service.Add(model, userId);
                response = new ItemResponse<int>() { Item = id };
            }
            catch (Exception e)
            {
                code = 500;
                Logger.LogError(e.ToString());
                response = new ErrorResponse(e.Message);
            }
            return StatusCode(code, response);
        }

        [HttpPost("invoice")]
        public ActionResult<ItemResponse<string>> GetInvoice(string subscriptionId)
        {
            int code = 200;
            BaseResponse response = null;
            StripeConfiguration.ApiKey = _stripe.StripeKey;
            try
            {
                string invoiceUrl = _service.CreateInvoicePdf(subscriptionId);

                response = new ItemResponse<string>() { Item = invoiceUrl };
            }
            catch (Exception ex)
            {
                code = 500;
                Logger.LogError(ex.ToString());
                response = new ErrorResponse($"Generic Error: {ex.Message}");
            }
            return StatusCode(code, response);
        }

        [HttpGet("session")]
        public ActionResult<ItemResponse<StripePayload>> Get(string sessionId)
        {
            int code = 200;
            BaseResponse response = null;
            StripeConfiguration.ApiKey = _stripe.StripeKey;
            try
            {
                int userId = _authService.GetCurrentUserId();
                var stripeSession = _service.GetSubscriptionDetail(sessionId, userId);
                response = new ItemResponse<StripePayload>() { Item = stripeSession };
            }
            catch (Exception e)
            {
                code = 500;
                Logger.LogError(e.ToString());
                response = new ErrorResponse(e.Message);
            }
            return StatusCode(code, response);
        }

        [HttpGet("invoice/{id:int}")]
        public ActionResult<ItemResponse<StripeInvoicePeriod>> Get(int id)
        {
            int code = 200;
            BaseResponse response = null;
            StripeConfiguration.ApiKey = _stripe.StripeKey;
            try
            {
                var period = _service.GetInvoicePeriod(id);
                response = new ItemResponse<StripeInvoicePeriod>() { Item = period };
            }
            catch (Exception e)
            {
                code = 500;
                Logger.LogError(e.ToString());
                response = new ErrorResponse(e.Message);
            }
            return StatusCode(code, response);
        }

        [HttpGet("invoice/current/{id:int}")]
        public ActionResult<ItemResponse<StripeSubscribedCustomer>> GetInvoice(int id)
        {
            int code = 200;
            BaseResponse response = null;
            StripeConfiguration.ApiKey = _stripe.StripeKey;
            try
            {
                var subscription = _service.GetInvoice(id);
                if (subscription == null)
                {
                    code = 404;
                    response = new ErrorResponse("Application Resource not found.");
                }
                else
                {
                    response = new ItemResponse<StripeSubscribedCustomer>() { Item = subscription };

                }
            }
            catch (Exception e)
            {
                code = 500;
                Logger.LogError(e.ToString());
                response = new ErrorResponse(e.Message);
            }
            return StatusCode(code, response);
        }
    }
}
