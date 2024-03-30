using Domain.Readers;
using Microsoft.Extensions.DependencyInjection;


namespace Readers
{
     public static class ReaderExtension
    {
        public static IServiceCollection AddReaders(this IServiceCollection services)
        {
            services.AddTransient<IConferenceAppsReader, ConferenceAppsReader>();

            return services;
        }
    }
}