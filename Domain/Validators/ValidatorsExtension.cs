using Domain.Readers;
using Domain.Repository;
using Domain.RepositoryContracts;
using Domain.Validators.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace Domain.Validators
{
    public static class ReaderExtension
    {
        public static IServiceCollection AddValidators(this IServiceCollection services)
        {
            services.AddTransient<IAppsValidator, AppsValidator>();

            return services;
        }
    }
}
