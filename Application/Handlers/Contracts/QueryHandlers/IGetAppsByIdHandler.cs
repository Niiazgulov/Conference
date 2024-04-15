using Domain.Models;

namespace Application.Handlers.Contracts.QueryHandlers
{
    public interface IGetAppsByIdHandler
    {
        Task<Applications?> GetAppsById(Guid id);
    }
}
