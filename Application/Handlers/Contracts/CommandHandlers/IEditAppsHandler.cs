using Application.Handlers.CommandHandlers.Results;
using Domain.Models;

namespace Application.Handlers.Contracts.CommandHandlers
{
    public interface IEditAppsHandler
    {
        Task<EditAppsResult> EditApps(Guid id, EditedAppDTO app);
    }
}
