using Application.Validators.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Validators
{
    public static class ReaderExtension
    {
        public static IServiceCollection AddValidators(this IServiceCollection services)
        {
            services.AddTransient<IAddAppsValidator, AddAppsValidator>();
            services.AddTransient<IEditAppsValidator, EditAppsValidator>();
            services.AddTransient<IGetSubOrUnsubValidator, GetSubOrUnsubValidator>();
            services.AddTransient<IDeleteAppsValidator, DeleteAppsValidator>();
            services.AddTransient<IReviewAddAppsValidator, ReviewAddAppsValidator>();

            return services;
        }
    }
}