namespace Domain.Handlers.Contracts
{
    public interface IGetUnsubmittedAppsHandler
    {
        Task<IEnumerable<Applications>> GetUnsubmittedApps(DateTime date);
    }
}
