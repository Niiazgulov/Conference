using Application.Handlers.CommandHandlers;
using Application.Handlers.Contracts.CommandHandlers;
using Application.Handlers.Contracts.QueryHandlers;
using Application.Handlers.QueryHandlers;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Handlers
{
    public static class HandlerExtensions
    {
        public static IServiceCollection AddHandlers(this IServiceCollection service)
        {
            service.AddScoped<IGetSubOrUnsubmittedAppsHandler, GetSubOrUnsubmittedAppsHandler>();
            service.AddScoped<IGetAppByAuthorIdHandler, GetAppByAuthorIdHandler>();
            service.AddScoped<IGetAppsByIdHandler, GetAppsByIdHandler>();
            service.AddScoped<IAddAppsToReviewHandler, AddAppsToReviewHandler>();
            service.AddScoped<IAddNewApplicationHandler, AddNewApplicationHandler>();
            service.AddScoped<IEditAppsHandler, EditAppsHandler>();
            service.AddScoped<IDeleteAppsHandler, DeleteAppsHandler>();
            service.AddScoped<IEditAppsHandler, EditAppsHandler>();
            service.AddScoped<IGetActivitiesRequestHandler, GetActivitiesRequestHandler>();

            return service;
        }
    }
}
