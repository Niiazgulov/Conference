namespace Domain.Handlers.Contract
{
    public interface IReaderHandler
    {
        Task<Applications> GetAppsById(Guid id);
        Task<Applications> GetAppByAuthorId(Guid id);
        Task<IEnumerable<Applications>> GetUnsubmittedApps(DateTime date);
        Task<IEnumerable<Applications>> GetSubmittedApps(DateTime date);
        Activities[] GetActivities();

    }
}
