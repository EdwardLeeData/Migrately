using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using Sabio.Models.Domain;
using Sabio.Models.Domain.Config;
using Sabio.Models.Requests;
using Sabio.Services;
using Sabio.Services.Interfaces;
using Sabio.Web.Controllers;
using Sabio.Web.Models.Responses;
using SendGrid;
using Stripe;
using Stripe.Checkout;
using System;
using System.Collections.Generic;
using System.Text.Json.Nodes;
using System.Threading.Tasks;


namespace Sabio.Web.Api.Controllers
{
    [Route("api/checkout")]
    [ApiController]
    public class CheckoutApiController : BaseApiController
    {
        private ICheckoutService _service = null;
        private StripeConfig _stripe = null;
        private IAuthenticationService<int> _authService = null;

        public CheckoutApiController(ICheckoutService service
            , ILogger<CheckoutApiController> logger
            , IOptions<StripeConfig> stripeModel,
            IAuthenticationService<int> authService) : base(logger)
        {
            _service = service;
            _stripe = stripeModel.Value;
            _authService = authService;

        }

        [HttpPost("session")]
        public ActionResult<SuccessResponse> CreateCheckoutSession(CheckoutSessionRequest model)
        {
            BaseResponse response = null;
            StripeConfiguration.ApiKey = _stripe.StripeKey;
            int code = 201;
            try
            {
                int userId = _authService.GetCurrentUserId();
                string sessionId = _service.CreateCheckoutSessionId(model, userId);
                response = new ItemResponse<string>() { Item = sessionId };
            }
            catch (StripeException e)
            {
                code = 500;
                Logger.LogError(e.ToString());
                response = new ErrorResponse(e.Message);
            }
            return StatusCode(code, response);
        }

        [HttpGet("order")]
        public ActionResult<ItemResponse<Session>> SessionDetail(string session_id)
        {
            int code = 200;
            BaseResponse response = null;
            StripeConfiguration.ApiKey = _stripe.StripeKey;
            try
            {
                var sessionService = new SessionService();
                Session session = sessionService.Get(session_id);
                response = new ItemResponse<Session>() { Item = session };
            }
            catch (Exception ex)
            {
                code = 500;
                Logger.LogError(ex.ToString());
                response = new ErrorResponse($"Generic Error: {ex.Message}");
            }
            return StatusCode(code, response);
        }


        [HttpPost("account")]
        public ActionResult<SuccessResponse> CreateAccount()
        {
            int code = 201;
            BaseResponse response = null;
            StripeConfiguration.ApiKey = _stripe.StripeKey;
            try
            {
                string accountId = _service.CreateNewAccount();
                string accountLink = _service.CreateAccountLink(accountId);
                response = new ItemResponse<string>() { Item = accountLink };
            }
            catch (StripeException e)
            {
                code = 500;
                Logger.LogError(e.ToString());
                response = new ErrorResponse(e.Message);
            }
            return StatusCode(code, response);
        }
    }
}