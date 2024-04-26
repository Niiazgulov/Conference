using Domain.Models;

namespace Application.Handlers.Contracts.QueryHandlers
{
    public interface IGetSubOrUnsubmittedAppsHandler
    {
        Task<(bool, string?, IEnumerable<Applications>?)> GetSubOrUnSubmittedApps(SubOrUnsubDTO req);
    }
}
