using Application.Handlers.CommandHandlers.Results;

namespace Application.Handlers.Contracts.CommandHandlers
{
    public interface IDeleteAppsHandler
    {
        Task<DeleteAppsResult> DeleteApps(Guid id);
    }
}
