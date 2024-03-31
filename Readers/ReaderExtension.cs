using Domain.Handlers.Contracts;
using Domain.Readers;
using Domain.Repository;
using Domain.RepositoryContracts;
using Microsoft.Extensions.DependencyInjection;
using Readers.Readers;
using Readers.Repository;


namespace Readers
{
    public static class ReaderExtension
    {
        public static IServiceCollection AddReaders(this IServiceCollection services)
        {
            services.AddTransient<IGetApplicationByIdReader, GetApplicationByIdReader>();
            services.AddTransient<IGetApplicationByAuthorIdReader, GetApplicationByAuthorIdReader>();
            services.AddTransient<IGetSubmittedAppsReader, GetSubmittedAppsReader>();
            services.AddTransient<IGetUnsubmittedAppsReader, GetUnsubmittedAppsReader>();
            services.AddTransient<IAddAppsToReviewRepository, AddAppsToReviewRepository>();
            services.AddTransient<IAddNewApplicationRepository, AddNewApplicationRepository>();
            services.AddTransient<IEditAppsRepository, EditAppsRepository>();
            services.AddTransient<IDeleteAppsRepository, DeleteAppsRepository>();
            services.AddTransient<ICheckSendedRepository, CheckSendedRepository>();
            services.AddTransient<ICheckUserByIdRepository, CheckUserByIdRepository>();
            return services;
        }
    }
}