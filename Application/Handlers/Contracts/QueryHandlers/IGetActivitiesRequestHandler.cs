using Domain.Models;

namespace Application.Handlers.Contracts.QueryHandlers
{
    public interface IGetActivitiesRequestHandler
    {
        Activities[] GetActivities();
    }
}
