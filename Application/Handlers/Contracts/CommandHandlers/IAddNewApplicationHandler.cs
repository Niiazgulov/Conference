using Application.Handlers.CommandHandlers.Results;
using Domain.Models;

namespace Application.Handlers.Contracts.CommandHandlers
{
    public interface IAddNewApplicationHandler
    {
        Task<AddAppsResult> AddApps(NewAppDTO app);
    }
}
