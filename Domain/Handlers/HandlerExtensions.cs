using Domain.Handlers;
using Domain.Handlers.Contract;
using Microsoft.Extensions.DependencyInjection;

namespace Domain.QueryHandlers
{
    public static class HandlerExtensions
    {
        public static IServiceCollection AddHandlers(this IServiceCollection service)
        {
            service.AddTransient<IReaderHandler, ReaderHandler>();

            return service;
        }
    }
}
