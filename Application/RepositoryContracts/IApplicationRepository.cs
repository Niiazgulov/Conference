using Domain.Models;

namespace Domain.RepositoryContracts
{
    public interface IApplicationRepository
    {
        Task<Applications> AddApps(NewAppDTO app);
        Task<Applications?> EditApps(Guid id, EditedAppDTO app);
        Task DeleteApps(Guid id);
        Task<string> CheckSended(Guid id);
        Task<bool> CheckUserById(Guid id);
        Task<(bool, string)> AddAppsToReview(Guid id);
    }
}
