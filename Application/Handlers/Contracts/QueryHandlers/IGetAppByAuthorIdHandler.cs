using Domain.Models;

namespace Application.Handlers.Contracts.QueryHandlers
{
    public interface IGetAppByAuthorIdHandler
    {
        Task<Applications?> GetAppByAuthorId(Guid id);
    }
}
