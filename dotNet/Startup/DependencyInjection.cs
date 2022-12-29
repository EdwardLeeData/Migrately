using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sabio.Data;
using Sabio.Models.Interfaces.Forums;
using Sabio.Models.Domain.Messages;
using Sabio.Services;
using Sabio.Services.ForumsService;
using Sabio.Services.Interfaces;
using Sabio.Services.SendGridEmail;
using Sabio.Web.Api.StartUp.DependencyInjection;
using Sabio.Web.Core.Services;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using ForumService = Sabio.Services.ForumsService.ForumService;

namespace Sabio.Web.StartUp
{
    public class DependencyInjection
    {
        public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            if (configuration is IConfigurationRoot)
            {
                services.AddSingleton<IConfigurationRoot>(configuration as IConfigurationRoot);   // IConfigurationRoot
            }

            services.AddSingleton<IConfiguration>(configuration);   // IConfiguration explicitly

            string connString = configuration.GetConnectionString("Default");

            services.AddSingleton<IAuthenticationService<int>, WebAuthenticationService>();

            services.AddSingleton<IBlogService, BlogService>();



            services.AddSingleton<Sabio.Data.Providers.IDataProvider, SqlDataProvider>(delegate (IServiceProvider provider)
            {
                return new SqlDataProvider(connString);
            }
            );

            services.AddSingleton<ICheckoutService, CheckoutService>();
            services.AddSingleton<IPaymentAccountService, PaymentAccountService>();
            services.AddSingleton<IStripeProductService, StripeProductService>();
            services.AddSingleton<IStripeSubscriptionService, StripeSubscriptionService>();
            services.AddSingleton<ISurveyService, SurveyService>();


            GetAllEntities().ForEach(tt =>
            {
                IConfigureDependencyInjection idi = Activator.CreateInstance(tt) as IConfigureDependencyInjection;

                idi.ConfigureServices(services, configuration);
            });
        }

        public static List<Type> GetAllEntities()
        {
            return AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes())
                 .Where(x => typeof(IConfigureDependencyInjection).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                 .ToList();
        }

        public static void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
        }
    }
}
