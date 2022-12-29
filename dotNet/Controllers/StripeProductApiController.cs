using Amazon.Runtime.Internal.Util;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Sabio.Models.Domain;
using Sabio.Models.Domain.Config;
using Sabio.Models.Requests;
using Sabio.Services;
using Sabio.Services.Interfaces;
using Sabio.Web.Controllers;
using Sabio.Web.Models.Responses;
using SendGrid;
using Stripe;
using System;

namespace Sabio.Web.Api.Controllers
{
    [Route("api/product")]
    [ApiController]
    public class StripeProductApiController : BaseApiController
    {
        private IStripeProductService _service = null;
        private IAuthenticationService<int> _authService = null;
        private StripeConfig _stripe = null;

        public StripeProductApiController(IStripeProductService service, ILogger<StripeProductApiController> logger, IOptions<StripeConfig> stripeModel, IAuthenticationService<int> authService) : base(logger)
        {
            _service = service;
            _authService = authService;
            _stripe = stripeModel.Value;

        }

        [AllowAnonymous]
        [HttpGet("{id:int}")]
        public ActionResult<ItemResponse<StripeProduct>> Get(int id)
        {
            int code = 200;
            BaseResponse response = null;
            StripeConfiguration.ApiKey = _stripe.StripeKey;
            try
            {
                StripeProduct product = _service.GetById(id);
                if (product == null)
                {
                    code = 404;
                    response = new ErrorResponse("Application Resource not found.");
                }
                else
                {
                    response = new ItemResponse<StripeProduct> { Item = product };
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

        [HttpGet("current")]
        public ActionResult<ItemResponse<LookUp>> Get()
        {
            int code = 200;
            BaseResponse response = null;
            StripeConfiguration.ApiKey = _stripe.StripeKey;
            try
            {
                int userId = _authService.GetCurrentUserId();
                LookUp product = _service.GetCurrentSubscription(77);
                if (product == null)
                {
                    code = 404;
                    response = new ErrorResponse("Application Resource not found.");
                }
                else
                {
                    response = new ItemResponse<LookUp> { Item = product };
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
