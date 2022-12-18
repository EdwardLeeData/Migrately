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
            // https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-2.2
            // The are a number of differe Add* methods you can use. Please verify which one you
            // should be using services.AddScoped<IMyDependency, MyDependency>();

            // services.AddTransient<IOperationTransient, Operation>();

            // services.AddScoped<IOperationScoped, Operation>();

            // services.AddSingleton<IOperationSingleton, Operation>();

            services.AddSingleton<IAuthenticationService<int>, WebAuthenticationService>();

            services.AddSingleton<IBlogService, BlogService>();



            services.AddSingleton<Sabio.Data.Providers.IDataProvider, SqlDataProvider>(delegate (IServiceProvider provider)
            {
                return new SqlDataProvider(connString);
            }
            );

            services.AddSingleton<ICheckoutService, CheckoutService>();
            services.AddSingleton<ICommentService, CommentService>();
            services.AddSingleton<IDictionary<string, UserConnection>>(opts => new Dictionary<string, UserConnection>());
            services.AddSingleton<IEmailService, EmailService>();
            services.AddSingleton<IFAQService, FAQService>();

            services.AddSingleton<IForumCategoryService, ForumCategoryService>();
            services.AddSingleton<IForumMemberService, ForumMemberService>();
            services.AddSingleton<IForumService, ForumService>();

            services.AddSingleton<IFilesService, FilesService>();

            services.AddSingleton<IGoogleAnalyticService, GoogleAnalyticService>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IIdentityProvider<int>, WebAuthenticationService>();
            services.AddSingleton<IImmigrantVisaCategoryService, ImmigrantVisaCategoryService>();
            services.AddSingleton<ILocationService, LocationService>();
            services.AddSingleton<ILookUpService, LookUpService>();
            services.AddSingleton<IMessageService, MessageService>();
            services.AddSingleton<INewsletterSubscriptionService, NewsletterSubscriptionService>();
            services.AddSingleton<INonImmigrantVisasService, NonImmigrantVisasService>();
            services.AddSingleton<IPaymentAccountService, PaymentAccountService>();
            services.AddSingleton<IProcessingTimeService, ProcessingTimeService>();
            services.AddSingleton<IResourceService, ResourceService>();           
            services.AddSingleton<ISiteReferenceService, SiteReferenceService>();
            services.AddSingleton<IStripeProductService, StripeProductService>();
            services.AddSingleton<IStripeSubscriptionService, StripeSubscriptionService>();
            services.AddSingleton<ISurveyService, SurveyService>();
            services.AddSingleton<IUSCISFormsService, USCISFormsService>();
            services.AddSingleton<IUserService, UserService>();
            services.AddSingleton<IVideochatService, VideochatService>();

            GetAllEntities().ForEach(tt =>
            {
                IConfigureDependencyInjection idi = Activator.CreateInstance(tt) as IConfigureDependencyInjection;

                //This will not error by way of being null. BUT if the code within the method does
                // then we would rather have the error loadly on startup then worry about debuging the issues as it runs
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