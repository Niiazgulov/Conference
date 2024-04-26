using DataAccess.Readers;
using DataAccess.Repository;
using Domain.ReadersContracts;
using Domain.RepositoryContracts;
using Microsoft.Extensions.DependencyInjection;


namespace DataAccess
{
    public static class DataAccessExtension
    {
        public static IServiceCollection AddReaders(this IServiceCollection services)
        {
            services.AddTransient<IApplicationReader, ApplicationReader>();
            services.AddTransient<IApplicationRepository, ApplicationRepository>();
 
            return services;
        }
    }
}