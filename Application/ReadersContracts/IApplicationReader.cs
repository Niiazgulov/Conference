using Domain.Models;

namespace Domain.ReadersContracts
{
    public interface IApplicationReader
    {
        Task<Applications?> GetAppsById(Guid id);
        Task<Applications?> GetAppByAuthorId(Guid author);
        Task<IEnumerable<Applications>> GetUnsubmittedApps(DateTime? datetime, bool sended);
        Task<IEnumerable<Applications>> GetSubmittedApps(DateTime? datetime, bool sended);
    }
}
