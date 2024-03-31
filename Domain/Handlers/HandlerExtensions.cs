using Domain.Handlers;
using Domain.Handlers.Contract;
using Domain.Handlers.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace Domain.QueryHandlers
{
    public static class HandlerExtensions
    {
        public static IServiceCollection AddHandlers(this IServiceCollection service)
        {
            service.AddScoped<IGetSubmittedAppsHandler, GetSubmittedAppsHandler>();
            service.AddScoped<IGetUnsubmittedAppsHandler, GetUnsubmittedAppsHandler>();
            service.AddScoped<IGetAppByAuthorIdHandler, GetAppByAuthorIdHandler>();
            service.AddScoped<IGetAppsByIdHandler, GetAppsByIdHandler>();
            service.AddScoped<IAddAppsToReviewHandler, AddAppsToReviewHandler>();
            service.AddScoped<IAddNewApplicationHandler, AddNewApplicationHandler>();
            service.AddScoped<IEditAppsHandler, EditAppsHandler>();
            service.AddScoped<IDeleteAppsHandler, DeleteAppsHandler>();
            service.AddScoped<ICheckSendedHandler, CheckSendedHandler>();
            service.AddScoped<IEditAppsHandler, EditAppsHandler>();
            service.AddScoped<IGetActivitiesRequestHandler, GetActivitiesRequestHandler>();

            return service;
        }
    }
}
